<script setup>
    import {
        ref,
        onMounted,
        watch
    } from 'vue';
    import {
        useStore
    } from 'vuex';
    import axios from "axios";
    import {
        useRouter
    } from 'vue-router';
    import {
        required
    } from '@vuelidate/validators'
    import useVuelidate from '@vuelidate/core'
    import {
        useToast
    } from 'primevue/usetoast';
    const toast = useToast();
    const rules = {
        PhoneNumber: {
            required
        },
        BankAccount: {
            required
        },
        Address1: {
            required
        },
        DistrictId: {
            required
        },
        PostalCode: {
            required
        }
    }

    const router = useRouter();


    const apiUrl = process.env["VUE_APP_BASED_URL"]
    let userData = null;
    const provincias = ref([]);
    const cantones = ref([]);
    const distritos = ref([]);
    const selectedProvincia = ref(null);
    const selectedCanton = ref(null);
    const selectedDistrito = ref( null);

    const backLabel = 'Cancelar';
    const homePage = () => {
        router.push({
            name: "myDashboard"
        });
    }
    const sendLabel = 'Actualizar';

    const fetchData = async (url, target) => {
        try {
            const response = await axios.get(url);
            target.value = response.data;
        } catch (error) {
            console.error(error);
        }
    };

    fetchData(`${apiUrl}/location/provinces`, provincias);

    watch(selectedProvincia, async (newValue) => {
        if (newValue) {
            cantones.value = [];
            distritos.value = [];
            fetchData(`${apiUrl}/location/province/${newValue}/cantons`, cantones);
        }
    });

    watch(selectedCanton, async (newValue) => {
        if (newValue) {
            distritos.value = [];
            fetchData(`${apiUrl}/location/canton/${newValue}/districts`, distritos);
        }
    });

    const onProvinciaChange = () => {
        selectedCanton.value = null;
        selectedDistrito.value = null;
    };

    const onCantonChange = () => {
        selectedDistrito.value = null;
    };
    const store = useStore()
    const PersonId = ref(null)
    const NumberId = ref(null)
    const FirstName = ref(null)
    const LastName1 = ref(null)
    const LastName2 = ref(null)

    const personalInfo = ref({
        PhoneNumber: null,
        BankAccount: null,
        Address1: null,
        Address2: null,
        DistrictId: selectedDistrito,
        PostalCode: null
    });

const storeUser = async () => {
    await store.dispatch('users/patchProfile', {
        personalInfo: personalInfo.value
    })
}

    const v$ = useVuelidate(rules, personalInfo);
    const validateForm = async () => {
        const result = await v$.value.$validate();
        console.log(v$)
        if (!result) {
            if (v$.value.$errors[0].$validator === 'required') {
                toast.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Por favor revisar los campos en rojo.',
                    life: 2000
                });
               
            }
            return false
        }
        return true;
    }

    const fetchUserData = async () => {
        await store.dispatch('users/getById');
        userData = store.getters["users/getUsers"];
        PersonId.value = userData.PersonId;
        NumberId.value = userData.NumberId;
        FirstName.value = userData.FirstName;
        LastName1.value = userData.LastName1;
        LastName2.value = userData.LastName2;
        personalInfo.value.PhoneNumber = userData.PhoneNumber;
        personalInfo.value.BankAccount = userData.BankAccount;
        personalInfo.value.Address1 = userData.Address1;
        personalInfo.value.Address2 = userData.Address2;
        selectedDistrito.value = userData.DistrictId;
        personalInfo.value.PostalCode = userData.PostalCode;

        axios.get(`${apiUrl}/location/district/${personalInfo.value.DistrictId}`)
            .then(response => {
                const responseData = response.data;
                selectedProvincia.value = responseData.ProvinceId;
                selectedCanton.value = responseData.CantonId;
                selectedDistrito.value = responseData.DistrictId;
            })
            .catch(error => {
                console.error(error);
            });
    };


    const submitData = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            if (isValid) {
            try {
              await  storeUser();
                toast.add({
                    severity: 'success',
                    summary: 'Felicidades',
                    detail: "Sus cambios han sido guardados.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                router.push({
                    name: 'myDashboard'
                });
            } catch (error) {
                toast.add({
                    severity: 'error',
                    summary: 'Error',
                    detail: 'Un error ocurrió.',
                    life: 2000
                });
            }
        }
    }
    }

    onMounted(fetchUserData);
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="form-column">
            <p><label><b>Código de empleado: </b></label>
                {{ PersonId }}</p>
            <p><label><b>Número de identificación: </b> </label>
                {{ NumberId }}</p>
            <p><label><b>Nombre completo: </b></label>
                {{ FirstName }} {{ LastName1 }}
                {{ LastName2 }}</p>
        </div>
        <div class="header">
            <div class="form-row">
                <input-text class="input-text form-margin-right" id="employee-phone" type="text"
                    placeholder="Número telefónico" v-model="personalInfo.PhoneNumber" :class="{'hasError': (v$?.BankAccount?.$error) }"/>
                <input-text class="input-text" id="employee-account" type="text" placeholder="Cuenta IBAN"
                    v-model="personalInfo.BankAccount"  :class="{'hasError': (v$?.BankAccount?.$error) }"/>
            </div>
            <div class="form-row">
                <input-text placeholder="Dirección 1" class="dropdown form-margin-right" id="employee-address1"
                    type="text" v-model="personalInfo.Address1"  :class="{'hasError': (v$?.Address1?.$error ) }"/>
                <input-text placeholder="Dirección 2" class="input-text" id="employee-address2" type="text"
                    v-model="personalInfo.Address2" />
            </div>
            <div class="form-row">
                <drop-down class="dropdown form-margin-right" :options="provincias" v-model="selectedProvincia"
                    optionLabel="ProvinceName" optionValue="ProvinceId" @onChange="onProvinciaChange"
                    placeholder="Provincia" :class="{'hasError': (v$?.selectedProvincia?.$error) }"/>
                <drop-down class="dropdown" :options="cantones" v-model="selectedCanton" optionLabel="CantonName"
                    optionValue="CantonId" @onChange="onCantonChange" placeholder="Cantón" :class="{'hasError': (v$?.selectedCanton?.$error) }"/>
            </div>
            <div class="form-row">
                <drop-down class="dropdown form-margin-right" :options="distritos" v-model="selectedDistrito"
                    optionLabel="DistrictName" optionValue="DistrictId" placeholder="Distrito" :class="{'hasError': (v$?.selectedDistrito?.$error) }"/>
                <input-text class="input-text" id="employee-zip" type="text" v-model="personalInfo.PostalCode"
                    placeholder="Código postal" :class="{'hasError': (v$?.PostalCode?.$error) }"/>
            </div>
        </div>
        <div class="actions">
            <base-button :label="backLabel" @click="homePage" :type="'button'" />
            <base-button :label="sendLabel" @click="submitData" :type="'submit'" />
        </div>
    </div>
</template>
<style scoped="scoped">
    .main {
        display: flex;
        flex-direction: column;
    }

    .header {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .form-column {
        display: flex;
        flex-direction: column;
        min-height: 10vh;
    }


    .form-row {
        display: flex;
        justify-content: space-between;
        align-self: center;
        margin-bottom: 2rem;
        width: 60%;
    }

    .form-margin-right {
        margin-right: 6rem;
    }

    .form-margin-left {
        margin-left: 6rem;
    }

    .hasError  {
    border-color: red;        
    }
    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;
    }
</style>
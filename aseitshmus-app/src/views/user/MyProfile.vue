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
    const router = useRouter();
    const store = useStore()

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


    const apiUrl = process.env["VUE_APP_BASED_URL"]
    let userData = null;
    const provincias = ref([]);
    const cantones = ref([]);
    const distritos = ref([]);
    const selectedProvincia = ref(null);
    const selectedCanton = ref(null);
    const selectedDistrito = ref(null);

    const backLabel = 'Atrás';
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
            toast.add({
                severity: 'error',
                detail: error,
                life: 2000
            });
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
        if (!result) {
            if (v$.value.$errors[0].$validator === 'required') {
                toast.add({
                    severity: 'error',
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
                toast.add({
                    severity: 'error',
                    detail: error,
                    life: 2000
                });
            });
    };

    const submitData = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
                try {
                    await storeUser();
                    toast.add({
                        severity: 'success',
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
                        detail: 'Un error ocurrió.',
                        life: 2000
                    });
                }
        }
    }

    onMounted(fetchUserData);
</script>

<template>
    <div class="main">
        <toast-component />
        
        <div class="form">
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
                <div class="p-float-label">
                    <input-mask class="input-text form-margin-right" id="employee-phone" type="text"
                        placeholder="Número telefónico" v-model="personalInfo.PhoneNumber"
                        :class="{'p-invalid': (v$?.PhoneNumber?.$error) }" mask="9999-9999" />
                    <label for="employee-code">Número telefónico</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text" id="employee-account" type="text" placeholder="Cuenta IBAN"
                        v-model="personalInfo.BankAccount" :class="{'p-invalid': (v$?.BankAccount?.$error) }" />
                    <label for="employee-code">Cuenta IBAN</label>
                </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                    <input-text placeholder="Dirección 1" class="dropdown form-margin-right" id="employee-address1"
                        type="text" v-model="personalInfo.Address1" :class="{'p-invalid': (v$?.Address1?.$error ) }" />
                    <label for="employee-code">Dirección 1</label>
                </div>
                <div class="p-float-label">
                    <input-text placeholder="Dirección 2" class="input-text" id="employee-address2" type="text"
                        v-model="personalInfo.Address2" />
                    <label for="employee-code">Dirección 2</label>
                </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                    <drop-down class="dropdown form-margin-right" :options="provincias" v-model="selectedProvincia"
                        optionLabel="ProvinceName" optionValue="ProvinceId" @onChange="onProvinciaChange"
                        placeholder="Provincia" :class="{'p-invalid': (v$?.selectedProvincia?.$error) }" />
                    <label for="employee-code">Provincia</label>
                </div>
                <div class="p-float-label">
                    <drop-down class="dropdown" :options="cantones" v-model="selectedCanton" optionLabel="CantonName"
                        optionValue="CantonId" @onChange="onCantonChange" placeholder="Cantón"
                        :class="{'p-invalid': (v$?.selectedCanton?.$error) }" />
                    <label for="employee-code">Cantón</label>
                </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                    <drop-down class="dropdown form-margin-right" :options="distritos" v-model="selectedDistrito"
                        optionLabel="DistrictName" optionValue="DistrictId" placeholder="Distrito"
                        :class="{'p-invalid': (v$?.selectedDistrito?.$error) }" />
                    <label for="employee-code">Distrito</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text" id="employee-zip" type="text" v-model="personalInfo.PostalCode"
                        placeholder="Código postal" :class="{'p-invalid': (v$?.PostalCode?.$error) }" />
                    <label for="employee-code">Código postal</label>
                </div>
            </div>
        </div>
        <div class="actions">
            <base-button small :label="backLabel" @click="homePage" :type="'button'" />
            <base-button small :label="sendLabel" @click="submitData" :type="'submit'" />
        </div>
    </div>
    </div>
</template>
<style scoped="scoped">
   .main {
        display: flex;
        justify-content: center;
        align-items: center;
        border: 1px solid #ebebeb;
        border-radius: 5px;
        margin: 1rem;
        padding: 2rem;
    }

    .form {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
    }
    .form-column {
        display: flex;
        flex-direction: column;
        min-height: 9vh;
        align-items: center;
    }

    .header{
        margin-top: 1.1rem;
    }

    .form-row {
        display: flex;
        justify-content: space-between;
        align-self: center;
        margin-top: 2rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 6rem;
    }

    .form-margin-left {
        margin-left: 6rem;
    }
    .actions {
        margin-top: 2rem;
        display: flex;
        flex-direction: row;
        justify-content: flex-end;
        align-self: flex-end;
    }

    .actions button {
        flex: 1;
        margin-right: 1rem;
    }
</style>
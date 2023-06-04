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
const router = useRouter();


    const apiUrl = process.env["VUE_APP_BASED_URL"]
let userData = null;
    const provincias = ref([]);
    const cantones = ref([]);
const distritos = ref([]);
const selectedProvincia = ref(userData ? userData.ProvinceId : null);
const selectedCanton = ref(userData ? userData.CantonId : null);
const selectedDistrito = ref(userData ? userData.DistrictId : null);

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
    const personalInfo = ref({
        PersonId: null,
        NumberId: null,
        FirstName: null,
        LastName1: null,
        LastName2: null,
        PhoneNumber: null,
        BankAccount: null,
        Address1: null,
        Address2: null,
        DistrictId: selectedDistrito,
        PostalCode: null
    });

    const fetchUserData = async () => {
        await store.dispatch('users/getById');
         userData = store.getters["users/getUser"];
        personalInfo.value.PersonId = userData.PersonId;
        personalInfo.value.NumberId = userData.NumberId;
        personalInfo.value.FirstName = userData.FirstName;
        personalInfo.value.LastName1 = userData.LastName1;
        personalInfo.value.LastName2 = userData.LastName2;
        personalInfo.value.PhoneNumber = userData.PhoneNumber;
        personalInfo.value.BankAccount = userData.BankAccount;
        personalInfo.value.Address1 = userData.Address1;
        personalInfo.value.Address2 = userData.Address2;
        selectedDistrito.value = userData.DistrictId;
        personalInfo.value.PostalCode = userData.PostalCode;  

 axios.get(`${apiUrl}/location/district/${personalInfo.value.DistrictId}`)
     .then(response => {
            console.log(response.data)
            const responseData = response.data;
           selectedProvincia.value = responseData.ProvinceId;
             selectedCanton.value = responseData.CantonId;
            selectedDistrito.value  = responseData.DistrictId;
        })
        .catch(error => {
            console.error(error);
        });

};



    onMounted(fetchUserData);
</script>

<template>
    <div class="main">
        
        <div class="form-column">
            <p><label><b>Código de empleado: </b></label>
                {{ personalInfo.PersonId }}</p>
            <p><label><b>Número de identificación: </b> </label>
                {{ personalInfo.NumberId }}</p>
            <p><label><b>Nombre: </b></label>
                {{ personalInfo.FirstName }} {{ personalInfo.LastName1 }}
                {{ personalInfo.LastName2 }}</p>
        </div>
        <div class="header">
            <div class="form-row">
                <input-text class="input-text form-margin-right" id="employee-phone" type="text"
                    placeholder="Número telefónico" v-model="personalInfo.PhoneNumber" />
                <input-text class="input-text" id="employee-account" type="text" placeholder="Cuenta IBAN"
                    v-model="personalInfo.BankAccount" />
            </div>
            <div class="form-row">
                <input-text placeholder="Dirección 1" class="dropdown form-margin-right" id="employee-address1"
                    type="text" v-model="personalInfo.Address1" />
                <input-text placeholder="Dirección 2" class="input-text" id="employee-address2" type="text"
                    v-model="personalInfo.Address2" />
            </div>
            <div class="form-row">
                <drop-down class="dropdown form-margin-right" :options="provincias" v-model="selectedProvincia"
                        optionLabel="ProvinceName" optionValue="ProvinceId" @onChange="onProvinciaChange"
                        placeholder="Provincia" />
                <drop-down class="dropdown" :options="cantones" v-model="selectedCanton" optionLabel="CantonName"
                        optionValue="CantonId" @onChange="onCantonChange" placeholder="Cantón" />
            </div>
            <div class="form-row">
            <drop-down class="dropdown form-margin-right" :options="distritos" v-model="selectedDistrito"
                        optionLabel="DistrictName" optionValue="DistrictId" placeholder="Distrito" />
                <input-text class="input-text" id="employee-zip" type="text"
                    v-model="personalInfo.PostalCode" placeholder="Código postal" />
            </div>
        </div>
    <div class="actions">
                <base-button :label="backLabel"  @click="homePage" :type="'button'" />
                <base-button :label="sendLabel"  :type="'submit'" />
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

       .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;
    }

</style>
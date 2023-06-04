<script setup>
    import {
        ref,
        onMounted, watch
    } from 'vue';
    import {
        useStore
    } from 'vuex';
import axios from "axios";

const apiUrl = process.env["VUE_APP_BASED_URL"]
const provincias = ref([]);
const cantones = ref([]);
const distritos = ref([]);
const selectedProvincia = ref(null);
const selectedCanton = ref(null);
const selectedDistrito = ref(null);

const fetchData = async (url, target) => {
    try {
        const response = await axios.get(url);
        target.value = response.data.map(item => ({
            value: item.DistrictId
        }))
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


    const store = useStore()

    const personalInfo = ref({
        PersonId: null,
        NumberId: null,
        FirstName: null,
        LastName1: null,
        LastName2: null,
        PhoneNumber: null,
        BankAccount: null,
        DistrictId: selectedDistrito,
    });

    const fetchUserData = async () => {
        await store.dispatch('users/getById');
        const userData = store.getters["users/getUser"];

        personalInfo.value.PersonId = userData.PersonId;

        personalInfo.value.NumberId = userData.NumberId;
        personalInfo.value.FirstName = userData.FirstName;
        personalInfo.value.LastName1 = userData.LastName1;
        personalInfo.value.LastName2 = userData.LastName2;
        personalInfo.value.PhoneNumber = userData.PhoneNumber;
        personalInfo.value.BankAccount = userData.BankAccount;
        personalInfo.value.DistrictId = userData.DistrictId;


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
                <input-text class="input-text form-margin-left" id="employee-phone" type="text"
                    placeholder="Número telefónico" v-model="personalInfo.PhoneNumber" />
                <input-text class="input-text form-margin-right" id="employee-account" type="text"
                    placeholder="Cuenta IBAN" v-model="personalInfo.BankAccount" />



                    <drop-down class="dropdown form-margin-right" :options="provincias" v-model="selectedProvincia"
      optionLabel="label" optionValue="value" @onChange="onProvinciaChange"
      placeholder="Provincia" />

    <drop-down class="dropdown" :options="cantones" v-model="selectedCanton" optionLabel="label"
      optionValue="value" @onChange="onCantonChange" placeholder="Cantón" />

    <drop-down class="dropdown form-margin-right" :options="distritos" v-model="selectedDistrito"
      optionLabel="label" optionValue="value" placeholder="Distrito" />
            </div>
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
        min-height: 28vh;
    }

    .form-column {
        display: flex;
        flex-direction: column;
        min-height: 10vh;
    }


    .form-row {
        display: flex;
        justify-content: space-between;
        margin-bottom: 2rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 6rem;
    }

    .form-margin-left {
        margin-left: 6rem;
    }
</style>
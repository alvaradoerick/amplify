<script setup="setup">
    import {
        defineEmits,
        watch,
        ref,
    } from 'vue'
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

    const emits = defineEmits(['address-info'])

    const addressInfo = ref({
        Address1: null,
        Address2: null,
        DistrictId: selectedDistrito,
        PostalCode: null
    });
    watch(addressInfo.value, (newValue) => {
        emits('address-info', newValue)
    })
</script>

<template>
    <div class="container">
        <div style="margin-top: 3rem;">
            <div class="form-row">
                <input-text placeholder="Dirección 1" class="dropdown form-margin-right-text" id="employee-address1"
                    type="text" v-model="addressInfo.Address1" />
                <input-text placeholder="Dirección 2" class="input-text" id="employee-address2" type="text"
                    v-model="addressInfo.Address2" />
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
                <input-text class="input-text" id="employee-zip" type="text" v-model="addressInfo.PostalCode"
                    placeholder="Código postal" />
            </div>
        </div>
    </div>
</template>

<style scoped="scoped">
    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        margin-bottom: 2.1rem;
    }

    .form-row {
        display: flex;
        justify-content: space-between;
        margin-bottom: 2rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 2rem;
    }
</style>
<script setup="setup">
    import {
        defineEmits,
        watch,
        ref,
    } from 'vue'
    import axios from "axios";
    import {
        useToast
    } from 'primevue/usetoast';
    
    const apiUrl = process.env["VUE_APP_BASED_URL"]
    const toast = useToast();

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
            <toast-component />
            <div class="form-row">
                <div class="p-float-label">
                <input-text placeholder="Dirección 1" class="dropdown form-margin-right-text" id="employee-address1"
                    type="text" v-model="addressInfo.Address1" />
                    <label for="employee-address1">Dirección 1</label>
            </div>
            <div class="p-float-label">
                <input-text placeholder="Dirección 2" class="input-text" id="employee-address2" type="text"
                    v-model="addressInfo.Address2" />
                    <label for="employee-address2">Dirección 2</label>
            </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                <drop-down class="dropdown form-margin-right" :options="provincias" v-model="selectedProvincia"
                    optionLabel="ProvinceName" optionValue="ProvinceId" @onChange="onProvinciaChange"
                    placeholder="Provincia" id="province"/>
                    <label for="province">Provincia</label>
            </div>
            <div class="p-float-label">
                <drop-down class="dropdown" :options="cantones" v-model="selectedCanton" optionLabel="CantonName"
                    optionValue="CantonId" @onChange="onCantonChange" placeholder="Cantón" id="canton"/>
                    <label for="canton">Cantón</label>
            </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                <drop-down class="dropdown form-margin-right" :options="distritos" v-model="selectedDistrito"
                    optionLabel="DistrictName" optionValue="DistrictId" placeholder="Distrito" id="district" />
                    <label for="district">Distrito</label>
            </div>
            <div class="p-float-label">
                <input-text class="input-text" id="employee-zip" type="text" v-model="addressInfo.PostalCode"
                    placeholder="Código postal" />
                    <label for="employee-zip">Código postal</label>
            </div>
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
        margin-bottom: 3rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 2rem;
    }
</style>
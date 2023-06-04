<script setup="setup">
    import {
        ref,
        defineEmits,
        watch
    } from 'vue';
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


    const personalInfo = ref({
        firstName: null,
        lastName1: null,
        LastName2: null,
        NumberId: null,
        PhoneNumber: null,
        BankAccount: null,
        Address1: null,
        Address2: null,
        DistrictId: selectedDistrito,
        PostalCode: null
    });


    const emits = defineEmits(['personal-info'])


    watch(personalInfo.value, (newValue) => {
        emits('personal-info', newValue)
    })
</script>

<template>
    <form>
        <div class="container">
            <div class="form-card slidepage">
                <div class="row form-row">
                    <div class="form-row row2 ">
                        <div class="form-group col-md-4">
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-name" type="text"
                                    v-model="personalInfo.firstName" disabled="true" />
                            </span>
                        </div>
                        <div class="form-group col-md-4">
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-lastname1" type="text"
                                    v-model="personalInfo.lastName1" disabled="true" />
                            </span>
                        </div>
                        <div class="form-group col-md-4">
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-lastname2" type="text"
                                    v-model="personalInfo.LastName2" disabled="true" />
                            </span>
                        </div>

                    </div>
                    <div class="row2 form-row">

                        <div class="form-group col-md-3">
                            <span class="p-float-label">
                                <input-text class="input-text" label="id-Number" type="text" id="id-Number"
                                    v-model="personalInfo.NumberId" disabled="true" />
                            </span>
                        </div>
                        <div class="form-group col-md-4">
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-number" type="text"
                                    v-model="personalInfo.PhoneNumber">
                                </input-text>
                                <label for="employee-number">Teléfono</label>
                            </span>
                        </div>
                        <div>
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-account" type="text"
                                    v-model="personalInfo.BankAccount">
                                </input-text>
                                <label for="employee-account">Cuenta bancaria (IBAN)</label>
                            </span>
                        </div>
                    </div>

                    <div class="row form-row">
                        <div class="form-group col-md-6">
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-address1" type="text"
                                    v-model="personalInfo.address1">
                                </input-text>
                                <label for="employee-address1">Dirección 1</label>
                            </span>
                        </div>
                        <div class="form-group col-md-5">
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-address2" type="text"
                                    v-model="personalInfo.Address2">
                                </input-text>
                                <label for="employee-address2">Dirección 2</label>
                            </span>
                        </div>
                    </div>
                    <div class="row form-row">
                        <div class="form-group col-md-6">
                            <drop-down class="dropdown" :options="provincias" v-model="selectedProvincia"
                                optionLabel="ProvinceName" optionValue="ProvinceId" @onChange="onProvinciaChange"
                                placeholder="Provincia" />
                        </div>
                        <div class="form-group col-md-5">
                            <drop-down class="dropdown" :options="cantones" v-model="selectedCanton"
                                optionLabel="CantonName" optionValue="CantonId" @onChange="onCantonChange"
                                placeholder="Cantón" />
                        </div>
                    </div>
                    <div class="row form-row">
                        <div class="form-group col-md-6">
                            <drop-down class="dropdown" :options="distritos" v-model="selectedDistrito"
                                optionLabel="DistrictName" optionValue="DistrictId" placeholder="Distrito" />
                        </div>
                        <div class="form-group col-md-5">
                            <span class="p-float-label">
                                <input-text class="input-text" id="employee-zip" type="text"
                                    v-model="personalInfo.PostalCode">
                                </input-text>
                                <label for="employee-zip">Código postal</label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</template>
<style scoped="scoped">
    .container {
        display: flex;
        flex-direction: column;
        align-items: flex-start;
    }

    .row {
        display: flex;
        flex-direction: row;
        gap: 1px;
        width: 70%;
        margin-bottom: 45px;
    }

    .row2 {
        display: flex;
        flex-direction: row;
        gap: 5px;
        width: 100%;
        margin-bottom: 45px;
    }

    .dropdown,
    .input-text {
        width: 230px;
    }
</style>
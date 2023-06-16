<script setup="setup">
    import countriesJson from '@/assets/countriesJson.json';
   
    import {
        ref,
        defineEmits,
        watch
    } from 'vue';

    const personalInfo = ref({
        PersonId: null,
        NumberId: null,
        FirstName: null,
        LastName1: null,
        LastName2: null,
        Nationality: null,
        DateBirth: null,
    });

    const countrySet = ref([]);
    const selectedType = ref('Cédula');
    const types = ref(['Cédula', 'DIMEX']);

    countrySet.value = countriesJson;
    const emits = defineEmits(['personal-info'])


    watch(personalInfo.value, (newValue) => {
        emits('personal-info', newValue)
    })
</script>

<template>
    <div class="container">
        <div class="form-row">
            <input-text class="input-text form-margin-right" id="employee-code" placeholder="Código de empleado"
                type="text" v-model="personalInfo.PersonId" v-tooltip.focus="'Código localizado en Workday'" />
            <drop-down class=" input-text" placeholder="Identificación" :options="types" v-model="selectedType" />
        </div>
        <div class="form-row">
            <input-mask v-if="selectedType === 'Cédula'" class="input-text form-margin-right" id="mask-input"
                placeholder="Cedula" v-model="personalInfo.NumberId" mask="99-9999-9999" />
            <input-text v-else-if="selectedType === 'DIMEX'" class="input-text form-margin-right" placeholder="Dimex"
                type="text" id="employee-dimex" v-model="personalInfo.NumberId" />

            <input-text class="input-text" placeholder="Nombre" id="employee-name" type="text"
                v-model="personalInfo.FirstName" />
        </div>
        <div class="form-row">
            <input-text class="input-text form-margin-right" placeholder="Primer apellido" id="employee-lastname1"
                type="text" v-model="personalInfo.LastName1" />

            <input-text class="input-text" placeholder="Segundo apellido" id="employee-lastname2" type="text"
                v-model="personalInfo.LastName2" />
        </div>
        <div class="form-row">
            <drop-down class="dropdown form-margin-right" placeholder="Nacionalidad" filter="filter"
                :options="countrySet" v-model="personalInfo.Nationality" optionLabel="name" optionValue="code" />
            <date-picker v-model="personalInfo.DateBirth" placeholder="Fecha de nacimiento" class="dropdown"
                dateFormat="dd-mm-yy" showIcon>
            </date-picker>
        </div>
    </div>
</template>
<style scoped="scoped">
    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
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
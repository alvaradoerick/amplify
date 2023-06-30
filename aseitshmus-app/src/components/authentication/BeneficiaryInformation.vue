<script setup="setup">
    import {
        ref,
        defineEmits,
        watch,
        computed
    } from 'vue';

    const beneficiaryInfo = ref([{
        BeneficiaryName: null,
        BeneficiaryNumberId: null,
        BeneficiaryRelation: null,
        BeneficiaryPercentage: null
    }]);

    const addRow = () => {
        beneficiaryInfo.value.push({
            BeneficiaryName: null,
            BeneficiaryNumberId: null,
            BeneficiaryRelation: null,
            BeneficiaryPercentage: null
        });
    };

    const removeRow = (index) => {
        beneficiaryInfo.value.splice(index, 1);
    };
    const emits = defineEmits(['beneficiary-info'])

    const showRemoveButton = computed(() => {
        return beneficiaryInfo.value.length > 1;
    });

    watch(beneficiaryInfo.value, (newValue) => {
        emits('beneficiary-info', newValue)
    })
</script>

<template>
    <div class="container">
        <div class="header">
            <base-button :label="'+'" style="width:3rem" class="buttons" @click="addRow" :type="'button'" />
        </div>
        <div class="body">
            <div v-for="(beneficiary, index) in beneficiaryInfo" :key="index" class="form-row">
                <div class="p-float-label">
                    <input-text placeholder="Nombre completo" class="input-text form-margin-right" :id="'beneficiary-name-' + index"
                        type="text" v-model="beneficiary.BeneficiaryName"></input-text>
                    <label :for="'beneficiary-name-' + index">Nombre completo</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text form-margin-right" id="beneficiary-id" placeholder="Identificación"
                        type="text" v-model="beneficiary.BeneficiaryNumberId">

                    </input-text>
                    <label for="beneficiary-id">Identificación</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text form-margin-right" placeholder="Parentesco" id="beneficiary-keen"
                        type="text" v-model="beneficiary.BeneficiaryRelation">
                    </input-text>
                    <label for="beneficiary-keen">Parentesco</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text form-margin-right" placeholder="Porcentaje"
                        id="beneficiary-percentage" type="text" v-model="beneficiary.BeneficiaryPercentage">
                    </input-text>
                    <label for="beneficiary-percentage">Porcentaje</label>
                </div>
                <base-button :label="'-'" style="width:3rem" class="buttons" v-if="showRemoveButton" @click="removeRow(index)"
                    :type="'button'" />
            </div>
        </div>
    </div>
</template>

<style scoped>
    .header {
        display: flex;
        width: 100%;
        justify-content: flex-end;
        margin-bottom: 1rem;
    }

    .body {
        margin-top: 2rem;
        overflow: scroll;
        min-height: 28vh;
        max-height: 28vh;
    }

    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        margin-bottom: 0.98em;
    }

    .form-row {
        display: flex;
        justify-content: space-between;
        margin-bottom: 2rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 1rem;
    }

    .dropdown,
    .input-text {
        width: 170px;
    }
</style>
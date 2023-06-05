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
            <base-button :label="'+'" small class="buttons" @click="addRow" :type="'button'" />
           </div>
           <div class="body">
            <div v-for="(beneficiary, index) in beneficiaryInfo" :key="index" class="form-row">
                        <input-text placeholder="Nombre completo" class="input-text form-margin-right" id="beneficiary-name" type="text"
                            v-model="beneficiary.BeneficiaryName">
                        </input-text>
                        <input-text class="input-text form-margin-right" id="beneficiary-id" placeholder="Identificación" type="text"
                            v-model="beneficiary.BeneficiaryNumberId">
                        </input-text>
                        <input-text class="input-text form-margin-right" placeholder="Parentesco" id="beneficiary-keen" type="text"
                            v-model="beneficiary.BeneficiaryRelation">
                        </input-text>
                        <input-text class="input-text form-margin-right" placeholder="Porcentaje" id="beneficiary-percentage" type="text"
                            v-model="beneficiary.BeneficiaryPercentage">
                        </input-text>
                        <base-button :label="'-'" small class="buttons" v-if="showRemoveButton" @click="removeRow(index)" :type="'button'" />
                </div>
            </div>
    </div>
</template>

<style scoped>
.header {
 display:flex;
    width: 100%;
    justify-content: flex-end;
    margin-bottom: 3rem;
}
.body {
   overflow: scroll;
    min-height: 23vh;
    max-height: 23vh;
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

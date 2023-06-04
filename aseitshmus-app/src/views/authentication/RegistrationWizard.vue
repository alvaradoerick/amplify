<script setup="setup">
import PersonalInformation from '@/components/authentication/PersonalInformation.vue'
import WorkInformation from '@/components/authentication/WorkInformation.vue'
import AddressInformation from '@/components/authentication/AddressInformation.vue'
import BeneficiaryInformation from '@/components/authentication/BeneficiaryInformation.vue'
import {
    useStore
} from 'vuex';
import {
    ref,

} from 'vue';
import {
    useRouter
} from 'vue-router';

import Stepper from '@/components/UI/Stepper.vue'

const router = useRouter();
const store = useStore()
const personalInfo = ref({
    PersonId: null,
    NumberId: null,
    firstName: null,
    lastName1: null,
    LastName2: null,
    Nationality: null,
    DateBirth: null
});
const workInfo = ref({
    WorkStartDate: null,
    PhoneNumber: null,
    EmailAddress: null,
    BankAccount: null
});
const addressInfo = ref({
    Address1: null,
    Address2: null,
    DistrictId: null,
    PostalCode: null
});
const beneficiaryInfo = ref([{
    BeneficiaryName: null,
    BeneficiaryNumberId: null,
    BeneficiaryRelation: null,
    BeneficiaryPercentage: null
}]);
const backLabel = 'Atrás';
const forwardLabel = 'Siguiente';
const submitLabel = 'Enviar';
const activeIndex = ref(1);

const getDataFromPersonalInfo = (value) => {
    personalInfo.value = {
        ...personalInfo.value,
        ...value
    }
}

const getDataFromWorkInfo = (value) => {
    workInfo.value = {
        ...workInfo.value,
        ...value
    }
}

const getDataFromAddressInfo = (value) => {
    addressInfo.value = {
        ...addressInfo.value,
        ...value
    }
}

const getDataFromBeneficiaryInfo = (value) => {
    beneficiaryInfo.value = value
}

const storeUser = () => {
    store.dispatch('auth/register', {
        personalInfo: personalInfo.value,
        workInfo: workInfo.value,
        addressInfo: addressInfo.value,
        beneficiaryInfo: beneficiaryInfo.value,
    })
}

const errors = ref("");

const submitData = () => {
    try {         storeUser();
        router.push({ name: "login" });
    } catch (e) {console.error(e) }
    
}


const prevStep = () => {
    activeIndex.value -= 1
}

const nextStep = () => {
    activeIndex.value += 1
}

const items = ref([{
    label: 'Información Personal',  
    id: 1
},
{
    label: 'Datos Empresariales',
    id: 2
},
{
    label: 'Domicilio',
    id: 3
},
{
    label: 'Beneficiarios',
    id: 4
}
]);

</script>

<template>
    <div class="register">
        <div class="header">
            <div class="steps">
                <stepper :items="items" :active="activeIndex" aria-label="Form Steps"/>
            </div>
            <div>
                <keep-alive>
                    <PersonalInformation v-if="activeIndex === 1" @personal-info="getDataFromPersonalInfo" />
                </keep-alive>
                <keep-alive>
                    <WorkInformation v-if="activeIndex === 2" @work-info="getDataFromWorkInfo" />
                </keep-alive>
                <keep-alive>
                    <AddressInformation v-if="activeIndex === 3" @address-info="getDataFromAddressInfo" />
                </keep-alive>
                <keep-alive>
                    <BeneficiaryInformation v-if="activeIndex === 4" @beneficiary-info="getDataFromBeneficiaryInfo" />
                </keep-alive>
            </div>
        </div>
        <div class="actions">
            <base-button :label="backLabel" v-if="activeIndex > 1" @click="prevStep" :type="'button'" />
            <base-button :label="forwardLabel" v-if="activeIndex < 4" @click="nextStep" :type="'button'" />
            <base-button :label="submitLabel" v-else :type="'submit'" @click="submitData" />
        </div>
        <div class="errors" if="errors">
            <span v-for="error in errors" :key="error.message">{{error.message}}</span>
        </div>
    </div>
</template>

<style scoped="scoped">

.register {
    display: flex;
    flex-direction: column;
}

.header {
    display: flex;
    flex-direction: column;
    align-items: center;
    min-height: 28vh;
}


.actions {
    display: flex;
    flex: 1;
    align-items: center;
    justify-content: space-between;
}

.steps {
    margin-bottom: 25px;
    width: 100%;
}

.steps .steps-item-active {
  background-color: #007bff;
  color: #fff;
}

.erros{
    display:flex;
    flex-direction: column;
    color: red;
}
</style>

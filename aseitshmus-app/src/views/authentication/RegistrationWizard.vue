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
        computed
    } from 'vue';
    //import {
     //   useRouter
    //} from 'vue-router';
    import {
        useToast
    } from 'primevue/usetoast';
    // import useVuelidate from '@vuelidate/core'
    // import {
    //     email,
    //     required
    // } from '@vuelidate/validators'

    import Stepper from '@/components/UI/Stepper.vue'
import RegistrationConfirmation from '@/components/authentication/RegistrationConfirmation.vue';
   // const router = useRouter();
const store = useStore()
const toast = useToast();
    
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

    // const rules = {
    //     PersonId: {
    //         required
    //     },
    //     NumberId: {
    //         required
    //     },
    //     firstName: {
    //         required
    //     },
    //     lastName1: {
    //         required
    //     },
    //     Nationality: {
    //         required
    //     },
    //     DateBirth: {
    //         required
    //     },
    //     WorkStartDate: {
    //         required
    //     },
    //     PhoneNumber: {
    //         required
    //     },
    // EmailAddress: {
    //     email,
    //     required
    // },
    //     BankAccount: {
    //         required
    //     },
    //     Address1: {
    //         required
    //     },
    //     DistrictId: {
    //         required
    //     },
    //     PostalCode: {
    //         required
    //     },
    //     BeneficiaryName: {
    //         required
    //     },
    //     BeneficiaryNumberId: {
    //         required
    //     },
    //     BeneficiaryRelation: {
    //         required
    //     },
    //     BeneficiaryPercentage: {
    //         required
    //     }
    // }

    const loginResponse = computed(() => {
        return store.getters["auth/getErrorResponse"];
    });
    
    const  storeUser = async () => {
       await store.dispatch('auth/register', {
            personalInfo: personalInfo.value,
            workInfo: workInfo.value,
            addressInfo: addressInfo.value,
            beneficiaryInfo: beneficiaryInfo.value,
        })
    }


//const resetValidation = () => {
//     v$.value.$reset();
// };
//const v$ = useVuelidate(rules, personalInfo, workInfo, addressInfo, beneficiaryInfo);
// const validateForm = async () => {      
//     const result = await v$.value.$validate();
//         if (!result) {
//             if (
//                 personalInfo.value.PersonId === null ||  
//             personalInfo.value.NumberId === null ||
//             personalInfo.value.firstName === null ||
//             personalInfo.value.lastName1 === null ||
//             personalInfo.value.Nationality === null ||
//             personalInfo.value.DateBirth === null ||
//             workInfo.value.WorkStartDate === null ||
//             workInfo.value.PhoneNumber === null ||
//             workInfo.value.EmailAddress === null ||
//             workInfo.value.BankAccount === null ||
//             addressInfo.value.Address1 === null ||
//             addressInfo.value.DistrictId === null ||
//             addressInfo.value.PostalCode === null ||
//                 beneficiaryInfo.value.BeneficiaryName === null ||
//             beneficiaryInfo.value.BeneficiaryName === undefined ||
//                 beneficiaryInfo.value.BeneficiaryNumberId === null ||
//              beneficiaryInfo.value.BeneficiaryNumberId === undefined ||
//                 beneficiaryInfo.value.BeneficiaryRelation === null ||
//              beneficiaryInfo.value.BeneficiaryRelation === undefined ||
//                 beneficiaryInfo.value.BeneficiaryPercentage === null ||
//              beneficiaryInfo.value.BeneficiaryPercentage === undefined) {
//             toast.add({
//                 severity: 'error',
//                 summary: 'Error',
//                 detail: 'Algunos campos requeridos están en blanco.',
//                 life: 2000
//             });
//             return false; }
       
//     else if (v$.value.EmailAddress.$error) {
//         toast.add({
//             severity: 'error',
//             summary: 'Error',
//             detail: 'El formato del correo es incorrecto.',
//             life: 2000
//         });
//         return false;
//     }
//     return true;
//         }       
// }
const submitData = async (event) => {
    event.preventDefault();
   // resetValidation();   
    //const isValid = await validateForm();
    // console.log(isValid)
   // if (isValid) {
        await storeUser();
        if (loginResponse.value !== null) {
            toast.add({
                severity: 'error',
                summary: 'Error',
                detail: loginResponse.value,
                life: 2000
            });
        } else {
            nextStep();
        }
   // }
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
        },
        {
            label: 'Confirmación',
            id: 5
        }
    ]);
</script>

<template>
    <div class="register">
        <toast-component />
        <div class="header">
            <div class="steps">
                <stepper :items="items" :active="activeIndex" aria-label="Form Steps" />
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
                <keep-alive>
                    <RegistrationConfirmation v-if="activeIndex === 5"/>
                </keep-alive>
            </div>
        </div>
        <div class="actions">
            <base-button :label="backLabel" v-if="activeIndex > 1 && activeIndex < 5" @click="prevStep" :type="'button'" />
            <base-button :label="forwardLabel" v-if="activeIndex < 4" @click="nextStep" :type="'button'" />
            <base-button :label="submitLabel" v-if="activeIndex == 4" :type="'submit'" @click="submitData" />
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

    .erros {
        display: flex;
        flex-direction: column;
        color: red;
    }
</style>
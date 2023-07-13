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
    import {
        useToast
    } from 'primevue/usetoast';

    import {
        required,
        email
    } from '@vuelidate/validators'
    import useVuelidate from '@vuelidate/core'
    import Stepper from '@/components/UI/Stepper.vue'
    import RegistrationConfirmation from '@/components/authentication/RegistrationConfirmation.vue';

    const store = useStore()
    const toast = useToast();

    const personalInfo = ref({
        PersonId: null,
        NumberId: null,
        FirstName: null,
        LastName1: null,
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
    let beneficiaryInvalid = false;

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


    const loginResponse = computed(() => {
        return store.getters["auth/getErrorResponse"];
    });

    const storeUser = async () => {
        await store.dispatch('auth/register', {
            personalInfo: personalInfo.value,
            workInfo: workInfo.value,
            addressInfo: addressInfo.value,
            beneficiaryInfo: beneficiaryInfo.value,
        })
    }


    const personalRules = {
        PersonId: {
            required
        },
        NumberId: {
            required
        },
        FirstName: {
            required
        },
        LastName1: {
            required
        },
        Nationality: {
            required
        },
        DateBirth: {
            required
        }

    };

    const vpersonal$ = useVuelidate(personalRules, personalInfo);

    const addressRules = {
        Address1: {
            required
        },
        DistrictId: {
            required
        },
        PostalCode: {
            required
        }
    }

    const vAddress$ = useVuelidate(addressRules, addressInfo)

    const workRules = {
        WorkStartDate: {
            required
        },
        PhoneNumber: {
            required
        },
        EmailAddress: {
            required,
            email
        },
        BankAccount: {
            required
        }
    }

    const vWork$ = useVuelidate(workRules, workInfo)

    const beneficiaryRules = {
        BeneficiaryName: {
            required
        },
        BeneficiaryNumberId: {
            required
        },
        BeneficiaryRelation: {
            required
        },
        BeneficiaryPercentage: {
            required
        }
    };

    const vBeneficiary$ = computed(() => {
        return beneficiaryInfo.value.map(beneficiary => useVuelidate(beneficiaryRules, beneficiary).value);
    });

    const validateBeneficiaryInfo = async () => {
        const results = await Promise.all(vBeneficiary$.value.map(validation => validation.$validate()));
        return results.every(result => result);
    };

    const validateForm = async () => {
        let result = false
        if (activeIndex.value === 1) {
            result = await vpersonal$?.value?.$validate();
        } else if (activeIndex.value === 2) {
            result = await vWork$ ?.value?.$validate();
            console.log(vWork$)
        } else if (activeIndex.value === 3) {
            result = await vAddress$?.value?.$validate();
        } else if (activeIndex.value === 4) {
            result = await validateBeneficiaryInfo();
            beneficiaryInvalid = true;
        }

        if (!result) {
            if (vpersonal$?.value?.$error) {
                toast.add({
                    severity: 'error',
                    detail: 'Por favor revisar los campos de la información personal en rojo.',
                    life: 2000
                });
            } else if (vWork$?.value?.$error) {
                toast.add({
                    severity: 'error',
                    detail: 'Por favor revisar los campos de los datos empresariales en rojo.',
                    life: 2000
                });
            } 
            else if (vAddress$?.value?.$error) {
                toast.add({
                    severity: 'error',
                    detail: 'Por favor revisar los campos del domicilio en rojo.',
                    life: 2000
                });
            } else if (beneficiaryInvalid) {
                toast.add({
                    severity: 'error',
                    detail: 'Por favor revisar los campos de los beneficiarios en rojo.',
                    life: 2000
                });
            }
            return false;
        }

        return true
    }

    const isValiData = ref(false)

    const submitData = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
                await storeUser();
                if (loginResponse.value !== null) {
                    isValiData.value = true
                    toast.add({
                        severity: 'error',
                        detail: loginResponse.value,
                        life: 2000
                    });
                    store.commit('auth/clearErrorResponse');
                } else {
                    nextStep();
                }
            } catch (error) {
                toast.add({
                    severity: 'error',
                    detail: 'Ocurrió un error.',
                    life: 2000
                });
            }
        }
    }


    const prevStep = () => {
        activeIndex.value -= 1
    }

    const nextStep = async () => {
        const isValid = await validateForm();
        if (isValid) {
            try {
                activeIndex.value += 1;
            } catch (error) {
                toast.add({
                    severity: 'error',
                    detail: 'Ocurrió un error',
                    life: 2000
                });
            }
        }
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
            <div class="body">
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
                    <RegistrationConfirmation v-if="activeIndex === 5" />
                </keep-alive>
            </div>
        </div>
        <div class="actions">
            <base-button :label="backLabel" v-if="activeIndex > 1 && activeIndex < 5" @click="prevStep"
                :type="'button'" />
            <base-button :label="forwardLabel" v-if="activeIndex < 4" @click="nextStep" :type="'button'" />
            <base-button :label="submitLabel" v-if="activeIndex == 4" :type="'submit'" @click="submitData" />
        </div>
    </div>
</template>

<style scoped="scoped">
    .register {
        display: flex;
        flex-direction: column;
        border: 1px solid #ccc;
        border-radius: 5px;
        padding: 2rem;
        width: 100%;
    }

    .header {
        display: flex;
        flex-direction: column;
        align-items: center;
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
</style>

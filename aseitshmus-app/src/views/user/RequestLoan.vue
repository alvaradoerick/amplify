<script setup>
    import useVuelidate from '@vuelidate/core'
    import {
        required
    } from '@vuelidate/validators'
    import axios from "axios";
    import {
        useStore
    } from 'vuex'
    import {
        useRouter
    } from 'vue-router';
    import {
        ref,
        onMounted,
        computed
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';

    const apiUrl = process.env["VUE_APP_BASED_URL"]

    const {
        getters,
        dispatch
    } = useStore();
    const router = useRouter();
    const toast = useToast();

    const backLabel = 'Cancelar';
    const toReturn = () => {
        router.push({
            name: "myDashboard"
        });
    }
    const enteredBankAccount = ref(null)
    const sendLabel = 'Enviar';
    const loanTypesList = ref([]);
    const selectedLoanType = ref(null);

    const selectedBankAccount = ref(null);
    const BankAccountList = ref([]);

    //el objeto para completar la solicitud
    const loanRequest = ref({
        LoansTypeId: selectedLoanType,
        AmountRequested: 0,
        Term: null,
        BankAccount: null,
        RequestedDate: null
    })

    //los calculos para el grid
    const calculatedValues = ref({
        AvailEmployeeAmt: null,
        AvailEmployerAmt: null,
        TotalAvailAmount: null,
        BiweeklyFee: null,
        TotalAmtToPay: null,
    })

    //para actualizar la nueva cuenta bancaria o la existente en el objeto
    const updatedLoanData = computed(() => {
        return {
            ...loanRequest.value,
            BankAccount: selectedBankAccount.value === 'Otra cuenta' ? enteredBankAccount.value :
                selectedBankAccount.value,
        };
    });

    const fetchUserData = async () => {
        await dispatch('users/getById');
       let  responseData = getters["users/getUsers"];
        selectedBankAccount.value = responseData.BankAccount ? responseData.BankAccount : null;
        if (responseData.BankAccount !== null && responseData.BankAccount !== undefined) {
            BankAccountList.value = [{
                    value: responseData.BankAccount,
                    label: responseData.BankAccount
                },
                {
                    value: 'Otra cuenta',
                    label: 'Otra cuenta'
                }
            ];
        } else {
            BankAccountList.value = [{
                value: 'Otra cuenta',
                label: 'Otra cuenta'
            }];
        }

    };

    const storeLoan = async () => {
        await dispatch('loanRequests/addLoanRequest', {
            loanRequest: updatedLoanData.value,
        });
    };

    const fetchActiveLoanTypes = async () => {
        try {
            const response = await axios.get(`${apiUrl}/LoansType/active-loans`);
            loanTypesList.value = response.data;
            if (response.data.length > 0) {
                selectedLoanType.value = response.data[0].LoansTypeId;
            }
        } catch (error) {
            toast.add({
                severity: 'success',
                detail: error,
                life: 2000
            });
        }
    };

    const getSelectedLoanType = async () => {

        const selectedType = loanTypesList.value.find(type => type.LoansTypeId === selectedLoanType.value);
        console.log(selectedLoanType);
        if (selectedType) {
             fetchCalculation();
               console.log("sera aqui?");
        }
        console.log(selectedType);
        return selectedType;
    };
  

    const fetchCalculation =  () => {
        const loanData = {
            Amount: loanRequest.value.AmountRequested,
            Term: loanRequest.value.Term,
            LoansTypeId: selectedLoanType.value,
        };
         dispatch('loanRequests/getLoanCalculation', {
            loanData: loanData,
        });
  calculatedValues.value = getters['loanRequests/getLoanCalculation'];
  console.log(calculatedValues.value)
    };
   
    const rules = {
        RequestedDate: {
            required
        },
        Term: {
            required
        },
    }

    const v$ = useVuelidate(rules, loanRequest);

    const validateForm = async () => {
        const result = await v$.value.$validate();
        if (!result) {
            if (v$.value.$errors[0].$validator === 'required') {
                toast.add({
                    severity: 'error',
                    detail: 'Todos los campos son requeridos.',
                    life: 2000
                });
            }
            return false
        }
        return true;
    }

    const onSend = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
                await storeLoan();
                toast.add({
                    severity: 'success',
                    detail: "Su solicitud de crédito ha sido enviada.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                toReturn();
            } catch (error) {
                toast.add({
                    severity: 'error',
                    detail: error,
                    life: 2000
                });
            }
        }
    }

    onMounted(fetchActiveLoanTypes(), fetchUserData(), getSelectedLoanType());

</script>
<template>
    <div class="main">
        <toast-component />
        <div class="form">
            <div>
                <div class="form-row">
                    <div class="p-float-label">
                        <drop-down v-model="selectedLoanType" :options="loanTypesList" optionLabel="Description"
                            optionValue="LoansTypeId" placeholder="Tipo de crédito" class="dropdown form-margin-right"
                            id="loan-type" />
                        <label for="loan-type">Tipo de crédito</label>
                    </div>
                    <div class="p-float-label">
                        <input-number placeholder="Monto quincenal" class=" input-text " id="amount" mode="currency"
                            currency="USD" locale="en-US" v-model="loanRequest.AmountRequested"
                            :class="{'p-invalid': v$?.AmountRequested?.$error}" />
                        <label for="amount">Monto a solicitar</label>
                    </div>
                    <div class="p-float-label  form-margin-left">
                        <date-picker v-model="loanRequest.RequestedDate" placeholder="Fecha de solicitud de crédito"
                            class="dropdown" dateFormat="dd-mm-yy" showIcon id="requested-day"
                            :class="{'p-invalid': v$?.RequestedDate?.$error}" />
                        <label for="requested-day">Fecha de solicitud de crédito</label>
                    </div>
                </div>
                <div class="form-row">
                    <div class="p-float-label">
                        <input-number placeholder="Monto quincenal" class="dropdown form-margin-right" id="term"
                            v-model="loanRequest.Term"
                            v-tooltip.focus="'De 1 mes hasta 5 años. Para compra de vehículo hasta por 10 años.'"
                            :class="{'p-invalid': v$?.Term?.$error}" />
                        <label for="term">Plazo (meses)</label>
                    </div>
                    <div class="p-float-label">
                        <drop-down v-model="selectedBankAccount" :options="BankAccountList" optionLabel="label"
                            optionValue="value" placeholder="Tipo de crédito" class="dropdown" id="bank-account" />
                        <label for="bank-account">Cuenta bancaria (IBAN)</label>
                    </div>
                    <div class="p-float-label form-margin-left" v-if="selectedBankAccount === 'Otra cuenta'">
                        <input-text placeholder="Otra cuenta" class="input-text" id="other-account" type="text"
                            v-model="enteredBankAccount" />
                        <label for="other-account">Otra cuenta</label>
                    </div>
                </div>
                <div v-if="loanTypesList.length > 0">
                    <data-table :value="[loanTypesList.find(type => type.LoansTypeId === selectedLoanType)]"
                        showGridlines :paginator="false">


                        <data-column header="Monto disponible de ahorro:" style="width: 200px">
                            <template #body="{ }">
                                {{ calculatedValues.AvailEmployeeAmt ?? 'N/A' }}
                            </template>
                        </data-column>
                        <data-column header="Monto disponible de aporte:" style="width: 200px">
                            <template #body="{ }">
                                {{ calculatedValues.AvailEmployerAmt ?? 'N/A' }}
                              
                            </template>
                        </data-column>
                        <data-column header="Total disponible:">
                            <template #body="{}">
                                {{ calculatedValues.TotalAvailAmount ?? 'N/A' }}
                            </template>
                        </data-column>
                        <data-column header="Tasa de interés:">
                            <template #body="{}">
                             
                            </template>
                        </data-column>
                        <data-column header="Cuota quincenal:">
                            <template #body="{}">
                                ferf
                            </template>
                        </data-column>
                        <data-column header="Total a pagar:">
                            <template #body="{}">
                                ferf
                            </template>
                        </data-column>
                    </data-table>
                </div>
            </div>
            <div class="actions">
                <base-button :label="backLabel" @click="toReturn" small :type="'button'" />
                <base-button :label="sendLabel" @click="onSend" small :type="'submit'" />
            </div>
        </div>
    </div>
</template>

<style scoped>
    .main {
        display: flex;
        justify-content: center;
        align-items: center;
        border: 1px solid #ebebeb;
        border-radius: 5px;
        margin: 1rem;
        padding: 2rem;
    }

    .form {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
    }

    .dropdownLarger {
        display: flex;
        width: 300px;
    }

    .form-row {
        margin-top: 2rem;
        display: flex;
        justify-content: space-between;
        align-self: center;
        margin-bottom: 2rem;
        width: 60%;
    }

    .form-margin-right {
        margin-right: 6rem;
    }

    .form-margin-left {
        margin-left: 6rem;
    }

    .actions {
        margin-top: 2rem;
        display: flex;
        flex-direction: row;
        justify-content: flex-end;
        align-self: flex-end;
    }

    .actions button {
        flex: 1;
        margin-right: 1rem;
    }

    .upload-button {
        display: flex;
        background-color: #253e8b;
        border-color: #253e8b;
        overflow: hidden;
        width: 300px;
        color: white;
        text-align: center;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }


    .upload-button:hover,
    .upload-button:focus {
        box-shadow: 0 0 0 2px white, 0 0 0 3px skyblue;
        color: white;
        background-color: #3f569b !important;
    }
</style>
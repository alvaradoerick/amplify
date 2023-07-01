<script setup>
    import {
        useStore
    } from 'vuex'
    import axios from "axios";
    import {
        useRouter
    } from 'vue-router';
    import {
        ref,
        onMounted
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';
    import {
        required
    } from '@vuelidate/validators'
    import useVuelidate from '@vuelidate/core'
    const apiUrl = process.env["VUE_APP_BASED_URL"]

    const router = useRouter();
    const toast = useToast();
    const store = useStore();

    const backLabel = 'Cancelar';
    const typeList = () => {
        router.push({
            name: "typeList"
        });
    }
    const sendLabel = 'Crear';
    const selectedState = ref(1);
    const status = ref([{
            name: 'Activo',
            value: 1
        },
        {
            name: 'Inactivo',
            value: 0
        }
    ]);

    const contributionName = ref(null)

    const fetchContributionData = async () => {
        try {
            const response = await axios.get(`${apiUrl}/contributionusage`);
            contributionName.value = response.data;
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: error,
                life: 2000
            });
        }
    };
    const selectedContribution = ref(1);

    const loanType = ref({
        LoanDescription: null,
        ContributionUsageId: selectedContribution,
        PercentageEmployeeCont: 0,
        PercentageEmployerCont: 0,
        Term: 0,
        InterestRate: 0,
        IsActive: selectedState
    })

    const rules = {
        LoanDescription: {
            required
        },
        PercentageEmployeeCont: {
            required
        },
        Term: {
            required
        },
        InterestRate: {
            required
        },
        IsActive: {
            required
        },
    };

    const storeType = async () => {
        await store.dispatch('loanTypes/addType', {
            loanType: loanType.value
        })
    }

    const v$ = useVuelidate(rules, loanType);
    const validateForm = async () => {
        const result = await v$.value.$validate();
        if (!result) {
            if (v$.value.$errors[0].$validator === 'required') {
                toast.add({
                    severity: 'error',
                    detail: 'Por favor revisar los campos en rojo.',
                    life: 2000
                });
            }
            return false
        }
        return true;
    }

    const submitData = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
            if (isValid) {
                try {
                    await storeType();
                    toast.add({
                        severity: 'success',
                        detail: "Su tipo de préstamo ha sido agregado.",
                        life: 2000
                    });
                    await new Promise((resolve) => setTimeout(resolve, 1000));
                    typeList()
                } catch (error) {
                    toast.add({
                        severity: 'error',
                        detail: error,
                        life: 2000
                    });
                }
        }
    }

    onMounted(fetchContributionData);
</script>

<template>

    <div class="main">
        <toast-component />
        <div class="form">
            <div>
                <div class="form-row">
                    <div class="p-float-label">
                        <input-text placeholder="Tipo de préstamo" class=" input-text form-margin-right" id="typeName"
                            type="text" v-model="loanType.LoanDescription"
                            :class="{'p-invalid': v$?.LoanDescription?.$error}" />
                        <label for="typeName">Tipo de préstamo</label>
                    </div>

                    <div class="p-float-label">
                        <input-number placeholder="Interés" class=" input-text" id="interest-rate" type="text"
                            v-model="loanType.InterestRate" :class="{'p-invalid': v$?.InterestRate?.$error}" :maxFractionDigits="2" />
                        <label for="interest-rate">Interés</label>
                        <span class="percentage-sign">%</span>
                    </div>

                    <div class="p-float-label form-margin-left">
                        <drop-down v-model="selectedState" :options="status" optionLabel="name" optionValue="value"
                            placeholder="Estado" class="dropdown" id="status"
                            :class="{'p-invalid': v$?.IsActive?.$error}" />
                        <label for="status">Estado</label>
                    </div>
                </div>
                <div class="form-row">
                    <div class="p-float-label form-margin-right">
                        <drop-down v-model="selectedContribution" :options="contributionName" optionLabel="Description"
                            optionValue="ContributionUsageId" class="dropdownLarger" id="contributionUsage"
                            :class="{'p-invalid': v$?.selectedContribution?.$error}" />
                        <label for="contributionUsage">Capacidad</label>
                    </div>
                    <div class="p-float-label">
                        <input-number placeholder="Porcentaje ahorro obrero" class=" input-text" id="percentage-employee"
                            type="text" v-model="loanType.PercentageEmployeeCont "
                            :class="{'p-invalid': v$?.PercentageEmployeeCont?.$error}" :maxFractionDigits="2" />
                        <label for="percentage-employee">Porcentaje ahorro obrero</label>
                        <span class="percentage-sign">%</span>
                    </div>
                    <div class="p-float-label form-margin-left" v-if="selectedContribution == '2'">
                        <input-number placeholder="Porcentaje ahorro patronal" class="input-text" id="percentage-employer"
                            type="text" v-model="loanType.PercentageEmployerCont"
                            :class="{'p-invalid': v$?.PercentageEmployerCont?.$error}" :maxFractionDigits="2" />
                        <label for="percentage-employer ">Porcentaje ahorro patronal</label>
                        <span class="percentage-sign">%</span>
                    </div>
                    <div class="p-float-label form-margin-left" v-if="selectedContribution == '1'">
                        <input-number placeholder="Plazo" class=" input-text" id="term"  
                            v-model="loanType.Term" :class="{'p-invalid': v$?.Term?.$error}"/>
                        <label for="term">Plazo (meses)</label>
                    </div>
                </div>
                <div class="form-row" v-if="selectedContribution == '2'">
                    <div class="p-float-label form-margin-right">
                        <input-number placeholder="Plazo" class=" input-text" id="term" 
                            v-model="loanType.Term" :class="{'p-invalid': v$?.Term?.$error}"/>
                        <label for="term">Plazo (meses)</label>
              
                    </div>
                </div>
            </div>
            <div class="actions">
                <base-button :label="backLabel" small @click="typeList" :type="'button'" />
                <base-button :label="sendLabel" small @click="submitData" :type="'submit'" />
            </div>
        </div>
    </div>
</template>

<style scoped="scoped">
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
        margin-top: 6rem;
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

    .percentage-sign {
        position: absolute;
        top: 50%;
        right: 1rem;
        transform: translateY(-50%);
    }
</style>
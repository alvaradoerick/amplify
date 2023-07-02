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

    const store = useStore();
    const router = useRouter();
    const toast = useToast();

    const dateFormat = {
        day: "numeric",
        month: "numeric",
        year: "numeric"
    };

    const backLabel = 'Cancelar';
    const toReturn = () => {
        router.push({
            name: "myDashboard"
        });
    }
    const sendLabel = 'Enviar';
    const savingsTypeList = ref([]);
    const selectedSavingsType = ref(null);

    const savingsData = ref({
        SavingsTypeId: selectedSavingsType,
        Amount: 0.00
    })

    const storeSavings = async () => {
        await store.dispatch('savingsRequests/addSavingsRequest', {
            savingsData: savingsData.value,
        });
    };

    const fetchActiveSavings = async () => {
        try {
            const response = await axios.get(`${apiUrl}/SavingsType/active-savings`);
            savingsTypeList.value = response.data;
            if (response.data.length > 0) {
                selectedSavingsType.value = response.data[0].SavingsTypeId;
            }
        } catch (error) {
            toast.add({
                severity: 'success',
                detail: error,
                life: 2000
            });
        }
    };
    const getSelectedSavingsType = () => {
        return savingsTypeList.value.find(type => type.SavingsTypeId === selectedSavingsType.value);
    };
    const rules = {
        Amount: {
            required
        }
    }


    const numberOfBiweeklies = computed(() => {
        const startDate = new Date(getSelectedSavingsType()?.StartDate);
        const endDate = new Date(getSelectedSavingsType()?.EndDate);

        const isStartBefore15th = startDate.getDate() < 15;
        const isStartOn15th = startDate.getDate() === 15;

        const isEndAfter15thBefore30th = endDate.getDate() > 15 && endDate.getDate() < 30;
        const isEndOn30th = endDate.getDate() === 30;
        const isEndBefore15th = endDate.getDate() < 15;
        const isEndOn15th = endDate.getDate() === 15;
        const isEndOnFeb = endDate.getMonth() === 1 && (endDate.getDate() === 29 || endDate.getDate() === 28);
        const isCurrentMonth = endDate.getMonth() === startDate.getMonth();



        let biweeklies = 0;
        let months = 0

        if (isStartBefore15th) {
            if (isEndBefore15th) {
                if (isCurrentMonth) {
                    biweeklies = 0;
                } else {
                    biweeklies = 2;
                }
            } else if (isEndOn15th) {
                if (isCurrentMonth) {
                    biweeklies = 1;
                } else {
                    biweeklies = 2;
                }
            } else if (isEndAfter15thBefore30th) {
                if (isCurrentMonth) {
                    biweeklies = 1;
                } else {
                    biweeklies = 2;
                }
            } else {
                biweeklies = 2
            }
        } else {
            if (isEndOnFeb) {
                if (isStartOn15th){biweeklies = 2;}
               else{biweeklies = 1;} 
            }   
else if (isEndAfter15thBefore30th) {
                if (isCurrentMonth) {
                    biweeklies = 0;

                } else {
                    biweeklies = 2;
                }
                } else if (isEndOn30th) {
                biweeklies = 1;
            } else if (isEndOn15th) {
                if (isCurrentMonth) {
                    biweeklies = 1;
                } else {
                    
                    if (isStartOn15th){biweeklies = 2;}
               else{biweeklies = 1;} 
                }
            } else {
                biweeklies = 1;
            }
        }

        //calculo de num de meses 

        if (endDate.getDate() < 15 && isStartBefore15th) {
            months = months = (endDate.getFullYear() - startDate.getFullYear()) * 12 + (startDate.getMonth() -
                endDate.getMonth()) * -1;
        } else if (endDate.getDate() === 15 && isStartOn15th) {
            months = months = (endDate.getFullYear() - startDate.getFullYear()) * 12 + (startDate.getMonth() -
                endDate.getMonth()) * -1;
        } else {
            months = (endDate.getFullYear() - startDate.getFullYear()) * 12 + (endDate.getMonth() - startDate
                .getMonth()) + 1;
        }
        //calculo de quincenas
        const totalBiweeklies = biweeklies * months;
        return totalBiweeklies;
    });

    const estimatedSavings = computed(() => {
        return numberOfBiweeklies.value * savingsData.value.Amount;
    });
    const v$ = useVuelidate(rules, savingsData);

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
                await storeSavings();
                toast.add({
                    severity: 'success',
                    detail: "Su solicitud de ahorro ha sido enviada.",
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
                console.log(error)
            }
        }
    }

    onMounted(fetchActiveSavings)
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="form">
            <div class="header">
                <div class="form-row">
                    <div class="p-float-label">
                        <drop-down v-model="selectedSavingsType" :options="savingsTypeList" optionLabel="Description"
                            optionValue="SavingsTypeId" placeholder="Ahorro" class="dropdown form-margin-right"
                            id="status" :class="{'p-invalid': v$?.selectedState?.$error}" />
                        <label for="savings-type">Ahorro</label>
                    </div>
                    <div class="p-float-label">
                        <input-number placeholder="Monto quincenal" class=" input-text " id="amount" mode="currency"
                            currency="USD" locale="en-US" v-model="savingsData.Amount"
                            :class="{'p-invalid': v$?.Amount?.$error}" />
                        <label for="amount">Monto quincenal</label>
                    </div>
                </div>
                <p v-if="savingsTypeList.length > 0">
                    <label><b>Empieza: </b></label>
                    {{new Date(getSelectedSavingsType()?.StartDate).toLocaleString("es-ES", dateFormat) }}
                </p>
                <p v-if="savingsTypeList.length > 0">
                    <label><b>Finaliza: </b></label>
                    {{new Date(getSelectedSavingsType()?.EndDate).toLocaleString("es-ES", dateFormat) }}
                </p>
                <p v-if="savingsTypeList.length > 0">
                    <label><b>Monto aproximado al final del ahorro: </b></label>
                    ${{ estimatedSavings }}<label
                        v-if="(estimatedSavings - Math.floor(estimatedSavings)) == 0">.00</label>
                </p>
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
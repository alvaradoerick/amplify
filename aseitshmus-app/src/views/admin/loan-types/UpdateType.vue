<script setup>
    import {
        useStore
    } from 'vuex'
    import {
        useRouter,
        useRoute
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

    const store = useStore();
    const router = useRouter();
    const route = useRoute();
    const toast = useToast();


    const backLabel = 'Cancelar';
    const typeyList = () => {
        router.push({
            name: "typeList"
        });
    }
    const sendLabel = 'Actualizar';
    const status = ref([{
            name: 'Activo',
            value: 1
        },
        {
            name: 'Inactivo',
            value: 0
        }
    ]);

    const loanType = ref({
        Description: null,
        ContributionUsageId:null,
        PercentageEmployeeCont:null,
        PercentageEmployerCont: null,
        Term: null,
        InterestRate: null,
        IsActive: null
    })
    const typeId = ref(route.params.id);
    const rules = {
        Description: {
            required
        },
        ContributionUsageId: {
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
        }
    }

    const storeType = async () => {
        await store.dispatch('loanTypes/updateType', {
            typeId: typeId.value,
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


    const fetchTypeData = async () => {
        await store.dispatch('loanTypes/getTypeById', {
            rowId: typeId.value
        });

        const type = store.getters["loanTypes/getType"];
        try {
            loanType.value.Description = type.LoanDescription,
            loanType.value.ContributionUsageId = type.ContributionUsageId,
            loanType.value.PercentageEmployeeCont = type.PercentageEmployeeCont,
            loanType.value.PercentageEmployerCont = type.PercentageEmployerCont,
            loanType.value.Term = type.Term,
            loanType.value.InterestRate = type.InterestRate,
            loanType.value.IsActive = type.IsActive ? 1 : 0;
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: error,
                life: 2000
            });
        }
    };

    const submitData = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            if (isValid) {
                try {
                    await storeType();
                    toast.add({
                        severity: 'success',
                        detail: "Sus cambios han sido guardados.",
                        life: 2000
                    });
                    await new Promise((resolve) => setTimeout(resolve, 1000));
                    typeyList()
                } catch (error) {
                    toast.add({
                        severity: 'error',
                        detail: error,
                        life: 2000
                    });
                }
            }
        }
    }


    onMounted(fetchTypeData);
</script>

<template>

    <div class="main">
        <toast-component />
        <div class="header">
            <div class="form-row">
                <span class="p-float-label">
                    <input-text placeholder="Tipo de préstamo" class=" input-text form-margin-right" id="typeName" type="text"
                        v-model="loanType.Description" :class="{'hasError': v$?.Description?.$error}" />
                    <label for="typeName">Tipo de préstamo</label>
                </span>
                <span class="p-float-label">
                    <drop-down v-model="loanType.IsActive" :options="status" optionLabel="name"
                        optionValue="value" placeholder="Estado" class="dropdown" id="status"
                        :class="{'hasError': v$?.IsActive?.$error}" />
                    <label for="status">Estado</label>
                </span>
            </div>
        </div>

    </div>
    <div class="actions">
        <base-button :label="backLabel" @click="typeyList" :type="'button'" />
        <base-button :label="sendLabel" @click="submitData" :type="'submit'" />
    </div>
</template>

<style scoped="scoped">
    .main {
        display: flex;
        flex-direction: column;
    }

    .header {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .hasError {
        border-color: red;
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
</style>
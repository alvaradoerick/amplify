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


    const router = useRouter();
    const route = useRoute();
    const toast = useToast();
    const store = useStore();

    const backLabel = 'Cancelar';
    const typeList = () => {
        router.push({
            name: "savingsList"
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
    const savingsType = ref({
        Description: null,
        ApplicationDeadline: null,
        StartDate: null,
        EndDate: null,
        IsActive: null
    })

    const typeId = ref(route.params.id);
    const rules = {
        Description: {
            required
        },
        ApplicationDeadline: {
            required
        },
        StartDate: {
            required
        },
        EndDate: {
            required
        },
        IsActive: {
            required
        },
    };

    const storeType = async () => {
        await store.dispatch('savingsTypes/updateType', {
            typeId: typeId.value,
            savingsType: savingsType.value
        })
    }

    const v$ = useVuelidate(rules, savingsType);
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
        await store.dispatch('savingsTypes/getTypeById', {
            rowId: typeId.value
        });

        const type = store.getters["savingsTypes/getType"];
        try {
            savingsType.value.Description = type.Description,
            savingsType.value.ApplicationDeadline =   new Date(type.ApplicationDeadline),
            savingsType.value.StartDate =   new Date(type.StartDate),
            savingsType.value.EndDate =   new Date(type.EndDate),
            savingsType.value.IsActive = type.IsActive ? 1 : 0;
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
    }


    onMounted(fetchTypeData);
</script>

<template>

    <div class="main">
        <toast-component />
        <div class="form">
            <div>
                <div class="form-row">
                    <div class="p-float-label">
                        <input-text placeholder="Tipo de préstamo" class=" input-text form-margin-right" id="typeName"
                            type="text" v-model="savingsType.Description"
                            :class="{'p-invalid': v$?.LoanDescription?.$error}" />
                        <label for="typeName">Tipo de préstamo</label>
                    </div>
                    <div class="p-float-label">
                        <date-picker v-model="savingsType.ApplicationDeadline" placeholder="Último día de inscripción"
                            class="dropdown form-margin-right" dateFormat="dd-mm-yy" showIcon id="last-day"
                            :class="{'p-invalid': v$?.ApplicationDeadline?.$error }" />
                        <label for="last-day">Último día de inscripción</label>
                    </div>

                    <div class="p-float-label form-margin-left">
                        <drop-down v-model="savingsType.IsActive" :options="status" optionLabel="name" optionValue="value"
                            placeholder="Estado" class="dropdown" id="status"
                            :class="{'p-invalid': v$?.IsActive?.$error}" />
                        <label for="status">Estado</label>
                        <label for="status">Estado</label>
                    </div>
                </div>
                <div class="form-row">
                    <div class="p-float-label">
                        <date-picker v-model="savingsType.StartDate" placeholder="Fecha de inicio"
                            class="dropdown form-margin-right" dateFormat="dd-mm-yy" showIcon id="start-day"
                            :class="{'p-invalid': v$?.StartDate?.$error }" />
                        <label for="start-day">Fecha de inicio</label>
                    </div>
                    <div class="p-float-label">
                        <date-picker v-model="savingsType.EndDate" placeholder="Fecha de finalización"
                            class="dropdown form-margin-right" dateFormat="dd-mm-yy" showIcon id="end-day"
                            :class="{'p-invalid': v$?.EndDate?.$error }" />
                        <label for="end-day">Fecha de finalización</label>
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


</style>
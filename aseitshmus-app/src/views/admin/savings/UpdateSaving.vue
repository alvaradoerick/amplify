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

    const backLabel = 'Atrás';
    const savingsList = () => {
        router.push({
            name: "savingsRequestList"
        });
    }
    const approveLabel = 'Aprobar';
    const rejectLabel = 'Rechazar';

    const savingsData = ref({
        Name: null,
        NumberId: null,
        SavingsTypeId: null,
        SavingsTypeName: null,
        Amount: null,
        ApplicationDate: null,
        IsActive: null,
        ApprovedDate: null,

    })

    const dateFormat = {
        day: "numeric",
        month: "numeric",
        year: "numeric"
    };

    const savingsState = ref({
        IsApproved: null,
    })

    const savingsRequestId = ref(route.params.id);
    const rules = {
        IsApproved: {
            required
        }
    };

    const storeSaving = async () => {
        await store.dispatch('savingsRequests/updateSavings', {
            savingsRequestId: savingsRequestId.value,
            savingsState: savingsState.value
        })
    }

    const v$ = useVuelidate(rules, savingsState);
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

    const fetchSavingsData = async () => {
        await store.dispatch('savingsRequests/getSavingsById', {
            rowId: savingsRequestId.value
        });

        const request = store.getters["savingsRequests/getSavings"];
        try {
            savingsData.value.Name = request.Name,
                savingsData.value.NumberId = request.NumberId,
                savingsData.value.SavingsTypeId = request.SavingsTypeId,
                savingsData.value.SavingsTypeName = request.SavingsTypeName,
                savingsData.value.ApplicationDate = new Date(request.ApplicationDate),
                savingsData.value.Amount = request.Amount,
                savingsData.value.IsActive = request.IsActive ? 'Activo' : 'Inactivo',
                savingsData.value.ApprovedDate = request.ApprovedDate !== null ? new Date(request.ApprovedDate) : null;
                savingsState.value.IsApproved = request.IsApproved !== null ? (request.IsApproved ? 'Aprobado' :
                    "Rechazado") : 'Pendiente'
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
            try {
                if (event.target.innerText === approveLabel) {
                    savingsState.value.IsApproved = 1;
                } else if (event.target.innerText === rejectLabel) {
                    savingsState.value.IsApproved = 0;
                }
                await storeSaving();
                toast.add({
                    severity: 'success',
                    detail: "Sus cambios han sido guardados.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                savingsList()
            } catch (error) {
                toast.add({
                    severity: 'error',
                    detail: error,
                    life: 2000
                });
            }
        }
    }


    onMounted(fetchSavingsData);
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="form">
            <div>
                <strong><label>Código del ahorro:</label></strong>
                <label>&nbsp;{{ savingsRequestId}}</label>
                <br>
                <br>
                <strong><label>Nombre completo:</label></strong>
                <label>&nbsp;{{ savingsData.Name}}</label>
                <br>
                <br>
                <strong><label>Tipo de ahorro:</label></strong>
                <label>&nbsp;{{ savingsData.SavingsTypeName}}</label>
                <br>
                <br>
                <strong><label>Fecha de solicitud:</label></strong>
                <label>&nbsp;{{ savingsData.ApplicationDate ? new Date(savingsData.ApplicationDate).toLocaleString("es-ES", dateFormat) : "N/A" }}</label>
                <br>
                <br>
                <strong><label>Cuota de ahorro (quincenal):</label></strong>
                <label>&nbsp; ${{savingsData.Amount}}<span
                        v-if="(savingsData.Amount - Math.floor(savingsData.Amount)) === 0">.00</span></label>
                <br>
                <br>
                <strong><label>Estado del ahorro:</label></strong>
                <label>&nbsp;{{ savingsData.IsActive}}</label>
                <br>
                <br>
                <strong><label>Estado del ahorro:</label></strong>
                <label>&nbsp;{{ savingsState.IsApproved}}</label>
                <br>
                <br>
                <strong><label>Fecha de aprobación (cuando aplique):</label></strong>
                <label>&nbsp;{{ savingsData.ApprovedDate}}</label>
                <br>
                <br>
                <div class="actions">
                    <base-button :label="backLabel" small @click="savingsList" :type="'button'" />
                    <base-button :label="approveLabel" class="green" small @click="submitData" :type="'submit'" />
                    <base-button :label="rejectLabel"  class="red" v-if="savingsState.IsApproved !== true" small @click="submitData"
                        :type="'submit'" />
                </div>
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

    .green,
    .green:hover,
    .green:focus {
        background-color: rgb(6, 100, 6) !important;
        border-color: rgb(6, 100, 6) !important;
    }

    .red,
    .red:hover,
    .green:focus {
        background-color: rgb(189, 90, 90) !important;
        border-color: rgb(189, 90, 90) !important;
    }
</style>
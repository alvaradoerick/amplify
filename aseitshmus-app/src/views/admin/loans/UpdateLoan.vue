<script setup>
    import {
        useStore,
    } from 'vuex'
    import {
        useRouter,
        useRoute
    } from 'vue-router';
    import {
        ref,
        onMounted,
        computed
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';
    import {
        required
    } from '@vuelidate/validators'
    import useVuelidate from '@vuelidate/core'
    import api from '../../../api/AxiosInterceptors.js';


    const router = useRouter();
    const route = useRoute();
    const toast = useToast();
    const store = useStore();

    const role = computed(() => {
        return store.getters["auth/getRole"];
    });

    const backLabel = 'Cancelar';
    const loanList = () => {
        router.push({
            name: "loanRequestList"
        });
    };


    const approveLabel = 'Aprobar';
    const rejectLabel = 'Rechazar';
    const reviewerlabel = 'Requiere Revisión';
    const reviewerApproveResponselabel = 'Aprobar Revisión';
    const reviewerRejectResponselabel = 'Rechazar Revisión';

    const loanRequestId = ref(route.params.id);




    const loanData = ref({
        Name: null,
        NumberId: null,
        LoansTypeId: null,
        LoanTypeName: null,
        AmountRequested: null,
        RequestedDate: null,
        IsActive: null,
        Term: null,
        ApprovedDate: null,
        IsReviewRequired: null,
        ReviewRequiredDate: null,
        IsReviewApproved: null,
    })

    const dateFormat = {
        day: "numeric",
        month: "numeric",
        year: "numeric"
    };

    const loanState = ref({
        IsApproved: null,
    })

    const sendReviewResponse = async (event) => {
        try {
            let isReviewApproved = null;

            if (event.target.innerText === reviewerApproveResponselabel) {
                isReviewApproved = true;
            } else if (event.target.innerText === reviewerRejectResponselabel) {
                isReviewApproved = false;
            }
            await api.patch(`loanrequest/respond-review/${loanRequestId.value}`, isReviewApproved, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            toast.add({
                severity: 'success',
                detail: "Se ha enviado el estado de revisión.",
                life: 2000
            });
            await new Promise((resolve) => setTimeout(resolve, 1000));
            loanList()
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: error,
                life: 2000
            });
        }
    };


    const sendReviewRequest = async () => {
        try {
            await api.patch(`loanrequest/request-review/${loanRequestId.value}`);
            toast.add({
                severity: 'success',
                detail: "Se ha enviado la solicitud de revisión.",
                life: 2000
            });
            await new Promise((resolve) => setTimeout(resolve, 1000));
            loanList()
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: error,
                life: 2000
            });
        }
    };

    const adminButtonsVisible = () => {
        if (role.value == 1 && loanData.value.IsApproved == 'Pendiente') {
            return true;
        }
        return false;
    }
    const reviewerButtonsVisible = () => {
        if ((role.value == 2 || role.value == 3) && loanData.value.ReviewRequiredDate == 'N/A') {
            return true;
        }
        return false;
    }

    const rules = {
        IsApproved: {
            required
        }
    };

    const storeLoan = async () => {
        await store.dispatch('loanRequests/updateSavings', {
            loanRequestId: loanRequestId.value,
            loanState: loanState.value
        })
    }

    const v$ = useVuelidate(rules, loanState);
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

    const fetchLoanData = async () => {
        await store.dispatch('loanRequests/getLoanById', {
            rowId: loanRequestId.value
        });
        const request = await store.getters["loanRequests/getLoans"];
        try {
            loanData.value.Name = request.Name,
                loanData.value.NumberId = request.NumberId,
                loanData.value.LoansTypeId = request.LoansTypeId,
                loanData.value.Term = request.Term,
                loanData.value.LoanTypeName = request.LoanTypeName,
                loanData.value.RequestedDate = new Date(request.RequestedDate),
                loanData.value.AmountRequested = request.AmountRequested,
                loanData.value.IsActive = request.IsActive ? 'Activo' : 'Inactivo',
                loanData.value.ApprovedDate = request.ApprovedDate ? new Date(request.ApprovedDate)
                .toLocaleString("es-ES", dateFormat) : "N/A",
                loanState.value.IsApproved = request.IsApproved !== null ? (request.IsApproved ? 'Aprobado' :
                    "Rechazado") : 'Pendiente',
                loanData.value.IsReviewRequired = request.IsReviewRequired !== null ? (request
                    .IsReviewRequired ? 'Sí' :
                    "No") : 'N/A',
                loanData.value.IsReviewApproved = request.IsReviewApproved !== null ? (request
                    .IsReviewApproved ? 'Aprobado' :
                    "Rechazado") : 'Pendiente',
                loanData.value.ReviewRequiredDate = request.ReviewRequiredDate ? new Date(request
                    .ReviewRequiredDate)
                .toLocaleString("es-ES", dateFormat) : "N/A"
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
                    loanState.value.IsApproved = 1;
                } else if (event.target.innerText === rejectLabel) {
                    loanState.value.IsApproved = 0;
                }
                await storeLoan();
                toast.add({
                    severity: 'success',
                    detail: "Sus cambios han sido guardados.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                loanList()
            } catch (error) {
                toast.add({
                    severity: 'error',
                    detail: error,
                    life: 2000
                });
            }
        }
    }


    onMounted(fetchLoanData);
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="form">
            <div>
                <strong><label>Código del préstamo:</label></strong>
                <label>&nbsp;{{ loanRequestId}}</label>
                <br>
                <br>
                <strong><label>Nombre completo:</label></strong>
                <label>&nbsp;{{loanData.Name }}</label>
                <br>
                <br>
                <strong><label>Tipo de ahorro:</label></strong>
                <label>&nbsp;{{ loanData.LoanTypeName}}</label>
                <br>
                <br>
                <strong><label>Fecha de solicitud:</label></strong>
                <label>&nbsp;{{ new Date(RequestedDate).toLocaleString("es-ES", dateFormat) }}</label>
                <br>
                <br>
                <strong><label>Monto solicitado:</label></strong>
                <label>&nbsp; ${{loanData.AmountRequested}}<span
                        v-if="(loanData.AmountRequested - Math.floor(loanData.AmountRequested)) === 0 || loanData.AmountRequested  === null">.00</span></label>
                <br>
                <br>
                <strong><label>Plazo:</label></strong>
                <label>&nbsp;{{ loanData.Term}} mes(es)</label>
                <br>
                <br>
                <strong><label>Estado del préstamo:</label></strong>
                <label>&nbsp;{{ loanData.IsActive}}</label>
                <br>
                <br>
                <strong><label>Requiere revisión del Presidente:</label></strong>
                <label>&nbsp;{{ loanData.IsReviewRequired}}</label>
                <br>
                <br>
                <strong><label>Fecha de revisión:</label></strong>
                <label>&nbsp;{{ new Date(loanData.ReviewRequiredDate).toLocaleString("es-ES", dateFormat) }}</label>
                <br>
                <br>
                <strong><label>Estado del revisión:</label></strong>
                <label>&nbsp;{{ loanData.IsReviewApproved}}</label>
                <br>
                <br>
                <div class="actions">
                    <base-button :label="backLabel" small @click="loanList" :type="'button'" />
                    <base-button :label="approveLabel" class="green" small @click="submitData" :type="'submit'"
                        v-if="loanState.IsApproved === 'Pendiente' && adminButtonsVisible() " />
                    <base-button :label="rejectLabel" class="red"
                        v-if="loanState.IsApproved === 'Pendiente' && adminButtonsVisible()" small @click="submitData"
                        :type="'submit'" />
                    <base-button :label="reviewerlabel" small @click="sendReviewRequest" :type="'submit'"
                        v-if="loanData.IsReviewRequired === null" />
                    <base-button :label="reviewerApproveResponselabel" class="green" small @click="sendReviewResponse"
                        :type="'submit'" v-if="loanData.IsReviewRequired === 'Sí' && reviewerButtonsVisible()" />
                    <base-button :label="reviewerRejectResponselabel" class="red" small @click="sendReviewResponse"
                        :type="'submit'" v-if="loanData.IsReviewRequired === 'Sí' && reviewerButtonsVisible()" />

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
        width: 45rem
    }

    .actions button {
        flex: 1;
        margin-right: 1rem;
        padding: .60rem;
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
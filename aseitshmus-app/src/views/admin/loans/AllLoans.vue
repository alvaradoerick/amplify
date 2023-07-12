<script setup>
    import { ref, onMounted, computed, watch } from 'vue';
    import { useStore } from 'vuex';
    import { useRouter } from 'vue-router';
    import {  useToast } from 'primevue/usetoast';

    const router = useRouter();
    const store = useStore()
    const toast = useToast();

    const backLabel = 'Principal';
    const deletionStatus = ref(false);

    const requestData = computed(() => {
        const loans = store.getters["loanRequests/getLoans"];
        if (!Array.isArray(loans)) {
    return [];
  }
        return loans.map(request => {
            return {
                ...request,
                IsActive: request.IsActive ? "Activo" : "Inactivo",
                IsApproved: request.IsApproved  ? "Activo"  : request.IsApproved === null  ? "Pendiente"  : "Rechazado",
                RequestedDate: new Date(request.RequestedDate).toLocaleString("es-ES", dateFormat),
                IsReviewRequired: request.IsReviewRequired ? "Sí" : request.IsReviewApproved === null & request.IsReviewRequired === true ? "Pendiente"  : "No",
                IsReviewApproved: request.IsReviewApproved  ? "Aprobado"  : request.IsReviewApproved === null  ? "Pendiente"  : "Rechazado",
 
            }
        });
    });

    const fetchRequestData = async () => {
        try {
            await store.dispatch('loanRequests/getAllLoans');
        } catch(error) {
            toast.add({
                severity: 'error',
                detail: error,
                life: 3000
            });
        }
    }
    
    const storeRequest = async (id) => {
        await store.dispatch('loanRequests/deleteLoan', {
            rowId: id
        })
    }

    const deleteResponse = computed(() => {
        return store.getters["loanRequests/getErrorResponse"];
    });

    const deleteRecord = async (rowData) => {
        try {
            await storeRequest(rowData.data.LoanRequestId);
            if (deleteResponse.value === null) {
                toast.add({
                    severity: 'warn',
                    detail: "Solicitud de préstamo ha sido eliminada.",
                    life: 2000
                });
                deletionStatus.value = true;
            } else {
                toast.add({
                    severity: 'error',
                    detail: deleteResponse.value,
                    life: 3000
                });
                store.commit('loanRequests/clearErrorResponse');
            }
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: error,
                life: 2000
            });
        }
    };
    const dateFormat = {
        day: "numeric",
        month: "numeric",
        year: "numeric"
    };
    watch(deletionStatus, (newStatus) => {
        if (newStatus) {
            fetchRequestData();
            deletionStatus.value = false;
        }
    });

    const cancel = () => {
        router.push({
            name: "dashboard"
        });
    }
    
    const updateRecord = (rowData) => {
        router.push({
            name: "updateLoanRequest",
            params: {
                id: rowData.data.LoanRequestId
            },
            props: true,
        });
    };

    onMounted(fetchRequestData);
</script>

<template>
    <toast-component />
    <div class="list">
        <data-table :value="requestData" paginator :rows="3" tableStyle="min-width: 80rem">
            <data-column field="Name" header="Nombre" sortable></data-column>
            <data-column field="LoanTypeName" header="Tipo de préstamo" sortable></data-column>
            <data-column field="RequestedDate" header="Fecha de solicitud" sortable style="width: 130px"></data-column>         
            <data-column field="IsApproved" header="Estado de aprobación" sortable style="width: 110px"></data-column>
            <data-column field="IsReviewRequired" header="Requiere revisión" sortable style="width: 110px"></data-column>
            <data-column field="IsReviewApproved" header="Estado de revisión" sortable style="width: 130px"></data-column>
            <data-column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Editar" @click="updateRecord(rowData)"
                        :type="'button'" />
                </template></data-column>
            <data-column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Eliminar" @click="deleteRecord(rowData)"
                        :type="'button'" />
                </template></data-column>
        </data-table>

        <div class="actions-container">
            <div class="actions">
                <base-button :label="backLabel" @click="cancel" :type="'button'" />
            </div>
        </div>
    </div>
</template>

<style scoped="scoped">
    .list {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
    }

    .action-buttons {
        display: flex;
        overflow: hidden;
        width: 105px;
        color: white;
        text-align: center;
        flex-direction: column;
        align-items: center;
    }

    .actions-container {
        position: static;
        bottom: 0;
        background-color: #fff;
        width: 100%;
        justify-content: center;
    }

    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        margin-top: 2rem;
        justify-content: space-between;
    }
</style>
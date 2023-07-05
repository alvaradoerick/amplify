<script setup>
    import DataTable from 'primevue/datatable';
    import Column from 'primevue/column';
    import {
        ref,
        onMounted,
        computed,
        watch
    } from 'vue';
    import {
        useStore
    } from 'vuex';
    import {
        useRouter
    } from 'vue-router';
    import {
        useToast
    } from 'primevue/usetoast';

    const router = useRouter();
    const store = useStore()
    const toast = useToast();

    const typeData = ref([]);
    const backLabel = 'Principal';
    const addLabel = 'Agregar';
    const deletionStatus = ref(false);

    const fetchTypeData = async () => {
        await store.dispatch('savingsTypes/getAllTypes');
        const types = store.getters['savingsTypes/getType'];
        typeData.value = types.map(type => {
            return {
                ...type,
                IsActive: type.IsActive ? "Activo" : "Inactivo",
                ApplicationDeadline: new Date(type.ApplicationDeadline).toLocaleString("es-ES", dateFormat),
                StartDate: new Date(type.StartDate).toLocaleString("es-ES", dateFormat),
            };
        });
    };

    const storeType = async (id) => {
        await store.dispatch('savingsTypes/deleteType', {
            rowId: id
        })
    }

    const deleteResponse = computed(() => {
        return store.getters["savingsTypes/getErrorResponse"];
    });

    const deleteRecord = async (rowData) => {
        try {
            await storeType(rowData.data.SavingsTypeId);
            if (deleteResponse.value === null) {
                toast.add({
                    severity: 'warn',
                    detail: "Tipo de ahorro ha sido eliminado.",
                    life: 2000
                });
                deletionStatus.value = true;
            } else {
                toast.add({
                    severity: 'error',
                    detail: deleteResponse.value,
                    life: 3000
                });
                store.commit('savingsTypes/clearErrorResponse');
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
            fetchTypeData();
            deletionStatus.value = false;
        }
    });

    const cancel = () => {
        router.push({
            name: "dashboard"
        });
    }

    const addRecord = () => {
        router.push({
            name: "createSavingsType"
        });
    }

    const updateRecord = (rowData) => {
        router.push({
            name: "updateSavingsType",
            params: {
                id: rowData.data.SavingsTypeId
            },
            props: true,
        });
    };
    onMounted(fetchTypeData);
</script>

<template>
    <toast-component />
    <div class="list">
        <DataTable :value="typeData" paginator :rows="3" tableStyle="min-width: 80rem">
            <Column field="Description" header="Tipo de ahorro" sortable></Column>
            <Column field="ApplicationDeadline" header="Último día de inscripción" sortable></Column>
            <Column field="StartDate" header="Fecha de inicio" sortable></Column>
            <Column field="IsActive" header="Estado" sortable style="width: 160px"></Column>
            <Column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Editar" @click="updateRecord(rowData)"
                        :type="'button'" />
                </template></Column>
            <Column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Eliminar" @click="deleteRecord(rowData)"
                        :type="'button'" />
                </template></Column>
        </DataTable>

        <div class="actions-container">
            <div class="actions">
                <base-button :label="backLabel" @click="cancel" :type="'button'" />
                <base-button :label="addLabel" @click="addRecord" :type="'button'" />
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
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
        await store.dispatch('loanTypes/getAllTypes');
        const types = store.getters['loanTypes/getType'];
        typeData.value = types.map(type => {
            return {
                ...type,
                IsActive: type.IsActive ? "Activo" : "Inactivo"
            };
        });
    };

    const storeType = async (id) => {
        await store.dispatch('loanTypes/deleteType', {
            rowId: id
        })
    }

    const deleteResponse = computed(() => {
        return store.getters["loanTypes/getErrorResponse"];
    });

    const deleteRecord = async (rowData) => {
        try {
            await storeType(rowData.data.LoansTypeId);
            if (deleteResponse.value === null) {
                toast.add({
                    severity: 'warn',
                    detail: "Tipo de préstamo ha sido eliminado.",
                    life: 2000
                });
                deletionStatus.value = true;
            } else {
                toast.add({
                    severity: 'error',
                    detail: deleteResponse.value,
                    life: 3000
                });
                store.commit('loanTypes/clearErrorResponse');
            }
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: error,
                life: 2000
            });
        }
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
            name: "createType"
        });
    }

    const updateRecord = (rowData) => {
        router.push({
            name: "updateType",
            params: {
                id: rowData.data.LoansTypeId
            },
            props: true,
        });
    };
    onMounted(fetchTypeData);
</script>

<template>
    <toast-component />
    <div class="category-list">
        <DataTable :value="typeData" paginator :rows="3" tableStyle="min-width: 80rem">
            <Column field="Description" header="Tipo de préstamo" sortable></Column>
            <Column field="Term" header="Plazo (meses)" sortable></Column>
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
    .category-list {
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
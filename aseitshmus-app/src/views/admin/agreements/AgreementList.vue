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

    const backLabel = 'Principal';
    const addLabel = 'Agregar';
    const agreementData = ref([]);
    const deletionStatus = ref(false);

 

    const fetchAgreementData = async () => {
        await store.dispatch('agreements/getAllAgreements');
        const agreements = store.getters['agreements/getAgreement'];
        agreementData.value = agreements.map(agreement => {
            return {
                ...agreement,
                IsActive: agreement.IsActive ? "Activo" : "Inactivo"
            };         
        });
    };

    const storeAgreement = async (id) => {
        await store.dispatch('agreements/deleteAgreement', {
            rowId: id
        })
    }

    const deleteResponse = computed(() => {
        return store.getters["agreements/getErrorResponse"];
    });

    const deleteRecord = async (rowData) => {
        console.log(rowData)
        try {
            await storeAgreement(rowData.data.AgreementId);
            if (deleteResponse.value === null) {
                toast.add({
                    severity: 'warn',
                    detail: "El convenio ha sido eliminado.",
                    life: 2000
                });
                deletionStatus.value = true;
            } else {
                toast.add({
                    severity: 'error',
                    detail: deleteResponse.value,
                    life: 3000
                });
                store.commit('agreements/clearErrorResponse');
            }
        } catch (error) {
            console.log(error)
            toast.add({
                severity: 'error',
                detail: `Un error ocurrió. ${error}`,
                life: 2000
            });
        }
    };

    watch(deletionStatus, (newStatus) => {
        if (newStatus) {
            fetchAgreementData();
            deletionStatus.value = false;
        }
    });


    const goBack = () => {
        router.push({
            name: "dashboard"
        });
    }

    const addAgreement = () => {
        router.push({
            name: "createAgreement"
        });
    }
    const updateCategory = (rowData) => {
        router.push({
            name: "updateAgreement",
            params: {
                id: rowData.data.AgreementId
            },
            props: true,
        });
    };


    onMounted(fetchAgreementData);
 
    
</script>

<template>
    <div>
        <toast-component />
        <DataTable :value="agreementData"  tableStyle="min-width: 80rem" paginator :rows="3">
            <Column field="Title" header="Convenio" sortable></Column>
            <Column field="CategoryName" header="Categoría" sortable style="width: 300px"></Column>
            <Column field="IsActive" header="Estado" sortable style="width: 100px"></Column>
            <Column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Editar" :type="'button'" @click="updateCategory(rowData)"/>
                </template></Column>
            <Column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Eliminar"  @click="deleteRecord(rowData)" :type="'button'" />
                </template></Column>
        </DataTable>
    </div>
    <div class="actions-container">
        <div class="actions">
            <base-button :label="backLabel" @click="goBack" :type="'button'" />
            <base-button :label="addLabel" @click="addAgreement" :type="'button'" />
        </div>
    </div>
</template>


<style scoped="scoped">
    .action-buttons {
        display: flex;
        background-color: #253e8b;
        border-color: #253e8b;
        overflow: hidden;
        width: 75px;
        color: white;
        text-align: center;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }

    .main {
        display: flex;
        flex-direction: column;
    }

    .header {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .form-column {
        display: flex;
        flex-direction: column;
        min-height: 10vh;
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

    .actions-container {
        position: static;
        bottom: 0;
        background-color: #fff;
        padding: 3rem;
    }

    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;

    }
</style>
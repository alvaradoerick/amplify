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

    const categoryData = ref([]);
    const backLabel = 'Principal';
    const addLabel = 'Agregar';
    const deletionStatus = ref(false);

    const fetchCategoryData = async () => {
        await store.dispatch('categories/getAllCategories');
        const categories = store.getters['categories/getCategory'];
        categoryData.value = categories.map(category => {
            return {
                ...category,
                IsActive: category.IsActive ? "Activo" : "Inactivo"
            };
        });
    };

    const storeUser = async (id) => {
        await store.dispatch('categories/deleteCategory', {
            rowId: id
        })
    }

    const deleteResponse = computed(() => {
        return store.getters["categories/getErrorResponse"];
    });

    const deleteRecord = async (rowData) => {
        try {
            await storeUser(rowData.data.CategoryAgreementId);
            if (deleteResponse.value === null) {
                toast.add({
                    severity: 'warn',
                    detail: "Categoría ha sido eliminada.",
                    life: 2000
                });
                deletionStatus.value = true;
            } else {
                toast.add({
                    severity: 'error',
                    detail: deleteResponse.value,
                    life: 3000
                });
                store.commit('categories/clearErrorResponse');
            }
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: 'Un error ocurrió.',
                life: 2000
            });
        }
    };

    watch(deletionStatus, (newStatus) => {
        if (newStatus) {
            fetchCategoryData();
            deletionStatus.value = false;
        }
    });

    const cancel = () => {
        router.push({
            name: "dashboard"
        });
    }

    const addCategory = () => {
        router.push({
            name: "createCategory"
        });
    }

    const updateCategory = (rowData) => {
        router.push({
            name: "updateCategory",
            params: {
                id: rowData.data.CategoryAgreementId
            },
            props: true,
        });
    };
    onMounted(fetchCategoryData);
</script>

<template>
    <div>
        <toast-component />
        <DataTable :value="categoryData" paginator :rows="3" tableStyle="min-width: 50rem">
            <Column field="Description" header="Nombre" sortable></Column>
            <Column field="IsActive" header="Estado" sortable style="width: 160px"></Column>
            <Column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Editar" @click="updateCategory(rowData)"
                        :type="'button'" />
                </template></Column>
            <Column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Eliminar" @click="deleteRecord(rowData)"
                        :type="'button'" />
                </template></Column>
        </DataTable>
    </div>
    <div class="actions-container">
        <div class="actions">
            <base-button :label="backLabel" @click="cancel" :type="'button'" />
            <base-button :label="addLabel" @click="addCategory" :type="'button'" />
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
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

    const storeCategory = async (id) => {
        await store.dispatch('categories/deleteCategory', {
            rowId: id
        })
    }

    const deleteResponse = computed(() => {
        return store.getters["categories/getErrorResponse"];
    });

    const deleteRecord = async (rowData) => {
        try {
            await storeCategory(rowData.data.CategoryAgreementId);
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
    <toast-component />
    <div class="category-list">
        <DataTable :value="categoryData" paginator :rows="3" tableStyle="min-width: 80rem">
            <Column field="Description" header="Categoría" sortable></Column>
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

        <div class="actions-container">
            <div class="actions">
                <base-button :label="backLabel" @click="cancel" :type="'button'" />
                <base-button :label="addLabel" @click="addCategory" :type="'button'" />
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
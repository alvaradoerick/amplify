<script setup>
    import DataTable from 'primevue/datatable';
    import Column from 'primevue/column';
    import {
        ref,
        onMounted,
        computed
    } from 'vue';
    import {
        useStore
    } from 'vuex';
    import {
        useRouter
    } from 'vue-router';

    const router = useRouter();
    const store = useStore()

    const usersData = ref([]);
    const backLabel = 'Principal';
    const dateFormat = {
        day: "numeric",
        month: "numeric",
        year: "numeric"
    };
    const fetchUsersData = async () => {
        await store.dispatch('users/getAll');
        const users = store.getters['users/getUsers'];
        usersData.value = users.map(users => {
            return {
                ...users,
                FullName: `${users.FirstName} ${users.LastName1} ${users.LastName2}`,
                WorkStartDate: new Date(users.WorkStartDate).toLocaleString("es-ES", dateFormat),
                IsActive: users.IsActive ? "Activo" : "Inactivo"
            };
        });
    };


    const sortedUsersData = computed(() => {
        return [...usersData.value].sort((a, b) => {
            if (a.IsActive === b.IsActive) {
                return new Date(a.WorkStartDate) - new Date(b.WorkStartDate);
            }
            return a.IsActive === 'Inactivo' ? -1 : 1;
        });
    });


    const cancel = () => {
        router.push({
            name: "dashboard"
        });
    }

    const updateUser = (rowData) => {
        router.push({
            name: "updateUser",
            params: {
                id: rowData.data.PersonId
            },
            props: true,
        });
    };
    onMounted(fetchUsersData);
</script>

<template>
    <div class="user-list">
        <DataTable :value="sortedUsersData" paginator :rows="3" tableStyle="min-width: 80rem">
            <Column field="FullName" header="Nombre" sortable></Column>
            <Column field="NumberId" header="Identificación" sortable style="width: 160px"></Column>
            <Column field="WorkStartDate" header="Fecha de ingreso" sortable style="width: 200px"></Column>
            <Column field="IsActive" header="Estado" sortable style="width: 160px"></Column>
            <Column header="" style="width: 100px"> <template #body="rowData">
                    <base-button class="action-buttons" label="Ver más" @click="updateUser(rowData)" :type="'button'" />
                </template></Column>
        </DataTable>
        <div class="actions-container">
            <div class="actions">
                <base-button :label="backLabel" @click="cancel" :type="'button'" />
            </div>
        </div>
    </div>
</template>

<style scoped="scoped">
    .user-list {
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
        justify-content: center;
    }

    .actions-container {
        position: static;
        bottom: 0;
        background-color: #fff;
        width: 100%;
    }
    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        margin-top: 2rem;
    }
</style>
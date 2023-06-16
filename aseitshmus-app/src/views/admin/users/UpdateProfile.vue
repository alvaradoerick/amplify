<script setup>
    import {
        ref,
        onMounted,
        computed
    } from 'vue';
    import {
        useStore
    } from 'vuex';
    import {
        useRouter,
        useRoute
    } from 'vue-router';
    import {
        roles
    } from "../../../constants/RolesConst.js";
    import {
        required
    } from '@vuelidate/validators'
    import useVuelidate from '@vuelidate/core'
    import {
        useToast
    } from 'primevue/usetoast';

    const toast = useToast();
    const route = useRoute();
    const store = useStore()
    const router = useRouter()


    const rules = {
        PersonId: {
            required
        },
        NumberId: {
            required
        },
        FirstName: {
            required
        },
        LastName1: {
            required
        },
        LastName2: {
            required
        },
        DateBirth: {
            required
        },
        RoleDescription: {
            required
        },
        EnrollmentDate: {
            required
        },
        WorkStartDate: {
            required
        },
    }


    const personId = ref(route.params.id);
    const roleSelected = ref();
    const statusDB = ref();


    const backLabel = 'Atrás';
    const sendLabel = 'Actualizar';
    const beneficiariesLabel = 'Beneficiarios';
    const activeLabel = 'Activar';
    const inactiveLabel = 'Inactivar';

    const userInfo = ref({
        PersonId: null,
        NumberId: null,
        FirstName: null,
        LastName1: null,
        LastName2: null,
        DateBirth: null,
        RoleId: roleSelected,
        EnrollmentDate: null,
        WorkStartDate: null
    });


    const roleList = ref([{
            name: 'Administrador',
            value: roles.ADMINISTRATOR
        },
        {
            name: 'Presidente',
            value: roles.PRESIDENT
        },
        {
            name: 'Vice-Presidente',
            value: roles.VICEPRESIDENT
        },
        {
            name: 'Asociado',
            value: roles.ASSOCIATE
        }
    ]);

    const status = ref([{
            name: 'Activo',
            value: 1
        },
        {
            name: 'Inactivo',
            value: 0
        }
    ]);

    const manageUserStatus = async () => {
        await store.dispatch('users/patchUserStatus', {
            personId: personId.value
        })
    }

    const userResponse = computed(() => {
        return store.getters["users/getErrorResponse"];
    });

    const manageUser = async () => {
        await manageUserStatus();
        await fetchUserData();
        if (userResponse.value !== null) {
            toast.add({
                severity: 'error',
                summary: 'Error',
                detail: userResponse.value,
                life: 2000
            });
            store.commit('users/clearErrorResponse');
        } else {       
            if (statusDB.value === 1) {
                toast.add({
                    severity: 'warn',
                    detail: "Usuario ha sido desactivado.",
                    life: 2000
                });              
            } else {
                toast.add({
                    severity: 'success',
                    detail: "Usuario ha sido activado.",
                    life: 2000
                });
            }
        }
    }

    const UserList = () => {
        router.push({
            name: "listUsers"
        });
    }

    const updateBeneficiaries = () => {
        router.push({
            name: "updateBeneficiary",
            params: {
                id: personId.value
            },
            props: true,
        });
    };

    const storeUser = async () => {
        await store.dispatch('users/patchUser', {
            personId: personId.value,
            userInfo: userInfo.value
        })
    }

    const v$ = useVuelidate(rules, userInfo);
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

    const fetchUserData = async () => {
        await store.dispatch('users/getUserById', {
            rowId: personId.value
        });
        const userData = store.getters["users/getUsers"];
        try {
            userInfo.value.PersonId = userData.PersonId;
            userInfo.value.NumberId = userData.NumberId;
            userInfo.value.FirstName = userData.FirstName;
            userInfo.value.LastName1 = userData.LastName1;
            userInfo.value.LastName2 = userData.LastName2;
            userInfo.value.RoleDescription = userData.RoleDescription;
            statusDB.value = userData.IsActive ? 1 : 0;
            userInfo.value.DateBirth = new Date(userData.DateBirth);
            userInfo.value.WorkStartDate = new Date(userData.WorkStartDate);
            roleSelected.value = userData.RoleId;
            if (userData.EnrollmentDate) {
                userInfo.value.EnrollmentDate = new Date(userData.EnrollmentDate);
            } else {
                userInfo.value.EnrollmentDate = null;
            }
        } catch (error) {
            console.error(error);
        }
    };

    const submitData = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            if (isValid) {
                try {
                    await storeUser();
                    toast.add({
                        severity: 'success',
                        detail: "Sus cambios han sido guardados.",
                        life: 2000
                    });
                } catch (error) {
                    toast.add({
                        severity: 'error',
                        detail: 'Un error ocurrió.',
                        life: 2000
                    });
                }
            }
        }
    }

    onMounted(fetchUserData);
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="header">
            <div class="form-row">
                <span class="p-float-label">
                    <input-text class="input-text form-margin-right" id="employee-code" type="text"
                        placeholder="Código de empleado" v-model="userInfo.PersonId"
                        :class="{'hasError': v$?.PersonId?.$error }" />
                    <label for="employee-code">Código de empleado</label>
                </span>
                <span class="p-float-label">
                    <input-text class="input-text" id="employee-code" type="text" placeholder="Identificación"
                        v-model="userInfo.NumberId" :class="{'hasError': v$?.BankAccount?.$error }" />
                    <label for="employee-code">Identificación</label>
                </span>
            </div>
            <div class="form-row">
                <span class="p-float-label">
                    <input-text placeholder="Nombre" class="input-text form-margin-right" id="employee-firstname"
                        type="text" v-model=" userInfo.FirstName" :class="{'hasError': v$?.FirstName?.$error }" />
                    <label for="employee-firstname">Nombre</label>
                </span>
                <span class="p-float-label">
                    <input-text placeholder="Apellido 1" class="dropdown" id="employee-lastname1" type="text"
                        v-model="userInfo.LastName1" :class="{'hasError': v$?.LastName1?.$error  }" />
                    <label for="employee-lastname1">Primer apellido</label>
                </span>

            </div>
            <div class="form-row">
                <span class="p-float-label">
                    <input-text placeholder="Apellido 2" class="input-text form-margin-right" id="employee-lastname2"
                        type="text" v-model="userInfo.LastName2" :class="{'hasError': v$?.LastName2?.$error }" />
                    <label for="employee-lastname2">Segundo apellido</label>
                </span>
                <drop-down v-model="statusDB" id="status-list" :options="status" optionLabel="name" optionValue="value"
                    placeholder="Estado" class="dropdown" disabled />
            </div>
            <div class="form-row">
                <span class="p-float-label">
                    <date-picker v-model="userInfo.DateBirth" placeholder="Nacimiento"
                        class="dropdown form-margin-right" dateFormat="dd-mm-yy" showIcon id="dob"
                        :class="{'hasError': v$?.DateBirth?.$error }" />
                    <label for="dob">Nacimiento</label>
                </span>
                <span class="p-float-label">
                    <date-picker v-model="userInfo.WorkStartDate" placeholder="Ingreso empresa" class="dropdown "
                        dateFormat="dd-mm-yy" showIcon :class="{'hasError': v$?.WorkStartDate?.$error}"
                        id="work-start-date" />
                    <label for="work-start-date">Ingreso empresa</label>
                </span>
            </div>
            <div class="form-row">
                <span class="p-float-label">
                    <date-picker v-model="userInfo.EnrollmentDate" placeholder="Ingreso asociación"
                        class="dropdown form-margin-right" dateFormat="dd-mm-yy" showIcon
                        :class="{'hasError': v$?.EnrollmentDate?.$error}" id="enrollment-date" />
                    <label for="enrollment-date">Ingreso asociación</label>
                </span>
                <span class="p-float-label">
                    <drop-down v-model="roleSelected" :options="roleList" optionLabel="name" optionValue="value"
                        id="role" placeholder="Rol" class="dropdown" :class="{'hasError': v$?.roleSelected?.$error}" />
                    <label for="role">Rol</label>
                </span>
            </div>

        </div>
        <div class="actions">
            <base-button class="action-buttons" :label="backLabel" @click="UserList" :type="'button'" />
            <base-button class="action-buttons" :label="beneficiariesLabel" @click="updateBeneficiaries" :type="'button'" />
            <base-button class="action-buttons green" v-if="statusDB === 0" @click="manageUser" :label="activeLabel"
                :type="'submit'" />
            <base-button class="action-buttons red" v-if="statusDB === 1" @click="manageUser" :label="inactiveLabel"
                :type="'submit'" />
            <base-button class="action-buttons" :label="sendLabel" @click="submitData" :type="'submit'" />
        </div>
    </div>
</template>
<style scoped="scoped">
    .main {
        display: flex;
        flex-direction: column;
    }

    .header {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .form-row {

        display: flex;
        justify-content: space-between;
        align-self: center;
        margin-bottom: 2.4rem;
        width: 75%;
    }

    .form-margin-right {
        margin-right: 6rem;
    }

    .form-margin-left {
        margin-left: 6rem;
    }

    .hasError {
        border-color: red;
    }

    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;
        margin-top: 1rem;
    }

    .action-buttons {
        display: flex;
        overflow: hidden;
        width: 125px;
        color: white;
        text-align: center;
        flex-direction: column;
        align-items: center;
        justify-content: center;
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
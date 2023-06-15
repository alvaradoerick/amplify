<script setup>
    import {
        ref,
        onMounted,
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


    // import {
    //     required
    // } from '@vuelidate/validators'
    //import useVuelidate from '@vuelidate/core'
    // import {
    //     useToast
    // } from 'primevue/usetoast';
    //const toast = useToast();
    const route = useRoute();

    // const rules = {
    //     PhoneNumber: {
    //         required
    //     },
    //     BankAccount: {
    //         required
    //     },
    //     Address1: {
    //         required
    //     },
    //     DistrictId: {
    //         required
    //     },
    //     PostalCode: {
    //         required
    //     }
    // }

    const router = useRouter();
    const userId = ref(route.params.id);

    
    const roleSelected = ref();
    const statusDB = ref();
    const selectedState = ref();

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

    const backLabel = 'Cancelar';
    const UserList = () => {
        router.push({
            name: "listUsers"
        });
    }
    const sendLabel = 'Actualizar';
    const beneficiariesLabel = 'Beneficiarios';
    const activeLabel = 'Activar';
    const inactiveLabel = 'Inactivar';

    const store = useStore()


    const userInfo = ref({
        PersonId: null,
        NumberId: null,
        FirstName: null,
        LastName1: null,
        LastName2: null,
        IsActive: selectedState,
        DateBirth: null,
        RoleDescription: roleSelected,
        EnrollmentDate: null,
        WorkStartDate: null
    });

    const storeUser = async () => {
        await store.dispatch('users/patchProfile', {
            userInfo: userInfo.value
        })
    }


    console.log(storeUser)

    // const v$ = useVuelidate(rules, userInfo);
    // const validateForm = async () => {
    //     const result = await v$.value.$validate();
    //     if (!result) {
    //         if (v$.value.$errors[0].$validator === 'required') {
    //             toast.add({
    //                 severity: 'error',
    //                 summary: 'Error',
    //                 detail: 'Por favor revisar los campos en rojo.',
    //                 life: 2000
    //             });

    //         }
    //         return false
    //     }
    //     return true;
    // }

    const formatDate = (dateString) => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, "0");
        const month = String(date.getMonth() + 1).padStart(2, "0");
        const year = date.getFullYear();

        return `${day}-${month}-${year}`;
    };

    const fetchUserData = async () => {
        await store.dispatch('users/getUserById', {
            rowId: userId.value
        });
        console.log()
        const userData = store.getters["users/getUsers"];
        console.log(userData)
        try {
            userInfo.value.PersonId = userData.PersonId;
            userInfo.value.NumberId = userData.NumberId;
            userInfo.value.FirstName = userData.FirstName;
            userInfo.value.LastName1 = userData.LastName1;
            userInfo.value.LastName2 = userData.LastName2;
            userInfo.value.RoleDescription = userData.RoleDescription;
            statusDB.value = userData.IsActive ? 1 : 0;
            selectedState.value = userData.IsActive ? 1 : 0;
            userInfo.value.DateBirth = formatDate(userData.DateBirth),
            userInfo.value.WorkStartDate = formatDate(userData.WorkStartDate),
            roleSelected.value = userData.RoleId;
            if (userData.EnrollmentDate) {
                userInfo.value.EnrollmentDate = formatDate(userData.EnrollmentDate);
            } else {
                userInfo.value.EnrollmentDate = '';
            }

        } catch (error) {
            console.error(error);
        }
    };


    // const submitData = async (event) => {
    //     event.preventDefault();
    //     const isValid = await validateForm();
    //     if (isValid) {
    //         if (isValid) {
    //             try {
    //                 await storeUser();
    //                 toast.add({
    //                     severity: 'success',
    //                     summary: 'Felicidades',
    //                     detail: "Sus cambios han sido guardados.",
    //                     life: 2000
    //                 });
    //                 await new Promise((resolve) => setTimeout(resolve, 1000));
    //                 router.push({
    //                     name: 'myDashboard'
    //                 });
    //             } catch (error) {
    //                 toast.add({
    //                     severity: 'error',
    //                     summary: 'Error',
    //                     detail: 'Un error ocurrió.',
    //                     life: 2000
    //                 });
    //             }
    //         }
    //     }
    // }

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
                        :class="{'hasError': (v$?.PersonId?.$error) }" />
                    <label for="employee-code">Código de empleado</label>
                </span>
                <span class="p-float-label">
                    <input-text class="input-text" id="employee-code" type="text" placeholder="Identificación"
                        v-model="userInfo.NumberId" :class="{'hasError': (v$?.BankAccount?.$error) }" />
                    <label for="employee-code">Identificación</label>
                </span>
            </div>
            <div class="form-row">
                <span class="p-float-label">
                    <input-text placeholder="Nombre" class="input-text form-margin-right" id="employee-firstname"
                        type="text" v-model=" userInfo.FirstName" :class="{'hasError': (v$?.FirstName?.$error) }" />
                    <label for="employee-firstname">Nombre</label>
                </span>
                <span class="p-float-label">
                    <input-text placeholder="Apellido 1" class="dropdown" id="employee-lastname1" type="text"
                        v-model="userInfo.LastName1" :class="{'hasError': (v$?.LastName1?.$error ) }" />
                    <label for="employee-lastname1">Primer apellido</label>
                </span>

            </div>
            <div class="form-row">
                <span class="p-float-label">
                    <input-text placeholder="Apellido 2" class="input-text form-margin-right" id="employee-lastname2"
                        type="text" v-model="userInfo.LastName2" :class="{'hasError': (v$?.LastName2?.$error) }" />
                    <label for="employee-lastname2">Segundo apellido</label>
                </span>
                <span class="p-float-label">
                    <drop-down v-model="selectedState" id="status-list" :options="status" optionLabel="name"
                        optionValue="value" placeholder="Estado" class="dropdown"
                        :class="{'hasError': v$?.selectedState?.$error}" />
                    <label for="status-list">Estado</label>
                </span>
            </div>
            <div class="form-row">
                <span class="p-float-label">
                    <date-picker v-model="userInfo.DateBirth" placeholder="Nacimiento"
                        class="dropdown form-margin-right" dateFormat="dd-mm-yy" showIcon id="dob" />
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
            <base-button class="action-buttons"  :label="beneficiariesLabel" :type="'submit'" />
            <base-button class="action-buttons green"  v-if="statusDB === 0"  :label="activeLabel" :type="'submit'" />
            <base-button class="action-buttons red"  v-if="statusDB === 1" :label="inactiveLabel" :type="'submit'" />
            <base-button class="action-buttons"  :label="sendLabel" :type="'submit'" />
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
        margin-bottom: 2rem;
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
        margin-top: 3rem;
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
    .green, .green:hover, .green:focus{
        background-color: rgb(6, 100, 6) !important;
        border-color: rgb(6, 100, 6) !important;
    }

    .red, .red:hover, .green:focus
    {
        background-color:  rgb(189, 90, 90) !important;
         border-color: rgb(189, 90, 90) !important;
        }

</style>
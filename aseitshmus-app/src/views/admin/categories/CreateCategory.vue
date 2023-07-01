<script setup>
    import useVuelidate from '@vuelidate/core'
    import {
        required
    } from '@vuelidate/validators'
    import {
        useStore
    } from 'vuex'
    import {
        useRouter
    } from 'vue-router';
    import {
        ref,
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';


    const store = useStore();
    const router = useRouter();
    const toast = useToast();
    const backLabel = 'Cancelar';
    const categoryList = () => {
        router.push({
            name: "categoryList"
        });
    }
    const sendLabel = 'Crear';
    const selectedState = ref(1);
    const status = ref([{
            name: 'Activo',
            value: 1
        },
        {
            name: 'Inactivo',
            value: 0
        }
    ]);

    const agreementCategory = ref({
        Description: null,
        IsActive: selectedState
    })

    const rules = {
        Description: {
            required
        },
        IsActive: {
            required
        }
    }

    const v$ = useVuelidate(rules, agreementCategory);

    const validateForm = async () => {
        const result = await v$.value.$validate();
        if (!result) {
            if (v$.value.$errors[0].$validator === 'required') {
                toast.add({
                    severity: 'error',
                    detail: 'Todos los campos son requeridos.',
                    life: 2000
                });
            }
            return false
        }
        return true;
    }

    const storeCategory = async () => {
        await store.dispatch('categories/addCategory', {
            agreementCategory: agreementCategory.value,
        })
    }
    const onSend = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
                await storeCategory();
                toast.add({
                    severity: 'success',
                    detail: "Su categoría ha sido agregada.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                router.push({
                    name: 'categoryList'
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
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="form">
            <div class="header">
                <div class="form-row">
                    <div class="p-float-label">
                        <input-text placeholder="Nombre" class=" input-text form-margin-right" id="categoryName"
                            type="text" v-model="agreementCategory.Description"
                            :class="{'hasError': v$?.Description?.$error}" />
                        <label for="categoryName">Nombre</label>
                    </div>
                    <div class="p-float-label">
                        <drop-down v-model="selectedState" :options="status" optionLabel="name" optionValue="value"
                            id="state" placeholder="Estado" class="dropdown"
                            :class="{'hasError': v$?.selectedState?.$error}" />
                        <label for="state">Estado</label>
                    </div>
                </div>
            </div>
            <div class="actions">
                <base-button :label="backLabel" :small="true" @click="categoryList" :type="'button'" />
                <base-button :label="sendLabel" :small="true" @click="onSend" :type="'submit'" />
            </div>
        </div>
    </div>
</template>

<style scoped>
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

    .hasError {
        border-color: red;
    }

    .form-row {
        margin-top: 2rem;
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
    }

    .actions button {
        flex: 1;
        margin-right: 1rem;
    }
</style>
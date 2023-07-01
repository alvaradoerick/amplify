<script setup>
    import {
        useStore
    } from 'vuex'
    import {
        useRouter,
        useRoute
    } from 'vue-router';
    import {
        ref,
        onMounted
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';
    import {
        required
    } from '@vuelidate/validators'
    import useVuelidate from '@vuelidate/core'

    const store = useStore();
    const router = useRouter();
    const route = useRoute();
    const toast = useToast();


    const backLabel = 'Cancelar';
    const categoryList = () => {
        router.push({
            name: "categoryList"
        });
    }
    const sendLabel = 'Actualizar';
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
        IsActive: 0
    })
    const categoryId = ref(route.params.id);
    const rules = {
        Description: {
            required
        },
        IsActive: {
            required
        }
    }
    const storeCategory = async () => {
        await store.dispatch('categories/updateCategory', {
            categoryId: categoryId.value,
            agreementCategory: agreementCategory.value
        })
    }

    const v$ = useVuelidate(rules, agreementCategory);
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


    const fetchCategoryData = async () => {
        await store.dispatch('categories/getCategoryById', {
            rowId: categoryId.value
        });

        const category = store.getters["categories/getCategory"];
        try {
            agreementCategory.value.Description = category.Description,
                agreementCategory.value.IsActive = category.IsActive ? 1 : 0;
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: 'Un error ocurrió.',
                life: 2000
            });
        }
    };

    const submitData = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            if (isValid) {
                try {
                    await storeCategory();
                    toast.add({
                        severity: 'success',
                        detail: "Sus cambios han sido guardados.",
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
    }


    onMounted(fetchCategoryData);
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="form">
            <div>
                <div class="form-row">
                    <div class="p-float-label">
                        <input-text placeholder="Nombre" class=" input-text form-margin-right" id="categoryName"
                            type="text" v-model="agreementCategory.Description"
                            :class="{'hasError': v$?.Description?.$error}" />
                        <label for="categoryName">Nombre</label>
                    </div>
                    <div class="p-float-label">
                        <drop-down v-model="agreementCategory.IsActive" :options="status" optionLabel="name"
                            optionValue="value" placeholder="Estado" class="dropdown" id="status"
                            :class="{'hasError': v$?.IsActive?.$error}" />
                        <label for="status" :class="{'float-label': agreementCategory.IsActive !== 0}">Estado</label>

                    </div>
                </div>
            </div>
            <div class="actions">
                <base-button :label="backLabel" :small="true" @click="categoryList" :type="'button'" />
                <base-button :label="sendLabel" :small="true" @click="submitData" :type="'submit'" />
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
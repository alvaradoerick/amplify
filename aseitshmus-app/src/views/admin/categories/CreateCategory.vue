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
        IsActive:
        {
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
                    summary: 'Error',
                    detail: 'Todos los campos son requeridos.',
                    life: 2000
                });              
            }
            return false
        }
        return true;
    }

    const storeCategory = async () => {
        await store.dispatch('agreements/addCategoryAgreement', {
            agreementCategory: agreementCategory.value,
        })
    }
    const onSend = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
              await  storeCategory();
                toast.add({
                    severity: 'success',
                    summary: 'Felicidades',
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
                    summary: 'Error',
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
        <p>La categoría creada deberá ser asignada al convenio.</p>
        <div class="header">
            <div class="form-row">
                <input-text placeholder="Nombre" class=" input-text form-margin-right" id="categoryName" type="text"
                    v-model="agreementCategory.Description"  :class="{'hasError': v$?.Description?.$error}"/>
                <drop-down v-model="selectedState" :options="status" optionLabel="name" optionValue="value"
                    placeholder="Estado" class="dropdown" :class="{'hasError': v$?.selectedState?.$error}"/>
            </div>
        </div>

    </div>
    <div class="actions">
        <base-button :label="backLabel" @click="categoryList" :type="'button'" />
        <base-button :label="sendLabel" @click="onSend" :type="'submit'" />
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

    .form-column {
        display: flex;
        flex-direction: column;
        min-height: 10vh;
    }
    .hasError  {
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

    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;
        margin-top: 14rem;
    }
</style>
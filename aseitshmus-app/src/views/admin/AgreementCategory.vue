<script setup>
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

    const router = useRouter();
    const store = useStore();
    const toast = useToast();
    const backLabel = 'Cancelar';
    const homePage = () => {
        router.push({
            name: "dashboard"
        });
    }
    const sendLabel = 'Crear';
    const selectedState = ref();
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

    const storeLogin = async () => {
        await store.dispatch('agreements/addCategoryAgreement', {
            agreementCategory: agreementCategory.value,
        })
    }
   const onSend = async (event) => {
    event.preventDefault();
    storeLogin();           
            toast.add({
            severity: 'success',
            summary: 'Felicidades',
            detail: 'Sus cambios han sido guardados.',
                life: 2000
            });
         setTimeout(() => {
            router.push({ name: 'dashboard' });
        }, 500);
   }
</script>

<template>

<div class="main">
        <toast-component />
        <p>La categoría creada deberá ser asignada al convenio.</p>
        <div class="header">     
            <div class="form-row">
                     <input-text placeholder="Nombre" class=" input-text form-margin-right" id="categoryName" type="text" v-model="agreementCategory.Description"/>
                <drop-down v-model="selectedState" :options="status" optionLabel="name" optionValue="value"
                    placeholder="Estado" class="dropdown" />
            </div>
        </div>
        
    </div>
    <div class="actions">
            <base-button :label="backLabel" @click="homePage" :type="'button'" />
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
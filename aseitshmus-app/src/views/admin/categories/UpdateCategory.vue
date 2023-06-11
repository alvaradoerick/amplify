<script setup>
    import {
        useStore
    } from 'vuex'
    import {
        useRouter
    } from 'vue-router';
    import {
        ref,onMounted
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';

    
    const store = useStore();
    const router = useRouter();

    
    const backLabel = 'Cancelar';
    const categoryList = () => {
        router.push({
            name: "categoryList"
        });
    }
    const sendLabel = 'Actualizar';
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

    const categoryData = ref(null);

    const fetchCategoryData = async () => {
        console.log("texto"+props.params.id)
        await store.dispatch('agreements/getCategoryById', {
       rowId: props.params.id
    });
      try {
        categoryData.value = response.data;
      } catch (error) {
        console.error(error);
      }
    };
    

    onMounted(fetchCategoryData);
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
        <base-button :label="sendLabel" :type="'submit'" />
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
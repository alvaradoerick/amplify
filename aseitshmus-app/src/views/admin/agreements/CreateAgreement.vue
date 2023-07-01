<script setup>
    import useVuelidate from '@vuelidate/core'
    import {
        required
    } from '@vuelidate/validators'
    import axios from "axios";
    import {
        useStore
    } from 'vuex'
    import {
        useRouter
    } from 'vue-router';
    import {
        ref, onMounted
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';


    import Textarea from 'primevue/textarea';
    
    const apiUrl = process.env["VUE_APP_BASED_URL"]

    const store = useStore();
    const router = useRouter();
    const toast = useToast();


    const backLabel = 'Cancelar';
    const toReturn = () => {
        router.push({
            name: "agrementList"
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
    const categories = ref([]);
    const selectedCategory = ref(null);

  const agreementData = ref({
        Title: null,
        Description: null,
        Image:  null,
        CategoryAgreementId: selectedCategory,
        IsActive: selectedState
    })

    const storeAgreement = async () => {
    const agreement = {
      ...agreementData.value,
    };

    await store.dispatch('agreements/addAgreement', {
      agreementData: agreement,
    });
  };

  
  const handleFileUpload = (event) => {
      const file = event.target.files[0];
      if (file) {
    const reader = new FileReader();
    reader.onload = (e) => {
      agreementData.value.Image = e.target.result; 
    };
    reader.readAsDataURL(file);
  }
    };

    const fetchActiveCategories = async () => {
        try {
            const response = await axios.get(`${apiUrl}/categoryagreement/active-categories`);
            categories.value = response.data;
        } catch (error) {
            console.error(error);
        }
    };

    const rules = {
        Title: {
            required
        },
        Description: {
            required
        },
        CategoryAgreementId: {
            required
        },
        IsActive: {
            required
        }
    }

    const v$ = useVuelidate(rules, agreementData);

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


    const onSend = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
                
                await storeAgreement();
                console.log(agreementData.value)
                toast.add({
                    severity: 'success',
                    detail: "Su convenio ha sido agregado.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                router.push({
                    name: 'agrementList'
                });
            } catch (error) {
                toast.add({              
                    severity: 'error',
                    detail: 'Un error ocurrió.',
                    life: 2000
                });
                console.log(error)
            }
        }
    }

    onMounted(fetchActiveCategories)
</script>

<template>
   <div class="main">
        <toast-component />
        <div class="form">
        <div class="header">
            <div class="form-row">
                <div class="p-float-label">
                <input-text placeholder="Convenio" class=" input-text form-margin-right" id="agreementName" type="text"
                    v-model="agreementData.Title" :class="{'p-invalid': v$?.Title?.$error}" />
                    <label for="agreementName">Convenio</label>
                </div>
                    <div class="p-float-label">
                <drop-down v-model="selectedState" :options="status" optionLabel="name" optionValue="value"
                    placeholder="Estado" class="dropdown" id="status" :class="{'p-invalid': v$?.selectedState?.$error}" />
                    <label or="status">Estado</label>
                </div>
                    <div class="p-float-label form-margin-left">
                <drop-down v-model="selectedCategory" :options="categories" optionLabel="Description"
                    optionValue="CategoryAgreementId" class="dropdownLarger " id="category"
                    :class="{'p-invalid': v$?.CategoryAgreementId?.$error}" />
                    <label for="category">Categoría</label>
                </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                <Textarea id="description" placeholder="Descripción" v-model="agreementData.Description" rows="5"
                    cols="45" class="form-margin-right" :class="{'p-invalid': v$?.Description?.$error}" ></Textarea>
                    <label for="description">Descripción</label>
                </div>
            </div>             
              <div class="form-row">     
                <input type="file" id="myfile" name="myfile" class="upload-button" @change="handleFileUpload" />
        </div>
    </div>
    <div class="actions">
        <base-button :label="backLabel" @click="toReturn" :small="true" :type="'button'" />
        <base-button :label="sendLabel" @click="onSend" :small="true" :type="'submit'" />
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
.form{
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
}
    .dropdownLarger {
        display: flex;
        width: 300px;
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

    .upload-button {
        display: flex;
        background-color: #253e8b;
        border-color: #253e8b;
        overflow: hidden;
        width: 300px;
        color: white;
        text-align: center;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }


    .upload-button:hover,
    .upload-button:focus {
        box-shadow: 0 0 0 2px white, 0 0 0 3px skyblue;
        color: white;
        background-color: #3f569b !important;
    }
</style>
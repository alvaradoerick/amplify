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
        ref, 
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';

    import Textarea from 'primevue/textarea';
    import FileUpload from 'primevue/fileupload';

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
  //const fileInput = ref(null);

  
  
  const agreementData = ref({
        Title: null,
        Description: null,
        Image:  null,
        CategoryAgreementId: selectedCategory,
        IsActive: selectedState
    })


  const storeAgreement = async () => {
    await store.dispatch('agreements/addAgreement', {
      agreementData: agreementData.value,
    });
  };


    const fetchData = async (url, target) => {
        try {
            const response = await axios.get(url);
            target.value = response.data;
        } catch (error) {
            console.error(error);
        }
    };
    
    fetchData(`${apiUrl}/categoryagreement/active-categories`, categories);
    
//     const customBase64Uploader = async (event) => {
//   const file = event.files[0];
//   console.log(file);
//   const reader = new FileReader();
//   let blob = await fetch(file.objectURL).then((r) => r.blob()); //blob:url

//   reader.onloadend = function () {
//     const base64data = reader.result;
//     const byteCharacters = atob(base64data.split(',')[1]); // Extract base64 data
//     const byteNumbers = new Array(byteCharacters.length);
//     for (let i = 0; i < byteCharacters.length; i++) {
//       byteNumbers[i] = byteCharacters.charCodeAt(i);
//     }
//     const byteArray = new Uint8Array(byteNumbers);
//     agreementData.value.Image = Array.from(byteArray);
    
//   };

//   reader.readAsDataURL(blob);
// };

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
                    summary: 'Error',
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
            }
        }
    }
</script>

<template>
    <div class="main">
        <toast-component />
        <div class="header">
            <div class="form-row">
                <input-text placeholder="Convenio" class=" input-text form-margin-right" id="agreementName" type="text"
                    v-model="agreementData.Title" :class="{'hasError': v$?.Title?.$error}" />
                <drop-down v-model="selectedState" :options="status" optionLabel="name" optionValue="value"
                    placeholder="Estado" class="dropdown" :class="{'hasError': v$?.selectedState?.$error}" />
                <drop-down v-model="selectedCategory" :options="categories" optionLabel="Description"
                    optionValue="CategoryAgreementId" placeholder="Categoría" class="dropdownLarger form-margin-left"
                    :class="{'hasError': v$?.CategoryAgreementId?.$error}" />
            </div>
            <div class="form-row">
                <Textarea id="description" placeholder="Descripción" v-model="agreementData.Description" rows="5"
                    cols="45" class="form-margin-right" :class="{'hasError': v$?.Description?.$error}"></Textarea>
            </div>
            <div class="form-row">
                <FileUpload  mode="basic" accept="image/*" :maxFileSize="1000000"  chooseLabel="Buscar" id="browse"  customUpload @uploader="customBase64Uploader"/>
            </div>
        </div>
    </div>
    <div class="actions">
        <base-button :label="backLabel" @click="toReturn" :type="'button'" />
        <base-button :label="sendLabel" @click="onSend" :type="'submit'" />
    </div>
</template>
<style scoped="scoped">
    .main {
        display: flex;
        flex-direction: column;
    }

    .dropdownLarger {
        display: flex;
        width: 600px;
    }

    .input-text,
    .dropdown {
        display: flex;
        width: 300px;
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

    .hasError {
        border-color: red;
    }

    .form-row {
        margin-top: 2rem;
        display: flex;
        justify-content: space-between;
        align-self: center;
        margin-bottom: 1rem;
        width: 80%;
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
        margin-top: 4rem;
    }
</style>
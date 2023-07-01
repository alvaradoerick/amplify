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
        onMounted,computed
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';
    import {
        required
    } from '@vuelidate/validators'
    import useVuelidate from '@vuelidate/core'
    import Textarea from 'primevue/textarea';

    const store = useStore();
    const router = useRouter();
    const route = useRoute();
    const toast = useToast();
    const {
        dispatch,
        getters
    } = useStore();

    const AgreementId = ref(route.params.id);
    const selectedState = ref(null);
    const selectedCategory = ref(null);
    const categoryName = computed(() => getters['categories/getCategory']);
    const backLabel = 'Cancelar';
    const agreementList = () => {
        router.push({
            name: "agrementList"
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

    const agreementData = ref({
        Title: null,
        Description: null,
        Image:  null,
        CategoryAgreementId: selectedCategory,
        IsActive: selectedState
    })


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
 

    const storeAgreement = async () => {
        await store.dispatch('agreements/updateAgreement', {
            AgreementId: AgreementId.value,
            agreementData: agreementData.value
        })
    }

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

    const v$ = useVuelidate(rules, agreementData);
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

    const fetchAgreementData = async () => {
        await store.dispatch('agreements/getAgreementById', {
            rowId: AgreementId.value 
         }        
         );

        const agreements = store.getters["agreements/getAgreement"];
        console.log(agreements)
        try {
            agreementData.value.Title = agreements.Title;
            agreementData.value.Description = agreements.Description;
            selectedCategory.value = agreements.CategoryAgreementId;
            categoryName.value = agreements.categoryName;
            selectedState.value = agreements.IsActive ? 1 : 0;

        } catch (error) {
            toast.add({
                severity: 'error',
                detail: `Un error ocurrió. ${error}`,
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
                    await storeAgreement();
                    toast.add({
                        severity: 'success',
                        detail: "Sus cambios han sido guardados.",
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
    }
    onMounted(async () => {
         fetchAgreementData(),
        await dispatch('categories/getActiveCategories');     
    })

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
                    <label for="status">Estado</label>
                </div>
                    <div class="p-float-label form-margin-left">
                <drop-down v-model="selectedCategory" :options="categoryName" optionLabel="Description"
                    optionValue="CategoryAgreementId" class="dropdownLarger" id="category"
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
        <base-button :label="backLabel" @click="agreementList" :type="'button'" />
        <base-button :label="sendLabel" @click="submitData" :type="'submit'" />
    </div> 
</div>
</div>
</template>

<style scoped="scoped">
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
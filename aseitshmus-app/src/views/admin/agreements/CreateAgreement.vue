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
        ref
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
    const file = ref(null);

    const handleFileUpload = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                const imageBytes = new Uint8Array(reader.result);
                file.value = imageBytes;
            };
            reader.readAsArrayBuffer(file);
        }
    }

    const agreementData = ref({
        Title: null,
        Description: null,
        Image: file.value,
        CategoryAgreementId: selectedCategory,
        IsActive: selectedState
    })


    const fetchData = async (url, target) => {
        try {
            const response = await axios.get(url);
            target.value = response.data;
        } catch (error) {
            console.error(error);
        }
    };




    fetchData(`${apiUrl}/categoryagreement/active-categories`, categories);

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

    const storeCategory = async () => {
        await store.dispatch('agreements/addAgreement', {
            agreementData: agreementData.value,
        })
    }


    const onSend = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        console.log(v$)

        if (isValid) {
            try {
                console.log(agreementData.value)
                await storeCategory();
                toast.add({
                    severity: 'success',
                    summary: 'Felicidades',
                    detail: "Su convenio ha sido agregado.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                router.push({
                    name: 'dashboard'
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
                <FileUpload v-model="file" mode="basic" accept="image/*" :maxFileSize="1000000" type="file"
                    @change="handleFileUpload" chooseLabel="Buscar" id="browse" />
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
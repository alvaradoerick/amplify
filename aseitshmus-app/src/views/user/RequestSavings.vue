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
    
    const apiUrl = process.env["VUE_APP_BASED_URL"]

    const store = useStore();
    const router = useRouter();
    const toast = useToast();


    const backLabel = 'Cancelar';
    const toReturn = () => {
        router.push({
            name: "myDashboard"
        });
    }
    const sendLabel = 'Enviar';
    const savingsTypeList = ref([]);
    const selectedSavingsType = ref(2);

  const savingsData = ref({
    SavingsTypeId: selectedSavingsType,
    Amount: null
    })

    const storeSavings = async () => {
    await store.dispatch('savingsRequests/addSavingsRequest', {
        savingsData: savingsData.value,
    });
  };

    const fetchActiveSavings = async () => {
        try {
            const response = await axios.get(`${apiUrl}/SavingsType/active-savings`);
            savingsTypeList.value = response.data;
            if (response.data.length > 0) {
      selectedSavingsType.value = response.data[0].SavingsTypeId;
    }
        } catch (error) {
            console.error(error);
        }
    };

    const rules = {
        Amount: {
            required
        }
    }

    const v$ = useVuelidate(rules, savingsData);

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
                await storeSavings();
                toast.add({
                    severity: 'success',
                    detail: "Su solicitud de ahorro ha sido enviada.",
                    life: 2000
                });
                await new Promise((resolve) => setTimeout(resolve, 1000));
                toReturn();
            } catch (error) {
                toast.add({              
                    severity: 'error',
                    detail: error,
                    life: 2000
                });
                console.log(error)
            }
        }
    }

    onMounted(fetchActiveSavings)
</script>

<template>
   <div class="main">
        <toast-component />
        <div class="form">
        <div class="header">
            <div class="form-row">
              
                    <div class="p-float-label">
                <drop-down v-model="selectedSavingsType" :options="savingsTypeList" optionLabel="Description" optionValue="SavingsTypeId"
                    placeholder="Ahorro" class="dropdown form-margin-right" id="status" :class="{'p-invalid': v$?.selectedState?.$error}" />
                    <label for="savings-type">Ahorro</label>
                </div>
                <div class="p-float-label">
                <input-number placeholder="Monto quincenal" class=" input-text " id="amount" mode="currency" currency="USD"  locale="en-US"
                    v-model="savingsData.Amount" :class="{'p-invalid': v$?.Amount?.$error}" />
                    <label for="amount">Monto quincenal</label>
                </div>       
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
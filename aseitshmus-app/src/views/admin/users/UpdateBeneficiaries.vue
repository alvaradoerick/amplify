<script setup>
    import {
        ref,
        onMounted,
        computed
    } from 'vue';
    import useVuelidate from '@vuelidate/core'
    import {
        required
    } from '@vuelidate/validators'
    import {
        useStore
    } from 'vuex';
    import {
        useRouter,
        useRoute
    } from 'vue-router';
    import {
        useToast
    } from 'primevue/usetoast';

    const store = useStore()
    const route = useRoute();
    const toast = useToast();
    const router = useRouter()

    const PersonId = ref(route.params.id);
    const backLabel = 'Atrás';
    const sendLabel = 'Actualizar';
    const beneficiaryInfo = ref([]);

    const rules = {
        BeneficiaryName: {
            required
        },
        BeneficiaryNumberId: {
            required
        },
        BeneficiaryRelation: {
      required
        },
        BeneficiaryPercentage: {
            required
        }
    }

    const addRow = () => {
        beneficiaryInfo.value.push({
            BeneficiaryName: null,
            BeneficiaryNumberId: null,
            BeneficiaryRelation: null,
            BeneficiaryPercentage: null
        });
    };

    const v$ = useVuelidate(rules, beneficiaryInfo);

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


    const removeRow = (index) => {
        beneficiaryInfo.value.splice(index, 1);
    };


    const showRemoveButton = computed(() => {
        return beneficiaryInfo.value.length > 1;
    });

    const fetchBeneficiaryData = async () => {
        await store.dispatch('beneficiaries/getBeneficiaries', {
            PersonId: PersonId.value
        });
        const beneficiariesData = store.getters["beneficiaries/getBeneficiaries"];
        try {
            beneficiaryInfo.value = beneficiariesData.map((beneficiary) => ({
                BeneficiaryName: beneficiary.BeneficiaryName,
                BeneficiaryNumberId: beneficiary.BeneficiaryNumberId,
                BeneficiaryRelation: beneficiary.BeneficiaryRelation,
                BeneficiaryPercentage: beneficiary.BeneficiaryPercentage
            }));
        } catch (error) {
            toast.add({
                severity: 'error',
                detail: 'Un error ocurrió.',
                life: 2000
            });
        }
    };


    const userInfo = () => {
        router.push({
            name: "updateUser",
            params: {
                id: PersonId.value
            },
            props: true,
        });
    }

    const storeBeneficiary = async () => {
        await store.dispatch('beneficiaries/deleteAndInsertBeneficiaries', {
            PersonId: PersonId.value,
            beneficiaryInfo: beneficiaryInfo.value
        })
    }
    const updateBeneficiaries = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
            await storeBeneficiary();
            toast.add({
                severity: 'success',
                detail: "Los cambios han sido guardados.",
                life: 2000
            });
            await new Promise((resolve) => setTimeout(resolve, 1000));
            router.push({
                name: "updateUser",
                params: {
                    id: PersonId.value
                },
                props: true,
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
    onMounted(fetchBeneficiaryData);
</script>
<template>
    
    <div class="main">
        <toast-component />      
        <div class="header">
            <base-button :label="'+'" style="width:3rem" class="buttons" @click="addRow" :type="'button'" />
        </div>
        <div class="body">
        <div class="form"  >  
            <div v-for="(beneficiary, index) in beneficiaryInfo" :key="index"   class="form-row">
                <div class="p-float-label">
                    <input-text placeholder="Nombre completo" class="input-text form-margin-right" :id="'beneficiary-name-' + index"
                        type="text" v-model="beneficiary.BeneficiaryName"  :class="{'hasError': v$?.BeneficiaryName?.$error}" />
                    <label :for="'beneficiary-name-' + index">Nombre completo</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text form-margin-right" id="beneficiary-id" placeholder="Identificación"
                        type="text" v-model="beneficiary.BeneficiaryNumberId" :class="{'hasError': v$?.BeneficiaryNumberId?.$error}" />

                    <label for="beneficiary-id">Identificación</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text form-margin-right" placeholder="Parentesco" id="beneficiary-keen"
                        type="text" v-model="beneficiary.BeneficiaryRelation"  :class="{'hasError': v$?.BeneficiaryRelation?.$error}" />
                    <label for="beneficiary-keen">Parentesco</label>
                </div>
                <div class="p-float-label">
                    <input-text class="input-text form-margin-right" placeholder="Porcentaje" v-model="beneficiary.BeneficiaryPercentage" id="beneficiary-percentage" :class="{'hasError': v$?.BeneficiaryPercentage?.$error}" />
                    <label for="beneficiary-percentage">Porcentaje</label>
                </div>
                <base-button :label="'-'" style="width:3rem" class="buttons" v-if="showRemoveButton" @click="removeRow(index)"
                    :type="'button'"></base-button>
            </div>
        </div>
  
</div>
    <div class="actions">
        <base-button :label="backLabel"  small @click="userInfo" :type="'button'" />
        <base-button :label="sendLabel" small  @click="updateBeneficiaries" :type="'submit'" />
    </div>
</div>
</template>

<style scoped>
.main {
        display: flex;
        flex-direction: column;
        border: 1px solid #ccc;
        border-radius: 5px;
        padding: 2rem;
        width: 100%;
    }

    .header {
        display: flex;
        flex-direction: column;
        align-items: center;
        align-self: flex-end;
        margin-bottom: 4rem;
    }
 

    .body {
        overflow: scroll;
        min-height: 30vh;
        max-height: 30vh;
        margin-bottom: 3rem;
    }
    .form {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
       
    }
 
    .form-row {
        display: flex;
        justify-content: space-between;
        margin-bottom: 2rem;
        width: 100%;
      

    }
    .dropdown,
    .input-text {
        gap: 10px;
        
    }
    .form-margin-right {
        margin-right: 1rem;
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


    .hasError  {
    border-color: red;        
    }
</style>
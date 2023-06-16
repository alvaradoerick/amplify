<script setup>
    import {
        ref,
        onMounted,
        computed
    } from 'vue';
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


    const addRow = () => {
        beneficiaryInfo.value.push({
            BeneficiaryName: null,
            BeneficiaryNumberId: null,
            BeneficiaryRelation: null,
            BeneficiaryPercentage: null
        });
    };

    const removeRow = (index) => {
        beneficiaryInfo.value.splice(index, 1);
    };

    const showRemoveButton = computed(() => {
        return beneficiaryInfo.value.length > 1;
    });

    const fetchBeneficiaryData = async () => {
        await store.dispatch('users/getBeneficiaries', {
            PersonId: PersonId.value
        });
        const beneficiariesData = store.getters["users/getBeneficiaries"];
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
        await store.dispatch('users/deleteAndInsertBeneficiaries', {
            PersonId: PersonId.value,
            beneficiaryInfo: beneficiaryInfo.value
        })
    }
    const updateBeneficiaries = async (event) => {
        event.preventDefault();
        try {
            await storeBeneficiary();
            toast.add({
                severity: 'success',
                detail: "Los cambios han sido guardados.",
                life: 2000
            });
            console.log(PersonId.value)
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

    onMounted(fetchBeneficiaryData);
</script>




<template>
    <toast-component />
    <div class="container">
        <div class="header">
            <base-button :label="'+'" small class="buttons" @click="addRow" :type="'button'" />
        </div>
        <div class="body">
            <div v-for="(beneficiary, index) in beneficiaryInfo" :key="index" class="form-row">
                <input-text placeholder="Nombre completo" class="input-text form-margin-right" id="beneficiary-name"
                    type="text" v-model="beneficiary.BeneficiaryName" />
                <input-text class="input-text form-margin-right" id="beneficiary-id" placeholder="Identificación"
                    type="text" v-model="beneficiary.BeneficiaryNumberId" />
                <input-text class="input-text form-margin-right" placeholder="Parentesco" id="beneficiary-keen"
                    type="text" v-model="beneficiary.BeneficiaryRelation" />
                <input-text class="input-text form-margin-right" placeholder="Porcentaje" id="beneficiary-percentage"
                    type="number" v-model="beneficiary.BeneficiaryPercentage" />
                <base-button :label="'-'" small class="buttons" v-if="showRemoveButton" @click="removeRow(index)"
                    :type="'button'"></base-button>
            </div>
        </div>
    </div>
    <div class="actions">
        <base-button :label="backLabel" @click="userInfo" :type="'button'" />
        <base-button :label="sendLabel" @click="updateBeneficiaries" :type="'submit'" />
    </div>
</template>

<style scoped>
    .header {
        display: flex;
        width: 100%;
        justify-content: flex-end;
        margin-bottom: 3rem;
    }

    .body {
        overflow: scroll;
        min-height: 23vh;
        max-height: 23vh;
    }

    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        margin-bottom: 0.98em;
    }

    .form-row {
        display: flex;
        justify-content: space-between;
        margin-bottom: 2rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 1rem;
    }

    .dropdown,
    .input-text {
        width: 170px;
    }

    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;
        margin-top: 9rem;
    }
</style>
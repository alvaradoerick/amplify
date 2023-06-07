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
// import {
//     useToast
// } from 'primevue/usetoast';

const router = useRouter();
const store = useStore();
//const toast = useToast();
const backLabel = 'Cancelar';
const homePage = () => {
    router.push({
        //name: "myDashboard"
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
            code: 2
        }
    ]);

const agreementCategory = ref({
    Description: null,
    IsActive: null
})

const storeLogin = async () => {
    await store.dispatch('auth/addCategoryAgreement', {
        agreementCategory: agreementCategory.value,
    })

    
}
console.log(storeLogin)
</script>


<template>
   <div class="main">

            <toast-component />
       <div class="header">
            <div class="form-row">
                <input-text placeholder="Nombre" class=" input-text form-margin-right" id="categoryName"
                    type="text" />
                <drop-down v-model="selectedState" :options="status" optionLabel="name" optionValue="value"
                    placeholder="Estado" class="dropdown"/>
            </div>
           
        </div>
     <div class="actions">
                    <base-button :label="backLabel" @click="homePage" :type="'button'" />
                    <base-button :label="sendLabel"  :type="'submit'" />
                </div>

    </div>
</template>
<style scoped="scoped">
.container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        margin-bottom: 2.1rem;
    }
.header {
    display: flex;
    flex-direction: column;
    align-items: center;
}


.form-row {
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
}
</style>
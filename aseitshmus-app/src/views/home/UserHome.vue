
<script setup>
import {
    useStore
} from 'vuex'
import {
    computed
} from 'vue';

const store = useStore();


const storeLogin = async () => {
    await store.dispatch('users/getById')
}


const userData = computed(() => {
    return store.getters["users/getUser"];
});

const displayUser = async () => {
    
    await storeLogin();
    console.log(userData.value)
    
};


</script>


<template >
    <div class="card p-fluid">
       home page
       <button @click="displayUser">delete al boton</button>
    </div>
</template>



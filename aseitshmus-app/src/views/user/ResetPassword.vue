<script setup>
import {
        ref
    } from 'vue';
    import {
        useRouter
    } from 'vue-router';

    const router = useRouter();

    const Pw = ref(null);
    const reenterPassword = ref(null);
    const backLabel = 'Principal';
    const sendLabel = 'Cambiar';

    const homePage = () => {
        router.push({
            name: "myDashboard"
        });
    }
</script>

<template>
    <div class="center-container">
        <toast-component />
        <div class="container">             
                <div class="form-row">
                    <div class="p-float-label">
                    <input-text class="input-text" id="password" :class="{ 'hasError': (v$?.Pw?.$error || isValiData) }" type="password" v-model="Pw"
                        autocomplete="Pw" placeholder="Contraseña" />
                        <label for="password">Contraseña</label>
                </div>
                </div>
                <div class="form-row">
                    <div class="p-float-label">
                    <input-text class="input-text" id="reenter-password" :class="{ 'hasError': (v$?.Pw?.$error || isValiData) }" type="password" v-model="reenterPassword"
                        autocomplete="reenterPassword" placeholder="Reingrese contraseña" />
                        <label for="reenter-password">Reingrese contraseña</label>
                </div>
                </div>
        </div>
    </div>
    <div class="actions">
            <base-button :label="backLabel" @click="homePage" :type="'button'" />
            <base-button :label="sendLabel" @click="submitData" :type="'submit'" />
        </div>
</template>

<style scoped>
    .center-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 48vh;
    }
    .hasError  {
    border-color: red;        
    }

    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        margin: auto;
    }

    .form-row {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 2rem;
        width: 100%;
    }
    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;

    }
</style>
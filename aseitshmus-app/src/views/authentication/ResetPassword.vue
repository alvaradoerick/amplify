<script setup>
    import {
        ref,
        computed,
    } from 'vue';
    import {
        useStore
    } from 'vuex';
    import {
        useToast
    } from 'primevue/usetoast';
    import {
        useRouter
} from 'vue-router'
import PasswordTemplate from '../../assets/PasswordTemplate.vue';
    
    const store = useStore();
    const toast = useToast();
const router = useRouter();
    const passwordResponse = computed(() => {
        return store.getters["auth/getErrorResponse"];
    });
    const resetData = ref({
        EmailAddress: null,
    })

    const emailInformation = ref({
        to: "ear288@gmail.com",
        subject: "Prueba desde vue",
        //body: "<h1>Esto es una prueba</h1>"
        body: PasswordTemplate,
    })

const storeUser = async () => {
        await store.dispatch('auth/resetPasswordUnauthenticated', {
            resetData: resetData.value,
            emailInformation: emailInformation.value
        })
}
    //const newPassword = "new pw"
    const resetPassword = async (event) => {
        event.preventDefault();
        await storeUser();
        if (passwordResponse.value !== null) {
            toast.add({
                severity: 'error',
                summary: 'Error',
                detail: passwordResponse.value,
                life: 2000
            });
        } else {
            router.push({
                name: "login"
            });
        }
    }

const sendButton = ref('Enviar');
const cancelButton = ref('Cancelar');
const loginPage = () => {
        router.push({
            name: "login"
        });
    }

</script>

<template>
    <toast-component />
    <div style="margin-left: 40px;">
        <p>Por favor ingrese el correo eléctronico con el que está asociada su cuenta.</p>
        <p>Recibirá un correo con su nueva contraseña.</p>
    </div>
    <div class="center-container">
        <div class="container">
                <div class="form-row">
                    <input-text class="input-text " type="email" id="email-address" v-model="resetData.EmailAddress"
                        placeholder="Correo eléctronico" />
                </div>
        </div>
        
    </div>
    <div class="actions">
            
            <base-button :label="cancelButton" type="login" @click="loginPage" />
            <base-button :label="sendButton" type="submit" @click="resetPassword" />
        </div>
</template>

<style scoped>
    .center-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 40vh;
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

    .input-text {
        display: flex;
        width: 300px;
        align-items: center;
    }

    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;
    }
</style>
<script setup>
    import useVuelidate from '@vuelidate/core'
    import {
        email,
        required
    } from '@vuelidate/validators'
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

    const store = useStore();
    const toast = useToast();
    const router = useRouter();
    const passwordResponse = computed(() => {
        return store.getters["auth/getErrorResponse"];
    });
    const resetData = ref({
        EmailAddress: null,
    })

    const rules = {
        EmailAddress: {
            email,
            required
        }
    }

    const storeUser = async () => {
        await store.dispatch('auth/resetPasswordUnauthenticated', {
            resetData: resetData.value,
        })
    }
    const v$ = useVuelidate(rules, resetData);
    const validateForm = async () => {
        const result = await v$.value.$validate();
        if (!result) {
            if (v$.value.$errors[0].$validator === 'required') {
                toast.add({
                    severity: 'error',
                    detail: 'Correo es requerido.',
                    life: 2000
                });
                return false
            } else if (v$.value.$errors[0].$validator === 'email') {
                toast.add({
                    severity: 'error',
                    detail: 'El formato del correo es incorrecto.',
                    life: 2000
                });
            }
            return false
        }
        return true;
    }

    const isValiData = ref(false)
    const resetPassword = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
                await storeUser();
                if (passwordResponse.value !== null) {
                    isValiData.value = true
                    toast.add({
                        severity: 'error',
                        detail: passwordResponse.value,
                        life: 2000
                    });
                    store.commit('auth/clearErrorResponse');
                } else {
                    toast.add({
                        severity: 'success',
                        detail: "Su nueva contraseña ha sido enviada.",
                        life: 2000
                    });
                    await new Promise((resolve) => setTimeout(resolve, 2000));
                    router.push({
                        name: "login"
                    });
                }

            } catch (error) {
                toast.add({
                    severity: 'error',
                    detail: 'Un error ocurrió.',
                    life: 2000
                });
            }
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
                <div class="p-float-label">         
                    <input-text class="input-text " type="email" id="email-address" v-model="resetData.EmailAddress"
                    placeholder="Correo eléctronico" :class="{'hasError': v$?.EmailAddress?.$error || isValiData }" />
                        <label for="email-address">Correo eléctronico</label>
                </div> 
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

    .hasError {
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
        gap: 10%;
    }
</style>
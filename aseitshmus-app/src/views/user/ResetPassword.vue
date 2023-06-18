<script setup>
    import {
        ref,computed
    } from 'vue';
    import {
        useRouter
    } from 'vue-router';
    import {
        required,
        minLength,
        sameAs
    } from '@vuelidate/validators'
    import {
        useVuelidate
    } from '@vuelidate/core'
    import {
        useToast
    } from 'primevue/usetoast';
    import {
        useStore
    } from 'vuex';

    const toast = useToast();
    const router = useRouter();
    const store = useStore()


    const password = ref(null);
    const confirmPassword = ref(null);
    const backLabel = 'Principal';
    const sendLabel = 'Cambiar';
    const isInvalidData = ref(false)

    const homePage = () => {
        router.push({
            name: "myDashboard"
        });
    }

    const rules = {
        password: {
            required,
            minLength: minLength(8)
        },
        confirmPassword: {
            required,
            sameAs: sameAs(password)
        }
    }

    const v$ = useVuelidate(rules, {
        password,
        confirmPassword
    });

    const passwordResponse = computed(() => {
        return store.getters["users/getErrorResponse"];
    });

    const validateForm = async () => {
        const result = await v$.value.$validate();
        if (!result) {
            if (v$.value.$errors[0].$validator === 'required') {
                toast.add({
                    severity: 'error',
                    detail: 'Todos los campos son requeridos.',
                    life: 2000
                });
                return false
            } else if (v$?.value?.password?.$error) {
                toast.add({
                    severity: 'error',
                    detail: 'La contraseña es inválida.',
                    life: 2000
                });
            } else if (v$?.value?.confirmPassword?.$error) {
                toast.add({
                    severity: 'error',
                    detail: 'Las contraseñas no son las mismas.',
                    life: 2000
                });
            }
            return false
        }
        return true;
    }


    const storeUser = async () => {
  const resetData = {
    Password: password.value,
  };

  await store.dispatch('users/resetPasswordAuthenticated', {
    resetData: resetData,
  });
};

    const onSend = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try {
               await storeUser();
                if (passwordResponse.value !== null) {
                    isInvalidData.value = true
                    toast.add({
                        severity: 'error',
                        detail: passwordResponse.value,
                        life: 2000
                    });
                    store.commit('users/clearErrorResponse');
                } else {
                    toast.add({
                        severity: 'success',
                        detail: "Su contraseña ha sido cambiada.",
                        life: 2000
                    });
                    await new Promise((resolve) => setTimeout(resolve, 2000));
                    router.push({
                        name: "myDashboard"
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
    };
</script>

<template>
    <div class="center-container">
        <toast-component />
        <div class="container">
            <div class="form-row">
                <div class="p-float-label">
                    <input-text class="input-text" id="password"
                        :class="{ 'hasError': (v$?.password?.$error || isInvalidData) }" type="password"
                        v-model="password" autocomplete="password" placeholder="Contraseña" />
                    <label for="password">Contraseña</label>
                </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                    <input-text class="input-text" id="reenter-password"
                        :class="{ 'hasError': (v$?.confirmPassword?.$error || isInvalidData) }" type="password"
                        v-model="confirmPassword" autocomplete="confirmPassword" placeholder="Reingrese contraseña" />
                    <label for="reenter-password">Reingrese contraseña</label>
                </div>
            </div>
        </div>
    </div>
    <div class="actions">
        <base-button :label="backLabel" @click="homePage" :type="'button'" />
        <base-button :label="sendLabel" @click="onSend" :type="'submit'" />
    </div>
</template>

<style scoped>
    .center-container {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 48vh;
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

    .actions {
        display: flex;
        flex: 1;
        align-items: center;
        justify-content: space-between;

    }
</style>
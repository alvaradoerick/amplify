<script setup>
    import useVuelidate from '@vuelidate/core'
    import {
        email,
        minLength,
        required
    } from '@vuelidate/validators'
    import {
        useStore
    } from 'vuex'
    import {
        useRouter
    } from 'vue-router';
    import {
        ref,
        computed
    } from 'vue';
    import {
        useToast
    } from 'primevue/usetoast';

    import {roles} from "../../constants/RolesConst.js";
    const router = useRouter();
    const store = useStore();
    const toast = useToast();

    const formData = ref({
        EmailAddress: null,
        Pw: null
    })

    const labelButton = ref('Ingresar');

    const rules = {
        EmailAddress: {
            email,
            required
        },
        Pw: {
            minLength: minLength(8),
            required
        }
    }

    const storeLogin = async () => {
        await store.dispatch('auth/login', {
            formData: formData.value,
        })
    }
    const token = computed(() => {
        return store.getters["auth/getToken"];
    });

    const loginResponse = computed(() => {
        return store.getters["auth/getErrorResponse"];
    });


    const role = computed(() => {
        return store.getters["auth/getRole"];
    });

    const v$ = useVuelidate(rules, formData);


    const validateForm = async () => {
        const result = await v$.value.$validate();
       
        if (!result) {
            if (formData.value.email === null || formData.value.Pw === null) {
                toast.add({
                    severity: 'error',
                    detail: 'Correo y contraseña son requeridos.',
                    life: 2000
                });
                return false
            } else if (v$?.value?.EmailAddress?.$error) {
                toast.add({
                    severity: 'error',
                    detail: 'El formato del correo es incorrecto.',
                    life: 2000
                });
            } else if (v$?.value?.Pw?.$error) {
                toast.add({
                    severity: 'error',
                    detail: 'La contraseña es inválida.',
                    life: 2000
                });
            }
            return false
        }
        return true;
    }
    const isInvalidData =  ref(false)

    const onSend = async (event) => {
        event.preventDefault();
        const isValid = await validateForm();
        if (isValid) {
            try{
            await storeLogin();
            if (token.value) {
        if (role.value === roles.ADMINISTRATOR) {            
            router.push({ name: "dashboard" });
        }
         else {           
                router.push({ name: "myDashboard" });
        }

    } else {
        isInvalidData.value = true
        toast.add({
          severity: 'error',
          detail: loginResponse.value || 'Un error ocurrió.',
          life: 2000,         
        });
        store.commit('auth/clearErrorResponse');
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
   
    const forgotPassword = () => {
        router.push({
            name: "resetPassword"
        });
    }
</script>
<template>
    <div class="login-container">
        <toast-component />
        <div class="login-form">
            <h2 class="title">
                INICIO DE SESIÓN
            </h2>             
                <div class="form-row">   
                    <div class="p-float-label">       
                    <input-text class="input-text " type="email" id="email-address" v-model="formData.EmailAddress"
                        placeholder="Correo eléctronico" :class="{'hasError': (v$?.EmailAddress?.$error || isInvalidData) }" />
                        <label for="email-address">Correo eléctronico</label>
                </div>  
            </div>
                <div class="form-row">
                    <div class="p-float-label"> 
                    <input-text class="input-text" id="password" :class="{ 'hasError': (v$?.Pw?.$error || isInvalidData) }" type="password" v-model="formData.Pw"
                        autocomplete="formData.Pw" placeholder="Contraseña" />
                        <label for="password">Contraseña</label>
                </div>
                </div>
                <div class="form-row sign-in">
                    <base-button :label="labelButton" type="submit" @click="onSend" />
                </div>
                <div class="form-row">
                    <a class="links" @click="forgotPassword">
                        ¿Olvidó su contraseña?
                    </a>
                </div>
        </div>
    </div>
</template>

<style scoped>
.login-form {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
}
.login-container {
    display: flex;
    justify-content: center;
    align-items: center;
    border: 1px solid #ebebeb;
    border-radius: 5px;
    margin: 1rem;
    padding: 2rem;
}
.title{
    margin-bottom: 3rem;
}
    .hasError  {
    border-color: red;        
    }

    .form-row {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 2rem;
        width: 100%;
    }

    .sign-in {
        margin-top: 3rem;
    }
    
    .input-text {
        width: 100%;
    }
</style>

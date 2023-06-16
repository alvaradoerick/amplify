<script setup>
    import {
        useRouter
    } from 'vue-router';
    const router = useRouter();
    const ladingPage = () => {
        router.push({
            name: "login"
        });
    }
</script>
<template>
    <div>
        <h2>Solicitud enviada</h2>
        <p>Su solicitud ha sido enviada para revisión. </p>
        <p>Si tiene menos de 3 meses laborando en la empresa, su solicitud quedará pendiente hasta cumplir los <strong>3
                meses.</strong></p>
        <div>
            <p>
                Recibirá un correo de confirmación con su contraseña. Le recomendamos cambiarla en el momento que su
                cuenta quede activada.
            </p>
            <p>
                En el momento que el administrador apruebe su solicitud, podrá ingresar a su cuenta.
            </p>
        </div>
        <div class="form-row">
            <a class="links" @click="ladingPage">
                Página Principal
            </a>
        </div>
    </div>
</template>

<style scoped>
    h2 {
        color: #fab03f;
        text-align: center;
        margin-top: 25px;
        margin-bottom: 50px;
    }

    .form-row {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 2rem;
        width: 100%;
    }
</style>
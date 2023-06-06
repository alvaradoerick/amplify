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
            <p>Si tiene menos de 3 meses laborando en la empresa, su solicitud quedará pendiente hasta cumplir los <strong>3 meses.</strong></p>
    <div>
        <p>
En el momento de que el administrador apruebe su solicitud, se le enviará un correo con su contraseña. <strong>Una vez recibida</strong>, puede ingresar a su perfíl y modificarla.
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
         margin-top:25px;
        margin-bottom:50px;
    }
    .form-row {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 2rem;
        width: 100%;
    }

</style>
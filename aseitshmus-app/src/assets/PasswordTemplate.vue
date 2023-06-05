<template>
    <div>
        <h1>Restablecimiento de contraseña</h1>
        <p>Hola,</p>
        <p>Tu contraseña ha sido restablecida correctamente.</p>
        <p>A continuación, te proporcionamos la nueva contraseña:</p>
        <p><strong>{{ newPassword }}</strong></p>
        <p>Te recomendamos cambiar la contraseña después de iniciar sesión.</p>
        <p>Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en contactarnos.</p>
        <p>¡Gracias!</p>
    </div>
</template>

<script>
    export default {
        props: {
            newPassword: {
                type: String,
                //required: true
            }
        }
    };
</script>

<style scoped>
    h1 {
        font-size: 24px;
        color: #333;
        margin-bottom: 16px;
    }

    p {
        font-size: 16px;
        color: #666;
        margin-bottom: 12px;
    }

    strong {
        font-weight: bold;
    }
</style>
<script setup>
    import {
        ref
    } from 'vue';

    const resetData = ref({
        EmailAddress: null,
    })

    const labelButton = ref('Enviar');
</script>

<template>
    <div style="margin-left: 40px;">
        <p>Por favor ingrese el correo eléctronico con el que está asociada su cuenta.</p>
        <p>Recibirá un correo con su nueva contraseña.</p>
    </div>
    <div class="center-container">
        <div class="container">
            <form>
                <div class="form-row">
                    <input-text class="input-text " type="email" id="email-address" v-model="resetData.EmailAddress"
                        placeholder="Correo eléctronico" />
                </div>
                <div class="form-row" style="margin-top: 40px;">
                    <base-button :label="labelButton" type="submit" />
                </div>
            </form>
        </div>
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
</style>
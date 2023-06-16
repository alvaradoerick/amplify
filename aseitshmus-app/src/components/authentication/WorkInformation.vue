<script setup="setup">
    import {
        defineEmits,
        ref,
        watch
    } from 'vue'

    const workInfo = ref({
        WorkStartDate: null,
        PhoneNumber: null,
        EmailAddress: null,
        BankAccount: null
    });

    const emits = defineEmits(['work-info'])
    watch(workInfo.value, (newValue) => {
        emits('work-info', newValue)
    })
</script>

<template>
    <div class="container">
        <div style="margin-top: 2rem;">
            <div class="form-row">
                <date-picker v-model="workInfo.WorkStartDate" placeholder="Fecha de ingreso"
                    class="dropdown form-margin-right" showIcon/>
                    <input-mask class="input-text" id="employee-phone" type="tel" v-model="workInfo.PhoneNumber"
                    placeholder="Número telefónico" mask="9999-9999" />
            </div>
            <div class="form-row">
                <input-text class="input-text form-margin-right" id="employee-email" type="email"
                    v-model="workInfo.EmailAddress" placeholder="Correo electrónico" />
                <input-text class="input-text" id="employee-account" type="text" v-model="workInfo.BankAccount"
                    placeholder="Cuenta IBAN" />
            </div>
        </div>
    </div>
</template>

<style scoped="scoped">
    .container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        margin-bottom: 8.2rem;
    }

    .form-row {
        display: flex;
        justify-content: space-between;
        margin-bottom: 2rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 2rem;
    }
</style>
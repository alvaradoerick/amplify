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
                <div class="p-float-label">
                <date-picker v-model="workInfo.WorkStartDate" placeholder="Fecha de ingreso" id="start-work-date"
                    class="dropdown form-margin-right" showIcon/>
                    <label for="start-work-date">Fecha de ingreso</label>
            </div>
            <div class="p-float-label">
                    <input-mask class="input-text" id="employee-phone" type="tel" v-model="workInfo.PhoneNumber"
                    placeholder="Número telefónico" mask="9999-9999" />
                    <label for="employee-phone">Número telefónico</label>
            </div>
            </div>
            <div class="form-row">
                <div class="p-float-label">
                <input-text class="input-text form-margin-right" id="employee-email" type="email"
                    v-model="workInfo.EmailAddress" placeholder="Correo electrónico" />
                    <label for="employee-email">Correo electrónico</label>
            </div>
            <div class="p-float-label">
                <input-text class="input-text" id="employee-account" type="text" v-model="workInfo.BankAccount"
                    placeholder="Cuenta IBAN" />
                    <label for="employee-account">Cuenta IBAN</label>
            </div>
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
        margin-bottom: 3.5rem;
        width: 100%;
    }

    .form-margin-right {
        margin-right: 2rem;
    }
</style>
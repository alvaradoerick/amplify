<script setup>
import { defineProps } from 'vue'

defineProps({
    items: {
        type: Array,
        required: true
    },
    active: {
        type: Number,
        required: true
    }
})
</script>

<template>
    <div class="stepper">
        <div class="stepper-item-container"  v-for="item in items" :key="item.id">
            <div class="stepper-item">
                <div class="stepper-item__circle" :class="{'stepper-item__circle--active': active === item.id}">
                    {{ item.id }}
                </div>
                <div class="stepper-item__label">{{ item.label }}</div>
                
            </div>
        </div>
    </div>
</template>
<style scoped="scoped">
.stepper {
    display: flex;
    justify-content: space-between;
}

.stepper-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
}

.stepper-item-container {
    display: flex;
    align-items: center;
}



.stepper-item__circle {
    width: 30px;
    height: 30px;
    border-radius: 50%;
    background-color: #e0e0e0;
    display: flex;
    justify-content: center;
    align-items: center;
    color: #fff;
    font-weight: bold;
}

.stepper-item__circle--active {
    background-color: #3f51b5;
}

.stepper-item__label {
    font-size: 0.8rem;
    margin-top: 0.5rem;
}
</style>

<script setup>
    import Button from "primevue/button"
    import {
        defineProps,
        defineEmits
    } from 'vue'

    const emits = defineEmits(['send'])

    defineProps({
        label: {
            type: String,
            required: true,
        },
        type: {
            type: String,
            required: true,
        },
        small: {
            type: Boolean,
            default: false,
        },
    })

    const send = () => {
        emits('send')
    }
</script>

<template>
    <Button class="buttons" :style="small ? 'width:3rem' : ''" :label=label :type=type @click.prevent="send"></Button>
</template>

<style scoped>
    .buttons {
        display: flex;
        background-color: #253e8b;
        border-color: #253e8b;
        overflow: hidden;
        width: 300px;
        color: white;
        text-align: center;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }


    .buttons:hover,
    .buttons:focus,
    .buttonsCentered:hover,
    .buttonsCentered:focus {
        box-shadow: 0 0 0 2px white, 0 0 0 3px skyblue;
        color: white;
        background-color: #3f569b !important;
    }
</style>

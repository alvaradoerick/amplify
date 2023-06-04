<script setup>
    import Card from 'primevue/card';
</script>

<template>
    <div>
        <Card class="default card">
            <template #title>
                <div class="card-header">
                    <h2>{{ $route.meta.title || "AseIsthmus" }}</h2>
                </div>
            </template>
            <template #content>
                <slot></slot>
            </template>
            <template #footer>
                <slot name="footer"></slot>
            </template>
        </Card>
    </div>
</template>

<style scoped>
    .default {
        width: 950px;
        margin-top: 25px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        height: 37rem;
    }

    h2 {
        color: #fab03f;
        text-align: center
    }
</style>
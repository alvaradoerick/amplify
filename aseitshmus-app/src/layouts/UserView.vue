<script setup>
    import UserNav from '@/components/UI/UserNav.vue'
</script>
<template>
    <div >
        <user-nav class="menu"></user-nav>
        <base-card class="container">
            <router-view />
        </base-card>
    </div>
</template>
<style scoped>
    .menu {
        display: flex;
        flex-direction: column;
        justify-content: flex-end;
        align-items: center;
    }

    .container {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        min-height: 37vh;
        height: 39rem;
        margin-top: 50px;
    }

    h1,
    h2,
    h3 {
        color: #fab03f;
        flex: auto;
        align-items: center;
    }

    h2 {
        text-align: center;
    }
</style>
<script setup>
import AdminNav from '@/components/UI/AdminNav.vue'


</script>
<template>
    <div>
        <admin-nav class="menu"></admin-nav>
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
}

html {
    height: 100%;
    font-family: "Roboto", sans-serif;
    font-size: 16px;
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

.input-text {
    width: 300px;
    margin-bottom: 20px;
}

body {
    margin: 0;
    padding: 0;
    font-size: 0.9rem;
    font-weight: 400;
    line-height: 1.6;
    color: #212529;
    text-align: left;
    background-color: white;
}



.center_component {
    display: flex;
    justify-items: center;
    justify-content: center;
}
</style>

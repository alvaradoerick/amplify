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
    align-items: flex-end;
    width: 100%;
    margin-top: .5rem;
    margin-bottom: 4rem;
  }

  .container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
</style>
<script setup>
  import NavMenu from '@/components/UI/NavMenu.vue'
</script>
<template>
  <div>
    <nav-menu class="menu"></nav-menu>
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
    margin-bottom: 1rem;
  }

  .input-text {
    width: 300px;
    margin-bottom: 20px;
  }
</style>
<script setup>
    import UserNav from '@/components/UI/UserNav.vue'
</script>
<template>
    <div>
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

  .input-text {
    width: 300px;
    margin-bottom: 20px;
  }


</style>
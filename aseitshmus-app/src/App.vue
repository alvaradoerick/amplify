<template>
  <div>
    <router-view></router-view>
  </div>
</template>
<style>
  .dropdown,
  .input-text {
    width: 370px;
  }
    .input-text {
        display: flex;
        width: 300px;
        align-items: center;
    }

    .links {
        color: #253E8B;
        display: flex;
        overflow: hidden;
        width: 20rem;
        text-align: center;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }

    .links:hover {
        color: #fab03f;
    }
</style>
<template>
    <div class="card-container">
        <slot name="title">
            <h2 class="title">{{ $route.meta.title || "AseIsthmus" }}</h2>
        </slot>
        <div class="card-body">
            <slot name="default"></slot>
        </div>
        <footer class="footer">
            Hecho con ❤️ en Costa Rica
            <slot name="footer"></slot>
        </footer>
    </div>
</template>
<style scoped>
    .card-container {
        display: flex;
        min-height: 85vh;
        flex-direction: column;
    }

    .card-body {
        flex: 1;
    }

    .title {
        font-size: 1.5rem;
        font-weight: bold;
        margin-bottom: 3rem;
        text-transform: uppercase;
        font-family: nunito;
    }
</style>
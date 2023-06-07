<script setup>
    import {
        useStore
    } from 'vuex'
    import {
        ref
    } from 'vue';
    const store = useStore();

    const logout = async () => {
        store.commit("auth/clearToken");
    };

    const isSubMenu1Visible = ref(false);

    const showSubMenu1 = () => {
        isSubMenu1Visible.value = true;
    };

    const hideSubMenu1 = () => {
        isSubMenu1Visible.value = false;
    };
</script>
<template>
    <div class="menu">
        <nav class="navbar navbar-expand-lg navbar-light navbar-laravel">
            <div class="container">
                <router-link class="nav-link" to="/"><img src="../../assets/logo.png" alt="Logo" /></router-link>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <router-link class="nav-link" to="/">Ingresar</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" to="/register">Registro</router-link>
                        </li>
                        <li class="nav-item" @click="showSubMenu1" @mouseleave="hideSubMenu1">
                            <a class="nav-link">Convenios</a>
                            <ul v-show="isSubMenu1Visible" class="sub-menu">
                                <li>
                                    <router-link class="nav-link" :to="{name:'createCategory'}">Crear Categoria</router-link>
                                </li>
                                <li>
                                    <router-link class="nav-link" :to="{name:'createAgreement'}">Crear Convenio</router-link>
                                </li>
                            </ul>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" to="/" @click="logout">Salir</router-link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

    </div>
</template>

<style scoped>
li {
    list-style: none;
}


img {
    height: 30px;
    width: 40px;
    margin-left: 10px
}

a {
    color: #253E8B !important;
}

a:hover {
    color: #fab03f !important;
}
</style>
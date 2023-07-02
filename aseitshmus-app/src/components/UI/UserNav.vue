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
    const isSubMenu2Visible = ref(false);

    const showSubMenu1 = () => {
        isSubMenu1Visible.value = true;
    };

    const hideSubMenu1 = () => {
        isSubMenu1Visible.value = false;
    };

    const showSubMenu2 = () => {
        isSubMenu2Visible.value = true;
    };

    const hideSubMenu2 = () => {
        isSubMenu2Visible.value = false;
    };
</script>

<template>
    <div class="menu">
        <nav class="navbar navbar-expand-lg navbar-light">

            <div class="menu collapse navbar-collapse" id="navbarSupportedContent">

                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <router-link class="nav-link" to="/"><img src="../../assets/logo.png" alt="Logo" />
                        </router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/my-dashboard">Principal</router-link>
                    </li>
                    <li class="nav-item" @click="showSubMenu1" @mouseleave="hideSubMenu1">
                        <a class="nav-link">Mi Cuenta</a>
                        <ul v-show="isSubMenu1Visible" class="sub-menu">
                            <li>
                                <router-link class="nav-link" :to="{name:'myProfile'}">Mi Perfil</router-link>
                            </li>
                            <li>
                                <router-link class="nav-link" :to="{name:'changePassword'}">Cambiar Contraseña
                                </router-link>
                            </li>
                        </ul>
                    </li>
                    <li class="nav-item" @click="showSubMenu2" @mouseleave="hideSubMenu2">
                        <a class="nav-link">Solicitudes</a>
                        <ul v-show="isSubMenu2Visible" class="sub-menu">
                            <li>
                                <router-link class="nav-link" :to="{name:'requestLoan'}">Préstamos</router-link>
                            </li>
                            <li>
                                <router-link class="nav-link" :to="{name:'requestSavings'}">Ahorros</router-link>
                            </li>
                        </ul>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" to="/" @click="logout">Salir</router-link>
                    </li>
                </ul>
            </div>
        </nav>
    </div>
</template>



<style scoped>
    .menu {
        display: flex;
        justify-content: flex-end;
        align-items: center;
        width: 100%;
    }

    li {
        list-style: none;
    }

    img {
        height: 30px;
        width: 40px;
        margin-left: 10px;
    }

    a {
        color: #060123 !important;
    }

    a:hover {
        color: #fab03f !important;
    }

    .nav-link {
        text-transform: uppercase;
        font-family: nunito;
    }
</style>
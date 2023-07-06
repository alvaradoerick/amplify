<template>
    <nav class="navbar navbar-expand-lg navbar-light">
        <div class="menu collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <router-link class="nav-link" to="/"><img class="logo-image" src="../../assets/logo2.png" alt="Logo" /></router-link>
                </li>
                <li class="nav-item">
                    <router-link class="nav-link" :to="{name:'login'}">Iniciar Sesión</router-link>
                </li>
                <li class="nav-item">
                    <router-link class="nav-link" :to="{name:'register'}">Registro</router-link>
                </li>
                <li class="nav-item">
                    <router-link class="nav-link" :to="{name:'allAgreements'}">Convenios</router-link>
                </li>
            </ul>
        </div>
    </nav>
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
        height: 38px;
        width: 80px;
        margin-left: 10px
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
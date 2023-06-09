<template>
    <div class="menu">
        <nav class="navbar navbar-expand-lg navbar-light navbar-laravel">
            <div class="container">
                <router-link class="nav-link" to="/"><img src="../../assets/logo.png" alt="Logo" /></router-link>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <router-link class="nav-link" to="/">Iniciar Sesión</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" to="/register">Registro</router-link>
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
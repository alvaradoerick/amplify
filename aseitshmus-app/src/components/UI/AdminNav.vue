<script setup>
import { useStore } from 'vuex';
import { ref } from 'vue';

const store = useStore();

const logout = async () => {
  store.commit("auth/clearToken");
};

const subMenuStates = {
  subMenu1: ref(false),
  subMenu2: ref(false),
  subMenu3: ref(false),
  subMenu4: ref(false)
};

const showSubMenu = (subMenu) => {
  hideAllSubMenus();
  
  subMenuStates[subMenu].value = true;
};

const hideSubMenu = (subMenu) => {
  subMenuStates[subMenu].value = false;
};

const hideAllSubMenus = () => {
  for (const key in subMenuStates) {
    subMenuStates[key].value = false;
  }
};
</script>

<template>
  <div class="menu">
    <nav class="navbar navbar-expand-lg navbar-light">
     
      <div class="menu collapse navbar-collapse" id="navbarSupportedContent">
       
        <ul class="navbar-nav ml-auto">
            <li class="nav-item">
        <router-link class="nav-link" to="/"><img src="../../assets/logo.png" alt="Logo" /></router-link>
      </li>
          <li class="nav-item">
            <router-link class="nav-link" :to="{name:'dashboard'}">Principal</router-link>
          </li>
          <li class="nav-item" @mouseover="showSubMenu('subMenu1')" @mouseleave="hideSubMenu('subMenu1')">
            <a class="nav-link">Usuarios</a>
            <ul v-show="subMenuStates.subMenu1" class="sub-menu">
              <li>
                <router-link class="nav-link" :to="{name:'listUsers'}">Lista de usuarios</router-link>
              </li>
            </ul>
          </li>
          <li class="nav-item" @mouseover="showSubMenu('subMenu2')" @mouseleave="hideSubMenu('subMenu2')">
            <a class="nav-link">Convenios</a>
            <ul v-show="subMenuStates.subMenu2" class="sub-menu">
              <li>
                <router-link class="nav-link" :to="{name:'categoryList'}">Lista de categorias</router-link>
              </li>
              <li>
                <router-link class="nav-link" :to="{name:'agrementList'}">Lista de convenios</router-link>
              </li>
            </ul>
          </li>
          <li class="nav-item" @mouseover="showSubMenu('subMenu3')" @mouseleave="hideSubMenu('subMenu3')">
            <a class="nav-link">Préstamos</a>
            <ul v-show="subMenuStates.subMenu3" class="sub-menu">
              <li>
                <router-link class="nav-link" :to="{name:'typeList'}">Lista de tipos de préstamos</router-link>
              </li>
            </ul>
          </li>
          <li class="nav-item" @mouseover="showSubMenu('subMenu4')" @mouseleave="hideSubMenu('subMenu4')">
            <a class="nav-link">Ahorros</a>
            <ul v-show="subMenuStates.subMenu3" class="sub-menu">
              <li>
                <router-link class="nav-link" :to="{name:'savingsList'}">Lista de tipos de ahorros</router-link>
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

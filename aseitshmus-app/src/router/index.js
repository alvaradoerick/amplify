import { createRouter, createWebHashHistory } from 'vue-router'
import RegistrationWizard from '@/views/authentication/RegistrationWizard.vue'

const routes = [
  {
    path: '/',
    component: () => import('../layouts/Auth.vue' /* webpackChunkName: "auth" */),
    children: [
        {
            path: '',
            name: 'login',
            component: () => import('../views/authentication/LoginForm.vue' /* webpackChunkName: "LoginForm" */),
            meta: {
                title: 'Inicio de Sesión',
            }
        },
        {
            path: '/register',
            name: 'register',
            component: RegistrationWizard,
            meta: {
              title: 'Registro',
          },
      },
      {
        path: '/reset-password',
        name: 'resetPassword',
        component: () => import('../views/authentication/ResetPassword.vue' /* webpackChunkName: "ResetPassword" */),
        meta: {
          title: 'Restablecer Contraseña',
        }
      },
    ]
  },
  {
    path: '/my-dashboard',
    name: 'userDashboard',
    component: () => import('../layouts/UserView.vue' /* webpackChunkName: "UserView" */),
    children: [
        {
            path: '',
            name: 'myDashboard',
            component: () => import('../views/home/UserHome.vue' /* webpackChunkName: "UserHome" */),
            meta: {
              auth: true,
              title: 'Estado de Cuentas',

            }
      },
      
    ]
  },

  {
    path: '/dashboard',
    name: 'adminDashboard',
    component: () => import('../layouts/AdminView.vue' /* webpackChunkName: "AdminView" */),
    children: [
      {
        path: '',
        name: 'dashboard',
        component: () => import('../views/home/AdminHome.vue' /* webpackChunkName: "AdminHome" */),
        meta: {
          auth: true,
          title: 'Resumen de Información',

        }
      },

    ]
  },
  {
    path: '/profile',
    component: () => import('../layouts/UserView.vue' /* webpackChunkName: "UserView" */),
    children: [
      {
        path: '',
        name: 'myProfile',
        component: () => import('../views/user/MyProfile.vue' /* webpackChunkName: "MyProfile" */),
        meta: {
          title: 'Mi Perfíl',
        }
      },
      {
        path: '/password',
        name: 'changePassword',
        component: () => import('../views/user/ResetPassword.vue' /* webpackChunkName: "ResetPassword" */),
        meta: {
          title: 'Restablecer Contraseña',
        }
      },
    ]
  },
  {
    path: '/agreements',
    name: 'agreements',
    component: () => import('../layouts/AdminView.vue' /* webpackChunkName: "AdminView" */),
    children: [
      {
        path: '/all-categories',
        name: 'categoryList',
        component: () => import('../views/admin/AgreementCategory.vue' /* webpackChunkName: "AgreementCategory" */),
        meta: {
          title: 'Categorias',
        }
      },
      {
        path: '/',
        name: 'agrementList',
        component: () => import('../views/admin/AgreementList.vue' /* webpackChunkName: "AgreementList" */),
        meta: {
          title: 'Convenios',
        }
      },
      {
        path: '/create-category',
        name: 'createCategory',
        component: () => import('../views/admin/CreateCategory.vue' /* webpackChunkName: "CreateCategory" */),
        meta: {
          title: 'Crear Categoria',
        }
      },
      {
        path: '/update-category',
        name: 'updateCategory',
        component: () => import('../views/admin/CreateCategory.vue' /* webpackChunkName: "CreateCategory" */),
        meta: {
          title: 'Actualizar Categoria',
        }
      },
      {
        path: '/create-agreement',
        name: 'createAgreement',
        component: () => import('../views/admin/CreateAgreement.vue' /* webpackChunkName: "CreateAgreement" */),
        meta: {
          title: 'Crear Convenio',
        }
      },

    ]
  },
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router

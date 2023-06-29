import {
  createRouter,
  createWebHashHistory
} from 'vue-router'
import RegistrationWizard from '@/views/authentication/RegistrationWizard.vue'

const routes = [{
    path: '/',
    component: () => import('../layouts/Auth.vue' /* webpackChunkName: "auth" */ ),
    children: [{
        path: '',
        name: 'login',
        component: () => import('../views/authentication/LoginForm.vue' /* webpackChunkName: "LoginForm" */ ),
        meta: {}
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
        component: () => import('../views/authentication/ResetPassword.vue' /* webpackChunkName: "ResetPassword" */ ),
        meta: {
          title: 'Restablecer Contraseña',
        }
      },
    ]
  },

  {
    path: '/my-dashboard',
    name: 'myDashboard',
    component: () => import('../layouts/UserView.vue' /* webpackChunkName: "UserView" */ ),
    children: [{
        //dashboard
        path: '',
        name: 'myDashboard',
        component: () => import('../views/home/UserHome.vue' /* webpackChunkName: "UserHome" */ ),
        meta: {
          title: 'Resumen de Cuenta',
        },
      },
        {
          path: '',
          name: 'myProfile',
          component: () => import('../views/user/MyProfile.vue' /* webpackChunkName: "MyProfile" */ ),
          meta: {
            title: 'Mi Perfíl',
          }
      },
      //agreements
      {
        path: '/agreements',
        name: 'allAgreements',
        component: () => import('../views/agreements/AllAgreements.vue' /* webpackChunkName: "AllAgreements" */ ),
          meta: {
            title: 'Convenios Activos',
          }
      },

          {
            path: '/password',
            name: 'changePassword',
            component: () => import('../views/user/ResetPassword.vue' /* webpackChunkName: "ResetPassword" */ ),
            meta: {
              title: 'Cambiar Contraseña',
            }

      }
    ]
  },

  {
    path: '/dashboard',
    name: 'adminDashboard',
    component: () => import('../layouts/AdminView.vue' /* webpackChunkName: "AdminView" */ ),
    children: [
      //home page
      {
        path: '/',
        name: 'dashboard',
        component: () => import('../views/home/AdminHome.vue' /* webpackChunkName: "AdminHome" */ ),
        meta: {
          auth: true,
          title: 'Resumen de Información',
        }
      },
      //agreements
      {
        path: '/agreements',
        name: 'agreements',
        children: [{
            path: '/all-agreements',
            name: 'agrementList',
            component: () => import('../views/admin/agreements/AgreementList.vue' /* webpackChunkName: "AgreementList" */ ),
            meta: {
              title: 'Convenios',
            }
          },
          {
            path: '/create-agreement',
            name: 'createAgreement',
            component: () => import('../views/admin/agreements/CreateAgreement.vue' /* webpackChunkName: "CreateAgreement" */ ),
            meta: {
              title: 'Crear Convenio',
              authentication: true // buscar ocmo hacer push si no tiene token logueado before each
            }
          },
          {
            path: '/update-agreement/:id',
            name: 'updateAgreement',
            component: () => import('../views/admin/agreements/UpdateAgreement.vue' /* webpackChunkName: "UpdateAgreement" */ ),
            meta: {
              title: 'Editar Convenio',
              authentication: true 
            }
          },

        ]
      },
      //categories
      {
        path: '/categories',
        name: 'Categories',
        children: [{
            path: '/all-categories',
            name: 'categoryList',
            component: () => import('../views/admin/categories/AgreementCategory.vue' /* webpackChunkName: "AgreementCategory" */ ),
            meta: {
              title: 'Categorías',
            }
          },
          {
            path: '/create-category',
            name: 'createCategory',
            component: () => import('../views/admin/categories/CreateCategory.vue' /* webpackChunkName: "CreateCategory" */ ),
            props: true,
            meta: {
              title: 'Crear Categoría',
            }
          },
          {
            path: '/update-category/:id',
            name: 'updateCategory',
            component: () => import('../views/admin/categories/UpdateCategory.vue' /* webpackChunkName: "UpdateCategory" */ ),
            meta: {
              title: 'Actualizar Categoría',
            }
          },
        ]
      },
      //users
      {
        path: '/users',
        name: 'users',
        children: [{
            path: '/all-users',
            name: 'listUsers',
            component: () => import('../views/admin/users/UserList.vue' /* webpackChunkName: "UserList" */ ),
            meta: {
              title: 'Usuarios',
            }
          },
          {
            path: '/update-user/:id',
            name: 'updateUser',
            component: () => import('../views/admin/users/UpdateProfile.vue' /* webpackChunkName: "UpdateProfile" */ ),
            meta: {
              title: 'Actualizar Usuario',
            }
          },
          {
            path: '/update-beneficiaries/:id',
            name: 'updateBeneficiary',
            component: () => import('../views/admin/users/UpdateBeneficiaries.vue' /* webpackChunkName: "UpdateBeneficiaries" */ ),
            meta: {
              title: 'Actualizar Beneficiarios',
            }
          },
        ]
      },
    ]
  },

]


const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
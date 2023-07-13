import {
  createRouter,
  createWebHashHistory
} from 'vue-router'
import store from '@/store/'
import RegistrationWizard from '@/views/authentication/RegistrationWizard.vue'
import {roles} from "../constants/RolesConst.js";
//import TokenService from '../api/TokenService.js';


const routes = [{
    path: '/',
    component: () => import('../layouts/Auth.vue' /* webpackChunkName: "auth" */ ),
    children: [
      {
        path: '',
        name: 'login',
        component: () => import('../views/authentication/LoginForm.vue' /* webpackChunkName: "LoginForm" */ ),
        meta: {}
      },
      {
        path: '/agreements',
        name: 'allAgreements',
        component: () => import('../views/agreements/AllAgreements.vue' /* webpackChunkName: "AllAgreements" */ ),
        meta: {
          title: 'Convenios Activos',
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
        component: () => import('../views/authentication/ResetPassword.vue' /* webpackChunkName: "ResetPassword" */ ),
        meta: {
          title: 'Restablecer Contraseña',
        }
      },
    ]
  },

  {
    path: '/my-dashboard',
    name: 'my-dashboard',
    meta:{authentication: true},
    component: () => import('../layouts/UserView.vue' /* webpackChunkName: "UserView" */ ),
    children: [{
        //dashboard
        path: '',
        name: 'myDashboard',
        component: () => import('../views/home/UserHome.vue' /* webpackChunkName: "UserHome" */ ),
        meta: {
          title: 'Resumen de Cuenta',
          role: [roles.ASSOCIATE,roles.PRESIDENT,roles.VICEPRESIDENT]
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
      
      //change pw
      {
        path: '/password',
        name: 'changePassword',
        component: () => import('../views/user/ResetPassword.vue' /* webpackChunkName: "ResetPassword" */ ),
        meta: {
          title: 'Cambiar Contraseña',
        }

      },
      //request loan
      {
        path: '/loan',
        name: 'requestLoan',
        component: () => import('../views/user/RequestLoan.vue' /* webpackChunkName: "RequestLoan" */ ),
        meta: {
          title: 'Solicitar Préstamo',
          role: [roles.ASSOCIATE,roles.PRESIDENT,roles.VICEPRESIDENT]
        }

      },
      //request savings
      {
        path: '/savings',
        name: 'requestSavings',
        component: () => import('../views/user/RequestSavings.vue' /* webpackChunkName: "RequestSavings" */ ),
        meta: {
          title: 'Solicitar Ahorro',
          authentication: true,
          role: [roles.ASSOCIATE,roles.PRESIDENT,roles.VICEPRESIDENT]
        }

      }
    ]
  },

  {
    path: '/dashboard',
    name: 'adminDashboard',
    meta:{authentication: true},
    component: () => import('../layouts/AdminView.vue' /* webpackChunkName: "AdminView" */ ),
    children: [
      //home page
      {
        path: '/',
        name: 'dashboard',
        component: () => import('../views/home/AdminHome.vue' /* webpackChunkName: "AdminHome" */ ),
        meta: {
          title: 'Resumen de Información',
          authentication: true,
          role: [roles.ADMINISTRATOR]
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
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/create-agreement',
            name: 'createAgreement',
            component: () => import('../views/admin/agreements/CreateAgreement.vue' /* webpackChunkName: "CreateAgreement" */ ),
            meta: {
              title: 'Crear Convenio',
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/update-agreement/:id',
            name: 'updateAgreement',
            component: () => import('../views/admin/agreements/UpdateAgreement.vue' /* webpackChunkName: "UpdateAgreement" */ ),
            meta: {
              title: 'Editar Convenio',
              role: [roles.ADMINISTRATOR]
            }
          },

        ]
      },
      //categories
      {
        path: '/categories',
        name: 'Categories',
        meta:{authentication: true},
        children: [{
            path: '/all-categories',
            name: 'categoryList',
            component: () => import('../views/admin/categories/AgreementCategory.vue' /* webpackChunkName: "AgreementCategory" */ ),
            meta: {
              title: 'Categorías',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/create-category',
            name: 'createCategory',
            component: () => import('../views/admin/categories/CreateCategory.vue' /* webpackChunkName: "CreateCategory" */ ),
            props: true,
            meta: {
              title: 'Crear Categoría',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/update-category/:id',
            name: 'updateCategory',
            component: () => import('../views/admin/categories/UpdateCategory.vue' /* webpackChunkName: "UpdateCategory" */ ),
            meta: {
              title: 'Actualizar Categoría',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
        ]
      },
      //users
      {
        path: '/users',
        name: 'users',
        meta:{authentication: true},
        children: [{
            path: '/all-users',
            name: 'listUsers',
            component: () => import('../views/admin/users/UserList.vue' /* webpackChunkName: "UserList" */ ),
            meta: {
              title: 'Usuarios',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/update-user/:id',
            name: 'updateUser',
            component: () => import('../views/admin/users/UpdateProfile.vue' /* webpackChunkName: "UpdateProfile" */ ),
            meta: {
              title: 'Actualizar Usuario',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/update-beneficiaries/:id',
            name: 'updateBeneficiary',
            component: () => import('../views/admin/users/UpdateBeneficiaries.vue' /* webpackChunkName: "UpdateBeneficiaries" */ ),
            meta: {
              title: 'Actualizar Beneficiarios',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
        ]
      },
      //loan types
      {
        path: '/loan-types',
        name: 'loanTypes',
        meta:{authentication: true},
        children: [{
            path: '/all-loan-types',
            name: 'typeList',
            component: () => import('../views/admin/loan-types/TypeList.vue' /* webpackChunkName: "TypeList" */ ),
            meta: {
              title: ' Tipos de Préstamo',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/create-type',
            name: 'createType',
            component: () => import('../views/admin/loan-types/CreateType.vue' /* webpackChunkName: "CreateType" */ ),
            props: true,
            meta: {
              title: 'Crear Tipo de Préstamo',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/update-type/:id',
            name: 'updateType',
            component: () => import('../views/admin/loan-types/UpdateType.vue' /* webpackChunkName: "UpdateType" */ ),
            meta: {
              title: 'Actualizar Tipo de Préstamo',
              authentication: true,
              role: [roles.ADMINISTRATOR]
            }
          },
        ]
      },
      //loan Requests
      {
        path: '/loans-requests',
        name: 'loansRequests',
        meta:{authentication: true},
        children: [{
            path: '/all-loans-requests',
            name: 'loanRequestList',
            component: () => import('../views/admin/loans/AllLoans.vue' /* webpackChunkName: "AllLoans" */ ),
            meta: {
              title: ' Solicitudes de Préstamos',
              role: [roles.ADMINISTRATOR,roles.PRESIDENT,roles.VICEPRESIDENT]
            }
          },

          {
            path: '/update-loan-request/:id',
            name: 'updateLoanRequest',
            component: () => import('../views/admin/loans/UpdateLoan.vue' /* webpackChunkName: "UpdateLoan" */ ),
            meta: {
              authentication: true,
              title: 'Aprobación de Solicitud de Préstamo',
              role: [roles.ADMINISTRATOR,roles.PRESIDENT,roles.VICEPRESIDENT]
            }
          },
        ]
      },
      //savings types
      {
        path: '/savings-types',
        name: 'savingsTypes',
        meta:{authentication: true},
        children: [{
            path: '/all-savings-types',
            name: 'savingsList',
            component: () => import('../views/admin/savings-types/SavingsList.vue' /* webpackChunkName: "SavingsList" */ ),
            meta: {
              title: ' Tipos de Ahorro',
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/create-savings',
            name: 'createSavingsType',
            component: () => import('../views/admin/savings-types/CreateSavings.vue' /* webpackChunkName: "CreateSavings" */ ),
            props: true,
            meta: {
              title: 'Crear Tipo de Ahorro',
              role: [roles.ADMINISTRATOR]
            }
          },
          {
            path: '/update-savings/:id',
            name: 'updateSavingsType',
            component: () => import('../views/admin/savings-types/UpdateSavings.vue' /* webpackChunkName: "UpdateSavings" */ ),
            meta: {
              title: 'Actualizar Tipo de Ahorro',
              role: [roles.ADMINISTRATOR]
            }
          },
        ]
      },
      //savings Requests
      {
        path: '/savings-requests',
        name: 'savingsRequests',
        meta: {
          authentication: true,
        },
        children: [{
            path: '/all-savings-requests',
            name: 'savingsRequestList',
            component: () => import('../views/admin/savings/AllSavings.vue' /* webpackChunkName: "AllSavings" */ ),
            meta: {
              title: ' Solicitudes de Ahorro',
              role: [roles.ADMINISTRATOR]
            }
          },

          {
            path: '/update-saving-request/:id',
            name: 'updateSavingRequest',
            component: () => import('../views/admin/savings/UpdateSaving.vue' /* webpackChunkName: "UpdateSaving" */ ),
            meta: {
              title: 'Aprobación de Solicitud de Ahorro',
              role: [roles.ADMINISTRATOR]
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


router.beforeEach(async (to, from, next) => {
  const isLoggedIn = store.getters['auth/isAuthenticated']
  const role = store.getters['auth/getRole']

 if (to.meta.authentication && !isLoggedIn) {
    next({ name: 'login' })
  } else if (to.meta.role && !to.meta.role.some(metaRole => role.includes(metaRole))) {
      store.dispatch('auth/logout');
      next({ name: 'login' })
   } else {
      next();
    }
});

export default router


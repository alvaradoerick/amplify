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
    name: 'my-dashboard',
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
        }

      },
      //request savings
      {
        path: '/savings',
        name: 'requestSavings',
        component: () => import('../views/user/RequestSavings.vue' /* webpackChunkName: "RequestSavings" */ ),
        meta: {
          title: 'Solicitar Ahorro',
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
      //loan types
      {
        path: '/loan-types',
        name: 'loanTypes',
        children: [{
            path: '/all-loan-types',
            name: 'typeList',
            component: () => import('../views/admin/loan-types/TypeList.vue' /* webpackChunkName: "TypeList" */ ),
            meta: {
              title: ' Tipos de Préstamo',
            }
          },
          {
            path: '/create-type',
            name: 'createType',
            component: () => import('../views/admin/loan-types/CreateType.vue' /* webpackChunkName: "CreateType" */ ),
            props: true,
            meta: {
              title: 'Crear Tipo de Préstamo',
            }
          },
          {
            path: '/update-type/:id',
            name: 'updateType',
            component: () => import('../views/admin/loan-types/UpdateType.vue' /* webpackChunkName: "UpdateType" */ ),
            meta: {
              title: 'Actualizar Tipo de Préstamo',
            }
          },
        ]
      },
            //loan Requests
            {
              path: '/loans-requests',
              name: 'loansRequests',
              children: [{
                  path: '/all-loans-requests',
                  name: 'loanRequestList',
                  component: () => import('../views/admin/loans/AllLoans.vue' /* webpackChunkName: "AllLoans" */ ),
                  meta: {
                    title: ' Solicitudes de Préstamos',
                   
                  }
                },
      
                {
                  path: '/update-loan-request/:id',
                  name: 'updateLoanRequest',
                  component: () => import('../views/admin/loans/UpdateLoan.vue' /* webpackChunkName: "UpdateLoan" */ ),
                  meta: {
                    title: 'Aprobación de Solicitud de Préstamo',
                  }
                },
              ]
            },
      //savings types
      {
        path: '/savings-types',
        name: 'savingsTypes',
        children: [{
            path: '/all-savings-types',
            name: 'savingsList',
            component: () => import('../views/admin/savings-types/SavingsList.vue' /* webpackChunkName: "SavingsList" */ ),
            meta: {
              title: ' Tipos de Ahorro',
            }
          },
          {
            path: '/create-savings',
            name: 'createSavingsType',
            component: () => import('../views/admin/savings-types/CreateSavings.vue' /* webpackChunkName: "CreateSavings" */ ),
            props: true,
            meta: {
              title: 'Crear Tipo de Ahorro',
            }
          },
          {
            path: '/update-savings/:id',
            name: 'updateSavingsType',
            component: () => import('../views/admin/savings-types/UpdateSavings.vue' /* webpackChunkName: "UpdateSavings" */ ),
            meta: {
              title: 'Actualizar Tipo de Ahorro',
            }
          },
        ]
      },
      //savings Requests
      {
        path: '/savings-requests',
        name: 'savingsRequests',
        children: [{
            path: '/all-savings-requests',
            name: 'savingsRequestList',
            component: () => import('../views/admin/savings/AllSavings.vue' /* webpackChunkName: "AllSavings" */ ),
            meta: {
              title: ' Solicitudes de Ahorro',
            }
          },

          {
            path: '/update-saving-request/:id',
            name: 'updateSavingRequest',
            component: () => import('../views/admin/savings/UpdateSaving.vue' /* webpackChunkName: "UpdateSaving" */ ),
            meta: {
              title: 'Aprobación de Solicitud de Ahorro',
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
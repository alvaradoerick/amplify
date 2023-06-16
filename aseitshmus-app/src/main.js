import {
    createApp
} from 'vue'
import PrimeVue from 'primevue/config';


import App from './App.vue'
import router from './router'
import store from './store'

import BaseCard from "./components/UI/BaseCard.vue";
import BaseButton from "./components/UI/BaseButton.vue";
import InputText from 'primevue/inputtext';
import InlineMessage from 'primevue/inlinemessage';
import Dropdown from 'primevue/dropdown';
import Calendar from 'primevue/calendar';
import Tooltip from 'primevue/tooltip';
import ToastService from 'primevue/toastservice';
import Toast from 'primevue/toast';
import InputMask from 'primevue/inputmask';


//bootstrap
import 'bootstrap/dist/css/bootstrap.css'
import './assets/bootstrap.js'

// theme
import 'primevue/resources/themes/lara-light-blue/theme.css';
// core
import 'primevue/resources/primevue.min.css';
//icons
import 'primeicons/primeicons.css';


const app = createApp(App);


//custom components
app.component('base-card', BaseCard);
app.component('base-button', BaseButton);


//prime vue components
app.component('input-text', InputText);
app.component('inline-message', InlineMessage);
app.component('drop-down', Dropdown);
app.component('date-picker', Calendar);
app.component('toast-component', Toast);
app.directive('tooltip', Tooltip);
app.component('input-mask', InputMask);


app.use(store);
app.use(PrimeVue);
app.use(router);
app.use(ToastService);

app.mount('#app');
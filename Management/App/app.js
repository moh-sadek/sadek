import Vue from 'vue';
import VueI18n from 'vue-i18n'
import VueRouter from 'vue-router';
import ElementUI from 'element-ui';
import Vuetify from 'vuetify'
import locale from 'element-ui/lib/locale/lang/en'
import BlockUIService from './Shared/BlockUIService.js';
import Layout from './Components/Layout/Layout.vue';
import Home from './Components/Home/Home.vue';
import Users from './Components/Users/Users.vue';
import AddUsers from './Components/Users/AddUsers/AddUsers.vue';
import EditUsersProfile from './Components/Users/EditUsersProfile/EditUsersProfile.vue';
import ChangePassword from './Components/Users/ChangePassword/ChangePassword.vue';
import DataService from './Shared/DataService';
import messages from './i18n';



import Doctors from './Components/Doctors/Doctors.vue';
import AddDoctors from './Components/Doctors/AddDoctors/AddDoctors.vue';
import Request from './Components/Doctors/Request/Request.vue';
import Patients from './Components/Patients/Patients.vue';
import MyPatients from './Components/Patients/MyPatients/MyPatients.vue';
import ConactUs from './Components/ConactUs/ConactUs.vue';




import Orginizations from './Components/Orginizations/Orginizations.vue';
import Repusets from './Components/Orginizations/Repusets/Repusets.vue';








Vue.use(Vuetify)
Vue.use(VueI18n);
Vue.use(VueRouter);
Vue.use(ElementUI,{ locale });

Vue.config.productionTip = false;

Vue.prototype.$http = DataService;
Vue.prototype.$blockUI = BlockUIService;


export const eventBus = new Vue();

const i18n = new VueI18n({
    locale: 'ar', // set locale
    messages, // set locale messages
})

const router = new VueRouter({
    mode: 'history',
    base: __dirname,
    linkActiveClass: 'active',
    routes: [
        { path: '/', component: Home },
        { path: '/home', component: Home },
        { path: '/Users', component: Users },
        { path: '/AddUsers', component: AddUsers },
        { path: '/EditUsersProfile', component: EditUsersProfile },
        { path: '/ChangePassword', component: ChangePassword },  


        { path: '/Doctors', component: Doctors },  
        { path: '/AddDoctors', component: AddDoctors },  
        { path: '/Request', component: Request },  
        { path: '/Patients', component: Patients },  
        { path: '/MyPatients', component: MyPatients },  
        { path: '/ConactUs', component: ConactUs },  



        { path: '/Orginizations', component: Orginizations },  
        { path: '/Repusets', component: Repusets },  

        ]

});

Vue.filter('toUpperCase', function (value) {
    if (!value) return '';
    return value.toUpperCase();
});

new Vue({
    i18n,
    router,
    render: h => {
        return h(Layout);
    }    
}).$mount('#app');

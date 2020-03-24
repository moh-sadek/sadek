import Vue from 'vue';
import VueI18n from 'vue-i18n'
import VueRouter from 'vue-router';
import ElementUI from 'element-ui';
import Vuetify from 'vuetify'
import locale from 'element-ui/lib/locale/lang/en'
import BlockUIService from './Shared/BlockUIService.js';
import Layout from './Components/Layout/Layout.vue';
import Home from './Components/Home/Home.vue';
import Courses from './Components/Courses/Courses.vue';
//import Packages from './Components/Packages/Packages.vue';
import Customers from './Components/Customers/Customers.vue';
import Users from './Components/Users/Users.vue';
import AddUsers from './Components/Users/AddUsers/AddUsers.vue';
import EditUsersProfile from './Components/Users/EditUsersProfile/EditUsersProfile.vue';
import ChangePassword from './Components/Users/ChangePassword/ChangePassword.vue';
//import AddOldPackage from './Components/Packages/AddOldPackage/AddOldPackage.vue';
import AddOldCustomers from './Components/Customers/AddOldCustomers/AddOldCustomers.vue';
//import AddPackage from './Components/Packages/AddPackage/AddPackage.vue';
import AddCustomers from './Components/Customers/AddCustomers/AddCustomers.vue';
import Reports from './Components/Reports/Reports.vue';
import Seting from './Components/Seting/Seting.vue';
//import UnknownPackage from './Components/Packages/UnknownPackage/UnknownPackage.vue';
/*import Students from './Components/Students/Students.vue';
import Companies from './Components/Companies/Companies.vue';
import Packages from './Components/Packages/Packages.vue';
import SuperPackages from './Components/Packages/SuperPackages/SuperPackages.vue';
import SubPackages from './Components/Packages/SubPackages/SubPackages.vue';
import SubPackagesMain from './Components/SubPackages/SubPackages.vue';
import Courses from './Components/Packages/Courses/Courses.vue';
import CoursesMain from './Components/Courses/Courses.vue';
import SubPackageCourses from './Components/Packages/SubPackages/Courses/Courses.vue'
import CourseFiles from './Components/CourseFiles/CourseFiles.vue'; */

import DataService from './Shared/DataService';
import messages from './i18n';



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
        { path: '/Courses', component: Courses },
        { path: '/home', component: Home },
        //{ path: '/Packages', component: Packages },
        { path: '/Users', component: Users },
        { path: '/AddUsers', component: AddUsers },
        { path: '/EditUsersProfile', component: EditUsersProfile },
        { path: '/ChangePassword', component: ChangePassword },  
        { path: '/Customers', component: Customers },
        //{ path: '/AddOldPackage', component: AddOldPackage },
        { path: '/AddOldCustomers', component: AddOldCustomers },
        //{ path: '/AddPackage', component: AddPackage },
        { path: '/AddCustomers', component: AddCustomers },
        { path: '/Reports', component: Reports },
        //{ path: '/UnknownPackage', component: UnknownPackage },
        { path: '/Seting', component: Seting },
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

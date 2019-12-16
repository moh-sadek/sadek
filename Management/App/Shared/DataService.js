﻿import axios from 'axios';

axios.defaults.headers.common['X-CSRF-TOKEN'] = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

const baseUrl = 'http://localhost:4810/Api';

export default {
    Login(loginName, password, secretNo) {
        return axios.post(baseUrl + '/security/login', { loginName, password, secretNo });
    },
    Logout() {
        return axios.post(baseUrl + '/security/logout');
    },    
    CheckLoginStatus() {
        return axios.post('/security/checkloginstatus');
    },  



    //Remove All APIs
    GetPackges(pageNo, pageSize, searchType, id) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.get(baseUrl + `/Admin/Packeges/GetPackges?pageNo=${pageNo}&pagesize=${pageSize}&searchType=${searchType}&id=${id}`);
    },

    GetCustomers() {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.get(baseUrl + `/Admin/Packeges/GetCustomers`);
    },

    GetCodes() {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.get(baseUrl + `/Admin/Packeges/GetCodes`);
    },

    AddCustomor(customersInfo) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.post(baseUrl + `/Admin/Customer/Add`, customersInfo);//change path
    },

    GetCustomersInfo(pageNo, pageSize,id) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.get(baseUrl + `/Admin/Customer/GetCustomers?pageNo=${pageNo}&pagesize=${pageSize}&id=${id}`);
    },

    AddCustomorPackage(serviceInfo) {
        debugger
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.post(baseUrl + `/Admin/Customer/AddPackage`, serviceInfo);//change path
    },

    EditCustomorInfo(customersInfo) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.post(baseUrl + `/Admin/Customer/EditCustomorInfo`, customersInfo);//change path
    },

    getPakegeByState(pageNo, pageSize, custmorId, state) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.get(baseUrl + `/Admin/Customer/getPakegeByState?pageNo=${pageNo}&pagesize=${pageSize}&custmorId=${custmorId}&state=${state}`);
    },

    GetHistoryPackges(pageNo, pageSize, custmorId) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.get(baseUrl + `/Admin/Customer/GetHistoryPackges?pageNo=${pageNo}&pagesize=${pageSize}&custmorId=${custmorId}`);
    },

    stopServeice(id, stopResone) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.post(baseUrl + `/Admin/Customer/stopServeice?id=${id}&disc=${stopResone}`);//change path
    },

    backServeice(id) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.post(baseUrl + `/Admin/Customer/backServeice?id=${id}`);//change path
    },

    rechargeService(id,ReloadserviceInfo) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.post(baseUrl + `/Admin/Customer/rechargeService?id=${id}`, ReloadserviceInfo);//change path
    },

    deleteCustmor(id) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.post(baseUrl + `/Admin/Customer/deleteCustmor?id=${id}`);//change path
    },

    //user
    getUser(pageNo, pageSize) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + document.querySelector('meta[name="api-token"]').getAttribute('content');
        return axios.get(baseUrl + `/Admin/User/getUser?pageNo=${pageNo}&pagesize=${pageSize}`);
    },



}
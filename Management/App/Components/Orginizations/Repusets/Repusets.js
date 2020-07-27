import Info from '../Info/Info.vue';
import moment from 'moment';
import CryptoJS from 'crypto-js';

export default {
    name: 'Repusets',
    created() {

        this.getRequest();
        this.GetCenter();

        this.CheckLoginStatus();

    },
    components: {
        'Info': Info
    },
    filters: {
        moment: function (date) {
            if (date === null) {
                return "فارغ";
            }
            // return moment(date).format('MMMM Do YYYY, h:mm:ss a');
            return moment(date).format('MMMM Do YYYY');
        }
    },
    data() {
        return {

            loginDetails: null,

            Centers: [],
            CenterId: '',

            pageNo: 1,
            pageSize: 5,
            pages: 0,
            state: 0,
            Requests:[],

            SelectedId: '',
        };
    },
    methods: {

        CheckLoginStatus() {
            try {
                this.loginDetails = this.decrypt(sessionStorage.getItem('currentUser'), sessionStorage.getItem('SECRET_KEY'));
                if (this.loginDetails == null) {
                    window.location.href = '/Security/Login';
                }
            } catch (error) {
                window.location.href = '/Security/Login';
            }
        },
        encrypt: function encrypt(data, SECRET_KEY) {
            var dataSet = CryptoJS.AES.encrypt(JSON.stringify(data), SECRET_KEY);
            dataSet = dataSet.toString();
            return dataSet;
        },
        decrypt: function decrypt(data, SECRET_KEY) {
            data = CryptoJS.AES.decrypt(data, SECRET_KEY);
            data = JSON.parse(data.toString(CryptoJS.enc.Utf8));
            return data;
        },


        getRequest(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }

            this.$blockUI.Start();
            this.$http.getRequest(this.pageNo, this.pageSize, this.CenterId)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Requests = response.data.requests;
                    this.pages = response.data.count;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
                });

        },

        ViewOrginzation(id) {
            this.state = 2;
            this.SelectedId = id;

        },

        delteOrginzation(id) {
            this.$confirm('سيؤدي ذلك إلى حذف المنظمة  . استمر؟', 'تـحذير', {
                confirmButtonText: 'نـعم',
                cancelButtonText: 'لا',
                type: 'warning'
            }).then(() => {


                this.$http.delteOrginzation(id)
                    .then(response => {
                        this.$message({
                            type: 'info',
                            message: response.data
                        });
                        this.$blockUI.Stop();
                        this.getRequest();
                    })
                    .catch((err) => {
                        this.$blockUI.Stop();
                        this.$message({
                            type: 'error',
                            message: err.response.data
                        });
                    });
            });
        },

        GetCenter() {
            this.$blockUI.Start();
            this.$http.GetCenter()
                .then(response => {
                    this.$blockUI.Stop();
                    this.Centers = response.data.centers;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });
        },

        addOrignization(id) {
            this.$confirm('سيؤدي ذلك إلى إضافة المنظمة  . استمر؟', 'تـحذير', {
                confirmButtonText: 'نـعم',
                cancelButtonText: 'لا',
                type: 'warning'
            }).then(() => {


                this.$http.addOrignization(id)
                    .then(response => {
                        this.$message({
                            type: 'info',
                            message: response.data
                        });
                        this.$blockUI.Stop();
                        this.getRequest();
                    })
                    .catch((err) => {
                        this.$blockUI.Stop();
                        this.$message({
                            type: 'error',
                            message: err.response.data
                        });
                    });
            });
        }

    }
}

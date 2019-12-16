import addUsers from './AddUsers/AddUsers.vue';
import editUsers from './EditUsers/EditUsers.vue';
import moment from 'moment';
import CryptoJS from 'crypto-js';

export default {
    name: 'Customers',
    created() {

        this.SearchType = [
            {
                id: 1,
                name: "الإسم"
            },
            {
                id: 2,
                name: 'رقم الخدمة'
            }, {
                id: 3,
                name: "الحالة"
            }


        ];

        this.pakegeStatus = [
            {
                id: 1,
                name: "Active"
            },
            {
                id: 2,
                name: 'Not Active'
            }, {
                id: 3,
                name: "Done"
            }


        ];

        this.getUser();
        this.getCodes();

    },
    components: {
        'add-Users': addUsers,
        'edit-Users': editUsers
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
            SearchType: [],
            SearchTypeSelected: [],
            Coustmors: [],
            CoustmorSelected: [],

            pakegeStatus: [],
            pakegeStatusSelected: [],
            packeges: [],
            custmors: [],
            codes: [],
            CodeSelectd: [],

            selectedCustmor: [],


            CourseEdit: [],
            SuperPackageParent: this.$parent.SuperPackageParent,
            pageNo: 1,
            pageSize: 10,
            pages: 0,
            Courses: [],
            state: 0,







            users:[],

        };
    },
    methods: {



        getUser(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }

            this.$blockUI.Start();
            this.$http.getUser(this.pageNo, this.pageSize)//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    this.users = response.data.users;
                    this.pages = response.data.count;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
                });

        },

        getCodes() {

            this.$blockUI.Start();
            this.$http.GetCodes()
                .then(response => {
                    this.$blockUI.Stop();
                    this.codes = response.data.codes;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
                });

        },

        addCustomur() {
            this.state = 1;
        },

        viewCustmor(item) {
            this.selectedCustmor = item;
            this.state = 2;
        },


        delteCustmor(item) {


            this.$confirm('سيؤدي ذلك إلى حدف العميل  . استمر؟', 'تـحذير', {
                confirmButtonText: 'نـعم',
                cancelButtonText: 'لا',
                type: 'warning'
            }).then(() => {


                this.$http.deleteCustmor(item.customerId)
                    .then(response => {
                        this.$message({
                            type: 'info',
                            message: response.data
                        });
                        this.$blockUI.Stop();
                        this.getCustomers(1, 0);
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
    }
}

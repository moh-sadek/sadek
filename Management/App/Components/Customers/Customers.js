import moment from 'moment';
import addCustomers from './AddCustomers/AddCustomers.vue';
import customersSteps from './CustomersSteps/CustomersSteps.vue';
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
                name: 'رقم الهاتف'
            }


        ];


        this.getCustomers(1,0);
        this.getCodes();
        this.getCustomersPhone();
        

        //console.log(this.$route.params.SuperPackageId)
        //console.log(this.$parent.SuperPackageParent);
        //if (this.$parent.SuperPackageParent==null) {
        //    this.$router.push("/Packages/SuperPackages");
        //}
       // this.GetCoursesBySuperPackageId(this.pageNo);  
        //welcome
    },
    components: {
        'add-coustomers': addCustomers,
        'customersSteps': customersSteps,
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

            selectedCustmor:[],


            CourseEdit:[],
            SuperPackageParent: this.$parent.SuperPackageParent,
            pageNo: 1,
            pageSize: 10,
            pages: 0,  
            Courses: [],
            state: 0,

            custmorPhone:[],
            PhoneSelected:[],
          
        };
    },
    methods: {

        getCustomers(pageNo,id) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }
            
            this.$blockUI.Start();
            this.$http.GetCustomersInfo(this.pageNo, this.pageSize,id)//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    this.custmors = response.data.custmor;
                    this.pages = response.data.count;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
                });

        },

        getCustomersPhone() {

            this.$blockUI.Start();
            this.$http.getCustomersPhone()//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    this.custmorPhone = response.data.customersPhone;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
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

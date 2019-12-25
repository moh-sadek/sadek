import moment from 'moment';
export default {
    name: 'Packages',    
    created() {

        this.SearchType = [
            {
                id: 1,
                name: "الإسم"
            },
            {
                id: 2,
                name: 'رقم الخدمة'
            },{
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
                name: "	Stopped"
            }


        ];

        this.getPackeges(1,0,0);
        this.getCustomers();
        this.getCodes();
        

        //console.log(this.$route.params.SuperPackageId)
        //console.log(this.$parent.SuperPackageParent);
        //if (this.$parent.SuperPackageParent==null) {
        //    this.$router.push("/Packages/SuperPackages");
        //}
       // this.GetCoursesBySuperPackageId(this.pageNo);  
        //welcome
    },
    components: {
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


            CourseEdit:[],
            SuperPackageParent: this.$parent.SuperPackageParent,
            pageNo: 1,
            pageSize: 15,
            pages: 0,  
            Courses: [],
            state: 0,

            StopSMSServiceDialog: false,
            stopResone: '',
            SMSstopResone: 'لقد تم إيقاف الخدمة الخاصة بكم الرجاء مراجعة إدارة الخدمات بشركة المدار',
            selectedPackege: [],

            SMSServiceInfoDialog: false,
            ReloadServiceDialog: false,

            ReloadserviceInfo:
            {
                custmorId: 0,
                amount: '',
                countMassage: '',
                to: '',
            },
          
        };
    },
    methods: {

        getPackeges(pageNo, searchType, typeid) {
            //searchType
            //1 name
            //2 code

            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }
            this.$blockUI.Start();
            this.$http.GetPackges(this.pageNo, this.pageSize,searchType,typeid)//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    this.packeges = response.data.packeges;
                    this.pages = response.data.count;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
                });

        },

        getCustomers() {
            
            this.$blockUI.Start();
            this.$http.GetCustomers()//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    this.custmors = response.data.custmor;
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

        
        showStopSMSDialog(item) {
            this.StopSMSServiceDialog = true;
            this.selectedPackege = item;
        },
        
        stopServeice() {
            if (!this.stopResone) {
                this.$message({
                    type: 'info',
                    message: 'الرجاء إدخال سبب الإيقاف'
                });
                return;
            }
            this.$http.stopServeice(this.selectedPackege.id, this.stopResone)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$blockUI.Stop();
                    this.StopSMSServiceDialog = false;
                    this.getPackeges(1, 0, 0);
                    this.getCustomers();
                    this.getCodes();
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

        canselStoped() {
            this.StopSMSServiceDialog = false;
        },

        backService(item) {


            this.$confirm('سيؤدي ذلك إلى إعادة تشغيل الباقة  . استمر؟', 'تـحذير', {
                confirmButtonText: 'نـعم',
                cancelButtonText: 'لا',
                type: 'warning'
            }).then(() => {


                this.$http.backServeice(item.id)
                    .then(response => {
                        this.$message({
                            type: 'info',
                            message: response.data
                        });
                        this.$blockUI.Stop();
                        this.getPackeges(1, 0, 0);
                        this.getCustomers();
                        this.getCodes();
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

        showSmsInfo(item) {
            this.SMSServiceInfoDialog = true;
            this.selectedPackege = item;
        },

        showReloadSMS(item) {
            this.ReloadServiceDialog = true;
            this.selectedPackege = item;
        },

        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.rechargeService();
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });


        },

        rechargeService() {
            this.$http.rechargeService(this.selectedPackege.id, this.ReloadserviceInfo)
                .then(response => {
                    //this.$parent.state = 0;
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$blockUI.Stop();
                    this.getPackeges(1, 0, 0);
                    this.ReloadServiceDialog = false;
                   // this.resetReloadserviceInfo();
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

        resetReloadserviceInfo() {
            this.ReloadserviceInfo.custmorId = '';
            this.ReloadserviceInfo.amount = '';
            this.ReloadserviceInfo.countMassage = '';
            this.ReloadserviceInfo.to = '';
        },

        addPackege() {
            this.state = 1;
        },

    }    
}

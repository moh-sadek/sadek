import moment from 'moment';
export default {
    name: 'AddCustomers',    
    created() {

        
        this.custmor = this.$parent.selectedCustmor;
        this.serviceInfo.customerId = this.custmor.customerId;
        this.customersInfo.name = this.custmor.fullName;
        this.customersInfo.phone = this.custmor.phone;
        this.customersInfo.date = this.custmor.birthDate;
        this.customersInfo.email = this.custmor.email;
        this.customersInfo.companyName = this.custmor.companyName;
        this.customersInfo.companyAddress = '\\\\\\\\\\\\';
        this.customersInfo.custmorId = this.custmor.customerId;

        this.getPakegeByState(1,1);
        this.getPakegeByState(1, 2);
        this.getPakegeByState(1, 3);
        this.getHistoryPackges();
      

    },
   
    data() {
        return {
            active: 0,
            baseStyles: {
                width: '434.443px',
                transform: 'translate3d(0px, 0px, 0px)',
                transition: 'transform 0.5s ease 0s'
            },
            baseStyles1: {
                width: '0',
                transform: 'translate3d(10px, 0px, 0px)',
                transition: 'transform 0.5s ease 0s'
            },
            
            icons: 'nc-icon nc-single-02',
            iconsHont: 'البيانات الشخصية',
            EditInfo:0,

            currentPage:0,


            custmor:[],

            currentState:1,

            SMSInfoDialog: false,
            StopSMSServiceDialog: false,
            ReloadServiceDialog: false,

            pageNo: 1,
            pageSize: 10,
            pages: 0,  

            serviceInfo:
            {
                custmorId:0,
                serviceName: '',
                code: '',
                amount: '',
                countMassage: '',
                from: '',
                to: '',
                discriptions: '',
            },



            customersInfo: {
                custmorId: 0,
                name: '',
                phone: '',
                date: '',
                email: '',
                companyName: '',
                companyAddress: '',
                
            },


            ActivePageNo: 1,
            ActivePageSize: 10,
            ActivePages: 0,  

            activePackges: [],

            notActivePageNo: 1,
            notActivePageSize: 10,
            notActivePages: 0,

            notactivePackges: [],

            stoppedPageNo: 1,
            stoppedPageSize: 10,
            stoppedPages: 0,

            stoppedPackges: [],

            historyPageNo: 1,
            historyPageSize: 4,
            historyPages: 0,

            historyPackges: [],

            stopResone:'',
            SMSstopResone:'لقد تم إيقاف الخدمة الخاصة بكم الرجاء مراجعة إدارة الخدمات بشركة المدار',

            selectedPackege:[],


        };
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

    methods: {
        ChangDivStyle(divNumber) {
            if (divNumber == 1) {
                this.baseStyles.transform = 'translate3d(0px, 0px, 0px)';
                this.icons = 'nc-icon nc-single-02';
                this.iconsHont ='البيانات الشخصية'
                this.currentPage=0;
            } else if (divNumber == 2) {
                this.baseStyles.transform = 'translate3d(-434.443px, 0px, 0px)';
                this.icons = 'nc-icon nc-layout-11';
                this.iconsHont = 'الباقات'
                this.currentPage=1;
            } else {
                this.baseStyles.transform = 'translate3d(-878.887px, 0px, 0px)';
                this.icons = 'nc-icon nc-simple-add';
                this.iconsHont = 'إضافة باقة'
                this.currentPage=2;
            }
        },



        allowEdit() {
            this.EditInfo = 1;
        },

        canselEdit() {
            this.EditInfo = 0;
        },

        addPackage()
        {

            //this.to=this.from;
            this.$http.AddCustomorPackage(this.serviceInfo)
                .then(response => {
                    //this.$parent.state = 0;
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$blockUI.Stop();
                    this.ChangDivStyle(2)
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

        changeCurrentState(index)
        {
            if(index==1)
            {
                this.currentState=1;
            }else if(index==2)
            {
                this.currentState=2;
            }
            else if(index==3)
            {
                this.currentState=3;
            } else if (index == 4) {
                this.currentState = 4;
            }
        },

        showSMSInfo()
        {
            this.SMSInfoDialog=true;
        },

        showStopSMSDialog(item)
        {
            this.StopSMSServiceDialog = true;
            this.selectedPackege = item;
        },

        showReloadDialog()
        {
            this.ReloadServiceDialog=true;
        },

        editPersnalInfo() {
            this.$http.EditCustomorInfo(this.customersInfo)
                .then(response => {
                    //this.$parent.state = 0;
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$blockUI.Stop();
                    //this.ChangDivStyle(2)
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },


        getPakegeByState(pageNo, state) {
            this.ActivePageNo = pageNo;
            if (this.ActivePageNo === undefined) {
                this.ActivePageNo = 1;
            } 

            this.$blockUI.Start();
            this.$http.getPakegeByState(this.ActivePageNo, this.ActivePageSize, this.custmor.customerId, state)//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    if (state == 1) {
                        this.activePackges = response.data.packeges;
                        this.ActivePages = response.data.count;
                    } else if (state == 2) {
                        this.notactivePackges = response.data.packeges;
                        this.notActivePages = response.data.count;
                    } else if (state == 3) {
                        this.stoppedPackges = response.data.packeges;
                        this.stoppedPages = response.data.count;
                    }
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.ActivePages = 0;
                });

        },

        getHistoryPackges(pageNo) {
            this.historyPageNo = pageNo;
            if (this.historyPageNo === undefined) {
                this.historyPageNo = 1;
            }
            this.$blockUI.Start();
            this.$http.GetHistoryPackges(this.historyPageNo, this.historyPageSize, this.custmor.customerId)//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    this.historyPackges = response.data.packeges;
                    this.historyPages = response.data.count;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.ActivePages = 0;
                });

        },





        //method 
        //1 add pakege
        //2 edit customr info
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if (this.currentPage == 0)
                    {
                        this.editPersnalInfo();

                    }else if(this.currentPage==2)
                    {
                        this.addPackage();
                    }
                    //this.addCustomor();
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });


        },

        addCustomor() {

            this.$http.AddCustomor(this.customersInfo)
                .then(response => {
                    this.$parent.state = 0;
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$blockUI.Stop();
                    this.$parent.state = 0;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
            
        },



        cansel() {
            this.$parent.getCustomers();
            this.$parent.state = 0;
            
        },

        canselStoped() {
            this.StopSMSServiceDialog = false;
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
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },




        resetForm(formName) {
            this.$refs[formName].resetFields();
        },
    }    
}



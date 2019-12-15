export default {
    name: 'AddCustomers',    
    created() {

        this.custmor = this.$parent.selectedCustmor;
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
                custmorId:1,
                serviceName: '',
                code: '',
                amount: '',
                countMassage: '',
                from: '',
                to: '',
                discriptions: '',
            },



            customersInfo: {
                name: '',
                phone: '',
                date: '',
                email: '',
                companyName: '',
                companyAddress: '',
                
                
                
            },
        };
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
            }
        },

        showSMSInfo()
        {
            this.SMSInfoDialog=true;
        },

        showStopSMSDialog()
        {
            this.StopSMSServiceDialog=true;
        },

        showReloadDialog()
        {
            this.ReloadServiceDialog=true;
        },





        //method 
        //1 add pakege
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if(this.currentPage==2)
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



        resetForm(formName) {
            this.$refs[formName].resetFields();
        },
    }    
}



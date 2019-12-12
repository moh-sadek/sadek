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
            icons: 'nc-icon nc-single-02',
            iconsHont: 'البيانات الشخصية',
            EditInfo:0,


            custmor:[],





            customersInfo: {
                name: '',
                phone: '',
                date: '',
                email: '',
                companyName: '',
                companyAddress: '',
                serviceName: '',
                code: '',
                amount: '',
                countMassage: '',
                from: '',
                to: '',
                discriptions: '',
                
                
            },
        };
    },

    methods: {
        ChangDivStyle(divNumber) {
            if (divNumber == 1) {
                this.baseStyles.transform = 'translate3d(0px, 0px, 0px)';
                this.icons = 'nc-icon nc-single-02';
                this.iconsHont ='البيانات الشخصية'
            } else if (divNumber == 2) {
                this.baseStyles.transform = 'translate3d(-434.443px, 0px, 0px)';
                this.icons = 'nc-icon nc-layout-11';
                this.iconsHont = 'الباقات'
            } else {
                this.baseStyles.transform = 'translate3d(-878.887px, 0px, 0px)';
                this.icons = 'nc-icon nc-simple-add';
                this.iconsHont = 'إضافة باقة'
            }
        },


        allowEdit() {
            this.EditInfo = 1;
        },

        canselEdit() {
            this.EditInfo = 0;
        },








        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    this.addCustomor();
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



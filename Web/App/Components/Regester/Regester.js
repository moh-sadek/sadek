export default {
    name: 'Regester',
    created() {
        //this.BranchId = this.$parent.BrancheModel;
        this.GetMunicipalitys();
    },

    data() {

        var validatePass = (rules, value, callback) => {
            if (value === '') {
                callback(new Error('الرجاء إدخال كلمة المرور'));
            } else {
                if (this.ruleForm.ConfimPassword !== '') {
                    this.$refs.ruleForm.validateField('ConfimPassword');
                }
                callback();
            }
        };
        var validatePass2 = (rrulesule, value, callback) => {
            if (value === '') {
                callback(new Error('الرجاء كتابه اعاده كلمه المرور'));
            } else if (value !== this.ruleForm.Password) {
                callback(new Error('الرجاء التأكد من تطابق كلمة المرور'));
            } else {
                callback();
            }
        };
        return {
            pageNo: 1,
            pageSize: 10,
            pages: 0,

            ruleForm: {
                UserId: 0,
                LoginName: '',
                Password: '',
                FullName: '',
                UserType: 1,
                Email: '',
                Gender: '',
                Phone: '',
                DateOfBirth: '',
                Status: 0,
                BranchId: 1,
                MunicipalitysId:'',




            },

            Municipalitys: [],

            ConfimPassword: '',

            rules: {
                DateOfBirth: [
                    { required: true, message: 'الرجاء إدخال تاريخ الميلاد', trigger: 'blur' }
                ],
                Password: [
                    { validator: validatePass, trigger: 'blur' },
                    { required: true, pattern: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]){8,}.*$/, message: ' يجب أن تتكون كلمة المرور من حروف صغيرة وكبيرو وأرقام', trigger: 'blur' }
                ],
                ConfimPassword: [
                    { validator: validatePass2, trigger: 'blur' },
                    { required: true, pattern: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]){8,}.*$/, message: 'يجب أن تتكون كلمة المرور من حروف صغيرة وكبيرو وأرقام', trigger: 'blur' }
                ],
                UserType: [
                    { required: true, message: 'الرجاء اختيار  الصلاحيه', trigger: 'blur' }
                ],
                FullName: [
                    { required: true, message: 'الرجاء إدخال الاسم التلاثي', trigger: 'blur' },
                    { required: true, message: /^[\u0621-\u064A\u0660-\u0669 ]+$/, trigger: 'blur' }
                ],
                LoginName: [
                    { required: true, message: 'الرجاء إدخال اسم الدخول', trigger: 'blur' },
                    { required: true, pattern: /^[A-Za-z]{0,40}$/, message: 'الرجاء إدخال اسم الدخول بطريقه صحيحه', trigger: 'blur' }
                ],
                Phone: [
                    { required: true, message: 'الرجاء إدخال رقم الهاتف', trigger: 'blur' },
                    { min: 9, max: 10, message: 'الرجاء إدخال رقم الهاتف  بطريقه صحيحه', trigger: 'blur' }
                ],


                Email: [
                    { required: true, message: 'الرجاء إدخال البريد الإلكتروني', trigger: 'blur' },
                    { required: true, pattern: /\S+@\S+\.\S+/, message: 'الرجاء إدخال البريد الإلكتروني بطريقه صحيحه', trigger: 'blur' }
                ],
                Gender: [
                    { required: true, message: 'الرجاء اختيار الجنس', trigger: 'change' }

                ],

                MunicipalitysId: [
                    { required: true, message: 'الرجاء اختيار البلدية', trigger: 'change' }

                ],

            }

        };


    },
    methods: {

        GetMunicipalitys() {
            this.$blockUI.Start();
            this.$http.GetMunicipalitys()
                .then(response => {
                    this.$blockUI.Stop();
                    this.Municipalitys = response.data.municipalitys;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });
        },


        validEmail: function (email) {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        },
        validLoginName: function (LoginName) {
            var login = /^[a-zA-Z]{0,40}$/;
            return login.test(LoginName);
        },
        validFullName: function (FullName) {
            var loginArabic = /^[\u0621-\u064A\u0660-\u0669 ]+$/;
            return loginArabic.test(FullName);
        },

        validPassword: function (Password) {

            var PasswordT = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]){8,}.*$/;

            return PasswordT.test(Password);
        },
        validPhone: function (Phone) {

            var mobileRegex = /^09[1|2|3|4|5][0-9]{7}$/i;

            return mobileRegex.test(Phone);
        },

        Save(formName) {

            this.$refs[formName].validate((valid) => {
                if (valid) {
                    //this.ruleForm.BranchLevel = this.$parent.permissionModale;
                    this.$blockUI.Start();
                    this.$http.AddUser(this.ruleForm)
                        .then(response => {
                            this.$blockUI.Stop();
                            //this.$parent.state = 0;
                            //this.$parent.GetUsers(this.pageNo);
                            this.$message({
                                type: 'success',
                                dangerouslyUseHTMLString: true,
                                duration: 5000,
                                message: '<strong>' + response.data + '</strong>'
                            });
                            this.resetForm(formName);
                            this.$router.push('/');
                        })
                        .catch((err) => {
                            this.$blockUI.Stop();
                            this.$message({
                                type: 'error',
                                dangerouslyUseHTMLString: true,
                                duration: 5000,
                                message: '<strong>' + err.response.data + '</strong>'
                            });
                        });
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });


        },

        resetForm(formName) {
            this.$refs[formName].resetFields();
        },




    }
}

﻿export default {
    name: 'AddDoctors',
    created() {

        this.UserType = [
            {
                id: 3,
                name: "Doctor A"
            },
            {
                id: 4,
                name: 'Doctor B'
            },
            {
                id: 5,
                name: 'Doctor C'
            }

        ];
         
    },
    data() {
        return {
            pageNo: 1,
            pageSize: 10,
            pages: 0,

            UserType: [],

            form: {
                LoginName: '',
                Password: null,
                FullName: '',
                UserType: '',
                Email: '',
                Gender: '',
                Phone: '',
                DateOfBirth: '',
                Status: 0,
               
            },
            ConfimPassword: '',
        };
    },
    methods: {

        resetform() {
            this.form.LoginName = '';
            this.form.Password = null;
            this.form.FullName = '';
            this.form.UserType = '';
            this.form.Email = '';
            this.form.Gender = '';
            this.form.Phone = '';
            this.form.DateOfBirth = '';
            this.ConfimPassword = '';
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


        Save() {
            this.$blockUI.Start();

            if (!this.form.LoginName) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الـرجاء إدخال اسم الدخول'
                });
                return;
            } else if (!this.validLoginName(this.form.LoginName)) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال اسم الدخول بطريقه صحيحه '
                });
                return;
            }

            if (!this.form.FullName) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال الاسم التلاثي '
                });
                return;
            } else if (!this.validFullName(this.form.FullName)) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال الاسم التلاثي بطريقه صحيحه '
                });
                return;
            }

            if (!this.form.Email) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال البريد الإلكتروني '
                });
                return;
            }
            else if (!this.validEmail(this.form.Email)) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال البريد الإلكتروني بطريقه صحيحه '
                });
                return;
            }

            if (!this.form.Gender) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إختيار الجنس '
                });
                return;
            }

            if (!this.form.Phone) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء رقم الهاتف '
                });
                return;
            } else if (!this.validPhone(this.form.Phone)) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال رقم الهاتف  بطريقه صحيحه '
                });
                return;
            }

            if (!this.form.DateOfBirth) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إختيار تاريخ الميلاد '
                });
                return;
            }

            if (!this.form.Password) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال الرقم السري '
                });
                return;
            }
            if (this.form.Password.length <= 6) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال الرقم السري تحتوي علي سته ارقام '
                });
                return;
            }
            if (!this.validPassword(this.form.Password)) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'عـفوا : يجب ان يحتوي الرقم السري علي حروف صغيرة وكبيرة وارقام'
                });
                return;
            }

            if (!this.ConfimPassword) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال تأكيد الرقم السري '
                });
                return;
            }


            if (this.form.Password != this.ConfimPassword) {
                this.$blockUI.Stop();
                this.$message({
                    type: 'error',
                    message: 'الرجاء التأكد من تطابق الرقم السري'
                });
                return;
            }

            
            this.$http.AddUser(this.form)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.resetform();
                    this.$blockUI.Stop();
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        }




    }
}

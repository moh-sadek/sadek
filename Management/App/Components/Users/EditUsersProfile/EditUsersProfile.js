import CryptoJS from 'crypto-js';
import moment from 'moment';
export default {
    name: 'EditUsersProfile',    
    created() {
        debugger
        this.CheckLoginStatus();

    },
    data() {  
       
        return {
            form: {
                LoginName: '',
                Password: '',
                FullName: '',
                UserType: '',
                Email: '',
                Gender: '',
                Phone:'',
                DateOfBirth: '',
                Status: 0,
                OfficeName: [],
                AllData: [],
                loginDetails: null,
                photo:[]

            }, 
            
            form2: {
                Phone: '',
                Email: '',
                LoginName: '',
                FullName: '',
                Gender: '',
                DateOfBirth: '',
                Status: 0,
            },   
            
        }
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
        CheckLoginStatus() {
            try {
                debugger
                this.loginDetails = this.decrypt(sessionStorage.getItem('currentUser'), sessionStorage.getItem('SECRET_KEY'));
                if (this.loginDetails != null) {
                    this.form.FullName = this.loginDetails.fullName;
                    this.form.Phone = this.loginDetails.phone;
                    this.form.LoginName = this.loginDetails.loginName;
                    this.form.Email = this.loginDetails.email;
                    this.form.Gender = this.loginDetails.gender;
                    this.form.DateOfBirth = this.loginDetails.dateOfBirth;
                } else {
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


        validPhone: function (Phone) {

            var mobileRegex = /^09[1|2|3|4|5][0-9]{7}$/i;

            return mobileRegex.test(Phone);
        },
        FileChanged(e) {
            var files = e.target.files;

            if (files.length <= 0) {
                return;
            }

            if (files[0].type !== 'image/jpeg' && files[0].type !== 'image/png') {
                this.$message({
                    type: 'error',
                    message: 'عفوا يجب انت تكون الصورة من نوع JPG ,PNG'
                });
                this.photo = null;
                return;
            }

            var $this = this;

            var reader = new FileReader();
            reader.onload = function () {
                $this.photo = reader.result;
                $this.UploadImage();
            };
            reader.onerror = function (error) {
                $this.photo = null;
            };
            reader.readAsDataURL(files[0]);
        },

        UploadImage() {
            console.log(this.photo);
            console.log(this.loginDetails.userId);
            this.$blockUI.Start();
            var obj = {
                Photo: this.photo,
                UserId: this.loginDetails.userId
            };
            this.$http.UploadImage(obj)
                .then(response => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'info',
                        message: response.data
                    });

                    setTimeout(() =>
                        window.location.href = '/EditUsersProfile'
                        , 500);

                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
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
        Save() {

            if (!this.form.LoginName) {
                this.$message({
                    type: 'error',
                    message: 'الـرجاء إدخال اسم الدخول'
                });
                return;
            } else if (!this.validLoginName(this.form.LoginName)) {
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال اسم الدخول بطريقه صحيحه '
                });
                return;
            }
            if (!this.form.Email) {
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال البريد الإلكتروني '
                });
                return;
            } else if (!this.validEmail(this.form.Email)) {
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال البريد الإلكتروني بطريقه صحيحه '
                });
                return;
            }

            if (!this.form.Gender) {
                this.$message({
                    type: 'error',
                    message: 'الرجاء إختيار الجنس '
                });
                return;
            }

            if (!this.form.DateOfBirth) {
                this.$message({
                    type: 'error',
                    message: 'الرجاء إختيار تاريخ الميلاد '
                });
                return;
            }
            if (!this.form.Phone) {
                this.$message({
                    type: 'error',
                    message: 'الرجاء رقم الهاتف '
                });
                return;
            } else if (!this.validPhone(this.form.Phone)) {
                this.$message({
                    type: 'error',
                    message: 'الرجاء إدخال رقم الهاتف  بطريقه صحيحه '
                });
                return;
            }

            this.form2.Phone = this.form.Phone
            this.form2.Email = this.form.Email;
            this.form2.LoginName = this.form.LoginName;
            this.form2.FullName = this.form.FullName;
            this.form2.Gender = this.form.Gender;
            this.form2.DateOfBirth = this.form.DateOfBirth;
       
            this.$http.EditUsersProfile(this.form2)
                .then(response => {
                    debugger
                    this.loginDetails.email = this.form2.Email;
                    this.loginDetails.phone = this.form2.Phone;
                    this.loginDetails.LoginName = this.form2.LoginName;
                    this.loginDetails.FullName = this.form2.FullName;
                    this.loginDetails.Gender = this.form2.Gender;
                    this.loginDetails.DateOfBirth = this.form2.DateOfBirth;
                    //sessionStorage.setItem('currentUser', JSON.stringify(this.loginDetails));
                    //sessionStorage.clear();
                    sessionStorage.setItem('SECRET_KEY', sessionStorage.getItem('SECRET_KEY'));
                    sessionStorage.setItem('currentUser', this.encrypt(this.loginDetails, sessionStorage.getItem('SECRET_KEY')));
                    this.CheckLoginStatus();
                   
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                  
                })
                .catch((err) => {
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

    }
}

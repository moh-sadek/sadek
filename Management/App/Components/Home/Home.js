
import CryptoJS from 'crypto-js';
export default {
    name: 'home',
    components: {
      
    },
    created() {  
        this.CheckLoginStatus();  
        this.GetDashpordInfo();
    },
    data() {
        return {
            open: false,
            loginDetails: null,

            form: {
                LoginName: '',
                Password: '',
                FullName: '',
                UserType: '',
                Email: '',
                Gender: '',
                Phone: '',
                createdOn: '',
                DateOfBirth: '',
                Status: 0,
                OfficeName: [],
                AllData: [],
                loginDetails: null,
                photo: []

            }, 

            Detals:[],

        };
    },
    methods: {
        CheckLoginStatus() {
            try {
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

        GetDashpordInfo() {
            this.$blockUI.Start();
            this.$http.GetDashpordInfo()
                .then(response => {
                    this.$blockUI.Stop();
                    this.Detals = response.data.detals;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err.error);
                });

        },
    }    
}

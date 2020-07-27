export default {
    name: 'ViewPatients',    
    created() {


        this.form.Id = this.$parent.SelectedId;
        this.CForm.Id = this.$parent.SelectedId;
        this.getCaseInfo();

        this.getuserType();
        


    },
    data() {
       
        return {

            ruleForm: {
                UserType: '',
            },

            
            form: {
                Id:'',
                FullName: '',
                BirthDate: '',
                Phone: '',

                HighFever: '',
                Temperature: '',
                Cough: '',
                CoughDry: '',
                SoreThroat: '',
                Breathing: '',

                Shivering: '',
                Diarrhea: '',
                Country: '',
                NearInfected: '',

                OtherSymptoms: '',             
               
            },

            CForm: {
                Id:'',
                DoctorNote: '',
            },

            BForm: {
                Id: '',
                DoctorNote: '',
            },

            AForm: {
                Id: '',
                DoctorNote: '',
            },

            

            Case: '',
            loginDetails:[],
        
        };
    },
    methods: {

        Back() {
            this.$parent.state = 0;
        },

        Edit(){

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

            //if (!this.form.Gender) {
              //  this.$message({
                //    type: 'error',
                //    message: 'الرجاء إختيار الجنس '
                //});
                //return;
            //}

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
        
           
            this.$http.EditUser(this.form)
                .then(response => {
                    
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$parent.EditUsersObj = [];
                    this.$parent.getUser(this.pageNo);
                  this.$parent.state = 0;
                 
                })
                .catch((err) => {
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

        getCaseInfo() {

            this.$blockUI.Start();
            this.$http.getCaseInfo(this.form.Id)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Case = response.data.info;
                    this.form.FullName = this.Case.name;
                    this.form.BirthDate = this.Case.birthDate;
                    this.form.Phone = this.Case.phone;
                    this.form.HighFever = this.Case.highFever;
                    this.form.Temperature = this.Case.temperature;
                    this.form.Cough = this.Case.cough;
                    this.form.CoughDry = this.Case.coughDry;
                    this.form.SoreThroat = this.Case.soreThroat;
                    this.form.Breathing = this.Case.breathing;
                    this.form.Shivering = this.Case.shivering;
                    this.form.Diarrhea = this.Case.diarrhea;
                    this.form.Country = this.Case.country;
                    this.form.NearInfected = this.Case.nearInfected;
                    this.form.OtherSymptoms = this.Case.otherSymptoms;
                    this.CForm.DoctorNote = this.Case.doctorCdisc;
                    this.BForm.DoctorNote = this.Case.doctorBdisc;
                    this.AForm.DoctorNote = this.Case.doctorAdisc;
                    
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$parent.state=0;
                    console.error(err);
                });

        },

        getuserType() {
            this.$blockUI.Start();
            this.$http.getuserType()
                .then(response => {
                    
                    this.ruleForm.UserType = response.data.userType;
                    this.$blockUI.Stop();
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

        DoctorCAnswer() {
            this.$blockUI.Start();
            this.$http.DoctorCAnswer(this.CForm)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$parent.state == 0;
                    this.$blockUI.Stop();
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

        DoctorBAnswer() {
            this.$blockUI.Start();
            this.$http.DoctorBAnswer(this.BForm)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$parent.state == 0;
                    this.$blockUI.Stop();
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    this.$message({
                        type: 'error',
                        message: err.response.data
                    });
                });
        },

        DoctorAAnswer() {
            this.$blockUI.Start();
            this.$http.DoctorAAnswer(this.AForm)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.$parent.state == 0;
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

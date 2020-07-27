import moment from 'moment';
export default {
    name: 'Request',

    created() {
        //this.GetMunicipalitys();
        this.GetLocations();
    },
    components: {
    },
    data() {
        return {
            form: {
                MunicipalitysId: '',
                FName: '',
                SName: '',
                LName: '',
                Gender: '',
                BirthDate: '',
                Phone: '',
                Email: '',
                
                
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

                locationId: '',
                
                OtherSymptoms: '',

            },

            locationId:'',
            Municipalitys: [],
            Locations: [],
        };
    },

    filters: {
        moment: function (date) {
            if (date === null) {
                return 'فارغ';
            }
            return moment(date).format('MMMM Do YYYY');
        }
    },


    methods: {

        GetMunicipalitys() {
            this.$blockUI.Start();
            this.$http.GetMunicipalitys(this.form.locationId)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Municipalitys = response.data.municipalitys;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });
        },

        GetLocations() {
            this.$blockUI.Start();
            this.$http.GetLocations()
                .then(response => {
                    this.$blockUI.Stop();
                    this.Locations = response.data.locations;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });
        },

        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if (!this.validPhone(this.form.Phone)) {
                        this.$blockUI.Stop();
                        this.$message({
                            type: 'error',
                            message: 'الرجاء إدخال رقم الهاتف  بطريقه صحيحه '
                        });
                        return;
                    }

                    this.addRequest(formName);
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });


        },

        resetForm(formName) {
            this.$refs[formName].resetFields();
            this.form.Email = '';
            this.form.Temperature = '';
            this.$router.push('/');
        },

        validPhone: function (Phone) {

            var mobileRegex = /^09[1|2|3|4|5][0-9]{7}$/i;

            return mobileRegex.test(Phone);
        },


        addRequest(formName) {
            this.$blockUI.Start();
            this.$http.addRequest(this.form)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
                    this.resetForm(formName);
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
    }
}

import moment from 'moment';
export default {
    name: 'ContactUs',

    created() {
    },
    components: {
    },
    data() {
        return {
            form: {
                MunicipalitysId: 0,
                FName: '',
                SName: '',
                Email: '',
                OtherSymptoms: '',

            },

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


        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {

                    this.addRequest(formName);
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });


        },

        resetForm(formName) {
            this.$refs[formName].resetFields();
            this.$router.push('/');
        },

        addRequest(formName) {
            this.$blockUI.Start();
            this.$http.conectUs(this.form)
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

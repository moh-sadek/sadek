import moment from 'moment';
export default {
    name: 'Seting',    
    created() {

        this.getFiles();
        this.getfileContent();
        this.getVarbile();
        
    },
    components: {
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
    data() {
        return {
            files: [],
            Selectedfile: '',

            pageNo: 1,
            pageSize: 10,
            pages: 0,

            fileContent: [],
            varbiles: [],

            grade: '',
            extensions: '',

            form: {
                path:'',
            }
          
        };
    },
    methods: {

        getFiles() {
            this.$blockUI.Start();
            this.$http.getFiles()
                .then(response => {
                    this.$blockUI.Stop();
                    this.files = response.data.files;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });

        },

        getfileContent(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }

            this.$blockUI.Start();
            this.$http.getfileContent(this.pageNo, this.pageSize, this.Selectedfile)
                .then(response => {
                    this.$blockUI.Stop();
                    this.fileContent = response.data.filesContent;
                    this.pages = response.data.fileCount;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
                });

        },

        getVarbile() {
            this.$blockUI.Start();
            this.$http.getVarbile()
                .then(response => {
                    this.$blockUI.Stop();
                    this.varbiles = response.data.varbiles;
                    this.form.path = this.varbiles.path;
                    this.grade = this.varbiles.grade;
                    this.extensions = this.varbiles.extensions;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });

        },

        EditPaht() {
            this.$blockUI.Start();
            this.$http.EditPaht(this.form)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
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

        Editgrade() {
            this.$blockUI.Start();
            this.$http.Editgrade(this.grad)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
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

        Editextensions() {
            this.$blockUI.Start();
            this.$http.Editextensions(this.extensions)
                .then(response => {
                    this.$message({
                        type: 'info',
                        message: response.data
                    });
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

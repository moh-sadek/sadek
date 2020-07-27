import ViewPatients from '../ViewMyPatients/ViewMyPatients.vue';
import moment from 'moment';

export default {
    name: 'Patients',
    created() {

        this.getAvaliblePatients();

    },
    components: {
        'ViewPatients': ViewPatients
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
            pageNo: 1,
            pageSize: 5,
            pages: 0,
            state: 0,
            Requests:[],

            SelectedId: '',
        };
    },
    methods: {

         

        getAvaliblePatients(pageNo) {
            this.pageNo = pageNo;
            if (this.pageNo === undefined) {
                this.pageNo = 1;
            }

            this.$blockUI.Start();
            this.$http.getMyPatients(this.pageNo, this.pageSize)
                .then(response => {
                    this.$blockUI.Stop();
                    this.Requests = response.data.requests;
                    this.pages = response.data.count;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.pages = 0;
                });

        },

        addUser() {
            this.state = 1;
        },

        viewCase(id) {
            this.state = 2;
            this.SelectedId = id;

        },

    }
}

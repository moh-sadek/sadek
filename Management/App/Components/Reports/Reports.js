import moment from 'moment';
export default {
    name: 'Reports',    
    created() {

        this.SearchType = [
            {
                id: 2,
                name: "إعادة شحن الباقة"
            },
            {
                id: 1,
                name: "تجديد الباقة"
            },
            {
                id: 3,
                name: "إيقاف الباقة"
            },
            {
                id: 4,
                name: "إلغاء إيقاف الباقة"
            },
            {
                id: 5,
                name: 'رقم الباقة'
            },
            {
                id: 6,
                name: 'الكل'
            }


        ];

        this.getHistoryPackges();
        this.getHistoryCodesPackges();

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
            SearchType: [],
            SearchTypeSelected: [],

            historyPageNo: 1,
            historyPageSize: 10,
            historyPages: 0,

            historyPackges: [],

            historyCodesPackges: [],
            selectedHCodPack: '',
          
        };
    },
    methods: {

        getHistoryPackges(pageNo, selectedHCodPack) {
            this.historyPageNo = pageNo;
            if (this.historyPageNo === undefined) {
                this.historyPageNo = 1;
            }
            this.$blockUI.Start();
            this.$http.GetHistoryPackges(this.historyPageNo, this.historyPageSize,0, selectedHCodPack, this.SearchTypeSelected)//this.$parent.SuperPackageParent.superPackageId
                .then(response => {
                    this.$blockUI.Stop();
                    this.historyPackges = response.data.packeges;
                    this.historyPages = response.data.count;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                    this.ActivePages = 0;
                });

        },

        getHistoryCodesPackges() {

            this.$blockUI.Start();
            this.$http.getHistoryCodesPackges(0)
                .then(response => {
                    this.$blockUI.Stop();
                    this.historyCodesPackges = response.data.historyCodesPackges;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });
        },

    }    
}

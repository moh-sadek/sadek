import moment from 'moment';
export default {
    name: 'Request',

    created() {
        //this.GetMunicipalitys();
        this.GetCenter();
    },
    components: {
    },
    data() {
        return {
            Centers:[],

            form: {

                Name: '',
                PublicityNo: '',
                StartDate: '',
                CenterId:'',
                ManagingDirectorName:'',
                ManagingDirectorPhone:'',
                ManagingDirectorExPhone: '',


                PresidentName:'',
                PresidentEmail:'',
                MembersCount:'',
                MembersMCount:'',
                MembersFCount: '',

                DAllLibya:'',
                DSouth:'',
                DWest:'',
                DEast: '',

                Advantages: '',


                IsGeneral: '',
                Ages: '',
                Quality: '',
                Ethnic: '',
                Geography: '',
                Social: '',
                Charitable: '',
                HumanRights: '',
                ElectoralAffairs: '',
                OtherS: '',
                OtherContentS: '',


                ResoneSpecialization: '',
                Vision: '',
                HaveDatabase: '',

                DatabaseGoal: '',
                DatabaseBenefits: '',

                DatabaseSupport: '',
                DatabaseSupportType: '',


                Cooperat: '',
                CooperatField: '',
                CooperatFieldOther: '',


                IsWatched: '',
                ParticipatedElections: '',
                ElectoralCommission: '',
                Observers: '',


                Issues: '',
                TraningPlace: '',
                TraningIssues: '',


                IsPrograms: '',
                PostersDistribution: '',
                Advertising: '',
                Workshops: '',
                Other: '',
                OtherContent: '',


                CooperatDescription: '',
                IsCoalitionBefor: '',
                CoalitionDescription: '',


                IsCompletedProject: '',
                IsCompletedProjectDesc: '',


                SharedProjects: '',
                SharedProjectsName: '',
                ExperiencesLessons: '',


                FundsProjects: '',
                GovernmentalSources: '',
                NationalBodies: '',
                InternationalBodies: '',
                AnotherSource: '',

                ByRequest: '',
                DirectOffer: '',
                PublicTender: '',
                DirectSponsorship: '',


                OtherFinancing: '',


                AdministrationCapacity: '',
                CommunicationCapacity: '',
                NetworksCapacity: '',
                ConsultingCapacity: '',
                ComputersCapacity: '',
                CampaignManagementCapacity: '',

                CapacityOtherAreas: '',

                Monitor: '',
                LegalFramework: '',
                Media: '',
                Education: '',
                Addressing: '',
                MonitorCampaign: '',
                OtherCapabilities: '',


            },

            rules:
            {

                Name: [
                    { required: true, message: 'الرجاءإدخال اسم المنظمة ', trigger: 'blur' },
                    { min: 2, max: 50, message: 'يجب ان يكون طول العنوان من 2 الي 50 الحرف', trigger: 'blur' },
                    { required: true, pattern: /[\u0600-\u06FF]/, message: 'الرجاء إدخال حروف العربية فقط', trigger: 'blur' },
                ],

                PublicityNo: [
                    { required: true, message: "الرجاء إدخال رقم إشهار المنظمة",trigger: 'blur'},
                    { min: 2,max: 20,message: "يجب ان يكون طول رقم القيد من 2 الي 20 الحرف",trigger: 'blur',},
                    { required: true, pattern: /^[0-9]*$/, message: 'الرجاء إدخال ارقام فقط', trigger: 'blur' }
                ],

                StartDate: [
                    { required: true, message: 'الرجاء إختيار تاريخ بداية النشاط ', trigger: 'blur' }
                ],

                CenterId: [
                    { required: true, message: 'الرجاء إختيار عنوان المنظمة ', trigger: 'blur' }
                ],


                ManagingDirectorName: [
                    { required: true, message: 'الرجاءإدخال اسم المدير التنفيدي  ', trigger: 'blur' },
                    { min: 2, max: 50, message: 'يجب ان يكون طول العنوان من 2 الي 50 الحرف', trigger: 'blur' },
                    { required: true, pattern: /[\u0600-\u06FF]/, message: 'الرجاء إدخال حروف العربية فقط', trigger: 'blur' },
                ],

                ManagingDirectorPhone: [{ required: true, message: "الرجاء إدخال رقم هاتفه الرئيسي", trigger: "blur" },
                { min: 9, max: 13, message: "يجب ان يكون طول رقم الهاتف 9 ارقام علي الاقل", trigger: "blur" },
                { required: true, pattern: /^[0-9]*$/, message: 'الرجاء إدخال ارقام فقط', trigger: 'blur' }
                ],

                ManagingDirectorExPhone: [{ required: true, message: "الرجاء إدخال رقم هاتفه الثانوي", trigger: "blur" },
                { min: 9, max: 13, message: "يجب ان يكون طول رقم الهاتف 9 ارقام علي الاقل", trigger: "blur" },
                { required: true, pattern: /^[0-9]*$/, message: 'الرجاء إدخال ارقام فقط', trigger: 'blur' }
                ],

                PresidentName: [
                    { required: true, message: 'الرجاءإدخال اسم رئيس المنظمة ', trigger: 'blur' },
                    { min: 2, max: 50, message: 'يجب ان يكون طول العنوان من 2 الي 50 الحرف', trigger: 'blur' },
                    { required: true, pattern: /[\u0600-\u06FF]/, message: 'الرجاء إدخال حروف العربية فقط', trigger: 'blur' },
                ],

                PresidentEmail: [
                    { required: true, message: 'الرجاء إدخال عنوان بريده الإلكتروني', trigger: 'blur' },
                    { min: 5, max: 40, message: 'يجب ان يكون طول البريد الإلكتروني من 5 الي 40 الحرف', trigger: 'blur' },
                    { required: true, pattern: /\S+@\S+\.\S+/, message: 'الرجاء إدخال البريد بطريقة الصحيحه', trigger: 'blur' } 
                ],

                MembersCount: [
                    { required: true, message: "الرجاء إدخال العدد", trigger: 'blur' },
                    { required: true, pattern: /^[0-9]*$/, message: 'الرجاء إدخال ارقام فقط', trigger: 'blur' }
                ],

                MembersMCount: [
                    { required: true, message: "الرجاء إدخال العدد", trigger: 'blur' },
                    { required: true, pattern: /^[0-9]*$/, message: 'الرجاء إدخال ارقام فقط', trigger: 'blur' }
                ],

                MembersFCount: [
                    { required: true, message: "الرجاء إدخال العدد", trigger: 'blur' },
                    { required: true, pattern: /^[0-9]*$/, message: 'الرجاء إدخال ارقام فقط', trigger: 'blur' }
                ],

                IsGeneral: [
                    { required: true, message: "الرجاء إختيار النوع", trigger: 'blur' },
                ],

                ResoneSpecialization: [
                    { required: true, message: "الرجاء إدخال الدافع لاختيار هذا التخصص", trigger: 'blur' },
                ],

                HaveDatabase: [
                    { required: true, message: "الرجاء الإجابة عن السؤال", trigger: 'blur' },
                ],

                SharedProjects: [
                    { required: true, message: "الرجاء الإجابة عن السؤال", trigger: 'blur' },
                ],

                FundsProjects: [
                    { required: true, message: "الرجاء الإجابة عن السؤال", trigger: 'blur' },
                ],


            },

        };
    },



    methods: {

        GetCenter() {
            this.$blockUI.Start();
            this.$http.GetCenter()
                .then(response => {
                    this.$blockUI.Stop();
                    this.Centers = response.data.centers;
                })
                .catch((err) => {
                    this.$blockUI.Stop();
                    console.error(err);
                });
        },

        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {

                    this.addOrganization(formName);
                } else {
                    this.$message({
                        type: 'error',
                        message: "الرجاء التأكد من إدخال جميع البيانات المطلوبة"
                    });
                    console.log('error submit!!');
                    return false;
                }
            });


        },

        resetForm(formName) {
            this.$refs[formName].resetFields();
        },

        validPhone: function (Phone) {

            var mobileRegex = /^09[1|2|3|4|5][0-9]{7}$/i;

            return mobileRegex.test(Phone);
        },


        addOrganization(formName) {
            this.$blockUI.Start();
            this.$http.addOrganization(this.form)
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

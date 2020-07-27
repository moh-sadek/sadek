export default {
    name: 'Info',    
    created() {


        //this.form.Id = this.$parent.SelectedId;
        //this.CForm.Id = this.$parent.SelectedId;
        //this.getCaseInfo();

        //this.getuserType();

        this.getInfo(this.$parent.SelectedId);

        


    },
    data() {
       
        return {

            ruleForm: {
                UserType: '',
            },

            Info:[],
            
            form: {

                id:'',
                name: '',
                publicityNo: '',
                startDate: '',
                centerId: '',
                center: '',

                managingDirectorName: '',
                managingDirectorPhone: '',
                managingDirectorExPhone: '',


                presidentName: '',
                presidentEmail: '',
                membersCount: '',
                membersMCount: '',
                membersFCount: '',

                dAllLibya: '',
                dSouth: '',
                dWest: '',
                dEast: '',

                advantages: '',


                isGeneral: '',
                ages: '',
                quality: '',
                ethnic: '',
                geography: '',
                social: '',
                charitable: '',
                humanRights: '',
                electoralAffairs: '',
                otherS: '',
                otherContentS: '',


                resoneSpecialization: '',
                vision: '',
                haveDatabase: '',

                databaseGoal: '',
                databaseBenefits: '',

                databaseSupport: '',
                databaseSupportType: '',


                cooperat: '',
                cooperatField: '',
                cooperatFieldOther: '',


                isWatched: '',
                participatedElections: '',
                electoralCommission: '',
                observers: '',


                issues: '',
                traningPlace: '',
                traningIssues: '',


                isPrograms: '',
                postersDistribution: '',
                advertising: '',
                workshops: '',
                other: '',
                otherContent: '',


                cooperatDescription: '',
                isCoalitionBefor: '',
                coalitionDescription: '',


                isCompletedProject: '',
                isCompletedProjectDesc: '',


                sharedProjects: '',
                sharedProjectsName: '',
                experiencesLessons: '',


                fundsProjects: '',
                governmentalSources: '',
                nationalBodies: '',
                internationalBodies: '',
                anotherSource: '',

                byRequest: '',
                directOffer: '',
                publicTender: '',
                directSponsorship: '',


                otherFinancing: '',


                administrationCapacity: '',
                communicationCapacity: '',
                networksCapacity: '',
                consultingCapacity: '',
                computersCapacity: '',
                campaignManagementCapacity: '',

                capacityOtherAreas: '',

                monitor: '',
                legalFramework: '',
                media: '',
                education: '',
                addressing: '',
                monitorCampaign: '',
                otherCapabilities: '',


            },

            rules:
            {

                Name: [
                    { required: true, message: 'الرجاءإدخال اسم المنظمة ', trigger: 'blur' },
                    { min: 2, max: 50, message: 'يجب ان يكون طول العنوان من 2 الي 50 الحرف', trigger: 'blur' },
                    { required: true, pattern: /[\u0600-\u06FF]/, message: 'الرجاء إدخال حروف العربية فقط', trigger: 'blur' },
                ],

                PublicityNo: [
                    { required: true, message: "الرجاء إدخال رقم إشهار المنظمة", trigger: 'blur' },
                    { min: 2, max: 20, message: "يجب ان يكون طول رقم القيد من 2 الي 20 الحرف", trigger: 'blur', },
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

        getInfo(id) {
            this.$blockUI.Start();
            this.$http.getInfo(id)
                .then(response => {

                    this.form = response.data.info;
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


        back() {
            this.$parent.state = 0;
        },




    }    
}

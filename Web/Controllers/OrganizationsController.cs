using Common;
using Managegment.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/Organizations")]
    public class OrganizationsController : Controller
    {

        private readonly CSORContext db;
        private Helper help;
        public OrganizationsController(CSORContext context)
        {
            this.db = context;
            help = new Helper();
        }

        [HttpGet("GetCenters")]
        public IActionResult GetCenters()
        {
            try
            {
                return Ok(new { centers = db.Centers.Where(x => x.Status != 9).ToList() });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddOrganization")]
        public IActionResult AddOrganization([FromBody] OrganizationObject form)
        {
            try
            {
                if (form == null)
                {
                    return BadRequest("خطأ في إرسال البيانات الرجاء إعادة المحاولة");
                }

                if (string.IsNullOrEmpty(form.Name))
                {
                    return BadRequest("الرجاء إدخال إسم المنظمة ");
                }

                if (form.PublicityNo < 0)
                {
                    return BadRequest("الرجاء إدخال رقم إشهار المنظمة");
                }

                if (form.CenterId < 0)
                {
                    return BadRequest("الرجاء إختيار عنوان المنظمة");
                }

                if (string.IsNullOrWhiteSpace(form.ManagingDirectorPhone))
                {
                    return StatusCode(404, "الرجاء إدخال رقم الهاتف");
                }

                if (string.IsNullOrWhiteSpace(form.ManagingDirectorExPhone))
                {
                    return StatusCode(404, "الرجاء إدخال رقم الهاتف الإحتياطي");
                }

                if (form.ManagingDirectorPhone.Length < 9)
                {
                    return StatusCode(404, "الرجاء إدخال الهـاتف بطريقة الصحيحة !!");
                }

                if (form.ManagingDirectorExPhone.Length < 9)
                {
                    return StatusCode(404, "الرجاء إدخال الهـاتف بطريقة الصحيحة !!");
                }

                form.ManagingDirectorPhone = form.ManagingDirectorPhone.Substring(form.ManagingDirectorPhone.Length - 9);

                if (form.ManagingDirectorPhone.Substring(0, 2) != "91" && form.ManagingDirectorPhone.Substring(0, 2) != "92" && form.ManagingDirectorPhone.Substring(0, 2) != "94")
                {
                    return StatusCode(404, "يجب ان يكون الهاتف يبدأ ب (91,92,94) ليبيانا او المدار !!");
                }

                form.ManagingDirectorExPhone = form.ManagingDirectorExPhone.Substring(form.ManagingDirectorExPhone.Length - 9);

                if (form.ManagingDirectorExPhone.Substring(0, 2) != "91" && form.ManagingDirectorExPhone.Substring(0, 2) != "92" && form.ManagingDirectorExPhone.Substring(0, 2) != "94")
                {
                    return StatusCode(404, "يجب ان يكون الهاتف يبدأ ب (91,92,94) ليبيانا او المدار !!");
                }

                if (!Validation.IsValidEmail(form.PresidentEmail))
                {
                    return BadRequest("الرجاء ادخال الايميل بالطريقة الصحيحة");
                }

                var exitstphones = db.Organizations.Where(x => x.ManagingDirectorPhone == form.ManagingDirectorPhone && x.Status != 9).SingleOrDefault();
                if (exitstphones != null)
                    return BadRequest("رقم الهاتف موجود مسبقا");

                var exitstName = db.Organizations.Where(x => x.Name == form.Name && x.Status != 9).SingleOrDefault();
                if (exitstphones != null)
                    return BadRequest("إسم المنظمة موجود مسبقا");

                var exitstNumber = db.Organizations.Where(x => x.PublicityNo == form.PublicityNo && x.Status != 9).SingleOrDefault();
                if (exitstphones != null)
                    return BadRequest("رقم التشهير موجود مسبقا");

                if (form.DAllLibya == false && form.DSouth == false && form.DWest == false && form.DEast == false)
                {
                    return BadRequest("الرجاء إختيار التوزيع الجغرافي");
                }

                if (form.IsGeneral == false)
                {
                    if (form.Ages == false && form.Quality == false && form.Ethnic == false && form.Geography == false
                        && form.Social == false && form.Charitable == false && form.HumanRights == false
                        && form.ElectoralAffairs == false && form.OtherS == false)
                        return BadRequest("الرجاء إختيار التخصص الخاص بكم");

                    if (form.OtherS == true && string.IsNullOrEmpty(form.OtherContentS))
                        return BadRequest("الرجاء كتابة التخصص الخاص بكم");
                }

                if (form.FundsProjects == true)
                {
                    if (form.GovernmentalSources == false && form.NationalBodies == false && form.InternationalBodies == false
                        && form.AnotherSource == false)
                        return BadRequest("الرجاء تحديد مصادر تمويلكم ");
                }

                if (form.IsPrograms == true)
                {
                    if (form.PostersDistribution == false && form.Advertising == false && form.Workshops == false
                        && form.Other == false)
                        return BadRequest("الرجاء تحديد نوع البرامج والأنشطة التي قمتم بها ");

                    if (form.Other == true && string.IsNullOrEmpty(form.OtherContent))
                        return BadRequest("الرجاء كتابة نوع الأنشطة الأخري");
                }

                if (form.AdministrationCapacity == false && form.CommunicationCapacity == false && form.NetworksCapacity == false
                        && form.ConsultingCapacity == false && form.ComputersCapacity == false
                        && form.CampaignManagementCapacity == false)
                    return BadRequest("الرجاء إختيار مجالات عامة تحتاجون لبناء قدرات فيها ");

                if (form.Monitor == false && form.LegalFramework == false && form.Media == false
                        && form.Education == false && form.Addressing == false
                        && form.MonitorCampaign == false)
                    return BadRequest("الرجاء إختيار قدرات تحتاجون إلى بنائها في المجال الانتخابي ");




                Organizations organization = new Organizations();
                organization.Name = form.Name;
                organization.PublicityNo = form.PublicityNo;
                organization.StartDate = form.StartDate;
                organization.CenterId = form.CenterId;
                organization.ManagingDirectorName = form.ManagingDirectorName;
                organization.ManagingDirectorPhone = form.ManagingDirectorPhone;
                organization.ManagingDirectorExPhone = form.ManagingDirectorExPhone;
                organization.PresidentName = form.PresidentName;
                organization.PresidentEmail = form.PresidentEmail;
                organization.MembersCount = form.MembersCount;
                organization.MembersMcount = form.MembersMCount;
                organization.MembersFcount = form.MembersFCount;
                organization.DallLibya = form.DAllLibya;
                organization.Dsouth = form.DSouth;
                organization.Dwest = form.DWest;
                organization.Deast = form.DEast;
                organization.Advantages = form.Advantages;
                organization.IsGeneral = form.IsGeneral;
                organization.Ages = form.Ages;
                organization.Quality = form.Quality;
                organization.Ethnic = form.Ethnic;
                organization.Geography = form.Geography;
                organization.Social = form.Social;
                organization.Charitable = form.Charitable;
                organization.HumanRights = form.HumanRights;
                organization.ElectoralAffairs = form.ElectoralAffairs;
                organization.OtherS = form.OtherS;
                organization.OtherContentS = form.OtherContentS;
                organization.ResoneSpecialization = form.ResoneSpecialization;
                organization.Vision = form.Vision;
                organization.HaveDatabase = form.HaveDatabase;
                organization.DatabaseGoal = form.DatabaseGoal;
                organization.DatabaseBenefits = form.DatabaseBenefits;
                organization.DatabaseSupport = form.DatabaseSupport;
                organization.DatabaseSupportType = form.DatabaseSupportType;
                organization.Cooperat = form.Cooperat;
                organization.CooperatField = form.CooperatField;
                organization.CooperatFieldOther = form.CooperatFieldOther;
                organization.IsWatched = form.IsWatched;
                organization.ParticipatedElections = form.ParticipatedElections;
                organization.ElectoralCommission = form.ElectoralCommission;
                organization.Observers = form.Observers;
                organization.Issues = form.Issues;
                organization.TraningPlace = form.TraningPlace;
                organization.TraningIssues = form.TraningIssues;
                organization.IsPrograms = form.IsPrograms;
                organization.PostersDistribution = form.PostersDistribution;
                organization.Advertising = form.Advertising;
                organization.Workshops = form.Workshops;
                organization.Other = form.Other;
                organization.OtherContent = form.OtherContent;
                organization.CooperatDescription = form.CooperatDescription;
                organization.IsCoalitionBefor = form.IsCoalitionBefor;
                organization.CoalitionDescription = form.CoalitionDescription;
                organization.IsCompletedProject = form.IsCompletedProject;
                organization.IsCompletedProjectDesc = form.IsCompletedProjectDesc;
                organization.SharedProjects = form.SharedProjects;
                organization.SharedProjectsName = form.SharedProjectsName;
                organization.ExperiencesLessons = form.ExperiencesLessons;
                organization.FundsProjects = form.FundsProjects;
                organization.GovernmentalSources = form.GovernmentalSources;
                organization.NationalBodies = form.NationalBodies;
                organization.InternationalBodies = form.InternationalBodies;
                organization.AnotherSource = form.AnotherSource;
                organization.ByRequest = form.ByRequest;
                organization.DirectOffer = form.DirectOffer;
                organization.PublicTender = form.PublicTender;
                organization.DirectSponsorship = form.DirectSponsorship;
                organization.OtherFinancing = form.OtherFinancing;
                organization.AdministrationCapacity = form.AdministrationCapacity;
                organization.CommunicationCapacity = form.CommunicationCapacity;
                organization.NetworksCapacity = form.NetworksCapacity;
                organization.ConsultingCapacity = form.ConsultingCapacity;
                organization.ComputersCapacity = form.ComputersCapacity;
                organization.CampaignManagementCapacity = form.CampaignManagementCapacity;
                organization.CapacityOtherAreas = form.CapacityOtherAreas;
                organization.Monitor = form.Monitor;
                organization.LegalFramework = form.LegalFramework;
                organization.Media = form.Media;
                organization.Education = form.Education;
                organization.Addressing = form.Addressing;
                organization.MonitorCampaign = form.MonitorCampaign;
                organization.OtherCapabilities = form.OtherCapabilities;
                organization.CreatedOn = DateTime.Now;
                organization.Level = 0;
                db.Organizations.Add(organization);

                db.SaveChanges();
                return Ok("تمت عملية الإظافة بنجاح سيتم إرسال بيانات الدخول الخاصة بكم علي رقم الهاتف  ");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        //[HttpGet("GetLocations")]
        //public IActionResult GetLocations()
        //{
        //    try
        //    {
        //        var municipalitys = from p in db.Municipalitys
        //                            where p.Status != 9 && p.ParentId==null
        //                            select p;

        //        var municipalitysList = (from p in municipalitys
        //                                 orderby p.Name
        //                                 select new
        //                                 {
        //                                     Id = p.Id,
        //                                     Name = p.Name,
        //                                 }).ToList();

        //        return Ok(new { locations = municipalitysList });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //public class RequestObj
        //{
        //    public long MunicipalitysId { get; set; }
        //    public string FName { get; set; }
        //    public string SName { get; set; }
        //    public string LName { get; set; }
        //    public string Phone { get; set; }
        //    public string Email { get; set; }
        //    public DateTime? BirthDate { get; set; }
        //    public short? Gender { get; set; }
        //    public short HighFever { get; set; }
        //    public double? Temperature { get; set; }
        //    public short SoreThroat { get; set; }
        //    public short Cough { get; set; }
        //    public short? CoughDry { get; set; }
        //    public short Breathing { get; set; }
        //    public short Country { get; set; }
        //    public short Shivering { get; set; }
        //    public short Diarrhea { get; set; }
        //    public short NearInfected { get; set; }
        //    public string OtherSymptoms { get; set; }
        //}

        //[HttpPost("addRequest")]
        //public IActionResult addRequest([FromBody] RequestObj form)
        //{
        //    try
        //    {
        //        if (form == null)
        //        {
        //            return BadRequest("خطأ في إرسال البيانات الرجاء إعادة المحاولة");
        //        }

        //        if (string.IsNullOrEmpty(form.FName) )
        //        {
        //            return BadRequest("الرجاء إدخال الاسم الأول");
        //        }

        //        if (string.IsNullOrEmpty(form.SName))
        //        {
        //            return BadRequest("الرجاء إدخال إسم الأب");
        //        }

        //        if (string.IsNullOrEmpty(form.LName))
        //        {
        //            return BadRequest("الرجاء إدخال اللقب");
        //        }

        //        if (!Validation.IsValidEmail(form.Email))
        //        {
        //            return BadRequest("الرجاء ادخال الايميل بالطريقة الصحيحة");
        //        }

        //        if (string.IsNullOrWhiteSpace(form.BirthDate.ToString()))
        //        {
        //            return BadRequest("الرجاء دخال تاريخ الميلاد ");
        //        }

        //        if (form.Gender != 1 && form.Gender != 2)
        //        {
        //            return BadRequest("الرجاء ادخال الجنس (ذكر - انثي)");

        //        }



        //        var PhoneExist = (from p in db.DetectionModels
        //                          where p.Phone == form.Phone
        //                          select p).SingleOrDefault();
        //        if (PhoneExist != null)
        //        {
        //            return BadRequest(" رقم الهاتف موجود مسبقا");

        //        }

        //        int ev = 0;

        //        if(form.HighFever==1)
        //        {
        //            ev += 1;
        //        }

        //        if (form.SoreThroat == 1)
        //        {
        //            ev += 1;
        //        }

        //        if (form.Cough == 1)
        //        {
        //            ev += 1;
        //        }

        //        if (form.Breathing == 1)
        //        {
        //            ev += 2;
        //        }

        //        if (form.Country == 1)
        //        {
        //            ev += 3;
        //        }

        //        if (form.Shivering == 1)
        //        {
        //            ev += 1;
        //        }

        //        if (form.Diarrhea == 1)
        //        {
        //            ev += 1;
        //        }

        //        if (form.NearInfected == 1)
        //        {
        //            ev += 3;
        //        }

        //        DetectionModels DetectionModels = new DetectionModels();


        //        DetectionModels.MunicipalitysId = form.MunicipalitysId;
        //        DetectionModels.Name = form.FName + " " + form.SName + " " + form.LName;
        //        DetectionModels.Phone = form.Phone;
        //        DetectionModels.Email = form.Email;
        //        DetectionModels.BirthDate = form.BirthDate;
        //        DetectionModels.CreatedOn = DateTime.Now;
        //        DetectionModels.Gender = (short)form.Gender;
        //        DetectionModels.HighFever = (short)form.HighFever;
        //        DetectionModels.Temperature = form.Temperature;
        //        DetectionModels.SoreThroat = (short)form.SoreThroat;
        //        DetectionModels.Cough = (short)form.Cough;
        //        DetectionModels.CoughDry = (short)form.CoughDry;
        //        DetectionModels.Breathing = (short)form.Breathing;
        //        DetectionModels.Country = (short)form.Country;
        //        DetectionModels.Shivering = (short)form.Shivering;
        //        DetectionModels.Diarrhea = (short)form.Diarrhea;
        //        DetectionModels.NearInfected = (short)form.NearInfected;
        //        DetectionModels.OtherSymptoms = form.OtherSymptoms;

        //        if(ev>=0 && ev<=2)
        //        {
        //            DetectionModels.Evaluation = 3;
        //        }
        //        else if (ev >= 3 && ev <= 5)
        //        {
        //            DetectionModels.Evaluation = 2;
        //        }
        //        else if (ev >= 6 && ev <= 12)
        //        {
        //            DetectionModels.Evaluation = 1;
        //        }
        //        else if (ev >= 12 && ev <= 24)
        //        {
        //            DetectionModels.Evaluation = 0;
        //        }
        //        else
        //        {
        //            DetectionModels.Evaluation = 6;
        //        }

        //        DetectionModels.CreatedOn = DateTime.Now;
        //        DetectionModels.State = 0;
        //        db.DetectionModels.Add(DetectionModels);

        //        db.SaveChanges();
        //        return Ok("تم إرسال البيانات سيتم الرد في اقرب وقت من قبل الإخصائين شكرا لحسن تعاونكم ");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("conectUs")]
        //public IActionResult conectUs([FromBody] RequestObj form)
        //{
        //    try
        //    {
        //        if (form == null)
        //        {
        //            return BadRequest("خطأ في إرسال البيانات الرجاء إعادة المحاولة");
        //        }

        //        if (string.IsNullOrEmpty(form.FName))
        //        {
        //            return BadRequest("الرجاء إدخال الاسم الأول");
        //        }

        //        if (string.IsNullOrEmpty(form.SName))
        //        {
        //            return BadRequest("الرجاء إدخال اللقب");
        //        }

        //        if (!Validation.IsValidEmail(form.Email))
        //        {
        //            return BadRequest("الرجاء ادخال الايميل بالطريقة الصحيحة");
        //        }


        //        ContactUs contactUs = new ContactUs();

        //        contactUs.Name = form.FName + " " + form.SName ;
        //        contactUs.Email = form.Email;
        //        contactUs.Note = form.OtherSymptoms;
        //        contactUs.CreatedOn = DateTime.Now;
        //        contactUs.State = 0;
        //        db.ContactUs.Add(contactUs);

        //        db.SaveChanges();
        //        return Ok("تم إرسال الملاحظة بنجاح ");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //public partial class UsersObject
        //{
        //    public long MunicipalitysId { get; set; }
        //    public string LoginName { get; set; }
        //    public string FullName { get; set; }
        //    public string Password { get; set; }
        //    public string Email { get; set; }
        //    public string Phone { get; set; }
        //    public DateTime DateOfBirth { get; set; }
        //    public short Gender { get; set; }

        //}

        //[HttpPost("AddUser")]
        //public IActionResult AddUser([FromBody] UsersObject user)
        //{
        //    try
        //    {
        //        if (user == null)
        //        {
        //            return BadRequest("خطأ في إرسال البيانات");
        //        }

        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (string.IsNullOrWhiteSpace(user.LoginName))
        //        {
        //            return BadRequest("الرجاء ادحال الإسم  بطريقة صحيحة");
        //        }

        //        if (string.IsNullOrWhiteSpace(user.FullName))
        //        {
        //            return BadRequest("الرجاء إدخال الاسم الرباعي");
        //        }

        //        if (!Validation.IsValidEmail(user.Email))
        //        {
        //            return BadRequest("الرجاء ادخال الايميل بالطريقة الصحيحة");
        //        }

        //        if (user.Gender != 1 && user.Gender != 2)
        //        {
        //            return BadRequest("الرجاء ادخال الجنس (ذكر - انثي)");

        //        }
        //        if (string.IsNullOrWhiteSpace(user.DateOfBirth.ToString()))
        //        {
        //            return BadRequest("الرجاء دخال تاريخ الميلاد الطبيب");
        //        }
        //        if ((DateTime.Now.Year - user.DateOfBirth.Year) < 18)
        //        {
        //            return BadRequest("يجب ان يكون العمر  اكبر من 18");
        //        }

        //        var cLoginName = (from u in db.Users
        //                          where u.LoginName == user.LoginName
        //                          select u).SingleOrDefault();
        //        if (cLoginName != null)
        //        {
        //            return BadRequest(" اسم الدخول موجود مسبقا");


        //        }


        //        var cPhone = (from u in db.Users
        //                      where u.Phone == user.Phone
        //                      select u).SingleOrDefault();
        //        if (cPhone != null)
        //        {
        //            return BadRequest(" رقم الهاتف موجود مسبقا");
        //        }

        //        var cUser = (from u in db.Users
        //                     where u.Email == user.Email && u.State != 9
        //                     select u).SingleOrDefault();

        //        if (cUser != null)
        //        {
        //            if (cUser.State == 0)
        //            {
        //                return BadRequest("هدا الحساب موجود من قبل يحتاج الي تقعيل الحساب فقط");
        //            }
        //            if (cUser.State == 1 || cUser.State == 2)
        //            {
        //                return BadRequest("هدا الحساب موجود من قبل يحتاج الي دخول فقط");
        //            }
        //        }

        //        cUser = new Users();


        //        cUser.Phone = user.Phone;
        //        cUser.MunicipalitysId = user.MunicipalitysId;
        //        cUser.LoginName = user.LoginName;
        //        cUser.Name = user.FullName;
        //        cUser.Email = user.Email;
        //        cUser.BirthDate = user.DateOfBirth;
        //        cUser.CreatedBy = userId;
        //        cUser.CreatedOn = DateTime.Now;
        //        cUser.Gender = (short)user.Gender;
        //        cUser.LoginTryAttempts = 0;
        //        //cUser.UserType = user.UserType;
        //        cUser.Password = Security.ComputeHash(user.Password, HashAlgorithms.SHA512, null);
        //        cUser.Image = Convert.
        //                FromBase64String("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////////////////////////////A+gAAAAA/////////////////////////////wPoAAAAAP////////////////////////////8D6AAAAAD/////////////////////////////A+gAADhCSU0EAAAAAAAAAgAAOEJJTQQCAAAAAAACAAA4QklNBDAAAAAAAAEBADhCSU0ELQAAAAAABgABAAAAAjhCSU0ECAAAAAAAEAAAAAEAAAJAAAACQAAAAAA4QklNBB4AAAAAAAQAAAAAOEJJTQQaAAAAAANJAAAABgAAAAAAAAAAAAAAjAAAAIwAAAAKAFUAbgB0AGkAdABsAGUAZAAtADEAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAIwAAACMAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAEAAAAAAABudWxsAAAAAgAAAAZib3VuZHNPYmpjAAAAAQAAAAAAAFJjdDEAAAAEAAAAAFRvcCBsb25nAAAAAAAAAABMZWZ0bG9uZwAAAAAAAAAAQnRvbWxvbmcAAACMAAAAAFJnaHRsb25nAAAAjAAAAAZzbGljZXNWbExzAAAAAU9iamMAAAABAAAAAAAFc2xpY2UAAAASAAAAB3NsaWNlSURsb25nAAAAAAAAAAdncm91cElEbG9uZwAAAAAAAAAGb3JpZ2luZW51bQAAAAxFU2xpY2VPcmlnaW4AAAANYXV0b0dlbmVyYXRlZAAAAABUeXBlZW51bQAAAApFU2xpY2VUeXBlAAAAAEltZyAAAAAGYm91bmRzT2JqYwAAAAEAAAAAAABSY3QxAAAABAAAAABUb3AgbG9uZwAAAAAAAAAATGVmdGxvbmcAAAAAAAAAAEJ0b21sb25nAAAAjAAAAABSZ2h0bG9uZwAAAIwAAAADdXJsVEVYVAAAAAEAAAAAAABudWxsVEVYVAAAAAEAAAAAAABNc2dlVEVYVAAAAAEAAAAAAAZhbHRUYWdURVhUAAAAAQAAAAAADmNlbGxUZXh0SXNIVE1MYm9vbAEAAAAIY2VsbFRleHRURVhUAAAAAQAAAAAACWhvcnpBbGlnbmVudW0AAAAPRVNsaWNlSG9yekFsaWduAAAAB2RlZmF1bHQAAAAJdmVydEFsaWduZW51bQAA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IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTExIDc5LjE1ODMyNSwgMjAxNS8wOS8xMC0wMToxMDoyMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIiB4bWxuczpwaG90b3Nob3A9Imh0dHA6Ly9ucy5hZG9iZS5jb20vcGhvdG9zaG9wLzEuMC8iIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUgKFdpbmRvd3MpIiB4bXA6Q3JlYXRlRGF0ZT0iMjAxNy0xMi0wMVQxOTozNDoxNyswMjowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAxNy0xMi0wMVQxOTozNDoxNyswMjowMCIgeG1wOk1vZGlmeURhdGU9IjIwMTctMTItMDFUMTk6MzQ6MTcrMDI6MDAiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MmE0MzNlNTUtNzk5ZC00NTRlLWI1ZTUtYWIwNjFmOTUwNThhIiB4bXBNTTpEb2N1bWVudElEPSJhZG9iZTpkb2NpZDpwaG90b3Nob3A6ZDM3ZThiZTYtZDZiZC0xMWU3LWIxNWEtOTViY2JlMzViMTFhIiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6ZjMzZTk0OWItYWNkYi04MjQxLWIxNTctNDgwNDEyMDdkMzNmIiBkYzpmb3JtYXQ9ImltYWdlL2pwZWciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSI+IDx4bXBNTTpIaXN0b3J5PiA8cmRmOlNlcT4gPHJkZjpsaSBzdEV2dDphY3Rpb249ImNyZWF0ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6ZjMzZTk0OWItYWNkYi04MjQxLWIxNTctNDgwNDEyMDdkMzNmIiBzdEV2dDp3aGVuPSIyMDE3LTEyLTAxVDE5OjM0OjE3KzAyOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxNSAoV2luZG93cykiLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249InNhdmVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjJhNDMzZTU1LTc5OWQtNDU0ZS1iNWU1LWFiMDYxZjk1MDU4YSIgc3RFdnQ6d2hlbj0iMjAxNy0xMi0wMVQxOTozNDoxNyswMjowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDwvcmRmOlNlcT4gPC94bXBNTTpIaXN0b3J5PiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8P3hwYWNrZXQgZW5kPSJ3Ij8+/+IMWElDQ19QUk9GSUxFAAEBAAAMSExpbm8CEAAAbW50clJHQiBYWVogB84AAgAJAAYAMQAAYWNzcE1TRlQAAAAASUVDIHNSR0IAAAAAAAAAAAAAAAEAAPbWAAEAAAAA0y1IUCAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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");
        //        cUser.CreatedOn = DateTime.Now;

        //        cUser.State = 6;
        //        db.Users.Add(cUser);

        //        db.SaveChanges();
        //        return Ok("تمت عملية إضافة الطبيب بنجاح سيتم النواصل معكم عند تفعيل الحساب");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}
    }
}

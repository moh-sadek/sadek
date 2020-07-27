using Managegment.Controllers;
using Management.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/Orginizations")]
    public class OrginizationsController : Controller
    {

        private readonly CSORContext db;
        private Helper help;
        public OrginizationsController(CSORContext context)
        {
            this.db = context;
            help = new Helper();
        }

        [HttpGet("getRequest")]
        public IActionResult getRequest(int pageNo, int pageSize, int centerId)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");

                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();

                if (user == null)
                    return StatusCode(401, "لم يتم العتور علي المستخدم الرجاء التأكد من البيانات");

                if (user.UserType == 1)
                {
                    if (centerId > 0)
                    {
                        var request = db.Organizations.Where(x => x.Status != 9 && x.Level == 0 && x.CenterId == centerId).Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.PublicityNo,
                            CenterName = x.Center.Name,
                            x.ManagingDirectorName,
                            x.ManagingDirectorPhone,
                            x.PresidentName,
                        }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                        var requestCount = request.Count();

                        return Ok(new { requests = request, count = requestCount });
                    }
                    else
                    {
                        var request = db.Organizations.Where(x => x.Status != 9 && x.Level == 0).Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.PublicityNo,
                            CenterName = x.Center.Name,
                            x.ManagingDirectorName,
                            x.ManagingDirectorPhone,
                            x.PresidentName,
                        }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                        var requestCount = request.Count();

                        return Ok(new { requests = request, count = requestCount });
                    }


                }
                else if (user.UserType == 2)
                {
                    if (user.CenterId <= 0)
                        return StatusCode(401, "لم يتم تسجيلك في أي مركز إنتخابي الرجاء مراجعة الإدارة");

                    var request = db.Organizations.Where(x => x.Status != 9 && x.Level == 0 && x.CenterId == user.CenterId).Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.PublicityNo,
                        CenterName = x.Center.Name,
                        x.ManagingDirectorName,
                        x.ManagingDirectorPhone,
                        x.PresidentName,
                    }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                    var requestCount = request.Count();

                    return Ok(new { requests = request, count = requestCount });
                }

                return StatusCode(401, "لم يتم تحديد الصفة الوظيفية الخاصة بك الرجاء مراجعة الإدارة");

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}/getInfo")]
        public IActionResult getInfo(long id)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");


                var Inofs = db.Organizations.Where(x => x.Status != 9 && x.Id == id).Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.PublicityNo,
                    x.StartDate,
                    x.CenterId,
                    Center = x.Center.Name,
                    x.ManagingDirectorName,
                    x.ManagingDirectorPhone,
                    x.ManagingDirectorExPhone,
                    x.PresidentName,
                    x.PresidentEmail,
                    x.MembersCount,
                    x.MembersMcount,
                    x.MembersFcount,
                    x.DallLibya,
                    x.Dsouth,
                    x.Dwest,
                    x.Deast,
                    x.Advantages,
                    x.IsGeneral,
                    x.Ages,
                    x.Quality,
                    x.Ethnic,
                    x.Geography,
                    x.Social,
                    x.Charitable,
                    x.HumanRights,
                    x.ElectoralAffairs,
                    x.OtherS,
                    x.OtherContentS,
                    x.ResoneSpecialization,
                    x.Vision,
                    x.HaveDatabase,
                    x.DatabaseGoal,
                    x.DatabaseBenefits,
                    x.DatabaseSupport,
                    x.DatabaseSupportType,
                    x.Cooperat,
                    x.CooperatField,
                    x.CooperatFieldOther,
                    x.IsWatched,
                    x.ParticipatedElections,
                    x.ElectoralCommission,
                    x.Observers,
                    x.Issues,
                    x.TraningPlace,
                    x.TraningIssues,
                    x.IsPrograms,
                    x.PostersDistribution,
                    x.Advertising,
                    x.Workshops,
                    x.Other,
                    x.OtherContent,
                    x.CooperatDescription,
                    x.IsCoalitionBefor,
                    x.CoalitionDescription,
                    x.IsCompletedProject,
                    x.IsCompletedProjectDesc,
                    x.SharedProjects,
                    x.SharedProjectsName,
                    x.ExperiencesLessons,
                    x.FundsProjects,
                    x.GovernmentalSources,
                    x.NationalBodies,
                    x.InternationalBodies,
                    x.AnotherSource,
                    x.ByRequest,
                    x.DirectOffer,
                    x.PublicTender,
                    x.DirectSponsorship,
                    x.OtherFinancing,
                    x.AdministrationCapacity,
                    x.CommunicationCapacity,
                    x.NetworksCapacity,
                    x.ConsultingCapacity,
                    x.ComputersCapacity,
                    x.CampaignManagementCapacity,
                    x.CapacityOtherAreas,
                    x.Monitor,
                    x.LegalFramework,
                    x.Media,
                    x.Education,
                    x.Addressing,
                    x.MonitorCampaign,
                    x.OtherCapabilities,
                    x.CreatedOn,
                    ConfirmedBy = x.ConfirmedBy == null ? null : db.Users.Where(y => y.Id == x.ConfirmedBy).SingleOrDefault().Name,
                }).SingleOrDefault();

                if (Inofs == null)
                    return StatusCode(401, "لم يتم العتور علي المنظمة");


                return Ok(new { Info = Inofs });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{id}/addOrignization")]
        public IActionResult addOrignization(long id)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                var request = db.Organizations.Where(x => x.Id == id).SingleOrDefault();

                if (request == null)
                {
                    return NotFound("خــطأ : لم يتم العتور علي المنظمة");
                }

                request.Level = 1;
                request.ConfirmedBy = userId;
                db.SaveChanges();
                return Ok("تمت عملية إضافة المنظمة بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getOrginizations")]
        public IActionResult getOrginizations(int pageNo, int pageSize, int centerId)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");

                var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();

                if (user == null)
                    return StatusCode(401, "لم يتم العتور علي المستخدم الرجاء التأكد من البيانات");


                if (user.UserType == 1)
                {
                    if (centerId > 0)
                    {
                        var request = db.Organizations.Where(x => x.Status != 9 && x.Level == 1 && x.CenterId == centerId).Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.PublicityNo,
                            CenterName = x.Center.Name,
                            x.ManagingDirectorName,
                            x.ManagingDirectorPhone,
                            x.PresidentName,
                        }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                        var requestCount = request.Count();

                        return Ok(new { requests = request, count = requestCount });
                    }
                    else
                    {
                        var request = db.Organizations.Where(x => x.Status != 9 && x.Level == 1).Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.PublicityNo,
                            CenterName = x.Center.Name,
                            x.ManagingDirectorName,
                            x.ManagingDirectorPhone,
                            x.PresidentName,
                        }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                        var requestCount = request.Count();

                        return Ok(new { requests = request, count = requestCount });
                    }


                }
                else if (user.UserType == 2)
                {
                    if (user.CenterId <= 0)
                        return StatusCode(401, "لم يتم تسجيلك في أي مركز إنتخابي الرجاء مراجعة الإدارة");

                    var request = db.Organizations.Where(x => x.Status != 9 && x.Level == 1 && x.CenterId == user.CenterId).Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.PublicityNo,
                        CenterName = x.Center.Name,
                        x.ManagingDirectorName,
                        x.ManagingDirectorPhone,
                        x.PresidentName,
                    }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                    var requestCount = request.Count();

                    return Ok(new { requests = request, count = requestCount });
                }

                return StatusCode(401, "لم يتم تحديد الصفة الوظيفية الخاصة بك الرجاء مراجعة الإدارة");

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{id}/delteOrginzation")]
        public IActionResult delteOrginzation(long id)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                var Organizations = db.Organizations.Where(x => x.Id == id).SingleOrDefault();

                if (Organizations == null)
                {
                    return NotFound("خــطأ : لم يتم العتور علي المنظمة");
                }

                Organizations.Status = 9;
                db.SaveChanges();
                return Ok("تمت عملية حذف المنظمة بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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







        //[HttpGet("getUser")]
        //public IActionResult getUser(int pageNo, int pageSize)//type of id 
        //{
        //    try
        //    {
        //        IQueryable<Users> Users = from p in db.Users where p.State != 9 && (p.UserType == 3 || p.UserType == 4 || p.UserType == 5) select p;

        //        var UsersCount = (from p in Users
        //                          select p).Count();


        //        var UserInfo = (from p in Users
        //                        orderby p.CreatedOn descending
        //                        select new
        //                        {
        //                            UserId = p.Id,
        //                            Name = p.Name,
        //                            LoginName = p.LoginName,
        //                            State = p.State,
        //                            Email = p.Email,
        //                            Password = p.Password,
        //                            CreatedOn = p.CreatedOn,
        //                            Phone = p.Phone,
        //                            gender = p.Gender,
        //                            BirthDate = p.BirthDate,
        //                            CreatedBy = p.CreatedBy,
        //                            Image = p.Image,
        //                            UserType = p.UserType
        //                        }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

        //        return Ok(new { users = UserInfo, count = UsersCount });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("getRequest")]
        //public IActionResult getRequest(int pageNo, int pageSize)
        //{
        //    try
        //    {
        //        IQueryable<Users> Users = from p in db.Users where p.State == 6 select p;

        //        var UsersCount = (from p in Users
        //                          select p).Count();


        //        var UserInfo = (from p in Users
        //                        orderby p.CreatedOn descending
        //                        select new
        //                        {
        //                            UserId = p.Id,
        //                            Name = p.Name,
        //                            LoginName = p.LoginName,
        //                            State = p.State,
        //                            Email = p.Email,
        //                            Password = p.Password,
        //                            CreatedOn = p.CreatedOn,
        //                            Phone = p.Phone,
        //                            gender = p.Gender,
        //                            BirthDate = p.BirthDate,
        //                            CreatedBy = p.CreatedBy,
        //                            Image = p.Image,
        //                            UserType = p.UserType
        //                        }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

        //        return Ok(new { users = UserInfo, count = UsersCount });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("{UserId}/ConfirmRequest")]
        //public IActionResult ConfirmRequest(long UserId)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        var User = (from p in db.Users
        //                    where p.Id == UserId && p.State != 9
        //                    select p).SingleOrDefault();

        //        if (User == null)
        //        {
        //            return NotFound("خــطأ : الطلب غير موجود");
        //        }

        //        User.State = 1;
        //        User.UserType = 5;
        //        db.SaveChanges();
        //        return Ok("تم العمليه بنجاح");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("getAvaliblePatients")]
        //public IActionResult getAvaliblePatients(int pageNo, int pageSize)
        //{
        //    try
        //    {
        //        IQueryable<DetectionModels> RequestInfo = from p in db.DetectionModels where p.State == 0 select p;

        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        var userTupe = (from p in db.Users where p.Id == userId select p.UserType).SingleOrDefault();

        //        if (userTupe == 3)
        //        {
        //            RequestInfo = from p in db.DetectionModels where p.State == 0 && p.UserId == null select p;
        //        }

        //        if (userTupe == 4)
        //        {
        //            RequestInfo = from p in db.DetectionModels where p.State == 0 && p.DoctorB == null && p.DoctorC != null select p;
        //        }

        //        if (userTupe == 5)
        //        {
        //            RequestInfo = from p in db.DetectionModels where p.State == 0 && p.DoctorC == null select p;
        //        }

        //        var Count = (from p in RequestInfo
        //                     select p).Count();


        //        var request = (from p in RequestInfo
        //                       orderby p.CreatedOn descending
        //                       select new
        //                       {
        //                           Id = p.Id,
        //                           Name = p.Name,
        //                           Phone = p.Phone,
        //                           birthDate = p.BirthDate,
        //                           gender = p.Gender,
        //                           createdOn = p.CreatedOn,
        //                           evaluation = p.Evaluation,

        //                       }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

        //        return Ok(new { Requests = request, count = Count });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("getuserType")]
        //public IActionResult getuserType(int pageNo, int pageSize)
        //{
        //    try
        //    {
        //        IQueryable<DetectionModels> RequestInfo = from p in db.DetectionModels where p.State == 0 select p;

        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        var userType = (from p in db.Users where p.Id == userId select p.UserType).SingleOrDefault();

        //        return Ok(new { userType = userType });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("getMyPatients")]
        //public IActionResult getMyPatients(int pageNo, int pageSize)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        IQueryable<DetectionModels> RequestInfo = from p in db.DetectionModels where p.State == 0 && (p.DoctorC == userId || p.DoctorB == userId || p.UserId == userId) select p;



        //        var Count = (from p in RequestInfo
        //                     select p).Count();


        //        var request = (from p in RequestInfo
        //                       orderby p.CreatedOn descending
        //                       select new
        //                       {
        //                           Id = p.Id,
        //                           Name = p.Name,
        //                           Phone = p.Phone,
        //                           birthDate = p.BirthDate,
        //                           gender = p.Gender,
        //                           createdOn = p.CreatedOn,
        //                           evaluation = p.Evaluation,

        //                       }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

        //        return Ok(new { Requests = request, count = Count });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("getCaseInfo")]
        //public IActionResult getCaseInfo(long id)
        //{
        //    try
        //    {
        //        var RequestInfo = (from p in db.DetectionModels where p.State != 9 && p.Id == id select p).SingleOrDefault();


        //        return Ok(new { info = RequestInfo });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //public class Answer
        //{
        //    public long Id { get; set; }
        //    public string DoctorNote { get; set; }
        //}

        //[HttpPost("DoctorCAnswer")]
        //public IActionResult DoctorCAnswer([FromBody] Answer ansewr)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        var RequestInfo = (from p in db.DetectionModels where p.Id == ansewr.Id select p).SingleOrDefault();

        //        if (RequestInfo == null)
        //        {
        //            return BadRequest("لم يتن العتور علي الطلب ربما تم مسحه مسبقا");
        //        }

        //        if (RequestInfo.UserId != null)
        //        {
        //            return BadRequest("تم إعتماد الإجابة من قبل طبيب أخر");
        //        }

        //        RequestInfo.DoctorC = userId;
        //        RequestInfo.DoctorCdisc = ansewr.DoctorNote;

        //        db.SaveChanges();
        //        return Ok("تم إعتماد النتيجة شكرا لتعاونكم ");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("DoctorBAnswer")]
        //public IActionResult DoctorBAnswer([FromBody] Answer ansewr)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        var RequestInfo = (from p in db.DetectionModels where p.Id == ansewr.Id select p).SingleOrDefault();

        //        if (RequestInfo == null)
        //        {
        //            return BadRequest("لم يتن العتور علي الطلب ربما تم مسحه مسبقا");
        //        }

        //        if (RequestInfo.DoctorB != null)
        //        {
        //            return BadRequest("تم إعتماد الإجابة من قبل طبيب أخر");
        //        }

        //        RequestInfo.DoctorB = userId;
        //        RequestInfo.DoctorBdisc = ansewr.DoctorNote;

        //        db.SaveChanges();
        //        return Ok("تم إعتماد النتيجة شكرا لتعاونكم ");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("DoctorAAnswer")]
        //public IActionResult DoctorAAnswer([FromBody] Answer ansewr)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        var RequestInfo = (from p in db.DetectionModels where p.Id == ansewr.Id select p).SingleOrDefault();

        //        if (RequestInfo == null)
        //        {
        //            return BadRequest("لم يتن العتور علي الطلب ربما تم مسحه مسبقا");
        //        }

        //        if (RequestInfo.UserId != null)
        //        {
        //            return BadRequest("تم إعتماد الإجابة من قبل طبيب أخر");
        //        }

        //        RequestInfo.UserId = userId;
        //        RequestInfo.DoctorAdisc = ansewr.DoctorNote;

        //        db.SaveChanges();
        //        return Ok("تم إعتماد النتيجة شكرا لتعاونكم ");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("GetDashpordInfo")]
        //public IActionResult GetDashpordInfo()
        //{
        //    try
        //    {
        //        IQueryable<Users> userInfo = from p in db.Users where p.State != 9 select p;

        //        var userCount = (from p in userInfo where p.UserType == 1 || p.UserType == 2 select p).Count();
        //        var doctorCount = (from p in userInfo where p.UserType != 1 && p.UserType != 2 select p).Count();
        //        var massegeCount = (from p in db.ContactUs where p.State != 9 select p).Count();
        //        var formsINfo = (from p in db.DetectionModels where p.State != 9 select p).Count();


        //        var Detalss = new
        //        {
        //            userCount = userCount,
        //            doctorCount = doctorCount,
        //            massegeCount = massegeCount,
        //            formsINfo = formsINfo
        //        };


        //        return Ok(new { Detals = Detalss });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpGet("getMassege")]
        //public IActionResult getMassege(int pageNo, int pageSize)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        IQueryable<ContactUs> RequestInfo = from p in db.ContactUs where p.State != 9 select p;



        //        var Count = (from p in RequestInfo
        //                     select p).Count();


        //        var request = (from p in RequestInfo
        //                       orderby p.CreatedOn descending
        //                       select new
        //                       {
        //                           Id = p.Id,
        //                           Name = p.Name,
        //                           Email = p.Email,
        //                           Note = p.Note,
        //                           CreatedOn = p.CreatedOn,

        //                       }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

        //        return Ok(new { Requests = request, count = Count });
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        //[HttpPost("{UserId}/deleteitem")]
        //public IActionResult deleteitem(long UserId)
        //{
        //    try
        //    {
        //        var userId = this.help.GetCurrentUser(HttpContext);

        //        if (userId <= 0)
        //        {
        //            return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
        //        }

        //        var User = (from p in db.ContactUs
        //                    where p.Id == UserId && p.State != 9
        //                    select p).SingleOrDefault();

        //        if (User == null)
        //        {
        //            return NotFound("خــطأ : الطلب غير موجود");
        //        }

        //        User.State = 9;
        //        db.SaveChanges();
        //        return Ok("تم العمليه بنجاح");
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}




    }
}

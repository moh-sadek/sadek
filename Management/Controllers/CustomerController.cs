using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managegment.Controllers;
using Managegment.objects;
using Management.Models;
using Management.objects;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/Customer")]
    public class CustomersController : Controller
    {
        private readonly VASContext db;
        private Helper help;
        public CustomersController(VASContext context)
        {
            this.db = context;
            help = new Helper();
        }

        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers(int pageNo, int pageSize, int id)//type of id 
        {
            try
            {
                IQueryable<Cutomers> CustmorsInfo = from p in db.Cutomers where p.Status != 9 select p;

                if(id!=0)
                {
                    CustmorsInfo = from p in db.Cutomers where p.CustomerId == id select p;
                }

                var CustmorsCount = (from p in CustmorsInfo
                                     select p).Count();


                var CustmorsInfos = (from p in CustmorsInfo
                                     orderby p.CreatedOn descending
                                     select new
                                     {
                                         CustomerId = p.CustomerId,
                                         FullName = p.FullName,
                                         Phone = p.Phone,
                                         BirthDate = p.BirthDate,
                                         CompanyName = p.CompanyName,
                                         Email = p.Email,
                                         Status = p.Status,
                                         CreatedBy = p.CreatedBy,
                                         CreatedOn = p.CreatedOn,
                                         packgeCount=(from q in db.ShoortNumber where q.CustomerId==p.CustomerId select q).Count(),
                                         countsms= (from q in db.ShoortNumber where q.CustomerId == p.CustomerId select q.Smscount).Sum(),
                                         remindsms= (from q in db.ShoortNumber where q.CustomerId == p.CustomerId select q.UsageSms).Sum(),
                                     }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return Ok(new { custmor = CustmorsInfos , count = CustmorsCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("Add")]
        public IActionResult AddCustomor([FromBody] CustomersObj customer)
        {
            try
            {

                if (customer == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                var NameExist = (from p in db.Cutomers where p.FullName == customer.name  select p).SingleOrDefault();
                if (NameExist != null)
                {
                    return BadRequest("الإسم موجود مسبقا الرجاءإعادة الإدخال");
                }

                var PhoneExist = (from p in db.Cutomers where p.Phone == customer.phone select p).SingleOrDefault();
                if (PhoneExist != null)
                {
                    return BadRequest("رقم الهاتف موجود مسبقا الرجاء التأكد من الإدخال");
                }

                var shortNumbersCode = (from p in db.ShoortNumber where p.Code == customer.code select p).SingleOrDefault();

                if (shortNumbersCode != null)
                {
                    return BadRequest("رقم الخدمة موجود مسبقا الرجاء إعادة الإدخال");
                }

                if (customer.from <= DateTime.Now)
                {
                    return BadRequest("خطأ في الإدخال الرجاء التحقق من التاريخ ");
                }





                var Cutomers = new Cutomers();
                var ShoortNumber = new ShoortNumber();

                Cutomers.FullName = customer.name;
                Cutomers.Phone = customer.phone;
                Cutomers.BirthDate = customer.date;
                Cutomers.CompanyName = customer.companyName;
                Cutomers.Email = customer.email;
                Cutomers.Status = 0;
                Cutomers.CreatedBy = userId;
                Cutomers.CreatedOn = DateTime.Now;
                db.Cutomers.Add(Cutomers);

                ShoortNumber.Code = customer.code;
                ShoortNumber.Amount = customer.amount;
                ShoortNumber.From = customer.from;
                ShoortNumber.To = customer.to;
                ShoortNumber.Smscount = customer.countMassage;
                ShoortNumber.UsageSms = 0;
                ShoortNumber.Service = customer.serviceName;
                ShoortNumber.CreatedBy = userId;
                ShoortNumber.CreatedOn = DateTime.Now;
                ShoortNumber.CustomerId = Cutomers.CustomerId;

                db.ShoortNumber.Add(ShoortNumber);

                db.SaveChanges();

                return Ok("لقد قمت بتسـجيل بيانات العميل بنــجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddOldCustomor")]
        public IActionResult AddOldCustomor([FromBody] CustomersObj customer)
        {
            try
            {

                if (customer == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                var Cutomers = new Cutomers();
                var ShoortNumber = new ShoortNumber();

                Cutomers.FullName = customer.name;
                Cutomers.Phone = customer.phone;
                Cutomers.BirthDate = customer.date;
                Cutomers.CompanyName = customer.companyName;
                Cutomers.Email = customer.email;
                Cutomers.Status = 0;
                Cutomers.CreatedBy = 0;//user dontforget
                Cutomers.CreatedOn = DateTime.Now;
                db.Cutomers.Add(Cutomers);

                ShoortNumber.Code = customer.code;
                ShoortNumber.Amount = customer.amount;
                ShoortNumber.From = customer.from;
                ShoortNumber.To = customer.to;
                ShoortNumber.Smscount = customer.countMassage;
                ShoortNumber.UsageSms = customer.countUseMassage;
                ShoortNumber.Service = customer.serviceName;
                ShoortNumber.CreatedBy = userId;
                ShoortNumber.CreatedOn = DateTime.Now;
                ShoortNumber.CustomerId = Cutomers.CustomerId;

                db.ShoortNumber.Add(ShoortNumber);

                db.SaveChanges();

                return Ok("لقد قمت بتسـجيل بيانات العميل بنــجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddPackage")]
        public IActionResult AddPackage([FromBody] PackegeObj serviceInfo)
        {
            try
            {

                if (serviceInfo == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                var shortNumbersCode = (from p in db.ShoortNumber where p.Code == serviceInfo.code select p).SingleOrDefault();

                if (shortNumbersCode != null)
                {
                    return BadRequest("رقم الخدمة موجود مسبقا الرجاء إعادة الإدخال");
                }

                if (serviceInfo.from>= DateTime.Now || serviceInfo.to >= DateTime.Now)
                {
                    return BadRequest("خطأ في الإدخال تاريخ البداية أصغر من تاريخ النهاية ");
                }


                var ShoortNumber = new ShoortNumber();
                

                ShoortNumber.Code = serviceInfo.code;
                ShoortNumber.Amount = serviceInfo.amount;
                ShoortNumber.From = DateTime.Now;
                ShoortNumber.To = serviceInfo.to;
                ShoortNumber.Smscount = serviceInfo.countMassage;
                ShoortNumber.UsageSms = 0;
                ShoortNumber.Service = serviceInfo.serviceName;
                ShoortNumber.CreatedBy = userId;
                ShoortNumber.CreatedOn = DateTime.Now;
                ShoortNumber.CustomerId = serviceInfo.custmorId;
                ShoortNumber.State = 1;

                db.ShoortNumber.Add(ShoortNumber);

                db.SaveChanges();

                return Ok("لقد قمت بتسـجيل بيانات الخدمة بنــجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddOldPackage")]
        public IActionResult AddOldPackage([FromBody] PackegeObj serviceInfo)
        {
            try
            {

                if (serviceInfo == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                //if (userId <= 0)
                //{
                //    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                //}

                var ShoortNumber = new ShoortNumber();


                ShoortNumber.Code = serviceInfo.code;
                ShoortNumber.Amount = serviceInfo.amount;
                ShoortNumber.From = serviceInfo.from;
                ShoortNumber.UsageSms = serviceInfo.countUseMassage;
                //ShoortNumber.To = package.to;
                ShoortNumber.To = DateTime.Now;
                ShoortNumber.Smscount = serviceInfo.countMassage;
                ShoortNumber.Service = serviceInfo.serviceName;
                //ShoortNumber.Description = serviceInfo.discriptions;
                ShoortNumber.CreatedBy = userId;//user dontforget
                ShoortNumber.CreatedOn = DateTime.Now;
                ShoortNumber.CustomerId = serviceInfo.custmorId;
                ShoortNumber.State = 1;

                db.ShoortNumber.Add(ShoortNumber);

                db.SaveChanges();

                return Ok("لقد قمت بتسـجيل بيانات الخدمة بنــجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("EditCustomorInfo")]
        public IActionResult EditCustomorInfo([FromBody] CustomersObj customersInfo)
        {
            try
            {

                if (customersInfo == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                //if (userId <= 0)
                //{
                //    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                //}


                var Cutomers = new Cutomers();

                


                Cutomers = (from p in db.Cutomers where p.CustomerId == customersInfo.custmorId select p).SingleOrDefault();

                if (Cutomers == null)
                {
                    return BadRequest("لم يتم العتور علي العميل");
                }

                Cutomers.FullName = customersInfo.name;
                Cutomers.Phone = customersInfo.phone;
                Cutomers.BirthDate = customersInfo.date;
                Cutomers.CompanyName = customersInfo.companyName.Trim();
                Cutomers.Email = customersInfo.email;
                //Cutomers.Status = 0;
                //Cutomers.CreatedBy = 0;//user dontforget
                //Cutomers.CreatedOn = DateTime.Now;
                db.Cutomers.Update(Cutomers);

                db.SaveChanges();

                return Ok("تمت عملية التعديل بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getPakegeByState")]
        public IActionResult GetPackges(int pageNo, int pageSize,long custmorId, int state)
        {
            try
            {
                //state
                //1 active
                //2 not active code
                //3 stoped
                //9 delete

                IQueryable<ShoortNumber> PackegesInfo = from p in db.ShoortNumber where p.CustomerId== custmorId && p.State==state select p;

                
                

                var PackegesCount = (from p in PackegesInfo
                                     select p).Count();

                var PackegesInfos = (from p in PackegesInfo
                                     orderby p.CreatedOn descending
                                     select new
                                     {
                                         UsagePercentage =(100 * p.UsageSms) / p.Smscount,
                                         Service = p.Service,
                                         Code = p.Code,
                                         From = p.From,
                                         To = p.To,
                                         Amount = p.Amount,
                                         SMSCount = p.Smscount,
                                         UsageSMS = p.UsageSms,
                                         RemindSMS=p.Smscount-p.UsageSms,
                                         Id = p.Id,
                                     }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return Ok(new { Packeges = PackegesInfos, count = PackegesCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetHistoryPackges")]
        public IActionResult GetHistoryPackges(int pageNo, int pageSize, long custmorId,int selectedHCodPack,int SearchType)
        {
            try
            {

                IQueryable<ShoortNumberActions> PackegesInfo = from p in db.ShoortNumberActions select p;

                if(custmorId!=0)
                {
                    PackegesInfo = from p in db.ShoortNumberActions where p.ShoortNumber.CustomerId == custmorId select p;
                }

                
                if(SearchType!=0 && SearchType!=5 && SearchType != 6)
                {
                    PackegesInfo = from p in PackegesInfo where p.ActionType == SearchType select p;
                }

                if(selectedHCodPack!=0 && SearchType != 6)
                {
                    PackegesInfo = from p in PackegesInfo where p.ShoortNumberId==selectedHCodPack  select p;
                }



                var PackegesCount = (from p in PackegesInfo
                                     select p).Count();

                var PackegesInfos = (from p in PackegesInfo
                                     orderby p.CreatecdOn descending
                                     select new
                                     {
                                         code=p.ShoortNumber.Code,
                                         ActionDescription = p.ActionDescription,
                                         Amount = p.Amount,
                                         SMSCount = p.Smscount,
                                         From = p.From,
                                         To = p.To,
                                         CreatedBy = p.CreatedBy,
                                         CreatecdOn = p.CreatecdOn,
                                         Id=p.ShoortNumberId,

                                     }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return Ok(new { Packeges = PackegesInfos, count = PackegesCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("stopServeice")]
        public IActionResult stopServeice(long id,string disc)
        {
            try
            {

                if (id == 0)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة المحاولة");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                //if (userId <= 0)
                //{
                //    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                //}


                var packge = new ShoortNumber();




                packge = (from p in db.ShoortNumber where p.Id == id select p).SingleOrDefault();

                if (packge == null)
                {
                    return BadRequest("لم يتم العتور علي الباقة الرجاء إعادة المحاولة");
                }

                packge.State = 3;
                db.ShoortNumber.Update(packge);

                var Action = new ShoortNumberActions();
                Action.ShoortNumberId = id;
                Action.ActionType = 3;
                Action.ActionDescription ="إيقاف الباقة - "+ disc;
                Action.Amount = 0;
                Action.Smscount = 0;
                Action.CreatedBy = userId;
                Action.CreatecdOn = DateTime.Now;
                db.ShoortNumberActions.Update(Action);

                db.SaveChanges();

                return Ok("تمت عملية إيقاف الخدمة بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("backServeice")]
        public IActionResult backServeice(long id)
        {
            try
            {

                if (id == 0)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة المحاولة");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                //if (userId <= 0)
                //{
                //    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                //}


                var packge = new ShoortNumber();




                packge = (from p in db.ShoortNumber where p.Id == id select p).SingleOrDefault();

                if (packge == null)
                {
                    return BadRequest("لم يتم العتور علي الباقة الرجاء إعادة المحاولة");
                }

                packge.State = 1;
                db.ShoortNumber.Update(packge);

                var Action = new ShoortNumberActions();
                Action.ShoortNumberId = id;
                Action.ActionType = 4;
                Action.ActionDescription = "إلغاء إيقاف الباقة";
                Action.Amount = 0;
                Action.Smscount = 0;
                Action.CreatedBy = userId;
                Action.CreatecdOn = DateTime.Now;
                db.ShoortNumberActions.Update(Action);

                db.SaveChanges();

                return Ok("تمت عملية إرجاع الخدمة بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("rechargeService")]
        public IActionResult rechargeService(long id,[FromBody] CustomersObj ReloadserviceInfo)
        {
            try
            {

                if (ReloadserviceInfo == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                var shoortNumber = new ShoortNumber();

                shoortNumber = (from p in db.ShoortNumber where p.Id == id select p).SingleOrDefault();

                if(shoortNumber==null)
                {
                    return BadRequest("لم يتم العتور علي الباقة الرجاء إعادة المحاولة");
                }

                if(ReloadserviceInfo.to<=DateTime.Now)
                {
                    return BadRequest("لايمكن التجديد لتاريخ قديم");
                }

                //if state 1 you need to add becuse the packege is active 
                //if state 2 you need to set as new packege 

                if(shoortNumber.State==1)
                {
                    shoortNumber.Amount += ReloadserviceInfo.amount;
                    shoortNumber.Smscount += ReloadserviceInfo.countMassage;
                    shoortNumber.To = ReloadserviceInfo.to;
                }
                else if (shoortNumber.State == 2)
                {
                    shoortNumber.State = 1;//its active now !!
                    shoortNumber.Amount = ReloadserviceInfo.amount;
                    shoortNumber.Smscount = ReloadserviceInfo.countMassage;
                    shoortNumber.From = ReloadserviceInfo.from;
                    shoortNumber.To = ReloadserviceInfo.to;
                    shoortNumber.UsageSms =0;
                }

                

                db.ShoortNumber.Update(shoortNumber);


                var shoortNumberActions = new ShoortNumberActions();

                shoortNumberActions.ShoortNumberId = id;
                shoortNumberActions.ActionType = 2;
                shoortNumberActions.ActionDescription = "إعادة شحن الباقة";
                shoortNumberActions.Smscount = ReloadserviceInfo.countMassage;
                shoortNumberActions.Amount = ReloadserviceInfo.amount;
                shoortNumberActions.From = ReloadserviceInfo.to;
                shoortNumberActions.To = ReloadserviceInfo.to;
                shoortNumberActions.CreatedBy = userId;
                shoortNumberActions.CreatecdOn = DateTime.Now;

                db.ShoortNumberActions.Add(shoortNumberActions);


                db.SaveChanges();

                return Ok("تمت عملية إعادة الشحن بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("deleteCustmor")]
        public IActionResult deleteCustmor(long id)
        {
            try
            {

                if ( id== 0)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                //if (userId <= 0)
                //{
                //    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                //}

                var custmor = new Cutomers();

                custmor = (from p in db.Cutomers where p.CustomerId == id select p).SingleOrDefault();

                if (custmor == null)
                {
                    return BadRequest("لم يتم العتور علي العميل الرجاء إعادة المحاولة");
                }

                custmor.Status = 9;

                db.Cutomers.Update(custmor);

                db.SaveChanges();

                return Ok("تمت عملية حدف العميل بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getCustomersPhone")]
        public IActionResult getCustomersPhone()//type of id 
        {
            try
            {
                IQueryable<Cutomers> CodesInfo = from p in db.Cutomers where p.Status!=9 select p;


                var CodesInfos = (from p in CodesInfo
                                  orderby p.CreatedOn descending
                                  select new
                                  {
                                      CustomerId = p.CustomerId,
                                      FullName = p.FullName,
                                      Phone=p.Phone,
                                  }).ToList();

                return Ok(new { CustomersPhone = CodesInfos });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getHistoryCodesPackges")]
        public IActionResult getHistoryCodesPackges(long id)//type of id 
        {
            try
            {
                IQueryable<ShoortNumber> CodesInfo = from p in db.ShoortNumber where p.State != 9  select p;

                if(id!=0)
                {
                    CodesInfo = from p in db.ShoortNumber where p.State != 9 && p.CustomerId == id select p;
                }

                var CodesInfos = (from p in CodesInfo
                                  orderby p.CreatedOn descending
                                  select new
                                  {
                                      Id=p.Id,
                                      Code=p.Code
                                  }).ToList();

                return Ok(new { historyCodesPackges = CodesInfos });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getHistoryPackge")]
        public IActionResult getHistoryPackge(int pageNo, int pageSize,long id, int SearchType)
        {
            try
            {

                IQueryable<ShoortNumberActions> PackegesInfo = from p in db.ShoortNumberActions  select p;

                if(id!=0)
                {
                    PackegesInfo = from p in db.ShoortNumberActions where p.ShoortNumberId == id select p;
                }

                if (SearchType != 0 && SearchType != 6)
                {
                    PackegesInfo = from p in PackegesInfo where p.ActionType == SearchType select p;
                }


                var PackegesCount = (from p in PackegesInfo
                                     select p).Count();

                var PackegesInfos = (from p in PackegesInfo
                                     orderby p.CreatecdOn descending
                                     select new
                                     {
                                         code = p.ShoortNumber.Code,
                                         ActionDescription = p.ActionDescription,
                                         Amount = p.Amount,
                                         SMSCount = p.Smscount,
                                         From = p.From,
                                         To = p.To,
                                         CreatedBy = p.CreatedBy,
                                         CreatecdOn = p.CreatecdOn,
                                         Id = p.ShoortNumberId,

                                     }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return Ok(new { Packeges = PackegesInfos, count = PackegesCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
    
}

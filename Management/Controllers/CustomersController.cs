using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managegment.Controllers;
using Managegment.objects;
using Management.Models1;
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

                //if (userId <= 0)
                //{
                //    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                //}

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
                ShoortNumber.UsageSms = 0;
                ShoortNumber.Service = customer.serviceName;
                ShoortNumber.Description = customer.discriptions;
                ShoortNumber.CreatedBy = 0;//user dontforget
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

                //if (userId <= 0)
                //{
                //    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                //}
                
                var ShoortNumber = new ShoortNumber();
                

                ShoortNumber.Code = serviceInfo.code;
                ShoortNumber.Amount = serviceInfo.amount;
                ShoortNumber.From = serviceInfo.from;
                //ShoortNumber.To = package.to;
                ShoortNumber.To = DateTime.Now; 
                ShoortNumber.Smscount = serviceInfo.countMassage;
                ShoortNumber.UsageSms = 0;
                ShoortNumber.Service = serviceInfo.serviceName;
                //ShoortNumber.Description = serviceInfo.discriptions;
                ShoortNumber.Description = "d";
                ShoortNumber.CreatedBy = 0;//user dontforget
                ShoortNumber.CreatedOn = DateTime.Now;
                ShoortNumber.CustomerId = serviceInfo.custmorId;

                db.ShoortNumber.Add(ShoortNumber);

                db.SaveChanges();

                return Ok("لقد قمت بتسـجيل بيانات الخدمة بنــجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
    
}

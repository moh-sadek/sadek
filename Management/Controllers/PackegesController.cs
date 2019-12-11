using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managegment.Controllers;
using Management.Models1;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/Packeges")]
    public class PackegesController : Controller
    {
        private readonly VASContext db;
        private Helper help;
        public PackegesController(VASContext context)
        {
            this.db = context;
            help = new Helper();
        }

        [HttpGet("GetPackges")]
        public IActionResult GetPackges(int pageNo, int pageSize, int searchType,int id)//type of id 
        {
            try
            {
                IQueryable<ShoortNumber> PackegesInfo= from p in db.ShoortNumber select p;

                //searchType
                //1 name
                //2 code
                if (searchType==1)
                {
                    PackegesInfo = from p in db.ShoortNumber where p.CustomerId==id select p;
                }else if(searchType == 2)
                {
                    PackegesInfo = from p in db.ShoortNumber where p.Code == id select p;
                }
                else if (searchType == 3)
                {
                   //id
                   //1 Finshe
                   //2 Not Finsh
                   //3 All MostFinsh
                    if(id==1)
                    {
                        PackegesInfo = from p in db.ShoortNumber where  DateTime.Now > p.To  select p;
                    }
                    else if(id == 1)
                    {
                        PackegesInfo = from p in db.ShoortNumber where DateTime.Now <= p.To   select p;
                    }
                    
                }

                var PackegesCount = (from p in PackegesInfo
                                    select p).Count();

                var PackegesInfos = (from p in PackegesInfo
                                     orderby p.CreatedOn descending
                                   select new
                                   {
                                       FullName = p.Customer.FullName,
                                       Code = p.Code,
                                       Service = p.Service,
                                       Amount = p.Amount,
                                       SMSCount = p.Smscount,
                                       UsageSMS = p.UsageSms,
                                       To = p.To,
                                       Status = DateTime.Now > p.To ? 1 :0,
                                       CustomerId = p.CustomerId,
                                       Id=p.Id,
                                   }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return Ok(new { Packeges = PackegesInfos, count = PackegesCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()//type of id 
        {
            try
            {
                IQueryable<Cutomers> CustmorsInfo = from p in db.Cutomers where p.Status!=9 select p;


                var CustmorsInfos = (from p in CustmorsInfo
                                     orderby p.CreatedOn descending
                                     select new
                                     {
                                         CustomerId = p.CustomerId,
                                         FullName = p.FullName,
                                         Phone=p.Phone,
                                         BirthDate=p.BirthDate,
                                         CompanyName=p.CompanyName,
                                         Email=p.Email,
                                         Status=p.Status,
                                         CreatedBy=p.CreatedBy,
                                         CreatedOn=p.CreatedOn
                                     }).ToList();

                return Ok(new { custmor = CustmorsInfos});
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetCodes")]
        public IActionResult GetCodes()//type of id 
        {
            try
            {
                IQueryable<ShoortNumber> CodesInfo = from p in db.ShoortNumber  select p;


                var CodesInfos = (from p in CodesInfo
                                     orderby p.CreatedOn descending
                                     select new
                                     {
                                         CustomerId = p.CustomerId,
                                         Id = p.Id,
                                         Code = p.Code
                                     }).ToList();

                return Ok(new { codes = CodesInfos });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
    
}

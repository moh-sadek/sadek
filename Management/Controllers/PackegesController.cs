using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Managegment.Controllers;
using Management;
using Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Management.Controllers
{
    [Produces("application/json")]
    [Route("Api/Admin/Packeges")]
    public class PackegesController : Controller
    {
        private readonly VASContext db;
        private Helper help;
        private Settings _settings;
        public PackegesController(VASContext context, IOptions<Settings> setttings)
        {
            this.db = context;
            help = new Helper();
            _settings = setttings.Value;
        }


        [HttpGet("getVarbile")]
        public IActionResult getVarbile()

        {
            try
            {
                var varbileList = new
                {
                    grade = _settings.grade,
                    path = _settings.path,
                    extensions = _settings.extensions
                };
                return Ok(new { varbiles = varbileList });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("EditPaht")]
        public IActionResult EditPaht([FromBody] Settings form)
        {
            try
            {

                if (form == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                _settings.path = form.path;

                return Ok("تمت عملية التعديل العميل بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{grad}/Editgrade")]
        public IActionResult Editgrade(int grad)
        {
            try
            {

                if (grad != 0)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                _settings.grade = grad;

                return Ok("تمت عملية التعديل العميل بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{extensions}/Editextensions")]
        public IActionResult Editextensions(string extensions)
        {
            try
            {

                if (extensions == null)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                _settings.extensions = extensions;

                return Ok("تمت عملية التعديل العميل بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetPackges")]
        public IActionResult GetPackges(int pageNo, int pageSize, int searchType, int id)//type of id 
        {
            try
            {
                IQueryable<ShoortNumber> PackegesInfo = from p in db.ShoortNumber select p;

                //searchType
                //1 name
                //2 code
                if (searchType == 1)
                {
                    PackegesInfo = from p in db.ShoortNumber where p.CustomerId == id select p;
                } else if (searchType == 2)
                {
                    PackegesInfo = from p in db.ShoortNumber where p.Id == id select p;
                }
                else if (searchType == 3)
                {
                    //id
                    //1 Finshe 
                    //2 Not Finsh
                    //3 All MostFinsh
                    if (id == 1)
                    {
                        PackegesInfo = from p in db.ShoortNumber where p.State == 1 select p;
                    }
                    else if (id == 2)
                    {
                        PackegesInfo = from p in db.ShoortNumber where p.State == 2 select p;
                    } else if (id == 3)
                    {
                        PackegesInfo = from p in db.ShoortNumber where p.State == 3 select p;
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
                                         State = p.State,
                                         CustomerId = p.CustomerId,
                                         Id = p.Id,
                                         RemindSMS = p.Smscount - p.UsageSms,
                                         UsagePercentage = (100 * p.UsageSms) / p.Smscount,
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
                IQueryable<Cutomers> CustmorsInfo = from p in db.Cutomers where p.Status != 9 select p;


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
                                         CreatedOn = p.CreatedOn
                                     }).ToList();

                return Ok(new { custmor = CustmorsInfos });
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
                IQueryable<ShoortNumber> CodesInfo = from p in db.ShoortNumber select p;


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

        [HttpGet("getPakegesInfo")]
        public IActionResult getPakegesInfo()//type of id 
        {
            try
            {
                IQueryable<ShoortNumber> PackegesInfo = from p in db.ShoortNumber select p;

                var PackegesCount = (from p in PackegesInfo select p).Count();
                var PackegeAmountsum = (from p in db.ShoortNumberActions select p.Amount).Sum();
                var CustomorCounts = (from p in db.Cutomers where p.Status != 9 select p).Count();
                var unkownPackage = (from p in db.UnknownNumber where p.Status != 9 select p).Count();
                var Active = (from p in db.ShoortNumber where p.State == 1 select p).Count();
                var NotActive = (from p in db.ShoortNumber where p.State == 2 select p).Count();
                var Stopped = (from p in db.ShoortNumber where p.State == 3 select p).Count();
                var Finsh = (from p in db.ShoortNumber where p.State == 4 select p).Count();
                var LastStep = (from p in db.ShoortNumberActions select p).OrderByDescending(p => p.Id).Select(x => new { code = x.ShoortNumber.Code, x.Amount, x.Smscount, x.ActionDescription }).Take(5).ToList();
                var LastSubScribtions = (from p in db.ShoortNumber select p).OrderByDescending(p => p.Id).Select(x => new { x.Code, x.Service, x.Amount, x.Smscount, x.UsageSms }).Take(5).ToList();

                var Detalss = new
                {
                    PackegesCount = PackegesCount,
                    PackegeAmountsum = PackegeAmountsum,
                    CustomorCounts = CustomorCounts,
                    unkownPackage = unkownPackage,
                    Active = Active,
                    NotActive = NotActive,
                    Stopped = Stopped,
                    Finsh = Finsh,
                    LastStep = LastStep,
                    LastSubScribtions = LastSubScribtions
                };


                return Ok(new { Detals = Detalss });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getUnkownPackage")]
        public IActionResult getUnkownPackage(int pageNo, int pageSize)//type of id 
        {
            try
            {
                IQueryable<UnknownNumber> PackegesInfo = from p in db.UnknownNumber where p.Status != 9 select p;


                var PackegesCount = (from p in PackegesInfo
                                     select p).Count();

                var PackegesInfos = (from p in PackegesInfo
                                     orderby p.CreatecdOn descending
                                     select new
                                     {
                                         id = p.Id,
                                         Code = p.Code,
                                         Count = p.Count,
                                         CreatecdOn = p.CreatecdOn,
                                     }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return Ok(new { unkownPackage = PackegesInfos, count = PackegesCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("{id}/deleteUnkownPackge")]
        public IActionResult deleteUnkownPackge(int id)
        {
            try
            {

                if (id == 0)
                {
                    return BadRequest("حذث خطأ في ارسال البيانات الرجاء إعادة الادخال");
                }

                var userId = this.help.GetCurrentUser(HttpContext);

                if (userId <= 0)
                {
                    return StatusCode(401, "الرجاء الـتأكد من أنك قمت بتسجيل الدخول");
                }

                var UnknownNumber = new UnknownNumber();

                UnknownNumber = (from p in db.UnknownNumber where p.Id == id select p).SingleOrDefault();

                if (UnknownNumber == null)
                {
                    return BadRequest("لم يتم العتور علي الباقة الرجاء إعادة المحاولة");
                }

                db.UnknownNumber.Remove(UnknownNumber);

                db.SaveChanges();

                return Ok("تمت عملية حدف العميل بنجاح");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getCount")]
        public IActionResult getCount()//type of id 
        {
            try
            {
                var PackegesCount = (from p in db.UnknownNumber where p.Status == 1 select p).Count();

                return Ok(new { count = PackegesCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getFiles")]
        public IActionResult getFiles()//type of id 
        {
            try
            {
                var files = (from p in db.Files
                             orderby p.CreatecdOn descending
                             select new
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                             }).ToList();

                return Ok(new { files = files });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getfileContent")]
        public IActionResult getfileContent(int pageNo, int pageSize, long id)//type of id 
        {
            try
            {
                IQueryable<FileContent> FileContent = from p in db.FileContent select p;

                if (id != 0)
                {
                    FileContent = from p in db.FileContent where p.FilesId == id select p;
                }


                var FileCount = (from p in FileContent select p).Count();

                var filesContent = (from p in FileContent
                                    orderby p.CreatecdOn descending
                                    select new
                                    {
                                        id = p.Id,
                                        Code = p.Code,
                                        Count = p.Count,
                                        CreatecdOn = p.CreatecdOn,
                                    }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return Ok(new { filesContent = filesContent, FileCount = FileCount });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //SMS Joup
        [AllowAnonymous]
        [HttpGet("PackagesCheck")]
        public IActionResult PackagesCheck()
        {
            try
            {
                String FolderPath = _settings.path;
                string[] filePaths = Directory.GetFiles(FolderPath, "*" + _settings.extensions);

                foreach (string item in filePaths)
                {
                    var files = new Files();
                    files = (from p in db.Files where p.Name == item select p).SingleOrDefault();

                    if(files!=null)
                    {
                        System.IO.File.Delete(Path.Combine(FolderPath, item));
                        continue;
                    }

                    files = new Files();
                    files.Name = item;
                    files.CreatecdOn = DateTime.Now;
                    db.Files.Add(files);

                    List < ShortTemp > ContentFile = new List<ShortTemp>();


                    string[] lines = System.IO.File.ReadAllLines(item, Encoding.UTF8);

                    foreach (string line in lines)
                    {
                        string[] words = line.Split(';');
                        if (words[14].Equals("No error") && words[2].Equals("MT"))
                        {
                            if (Regex.Matches(words[3], @"[a-zA-Z]").Count > 0)
                            {
                                ContentFile.Add(new ShortTemp(words[3].ToUpper()));
                            }
                            else if(words[3].Length<=7)
                            {
                                ContentFile.Add(new ShortTemp(words[3].ToUpper()));
                            }
                            
                        }
                    }

                    ManageShortNumber(ContentFile, files);

                    db.SaveChanges();

                    System.IO.File.Delete(Path.Combine(FolderPath, item));
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //Looping in ccontent file item to handel the new short code
        public void ManageShortNumber(List<ShortTemp> ContentFile, Files files)
        {
            var ContentFileGroup = from p in ContentFile group p by p.code into g select new Temp { code = g.Key, count = g.Count() };

            foreach (Temp items in ContentFileGroup)
            {
                FileContent fileContent = new FileContent();
                fileContent.FilesId = files.Id;
                fileContent.Code = items.code;
                fileContent.Count = items.count;
                fileContent.CreatecdOn = DateTime.Now;
                db.FileContent.Add(fileContent);

                ShoortNumber PackgeInfo = new ShoortNumber();
                PackgeInfo = (from p in db.ShoortNumber where p.Code == items.code || p.Service == items.code select p).SingleOrDefault();

                if (PackgeInfo == null)
                {
                    UnknownNumber unknown = new UnknownNumber();
                    unknown = (from p in db.UnknownNumber where p.Code == items.code select p).SingleOrDefault();
                    if (unknown != null)
                    {
                        unknown.Count += items.count;
                        unknown.Status = 1;
                        unknown.CreatecdOn = DateTime.Now;
                        db.UnknownNumber.Update(unknown);

                    }
                    else
                    {
                        UnknownNumber Unknown = new UnknownNumber();
                        Unknown.Code = items.code;
                        Unknown.Count = items.count;
                        Unknown.CreatecdOn = DateTime.Now;
                        Unknown.Status = 1;
                        db.UnknownNumber.Add(Unknown);
                        continue;
                    }
                }
                else
                {
                    int remind = PackgeInfo.Smscount.GetValueOrDefault()- PackgeInfo.UsageSms.GetValueOrDefault();

                    if (items.count <= remind)
                    {
                        PackgeInfo.UsageSms += items.count;
                        int nespa = (int)Math.Round((double)(100 * PackgeInfo.UsageSms.GetValueOrDefault()) / PackgeInfo.Smscount.GetValueOrDefault());

                        if (nespa >= _settings.grade && nespa <= 100)
                        {
                            PackgeInfo.State = 2;//al most done 
                        }
                    }
                    else
                    {
                        PackgeInfo.UsageSms += items.count;
                        PackgeInfo.State = 4;
                    }

                    db.ShoortNumber.Update(PackgeInfo);

                }



            }
        }

        [AllowAnonymous]
        [HttpGet("CheckDate")]
        public IActionResult CheckDate()
        {
            try
            {
                var packege = (from p in db.ShoortNumber where p.State == 1 select p).ToList();

                foreach (ShoortNumber item in packege)
                {
                    if(item.To>=DateTime.Now)
                    {
                        item.State = 2;
                    }
                    db.SaveChanges();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
    
}

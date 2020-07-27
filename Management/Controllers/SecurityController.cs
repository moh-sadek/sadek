﻿using Common;
using Managegment.Controllers;
using Management.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Controllers;

namespace Management.Controllers
{
    public class SecurityController : Controller
    {
        [TempData]
        public string ErrorMessage { get; set; }
        private Helper help;
        private readonly CSORContext db;
        private IConfiguration Configuration { get; }
        public SecurityController(CSORContext context, IConfiguration configuration)
        {
            this.db = context;
            help = new Helper();
            Configuration = configuration;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccountActivate(string confirm, string account)
        {
            try
            {

                confirm = confirm + "@cra.gov.ly";
                account = Security.DecryptBase64(account);

                if (!Security.VerifyHash(confirm, account, HashAlgorithms.SHA512))
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ViewData["ApiServer"] = Configuration.GetSection("Links")["ApiServer"];
                    return View();
                }
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        public class user
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class userPassword
        {
            public int UserId { get; set; }
            public string NewPassword { get; set; }
            public string Password { get; set; }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> loginUser([FromBody] user loginUser)
        {
            try
            {
                if (loginUser == null)
                {
                    return NotFound("الرجاء ادخال البريد الالكتروني او اسم الدخول");
                }

                if (string.IsNullOrWhiteSpace(loginUser.Email))
                {
                    return BadRequest("الرجاء ادخال البريد الالكتروني او اسم الدخول");
                }

                if (string.IsNullOrWhiteSpace(loginUser.Password))
                {
                    return BadRequest("الرجاء ادخال كلمه المرور");
                }

                var cUser = (from p in db.Users
                             where (p.Email == loginUser.Email || p.LoginName == loginUser.Email) && p.State != 9
                             select p).SingleOrDefault();

                if (cUser == null)
                {
                    return NotFound("الرجاء التاكد من البريد الالكتروني وكلمة المرور");

                }


                if (cUser.State == 0)
                {
                    return BadRequest("حسابك غير مفعل");
                }
                if (cUser.State == 2)
                {
                    if (cUser.LoginTryAttemptDate != null)
                    {
                        DateTime dt = cUser.LoginTryAttemptDate.Value;
                        double minuts = 30;
                        dt = dt.AddMinutes(minuts);
                        if (dt >= DateTime.Now)
                        {
                            return BadRequest("لايمكنك الدخول للنظام: تم ايقافك");
                        }
                        else
                        {
                            cUser.State = 1;

                            db.SaveChanges();
                        }
                    }
                    else { return BadRequest("لايمكنك الدخول للنظام: تم ايقافك"); }
                }

                if (!Security.VerifyHash(loginUser.Password, cUser.Password, HashAlgorithms.SHA512))
                {

                    cUser.LoginTryAttempts++;
                    if (cUser.LoginTryAttempts >= 5 && cUser.State == 1)
                    {
                        cUser.LoginTryAttemptDate = DateTime.Now;
                        cUser.State = 2;
                    }
                    db.SaveChanges();
                    return NotFound("الرجاء التاكد من البريد الالكتروني وكلمة المرور");
                }


                cUser.LoginTryAttempts = 0;
                cUser.LastLoginOn = DateTime.Now;
                db.SaveChanges();

                var userInfo = new
                {
                    userId = cUser.Id,
                    fullName = cUser.Name,
                    LoginName = cUser.LoginName,
                    DateOfBirth = cUser.BirthDate,
                    Email = cUser.Email,
                    Gender = cUser.Gender,
                    State = cUser.State,
                    Phone = cUser.Phone,
                    UserType = cUser.UserType,
                    SecretKey = Guid.NewGuid()
                    //OfficeState=cUser.Office.State        
                };

                //const string Issuer = "http://www.nid.ly";
                const string Issuer = "http://localhost:4810";
                var claims = new List<Claim>();
                claims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/id", cUser.Id.ToString(), ClaimValueTypes.Integer64, Issuer));
                claims.Add(new Claim(ClaimTypes.Name, cUser.Name, ClaimValueTypes.String, Issuer));
                //  claims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/OfficeId", cUser.OfficeId.ToString(), ClaimValueTypes.Integer64, Issuer));
                //claims.Add(new Claim("userType", cUser.UserType.ToString(), ClaimValueTypes.Integer32, Issuer));
                var userIdentity = new ClaimsIdentity("thisisasecreteforauth");
                userIdentity.AddClaims(claims);
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(1),
                        IsPersistent = true,
                        AllowRefresh = true
                    });

                return Ok(userInfo);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "error while logout");
            }

        }


        [HttpPost]
        public IActionResult ChangePassword([FromBody] userPassword loginUser)
        {
            try
            {
                var userId = this.help.GetCurrentUser(HttpContext);
                if (loginUser.Password != null)
                {
                    var User = (from p in db.Users
                                where p.Id == userId && p.State != 9
                                select p).SingleOrDefault();

                    if (Security.VerifyHash(loginUser.Password, User.Password, HashAlgorithms.SHA512))
                    {

                        User.Password = Security.ComputeHash(loginUser.NewPassword, HashAlgorithms.SHA512, null);
                        //User.ModifiedBy = userId;
                        //User.ModifiedOn = DateTime.Now;
                        db.SaveChanges();


                    }
                    else
                    {

                        return BadRequest("الرجاء التاكد من كلمة المرور");


                    }

                }

                else
                {
                    var User = (from p in db.Users
                                where p.Id == loginUser.UserId && p.State != 9
                                select p).SingleOrDefault();
                    if (User == null)
                    {
                        return BadRequest("خطأ بيانات المستخدم غير موجودة");
                    }
                    User.Password = Security.ComputeHash(loginUser.NewPassword, HashAlgorithms.SHA512, null);
                    //User.ModifiedBy = userId;
                    //User.ModifiedOn = DateTime.Now;
                    db.SaveChanges();

                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "error while logout");
            }

        }
        [HttpGet]

        public IActionResult GetUserImage(long userId)
        {
            var userimage = (from p in db.Users
                             where p.Id == userId
                             select p.Image).SingleOrDefault();

            return File(userimage, "image/jpg");
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public IActionResult Unsupported()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }



        [HttpPost("Security/ResetPassword/{email}")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email)
        {
            try
            {

                if (!Validation.IsValidEmail(email))
                {
                    return BadRequest("الرجاء ادخال البريد الالكتروني بطريقة الصحيحه");
                }

                var user = (from p in db.Users
                            where p.Email == email && p.State != 9
                            select p).SingleOrDefault();

                if (user == null)
                {
                    return NotFound("البريد الإلكتروني غير مسجل بالنظـام !");
                }

                if (user.State == 0)
                {
                    return BadRequest("تم إيقاف هذا المستخدم من النظام !");
                }


                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("noreply@cra.gov.ly");

                mail.To.Add(email);

                string confirm = Security.ComputeHash(user.Id.ToString() + "@cra.gov.ly", HashAlgorithms.SHA512, null);

                mail.Subject = "مصلحة الاحوال المدنية - إعادة تعيين كلمة المرور";

                mail.Body = GetResetPasswordHTML(user.Name, "/security/AccountActivate?confirm=" + user.Id.ToString() + "&account=" + Security.EncryptBase64(confirm));

                mail.IsBodyHtml = true;

                var smtp = new SmtpClient("webmail.cra.gov.ly")
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("noreply@cra.gov.ly", "Qwerty@!@#123"),
                    Port = Int32.Parse(Configuration.GetSection("Links")["SMTPPORT"]),
                    EnableSsl = Configuration.GetSection("Links")["SMTSSL"] == "1"
                };
                smtp.Send(mail);
                //Task.Factory.StartNew(() => smtp.Send(mail));
                return Ok("تم ارسال بريد التحقق بنجاح الرجاء فتح بريدك الإلكتروني");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }
        }

        public class UserActivation
        {
            public string password { get; set; }
            public string cpassword { get; set; }
            public int confirm { get; set; }
            public string account { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("Security/ActivateUser")]
        public IActionResult ActivateUser([FromBody] UserActivation userActivate)
        {
            try
            {
                if (!Security.VerifyHash(userActivate.confirm.ToString() + "@cra.gov.ly", Security.DecryptBase64(userActivate.account), HashAlgorithms.SHA512))
                {
                    return BadRequest("الرابط غير مفعل");
                }

                var user = (from u in db.Users
                            where u.Id == userActivate.confirm
                            select u).SingleOrDefault();

                if (user == null)
                {
                    return NotFound(" المستخدم غير موجود ");
                }


                if (String.IsNullOrEmpty(userActivate.password))
                {
                    return BadRequest("يجب إدخال كلمة المرور");
                }
                else if (userActivate.password.Length <= 7)
                {
                    return BadRequest("يجب ان تكون كلمة المرور اكبر من سبع خانات");
                }
                else if (String.IsNullOrEmpty(userActivate.cpassword))
                {
                    return BadRequest("يجب إدخال تأكيد كلمة المرور ");
                }
                if (userActivate.password != userActivate.cpassword)
                {
                    return BadRequest("خطأ في عملية المطابقة لكلمة المرور");
                }


                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (user.State == 0)
                        {
                            user.State = 1;
                        }
                        user.Password = Security.ComputeHash(userActivate.password, HashAlgorithms.SHA512, null);
                        db.SaveChanges();
                        trans.Commit();
                        return Ok("لقد قمت بتغير كلمة المرور بنجاح");
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        return StatusCode(500, null);

                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(500, null);
            }

        }


        public string GetResetPasswordHTML(string UserName, string path)
        {

            string WebServer = Configuration.GetSection("Links")["WebServer"],
                   EmailSupport = Configuration.GetSection("Links")["EmailSupport"];


            return "<!DOCTYPE html>" +
                   "<html lang = \"ar\" dir = \"rtl\"><head><meta charset = \"UTF-8\"><style>" +
                   "div.wrapper{ margin: auto; margin-top:13vh; max-width:550px; }" +
                   "img{ width: 100 %; height: 45px; } footer{ width: 85 %; margin: auto; }" +
                    "p{ line-height: 1.4; text-align: justify; }" +
                  ".grey{ color: grey; }.padd{padding: 10px 5px; }" +
                  ".Helvetica{ font-family: Helvetica; font-size: 14.5px; }" +
                  "footer div { text-align: center; }" +
                  "body{ font-family: Arial; font-size:15.5px; }" +
                  "</style></head><body>" +
                  "<div class=\"wrapper\"><header><img src = \"" + WebServer + "/img/ddd.png\" /></header><div class=\"padd\"><p>عزيزي المستخدم<span>" + " " + UserName + " " + "</span></p>" +
                  "<p>  لتتمكن من استرجاع حسابك عليك ادخال كلمة مرور جديدة عن طريق النقر على الرابط أدناه :</p> " +
                 "<p>الرابط: <a href = \" " + WebServer + path + "\" > Click Here</a></p>" +
                 "<br><p>فريق عمل مشروع<b>مصلحة الاحوال المدنية</b> </p>" +
                "</div><footer class=\"Helvetica\"><div class=\"grey\"><a href = \"" + WebServer + "\"> visit our website</a> | <a href = \"" + WebServer + "\"> log in to your account</a> | <a href = \"mailto:" + EmailSupport + "\"> get support</a></div>" +
                "<div class=\"grey\"> All rights reserved ,مشروع مصلحة الاحوال المدنية  Copyright © CRA</div></footer></div></body></html>";

        }


    }
}
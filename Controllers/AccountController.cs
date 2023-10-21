using lab_1.Models.Entities;
using lab_1.Models.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace lab_1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserVM webUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Entities1())
                {
                    Пользователи user = null;
                    user = db.Пользователи.Where(u =>u.Логин == webUser.Login).FirstOrDefault();
                    if (user != null)
                    {
                        string passwordHash = ReturnHashCode(webUser.Password + user.Соль.ToString().ToUpper());
                        if (passwordHash == user.Хэш_пароля) {
                            string userRole = "";
                            switch (user.Роль_пользователя)
                            {
                                case 1:
                                    userRole = "Admin";
                                    break;
                                case 2:
                                    userRole = "Student";
                                    break;
                            }
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                1,
                                user.Логин,
                                DateTime.Now,
                                DateTime.Now.AddDays(1),
                                false,
                                userRole);
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
                            return RedirectToAction("ListOfStudents","lab2");
                        }
                    }
                }

            }
            ViewBag.Error = "Пользователя с таким логином и паролем не существует";
            return View(webUser);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ListOfStudents", "Lab2");
        }


        private string ReturnHashCode(string v)
        {
            string hash = "";
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(v));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }
                hash = stringBuilder.ToString().ToUpper();
            }
            return hash;
        }
    }
}
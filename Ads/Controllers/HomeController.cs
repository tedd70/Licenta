﻿using Ads.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ads.Database;
using Ads.Models;

namespace Ads.Controllers
{
    public class HomeController : Controller
    {
        private IUserDbContext _userDbContext;
        private IEmailService _emailService;
        public HomeController()
        {
            _userDbContext = new UserDbContext();
            _emailService = new EmailService();
        }

        public ActionResult Login(string userName, string password)
        {
            var user = _userDbContext.FindByEmail(userName);
            var md5Password = CalculateMD5Hash(password);

            if (user == null || !user.Password.Equals(md5Password) || !user.IsActive)
            {
                TempData["hasErrors"] = true;
                return RedirectToAction("Index", "Home");
            }

            HttpCookie cookie = new HttpCookie("userId");
            cookie.Value = user.Id.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Configuration");
        }

        private string CalculateMD5Hash(string input)

        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string userName)
        {
            var user = _userDbContext.FindByEmail(userName);
            if (user != null)
            {
                var userId = user.Id.ToString();
                var identifier = Convert.ToBase64String(Encoding.Unicode.GetBytes(userId));
                var resetLink = $"http://adslicenta.azurewebsites.net/Home/ResetPassword?id={identifier}";
                var subject = "Reset your password";
                var body = $"Click <a href='{resetLink}' target='_blank'>here</a> to reset your password.";
                _emailService.SendEmail(user.Email, subject, body);
                TempData["isSuccess"] = true;
                return RedirectToAction("ForgotPassword", "Home");
            }

            TempData["hasErrors"] = true;
            return RedirectToAction("ForgotPassword", "Home");
        }



        public ActionResult ResetPassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            string userId = Encoding.Unicode.GetString(Convert.FromBase64String(id));
            var user = _userDbContext.GetById(int.Parse(userId));
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserId = user.Id;
            return View();
        }

        public ActionResult ConfirmEmail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Home");
            }

            string userId = Encoding.Unicode.GetString(Convert.FromBase64String(id));
            var user = _userDbContext.GetById(int.Parse(userId));
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            user.IsActive = true;
            _userDbContext.Update(user);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword(string password, string confirmPassword, string userId)
        {
            var user = _userDbContext.GetById(int.Parse(userId));
            if (user == null || password != confirmPassword)
            {
                return RedirectToAction("Index", "Home");
            }
            var md5Password = CalculateMD5Hash(password);
            user.Password = md5Password;
            _userDbContext.Update(user);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string userName, string password, string firstName, string lastName)
        {

            var user = _userDbContext.FindByEmail(userName);
            if (user != null)
            {
                var errorResponse = new ErrorResponseViewModel
                {
                    HasErrors = true,
                    Errors = new List<string> { "This email address is already registered" }
                };
                return View(errorResponse);
            }

            var createdUser = _userDbContext.Add(new User
            {
                Email = userName,
                Password = CalculateMD5Hash(password),
                IsActive = false,
                FirstName = firstName,
                LastName = lastName
            });

            if (createdUser != null)
            {
                var userId = createdUser.Id.ToString();
                var identifier = Convert.ToBase64String(Encoding.Unicode.GetBytes(userId));
                var confirmationLink = $"http://adslicenta.azurewebsites.net/Home/ConfirmEmail?id={identifier}";
                var subject = "Conrfim your email";
                var body = $"Click <a href='{confirmationLink}' target='_blank'>here</a> to register.";

                _emailService.SendEmail(createdUser.Email, subject, body);
                TempData["isEmailSent"] = true;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public void DeleteUser(string email)
        {
            var user = _userDbContext.FindByEmail(email);
            if (user != null) {
                _userDbContext.Delete(user.Id);
            }

        }
    }
}
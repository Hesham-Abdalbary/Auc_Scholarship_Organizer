using Auc_Scholarship_Organizer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auc_Scholarship_Organizer.Controllers
{
    public class LoginController : Controller
    {
        UserService userService;
        public LoginController()
        {
            userService = new UserService();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var email = formCollection["EmailAddress"];
            var password = formCollection["Password"];
            var user = userService.GetByEmailAndPass(email, password);
            if (user == null)
            { 
                ViewBag.ErrorMessage = "Please enter a valid user name and password";
                return View("Index");
            }
            if (user.IsSystemAdmin == true)
                return RedirectToAction("Index", "SystemAdmin");
            else
                return RedirectToAction("Index", "Student", new { id = user.Id });
        }
    }
}
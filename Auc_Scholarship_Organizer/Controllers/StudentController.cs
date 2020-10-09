using Auc_Scholarship_Organizer.DTOs;
using Auc_Scholarship_Organizer.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auc_Scholarship_Organizer.Controllers
{
    public class StudentController : Controller
    {
        UserService userService;
        StudentApplicationService studentApplicationService;
        public StudentController()
        {
            userService = new UserService();
            studentApplicationService = new StudentApplicationService();
        }
        // GET: Student
        public ActionResult Index(int? id)
        {
            if (id == null)
                return View(new List<UserDto>());
            var student = userService.GetUserById(id.Value); 
            return View(new List<UserDto>() { student });
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(UserDto user)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View();
                }
                string filePath = "";
                if (user.ResumeFile != null)
                {
                    string resume = System.IO.Path.GetFileName(user.ResumeFile.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resumes"), resume);
                    // file is uploaded
                    user.ResumeFile.SaveAs(path);
                    string serverPath = ConfigurationManager.AppSettings["ServerUrl"];
                    filePath = serverPath + "/Resumes/" + user.ResumeFile.FileName;
                }
                user.StudentApplication.Resume = filePath;
                user.IsSystemAdmin = false;
                user.StudentApplication.ApprovalStatus = 0;
                UserDto addedUser = userService.Add(user);
                if (addedUser != null)
                    return RedirectToAction("Index", new { id = addedUser.Id });
                return View("Create");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            var student = userService.GetUserById(id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserDto user)
        {
            try
            {
                string filePath = "";
                if (user.ResumeFile != null)
                {
                    string resume = System.IO.Path.GetFileName(user.ResumeFile.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resumes"), resume);
                    // file is uploaded
                    user.ResumeFile.SaveAs(path);
                    string serverPath = ConfigurationManager.AppSettings["ServerUrl"];
                    filePath = serverPath + "/Resumes/" + user.ResumeFile.FileName;
                }
                else
                {
                    var studentModel = userService.GetUserById(id);
                    filePath = studentModel.StudentApplication.Resume;
                }
                user.StudentApplication.Resume = filePath;
                user.IsSystemAdmin = false;
                user.StudentApplication.ApprovalStatus = 0;
                user.StudentApplication.Id = id;
                int updateUserResult = studentApplicationService.UpdateAll(user);
                if (updateUserResult > 0)
                    return RedirectToAction("Index", new { id = id });
                var student = userService.GetUserById(id);
                return View(student);
            }
            catch (Exception ex)
            {
                var student = userService.GetUserById(id);
                return View(student);
            }
        }
    }
}

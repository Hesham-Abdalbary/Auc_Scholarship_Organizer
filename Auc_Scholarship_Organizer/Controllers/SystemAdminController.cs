using Auc_Scholarship_Organizer.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Auc_Scholarship_Organizer.DTOs;

namespace Auc_Scholarship_Organizer.Controllers
{
    public class SystemAdminController : Controller
    {
        UserService userService;
        StudentApplicationService studentApplicationService;
        public SystemAdminController()
        {
            userService = new UserService();
            studentApplicationService = new StudentApplicationService();
        }
        // GET: SystemAdmin
        public ActionResult Index()
        {
            var users = userService.GetUsersAndApplications();
            return View(users);
        }

        // GET: SystemAdmin/Details/5
        public ActionResult Edit(int id)
        {
            var user = userService.GetUserById(id);
            return View(user);
        }

        // POST: SystemAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var approvalStatus = int.Parse(collection["Status"]);
                var updateResult = studentApplicationService.ChangeUserApproval(id, approvalStatus);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var user = userService.GetUserById(id);
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null && postedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
                {
                    ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
                }

                else
                {
                    string application = Path.GetFileName(postedFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), application);
                    postedFile.SaveAs(path);

                    var workbook = new XLWorkbook(path);
                    var ws1 = workbook.Worksheet(1);
                    try
                    {
                        UserDto user = new UserDto()
                        {
                            FirstName = ws1.Row(2).Cell(1).Value.ToString(),
                            LastName = ws1.Row(2).Cell(2).Value.ToString(),
                            EmailAddress = ws1.Row(2).Cell(3).Value.ToString(),
                            Password = ws1.Row(2).Cell(4).Value.ToString(),
                            BirthDate = DateTime.Parse(ws1.Row(2).Cell(5).Value.ToString()),
                            NationalID = ws1.Row(2).Cell(6).Value.ToString(),
                            IsSystemAdmin = false,
                            StudentApplication = new StudentApplicationDto()
                            {
                                University = ws1.Row(2).Cell(7).Value.ToString(),
                                Major = ws1.Row(2).Cell(8).Value.ToString(),
                                Gpa = ws1.Row(2).Cell(9).Value.ToString(),
                                Resume = ws1.Row(2).Cell(10).Value.ToString(),
                                ApprovalStatus = 0
                            }

                        };
                        userService.Add(user);
                        ViewBag.ImportExcel = "Excel Has been Imported successfully";
                        return RedirectToAction("Index", "SystemAdmin", null);
                    }
                    catch
                    {
                        ViewBag.ErrorMessage = "Excel file is not formatted correctly";
                        return RedirectToAction("Index", "SystemAdmin", null);
                    }
                }
            }
            //return View(postedFile);  
            return Json("no files were selected or the file is not formatted correctly !");
        }

        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("FirstName"),
                                                     new DataColumn("LastName"),
                                                     new DataColumn("EmailAddress"),
                                                     new DataColumn("DateOfBirth"),
                                                     new DataColumn("NationalId"),
                                                     new DataColumn("University"),
                                                     new DataColumn("Major"),
                                                     new DataColumn("Gpa"),
                                                     new DataColumn("ResumeLink"),
                                                     new DataColumn("Approval Status")});

            var users = userService.GetUsersAndApplications();
            foreach (var user in users)
            {
                dt.Rows.Add(user.FirstName, user.LastName, user.EmailAddress, user.BirthDate,
                    user.NationalID, user.StudentApplication.University, user.StudentApplication.Major, user.StudentApplication.Gpa,
                    user.StudentApplication.Resume, ((Helper.ApplicationStatus)user.StudentApplication.ApprovalStatus).ToString());
            }

            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }
    }
}

using Auc_Scholarship_Organizer.DTOs;
using Auc_Scholarship_Organizer.Models;
using Auc_Scholarship_Organizer.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Services
{
    public class StudentApplicationService
    {
        StudentApplicationRepository _repo;
        UserService userService;
        EmailService emailService;
        public StudentApplicationService()
        {
            _repo = new StudentApplicationRepository();
            userService = new UserService();
        }

        public int Update(StudentApplicationDto model)
        {
            var entity = Mapper.Map<StudentApplication>(model);
            var updateResult = _repo.Update(entity);
            return updateResult;
        }

        public int UpdateAll (UserDto model)
        {
            int updateResult = 0;
            var entity = Mapper.Map<StudentApplication>(model.StudentApplication);
            updateResult += _repo.Update(entity);

            updateResult += userService.Update(model);
            return updateResult;
        }

        public int ChangeUserApproval(int id, int approvalStatus)
        {
            var student = userService.GetUserById(id);
            student.StudentApplication.ApprovalStatus = approvalStatus;
            var updateResult = Update(student.StudentApplication);
            string emailBody = GetEmailBody(approvalStatus);
            emailService = new EmailService();
            try
            {
                emailService.SendEmail("Auc Scholarship Approval Status", emailBody, student.EmailAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return updateResult;
        }

        private string GetEmailBody(int approvalStatus)
        {
            if (approvalStatus == 1)
                return "Dear,  Congratulations, you have been accepted at the auc scholarship.  Best Regards ";
            else
                return "Dear, We are sorry, your application has been rejected because the qualifications doesn't match what we are looking for at the auc scholarship. Best Regards ";
        }

    }
}
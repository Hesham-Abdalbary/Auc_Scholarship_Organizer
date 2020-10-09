using Auc_Scholarship_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Repositories
{
    public class StudentApplicationRepository
    {
        private Context context;
        public StudentApplicationRepository()
        {
            context = new Context();
        }

        public StudentApplication GetStudentApplication(Func<StudentApplication, bool> condition)
        {
            return context.StudentApplication.Include("User").Where(condition).FirstOrDefault();
        }

        public IEnumerable<StudentApplication> GetStudentApplications(Func<StudentApplication, bool> condition)
        {
            if (condition != null)
                return context.StudentApplication.Where(condition);
            else
                return context.StudentApplication;
        }

        public int Add(StudentApplication studentApplication)
        {
            context.StudentApplication.Add(studentApplication);
            return context.SaveChanges();
        }

        public int Update(StudentApplication studentApplication)
        {
            context.StudentApplication.AddOrUpdate(studentApplication);
            return context.SaveChanges();
        }
    }
}
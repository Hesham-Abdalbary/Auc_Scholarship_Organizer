using Auc_Scholarship_Organizer.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        [PastDate(ErrorMessage = "Date should be in the past.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        public string NationalID { get; set; }
        public bool IsSystemAdmin { get; set; }
        [NotMapped]
        public HttpPostedFileBase ResumeFile { get; set; }
        public StudentApplicationDto StudentApplication { get; set; }
    }
}
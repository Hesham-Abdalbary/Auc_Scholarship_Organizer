using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public string NationalID { get; set; }
        public bool IsSystemAdmin { get; set; }
        public virtual StudentApplication StudentApplication { get; set; }
    }
}
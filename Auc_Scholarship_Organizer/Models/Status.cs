using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ApplicationStatus { get; set; }
        public ICollection<StudentApplication> StudentApplication { get; set; }
    }
}
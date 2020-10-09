using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Models
{
    public class StudentApplication
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public string University { get; set; }
        public string Major { get; set; }
        public decimal Gpa { get; set; }
        public string Resume { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
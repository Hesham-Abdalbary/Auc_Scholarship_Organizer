using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.DTOs
{
    public class StudentApplicationDto
    {
        public int Id { get; set; }
        public string University { get; set; }
        public string Major { get; set; }
        public string Gpa { get; set; }
        public string Resume { get; set; }
        public int ApprovalStatus { get; set; }
        public StatusDto Status { get; set; }
    }
}
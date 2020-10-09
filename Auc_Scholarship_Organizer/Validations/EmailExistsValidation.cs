using Auc_Scholarship_Organizer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Validations
{
    public class EmailExistsValidation : ValidationAttribute
    {
        UserService userService;
        public EmailExistsValidation()
        {
            userService = new UserService();
        }
        public override bool IsValid(object email)
        {
            return (userService.GetByEmail(email.ToString()) == null);
        }
    }
}
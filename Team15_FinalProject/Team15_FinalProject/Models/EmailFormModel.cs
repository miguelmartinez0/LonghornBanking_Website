using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your Name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your Email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Emailsubject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
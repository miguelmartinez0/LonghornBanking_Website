using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Deployment;

namespace Team15_FinalProject.Models
{
    public class Checking
    {
        public Int32 CheckingID { get; set; }

        [Required(ErrorMessage ="A checking account name is required")]
        [Display(Name = "Name")]
        public string CheckingName { get; set; }

        [Required(ErrorMessage = "A checking account balance is required")]
        [Display(Name ="Balance")]
        public Decimal CheckingBalance { get; set; }

        [Required(ErrorMessage = "A savings account status is required")]
        [Display(Name = "Is this an active account?")]
        public Boolean Status { get; set; }

        //[Required(ErrorMessage = "An account number is required")]
        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; set; }

        [Display(Name = "Secure Number")]
        public string SecureNumber { get; set; }

        public virtual AppUser Owner { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
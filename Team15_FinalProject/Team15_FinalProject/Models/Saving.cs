using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public class Saving
    {
        public Int32 SavingID { get; set; }

        [Display(Name = "Name")]
        [System.ComponentModel.DefaultValue("Longhorn Savings")]
        public string SavingName { get; set; }

        [Required(ErrorMessage = "A savings account balance is required")]
        [Display(Name = "Balance")]
        public Decimal SavingBalance { get; set; }

        [Required(ErrorMessage = "A savings account status is required")]
        [System.ComponentModel.DefaultValue(true)]
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
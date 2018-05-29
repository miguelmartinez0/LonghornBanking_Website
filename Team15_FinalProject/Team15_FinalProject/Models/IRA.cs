using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public class IRA
    {
        public Int32 IRAID { get; set; }

        [Required(ErrorMessage = "An IRA account name is required")]
        [Display(Name = "Name")]
        public string IRAName { get; set; }

        [Required(ErrorMessage = "An IRA account balance is required")]
        [Display(Name = "Balance")]
        public Decimal IRABalance { get; set; }

        [Required(ErrorMessage = "An IRA cumulative balance is required")]
        [Display(Name = "Cumulative")]
        public Decimal IRACumulative { get; set; }

        [Required(ErrorMessage = "An IRA account status is required")]
        [Display(Name = "Is this an active account?")]
        public Boolean Status { get; set; }

        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; set; }

        [Display(Name = "Secure Number")]
        public string SecureNumber { get; set; }

        public virtual AppUser Owner { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
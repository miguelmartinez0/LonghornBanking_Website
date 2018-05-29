using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public class Stock
    {
        public Int32 StockID { get; set; }

        [Required(ErrorMessage = "A stock account name is required")]
        [Display(Name = "Name")]
        public string StockName { get; set; }

        [Required(ErrorMessage = "A stock account balance is required")]
        [Display(Name = "Balance")]
        public Decimal StockBalance { get; set; }

        [Required(ErrorMessage = "A stock account status is required")]
        //[Display(Name = "Is this a active account?")]
        public Boolean Status { get; set; }

        public Boolean BalanceStatus { get; set; }

        //[Required(ErrorMessage = "An account number is required")]
        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; set; }

        [Display(Name = "Account Number")]
        public string SecureNumber { get; set; }

        public virtual List<Portfolio> Portfolios { get; set; }
        public virtual AppUser Owner { get; set; }
    }
}
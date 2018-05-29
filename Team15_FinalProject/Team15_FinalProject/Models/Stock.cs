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
        [Display(Name = "Cash-Balance")]
        public Decimal StockBalance { get; set; }

        [Display(Name = "Fees")]
        public Decimal Fees { get; set; }

        [Display(Name = "Gains")]
        public Decimal Gains { get; set; }

        [Display(Name = "Bonus")]
        public Decimal Bonus { get; set; }

        [Required(ErrorMessage = "A total stock account balance is required")]
        [Display(Name = "Total Stock Balance")]
        public Decimal TotalBalance { get; set; }

        [Required(ErrorMessage = "A stock account status is required")]
        public Boolean Status { get; set; }

        public Boolean BalanceStatus { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; set; }

        [Display(Name = "Account Number")]
        public string SecureNumber { get; set; }

        public virtual List<Portfolio> Portfolios { get; set; }
        public virtual AppUser Owner { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public class Portfolio
    {
        public Int32 PortfolioID { get; set; }

        [Required(ErrorMessage = "A stock ticker symobol is required")]
        [Display(Name = "Ticker")]
        public string TickerSymbol { get; set; }

        [Required(ErrorMessage = "A stock price is required")]
        [Display(Name = "Price")]
        public Decimal StockPrice { get; set; }

        [Display(Name = "Transaction Type")]
        public StockType StockType { get; set; }

        [Display(Name = "Share Volume")]
        public Int32 Shares { get; set; }

        public virtual Stock stock { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual AppUser Owner { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
public enum StockType { all, Ordinary_Stocks, Index_Funds, Exchange_Traded_Funds, Mutual_Funds, Futures_Shares }

namespace Team15_FinalProject.Models
{
    public class StockQuote
    {
        private static int m_Counter = 0;

        public StockQuote()
        {
            this.StockQuoteID = System.Threading.Interlocked.Increment(ref m_Counter);

        }
        [Key]
        [Required]
        public Int32 StockQuoteID { get; set; }

        [Required(ErrorMessage = "A stock symobol is required")]
        [Display(Name = "Symbol")]
        public String Symbol { get; set; }
        //[StringLength(5)]

        [Display(Name = "Stock Type")]
        public StockType StockType { get; set; }

        [Required(ErrorMessage = "A name is required")]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Previous Close")]
        public Double PreviousClose { get; set; }

        [Display(Name = "Last Trade Price")]
        public Double LastTradePrice { get; set; }

        public Double Volume { get; set; }

        [Display(Name = "Purchase Price")]
        public Decimal PurchasePrice { get; set; }

        [Required(ErrorMessage = "A purchase fee is required")]
        [Display(Name = "Purchase Fee")]
        public Decimal PurchaseFee { get; set; }
    }
}

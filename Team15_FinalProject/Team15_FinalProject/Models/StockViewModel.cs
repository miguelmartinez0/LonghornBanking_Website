using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team15_FinalProject.Models
{
    public class StockViewModel
    {
        public Int32 StockViewModelID { get; set; }
        public List<Portfolio> portfolio { get; set; }
        public Stock stock { get; set; }
        public StockViewModel() { portfolio = new List<Portfolio>();}

        public virtual AppUser Owner { get; set; }
    }
}
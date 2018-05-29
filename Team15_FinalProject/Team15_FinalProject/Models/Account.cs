using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum ProductType { checking, saving, IRA, stock}

namespace Team15_FinalProject.Models
{
    public class Account
    {
        public Int32 AccountID { get; set; }
        public Int32 ProductID { get; set; }
        public string ProductName { get; set; }
        public Int32 ProductNumber { get; set; }
        public string ProductString { get; set; }
        public Decimal ProductBalance { get; set; }
        public ProductType ProductType { get; set; }

        public virtual AppUser Owner { get; set; }
    }
}
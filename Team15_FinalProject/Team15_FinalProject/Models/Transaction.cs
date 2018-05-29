using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public enum TransactionType { all, deposit, withdrawal, transfer, fee }

namespace Team15_FinalProject.Models
{
    public class Transaction
    {
        public Int32 TransactionID { get; set; }

        [Required(ErrorMessage = "A transaction description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A transaction type is required")]
        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "A transaction amount is required")]
        public Decimal Amount { get; set; }

        [Required(ErrorMessage = "A date must be specified")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "A transaction number is required")]
        [Display(Name = "Transaction Number")]
        public Int32 TransactionNumber { get; set; }

        public Int32 OriginID { get; set; }
        public Int32 DestinationID { get; set; }

        public string Comment { get; set; }

        public virtual List<Dispute> Disputes { get; set; }
        public virtual AppUser Owner { get; set; }
        public virtual IRA IRA { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual List<Checking> Checkings { get; set; }
        public virtual List<Saving> Savings { get; set; }

        public Transaction() { Disputes = new List<Dispute>(); Checkings = new List<Checking>();Savings = new List<Saving>(); }
    }

}
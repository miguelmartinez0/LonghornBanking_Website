using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum DisputeStatus { Submitted, Accepted, Rejected, Adjusted }
public enum Delete { Yes, No }

namespace Team15_FinalProject.Models
{
    public class Dispute
    {
        [Key]
        [Required(ErrorMessage ="Dispute ID is required")]
        public Int32 DisputeID { get; set; }

        [DefaultValue("Submitted")]
        public DisputeStatus Status { get; set; }

        [Required(ErrorMessage ="Please comment as to why this transaction is being disputed")]
        [Display(Name ="Dispute Comment")]
        public string DisputeComment { get; set; }

        [Required(ErrorMessage = "Please enter the correct amount of the transaction")]
        [Display(Name ="Correct Amount")]
        public Decimal CorrectAmount { get; set; }

        [Required(ErrorMessage = "Please decide whether you think this transaction should be deleted")]
        [Display(Name ="Delete Transaction")]
        public Delete DeleteTransaction { get; set; }

        public virtual AppUser Owner { get; set; }
        public virtual Transaction Transactions { get; set; }
    }
}
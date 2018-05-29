using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team15_FinalProject.Models
{
    public class TransferViewModel
    {
        public Int32 TransferViewModelID { get; set; }

        [Required(ErrorMessage = "A transaction amount is required")]
        public Decimal Amount { get; set; }

        [Required(ErrorMessage = "An origin account is required")]
        [Display(Name ="Origin Account ID")]
        public Int32 OriginID { get; set; }

        [Required(ErrorMessage = "A destination is required")]
        [Display(Name = "Destination Account ID")]
        public Int32 DestinationID { get; set; }

        [Required(ErrorMessage = "A date must be specified")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Boolean Overdraft { get; set; }

        public Boolean Excessive { get; set; }

        public string Comment { get; set; }
    }
}
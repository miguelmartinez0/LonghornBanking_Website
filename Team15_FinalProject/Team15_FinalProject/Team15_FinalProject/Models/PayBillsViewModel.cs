using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public class PayBillsViewModel
    {
        [Key]
        public Int32 PayBillsViewModelID { get; set; }

        [Required(ErrorMessage = "Please enter the Payee Name")]
        [Display(Name = "Payee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Payee Type")]
        [Display(Name = "Payee Type")]
        public Type Type { get; set; }

        [Display(Name = "Transaction Amount")]
        public Decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Int32 ProductID { get; set; }
    }
}
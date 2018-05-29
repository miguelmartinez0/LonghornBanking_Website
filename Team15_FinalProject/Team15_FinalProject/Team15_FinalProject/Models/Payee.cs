﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public enum Type {CreditCard, Utilities, Rent, Mortgage, Other}

    public class Payee
    {
        [Key]
        [Required(ErrorMessage = "Please enter the Payee ID")]
        public Int32 PayeeID { get; set; }

        [Required(ErrorMessage = "Please enter the Payee Name")]
        [Display(Name = "Payee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Street Address")]
        [Display(Name = "Street Address")]
        public string StreeAddress { get; set; }

        [Required(ErrorMessage = "Please enter the City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the State")]
        public States State { get; set; }

        [Required(ErrorMessage = "Please enter the Zip Code")]
        [DisplayFormat(DataFormatString = "{0:#####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Zip Code")]
        public int Zip { get; set; }

        [Required(ErrorMessage = "Please enter the Phone Number")]
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0:(###)###-####}", ApplyFormatInEditMode = true)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter the Payee Type")]
        [Display(Name = "Payee Type")]
        public Type Type { get; set; }

        //Create a foreign key to connect payees to the customer
        public virtual List<AppUser> AppUsers { get; set; }
    }
}
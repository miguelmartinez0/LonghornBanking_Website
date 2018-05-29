using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team15_FinalProject.Models
{
    public class HomeViewModel
    {
        public Int32 HomeViewModelID { get; set; }
        public List<Int32> ProductIDs { get; set; }
        public List<string> ProductNames { get; set; }
        public List<Checking> checking { get; set; }
        public List<Saving> saving { get; set; }
        public IRA ira { get; set; }
        public Stock stock { get; set; }
        public HomeViewModel() { checking = new List<Checking>(); saving = new List<Saving>(); ProductIDs = new List<Int32>(); ProductNames = new List<string>(); }
    }
}
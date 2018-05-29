using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team15_FinalProject.Controllers
{
    [Authorize]
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IRATooOld()
        {
            ViewBag.ErrorMessage = "You are too old to contribute to an IRA. You must be younger than 70.";
            return View();
        }

        public ActionResult UnqualifyMax()
        {
            ViewBag.ErrorMessage = "An unqualified distribution has a maximum withdrawal amount of $3000.";
            return View();
        }

        public ActionResult InsufficientFunds()
        {
            ViewBag.ErrorMessage = "Insufficient funds to complete transaction.";
            return View();
        }

        public ActionResult ExcessiveOverdraft()
        {
            ViewBag.ErrorMessage = "Overdraft exceeds $50 limit from account balance.";
            return View();
        }

        public ActionResult IRALimit()
        {
            ViewBag.ErrorMessage = "Maximum of 1 IRA account allowed.";
            return View();
        }

        public ActionResult StockLimit()
        {
            ViewBag.ErrorMessage = "Maximum of 1 stock account allowed.";
            return View();
        }
    }
}
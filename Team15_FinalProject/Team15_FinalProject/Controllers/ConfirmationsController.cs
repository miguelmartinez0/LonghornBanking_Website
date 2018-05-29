using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team15_FinalProject.Models;

namespace Team15_FinalProject.Controllers
{
    public class ConfirmationsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Confirmations
        public ActionResult AccountSuccess()
        {
            ViewBag.ConfirmationMessage = "Account successfully created.";
            return View();
        }

        public ActionResult DisputeSuccess()
        {
            ViewBag.ConfirmationMessage = "Dispute successfully created.";
            return View();
        }

        public ActionResult PayeeSuccess()
        {
            ViewBag.ConfirmationMessage = "Payeee successfully created.";
            return View();
        }

        public ActionResult ApplyBonus()
        {
            ViewBag.ConfirmationMessage = "Balanced portfolio bonus applied.";
            return View();
        }

        public ActionResult TransferSuccess()
        {
            string Origin = "";
            string Destination = "";
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            decimal Amount = TVM.Amount;
            if (SearchSaving(TVM.OriginID))
            {
                Saving save = GetSaving(TVM.OriginID);
                Origin = save.SavingName;
            }
            if (SearchIRA(TVM.OriginID))
            {
                IRA ira = GetIRA(TVM.OriginID);
                Origin = ira.IRAName;
            }
            if (SearchChecking(TVM.OriginID))
            {
                Checking check = GetChecking(TVM.OriginID);
                Origin = check.CheckingName;
            }
            if (SearchSaving(TVM.DestinationID))
            {
                Saving save = GetSaving(TVM.DestinationID);
                Destination = save.SavingName;
            }
            if (SearchIRA(TVM.DestinationID))
            {
                IRA ira = GetIRA(TVM.DestinationID);
                Destination = ira.IRAName;
            }
            if (SearchChecking(TVM.DestinationID))
            {
                Checking check = GetChecking(TVM.DestinationID);
                Destination = check.CheckingName;
            }
            ViewBag.ConfirmationMessage = "Transfer of $" + Amount.ToString() + " from " + Origin + " to " + Destination + " successfully completed.";
            return View();
        }

        public Boolean SearchChecking(Int32 Number)
        {
            var query = from x in db.Checkings
                        select x.AccountNumber;
            List<Int32> AccountNumbers = query.ToList();
            if (AccountNumbers.Contains(Number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean SearchSaving(Int32 Number)
        {
            var query = from x in db.Savings
                        select x.AccountNumber;
            List<Int32> AccountNumbers = query.ToList();
            if (AccountNumbers.Contains(Number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean SearchIRA(Int32 Number)
        {
            var query = from x in db.IRAs
                        select x.AccountNumber;
            List<Int32> AccountNumbers = query.ToList();
            if (AccountNumbers.Contains(Number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Checking GetChecking(Int32 Number)
        {
            var query = from x in db.Checkings
                        select x;
            List<Checking> Checkings = query.ToList();
            foreach (Checking c in Checkings)
            {
                if (c.AccountNumber == Number)
                {
                    return c;
                }
            }
            return null;
        }

        public Saving GetSaving(Int32 Number)
        {
            var query = from x in db.Savings
                        select x;
            List<Saving> Savings = query.ToList();
            foreach (Saving s in Savings)
            {
                if (s.AccountNumber == Number)
                {
                    return s;
                }
            }
            return null;
        }

        public IRA GetIRA(Int32 Number)
        {
            var query = from x in db.IRAs
                        select x;
            List<IRA> IRAs = query.ToList();
            foreach (IRA i in IRAs)
            {
                if (i.AccountNumber == Number)
                {
                    return i;
                }
            }
            return null;
        }
    }
}
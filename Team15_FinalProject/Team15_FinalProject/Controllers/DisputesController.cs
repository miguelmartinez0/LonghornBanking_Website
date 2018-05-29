using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Team15_FinalProject.Models;

namespace Team15_FinalProject.Controllers
{
    [Authorize]
    public class DisputesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Disputes
        public ActionResult Index()
        {
            var query = from x in db.Disputes
                        select x;
            query = query.Where(x => x.Status == DisputeStatus.Submitted);
            if (query.Count()>0)
            {
                ViewBag.Message = "There are pending disputes!";
            }
            return View(query.ToList());
        }

        public ActionResult AllDisputes()
        {
            return View(db.Disputes.ToList());
        }

        // GET: Disputes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            AppUser person = dispute.Owner;
            Transaction transaction = dispute.Transactions;
            ViewBag.FName = person.FName;
            ViewBag.LName = person.LName;
            ViewBag.Email = person.Email;
            ViewBag.Name = transaction.TransactionNumber;
            ViewBag.Amount = transaction.Amount;
            ViewBag.Correct = dispute.CorrectAmount;
            return View(dispute);
        }

        // GET: Disputes/Create
        //[Authorize(Roles ="Customer")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disputes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisputeID,Status,DisputeComment,CorrectAmount,DeleteTransaction")] Dispute dispute, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Int32 TransID = Convert.ToInt32(id);
            Transaction transaction = new Transaction();
            var query = from x in db.Transactions
                        select x;
            query = query.Where(x => x.TransactionID == TransID);
            List<Transaction> transactions = query.ToList();
            foreach (var x in transactions)
            {
                transaction = x;
            }
            AppUser Owner = db.Users.Find(User.Identity.GetUserId());
            dispute.Owner = Owner;
            if (ModelState.IsValid)
            {
                dispute.Transactions = transaction;
                db.Disputes.Add(dispute);
                db.SaveChanges();
                //Todo: Send a message to all managers asking them to review the dispute
                ViewBag.Message = "Dispute has been successfully submitted.";
                return RedirectToAction("DisputeSuccess", "Confirmations");
            }

            ViewBag.Message = "Incomplete Submission. Please try again.";
            return View(dispute);
        }

        // GET: Disputes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        // POST: Disputes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisputeID,Status,DisputeComment,CorrectAmount,DeleteTransaction")] Dispute dispute, int? id)
        {
            Transaction trans = db.Transactions.Find(db.Disputes.Find(id));
            if (ModelState.IsValid)
            {
                dispute.Transactions = trans;
                db.Entry(dispute).State = EntityState.Modified;
                db.SaveChanges();
                //Todo: Send back to the respective details page of the transaction for which a claim has been made
                return RedirectToAction("Details", "Transaction", new { id = dispute.Transactions.TransactionID });
            }
            return View(dispute);
        }

        // GET: Disputes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        // POST: Disputes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dispute dispute = db.Disputes.Find(id);
            db.Disputes.Remove(dispute);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Resolve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        public ActionResult Adjust(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adjust([Bind(Include = "DisputeID,Status,DisputeComment,CorrectAmount,DeleteTransaction")] Dispute dispute, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction trans = db.Transactions.Find(db.Disputes.Find(id).Transactions.TransactionID);
            if (ModelState.IsValid)
            {
                db.Entry(dispute).State = EntityState.Modified;
                dispute.Status = DisputeStatus.Adjusted;
                dispute.Transactions = trans;
                db.SaveChanges();
                //Todo: Send back to the respective details page of the transaction for which a claim has been made
                return RedirectToAction("Details", "Transaction", new { id = dispute.Transactions.TransactionID });
            }
            return View(dispute);
        }

        public ActionResult Reject([Bind(Include = "DisputeID,Status,DisputeComment,CorrectAmount,DeleteTransaction")] Dispute dispute, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dispute = db.Disputes.Find(id);
            dispute.Status = DisputeStatus.Rejected;
            db.SaveChanges();
            return RedirectToAction("Details", "Transaction", new { id = dispute.Transactions.TransactionID });
        }

        public ActionResult Accept([Bind(Include = "DisputeID,Status,DisputeComment,CorrectAmount,DeleteTransaction")] Dispute dispute, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dispute = db.Disputes.Find(id);
            Transaction transaction = db.Transactions.Find(dispute.Transactions.TransactionID);
            if(transaction.TransactionType==TransactionType.deposit)
            {
                if (SearchChecking(transaction.OriginID))
                {
                    Checking check = GetChecking(transaction.OriginID);
                    check.CheckingBalance -= transaction.Amount;
                    check.CheckingBalance += dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchSaving(transaction.OriginID))
                {
                    Saving save = GetSaving(transaction.OriginID);
                    save.SavingBalance -= transaction.Amount;
                    save.SavingBalance += dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchIRA(transaction.OriginID))
                {
                    IRA ira = GetIRA(transaction.OriginID);
                    ira.IRABalance -= transaction.Amount;
                    ira.IRABalance += dispute.CorrectAmount;
                    db.SaveChanges();
                }
            }
            if (transaction.TransactionType==TransactionType.withdrawal)
            {
                if (SearchChecking(transaction.OriginID))
                {
                    Checking check = GetChecking(transaction.OriginID);
                    check.CheckingBalance += transaction.Amount;
                    check.CheckingBalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchSaving(transaction.OriginID))
                {
                    Saving save = GetSaving(transaction.OriginID);
                    save.SavingBalance += transaction.Amount;
                    save.SavingBalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchIRA(transaction.OriginID))
                {
                    IRA ira = GetIRA(transaction.OriginID);
                    ira.IRABalance += transaction.Amount;
                    ira.IRABalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
            }
            if (transaction.TransactionType==TransactionType.transfer)
            {
                if (SearchChecking(transaction.OriginID))
                {
                    Checking check = GetChecking(transaction.OriginID);
                    check.CheckingBalance += transaction.Amount;
                    check.CheckingBalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchSaving(transaction.OriginID))
                {
                    Saving save = GetSaving(transaction.OriginID);
                    save.SavingBalance += transaction.Amount;
                    save.SavingBalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchIRA(transaction.OriginID))
                {
                    IRA ira = GetIRA(transaction.OriginID);
                    ira.IRABalance += transaction.Amount;
                    ira.IRABalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchChecking(transaction.DestinationID))
                {
                    Checking check = GetChecking(transaction.DestinationID);
                    check.CheckingBalance -= transaction.Amount;
                    check.CheckingBalance += dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchSaving(transaction.DestinationID))
                {
                    Saving save = GetSaving(transaction.DestinationID);
                    save.SavingBalance -= transaction.Amount;
                    save.SavingBalance += dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchIRA(transaction.DestinationID))
                {
                    IRA ira = GetIRA(transaction.DestinationID);
                    ira.IRABalance -= transaction.Amount;
                    ira.IRABalance += dispute.CorrectAmount;
                    db.SaveChanges();
                }
            }
            if (transaction.TransactionType==TransactionType.fee)
            {
                if (SearchChecking(transaction.OriginID))
                {
                    Checking check = GetChecking(transaction.OriginID);
                    check.CheckingBalance += transaction.Amount;
                    check.CheckingBalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchSaving(transaction.OriginID))
                {
                    Saving save = GetSaving(transaction.OriginID);
                    save.SavingBalance += transaction.Amount;
                    save.SavingBalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
                if (SearchIRA(transaction.OriginID))
                {
                    IRA ira = GetIRA(transaction.OriginID);
                    ira.IRABalance += transaction.Amount;
                    ira.IRABalance -= dispute.CorrectAmount;
                    db.SaveChanges();
                }
            }
            dispute.Status = DisputeStatus.Accepted;
            if (dispute.DeleteTransaction == global::Delete.Yes)
            {
                db.Transactions.Remove(transaction);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Disputes");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

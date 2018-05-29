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
    public class SavingsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Events
        public ActionResult Index()
        {
            var query = from x in db.Savings
                        select x;
            List<Saving> Savings = query.ToList();
            List<Saving> MySavings = new List<Saving>();
            foreach (Saving x in Savings)
            {
                if (x.Owner.Id == User.Identity.GetUserId() && x.Status == true)
                {
                    MySavings.Add(x);
                }
            }
            return View(MySavings);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saving @saving = db.Savings.Find(id);
            if (@saving == null)
            {
                return HttpNotFound();
            }
            return View(@saving);
        }

        // GET: Events/Create
        //[Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SavingID,SavingName,SavingBalance,AccountNumber,Status")] Saving saving)
        {
            AppUser SavingOwner = db.Users.Find(User.Identity.GetUserId());
            saving.Owner = SavingOwner;
            Int32 NextAccNum = db.Accounts.ToList().Count() + 1000000001;
            saving.AccountNumber = NextAccNum;
            Int32 NextSecureNum = db.Accounts.ToList().Count() + 1;
            string SecureNum = "XXXXXX0000";
            if (NextSecureNum < 10)
            {
                SecureNum = "XXXXXX000" + Convert.ToString(NextSecureNum);
            }
            if (NextSecureNum < 100 && NextSecureNum >= 10)
            {
                SecureNum = "XXXXXX00" + Convert.ToString(NextSecureNum);
            }
            if (NextSecureNum < 1000 && NextSecureNum >= 100)
            {
                SecureNum = "XXXXXX0" + Convert.ToString(NextSecureNum);
            }
            if (NextSecureNum < 10000 && NextSecureNum >= 1000)
            {
                SecureNum = "XXXXXX" + Convert.ToString(NextSecureNum);
            }
            if (NextSecureNum >= 10000)
            {
                NextSecureNum -= 10000;
                SecureNum = "XXXXXX" + Convert.ToString(NextSecureNum);
            }
            saving.SecureNumber = SecureNum;

            if (ModelState.IsValid)
            {
                db.Savings.Add(saving);
                db.SaveChanges();
                Int32 ProdID = saving.SavingID;
                string ProdName = saving.SavingName;
                Int32 ProdNum = saving.AccountNumber;
                string ProdString = saving.SecureNumber;
                Decimal ProdBal = saving.SavingBalance;
                ProductType ProdType = ProductType.saving;
                Account AccountToAdd = new Account { ProductID = ProdID, ProductName = ProdName, ProductString = ProdString, ProductType = ProdType, ProductBalance = ProdBal, ProductNumber=ProdNum };
                db.Accounts.Add(AccountToAdd);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(saving);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saving @saving = db.Savings.Find(id);
            if (@saving == null)
            {
                return HttpNotFound();
            }

            return View(@saving);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SavingID,SavingName,SavingBalance,AccountNumber,Status")] Saving saving)
        {
            if (ModelState.IsValid)
            {
                Saving SavingsToChange = db.Savings.Find(saving.SavingID);

                SavingsToChange.SavingName = saving.SavingName;
                SavingsToChange.SavingBalance = saving.SavingBalance;
                SavingsToChange.Status = saving.Status;
                
                db.Entry(SavingsToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saving);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saving @savings = db.Savings.Find(id);
            if (@savings == null)
            {
                return HttpNotFound();
            }
            return View(@savings);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Saving @saving = db.Savings.Find(id);
            db.Savings.Remove(@saving);
            db.SaveChanges();
            return RedirectToAction("Index");
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

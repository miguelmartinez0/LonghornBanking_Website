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
    public class IRAsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: IRAs
        public ActionResult Index()
        {
            var query = from x in db.IRAs
                        select x;
            List<IRA> IRAs = query.ToList();
            List<IRA> MyIRAs = new List<IRA>();
            foreach (IRA x in IRAs)
            {
                if (x.Owner.Id == User.Identity.GetUserId() && x.Status == true)
                {
                    MyIRAs.Add(x);
                }
            }
            return View(MyIRAs);
        }

        // GET: IRAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRA iRA = db.IRAs.Find(id);
            if (iRA == null)
            {
                return HttpNotFound();
            }
            return View(iRA);
        }

        // GET: IRAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IRAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IRAID,IRAName,IRABalance,Status,IRANumber")] IRA ira)
        {
            AppUser IRAOwner = db.Users.Find(User.Identity.GetUserId());
            ira.Owner = IRAOwner;
            Int32 NextAccNum = db.Accounts.ToList().Count() + 1000000001;
            ira.AccountNumber = NextAccNum;
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
            ira.SecureNumber = SecureNum;

            if (ModelState.IsValid)
            {
                db.IRAs.Add(ira);
                db.SaveChanges();
                Int32 ProdID = ira.IRAID;
                string ProdName = ira.IRAName;
                Int32 ProdNum = ira.AccountNumber;
                string ProdString = ira.SecureNumber;
                Decimal ProdBal = ira.IRABalance;
                ProductType ProdType = ProductType.IRA;
                Account AccountToAdd = new Account { ProductID = ProdID, ProductName = ProdName, ProductType = ProdType, ProductNumber=ProdNum, ProductString = ProdString, ProductBalance=ProdBal };
                db.Accounts.Add(AccountToAdd);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(ira);
        }

        // GET: IRAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRA iRA = db.IRAs.Find(id);
            if (iRA == null)
            {
                return HttpNotFound();
            }
            return View(iRA);
        }

        // POST: IRAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IRAID,IRAName,IRABalance,Status,IRANumber")] IRA ira)
        {
            if (ModelState.IsValid)
            {
                IRA iraToChange = db.IRAs.Find(ira.IRAID);

                iraToChange.IRAName = ira.IRAName;
                iraToChange.IRABalance = ira.IRABalance;
                iraToChange.Status = ira.Status;

                db.Entry(iraToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(ira);
        }

        // GET: IRAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IRA iRA = db.IRAs.Find(id);
            if (iRA == null)
            {
                return HttpNotFound();
            }
            return View(iRA);
        }

        // POST: IRAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IRA iRA = db.IRAs.Find(id);
            db.IRAs.Remove(iRA);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Error()
        {
            return View();
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

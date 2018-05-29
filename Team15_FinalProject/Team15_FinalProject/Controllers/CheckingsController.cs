using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Team15_FinalProject.Models;

namespace Team15_FinalProject.Controllers
{
    [Authorize]
    public class CheckingsController : Controller
    {
        private AppUserManager _userManager;
        private AppDbContext db = new AppDbContext();

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Checking
        public ActionResult Index()
        {
            var query = from x in db.Checkings
                        select x;
            List<Checking> Checkings = query.ToList();
            List<Checking> MyCheckings = new List<Checking>();
            foreach (Checking x in Checkings)
            {
                if(x.Owner.Id==User.Identity.GetUserId())
                {
                    MyCheckings.Add(x);
                }
            }
            return View(MyCheckings);
        }

        // GET: Checking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checking check = db.Checkings.Find(id);
            if (check.Owner.Id != User.Identity.GetUserId() && !(User.IsInRole("Manager")||User.IsInRole("Employee")))
            {
                return RedirectToAction("Login", "Account");
            }
            if (check == null)
            {
                return HttpNotFound();
            }
            List<Transaction> transactions = new List<Transaction>();
            var query = from x in db.Transactions
                        select x;
            foreach (var x in query)
            {
                if(x.Checkings.Contains(check) && x.Owner.Id == User.Identity.GetUserId())
                {
                    transactions.Add(x);
                }
            }
            if (transactions.Count()>5)
            {
                var count = transactions.Count();
                transactions.Skip(count - 5);
            }
            ViewBag.transactions = transactions;
            List<Dispute> disputes = new List<Dispute>();
            var query2 = from x in db.Disputes
                        select x;
            foreach (var x in query2)
            {
                if (x.Owner.Id==User.Identity.GetUserId())
                {
                    disputes.Add(x);
                }
            }
            ViewBag.disputes = disputes;
            return View(check);
        }

        // GET: Checking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Checking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckingID,CheckingBalance,AccountNumber,Status")] Checking checking)
        {
            AppUser CheckingOwner = db.Users.Find(User.Identity.GetUserId());
            checking.Owner = CheckingOwner;
            checking.CheckingName = "Longhorn Checking";
            checking.Status = true;
            Int32 NextAccNum = db.Accounts.ToList().Count() + 1000000001;
            checking.AccountNumber = NextAccNum;
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
            checking.SecureNumber = SecureNum;

            if (ModelState.IsValid)
            {
                db.Checkings.Add(checking);
                db.SaveChanges();
                Int32 ProdID = checking.CheckingID;
                string ProdName = checking.CheckingName;
                Int32 ProdNum = checking.AccountNumber;
                string ProdString = checking.SecureNumber;
                Decimal ProdBal = checking.CheckingBalance;
                ProductType ProdType = ProductType.checking;
                Account AccountToAdd = new Account { ProductID = ProdID, ProductName = ProdName, ProductType = ProdType, ProductNumber=ProdNum, ProductString = ProdString, ProductBalance=ProdBal, Owner = CheckingOwner };
                db.Accounts.Add(AccountToAdd);
                if (User.IsInRole("User"))
                {
                    UserManager.AddToRole(User.Identity.GetUserId(), "Customer");
                }
                List<Transaction> transactions = new List<Transaction>();
                var query5 = from x in db.Transactions
                             select x;
                foreach(var x in query5)
                {
                    if (x.Owner.Id == User.Identity.GetUserId())
                    {
                        transactions.Add(x);
                    }
                }
                Int32 Count = transactions.Count() + 1;
                Transaction trans = new Transaction { Description = "Initial deposit of $" + ProdBal.ToString() + " into " + ProdName + ".", TransactionType = TransactionType.deposit, Amount = ProdBal, Date = DateTime.Now, TransactionNumber = Count, Comment = "", OriginID = ProdNum, Owner = CheckingOwner };
                db.Transactions.Add(trans);
                db.SaveChanges();
                return RedirectToAction("AccountSuccess", "Confirmations");
            }

            return View(checking);
        }

        // GET: Checking/Edit/5
        [Authorize(Roles = "Customer")]
        public ActionResult CustomerEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checking @checking = db.Checkings.Find(id);
            if (@checking == null)
            {
                return HttpNotFound();
            }
            return View(@checking);
        }

        // POST: Checking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerEdit([Bind(Include = "CheckingID,CheckingName,CheckingBalance,AccountNumber,Status")] Checking checking)
        {
            if (checking.CheckingName==""||checking.CheckingName==null)
            {
                checking.CheckingName = "Longhorn Checking";
                checking.Status = true;
            }
            if (ModelState.IsValid)
            {
                Checking checkingToChange = db.Checkings.Find(checking.CheckingID);

                checkingToChange.CheckingName = checking.CheckingName;
                checkingToChange.CheckingBalance = checking.CheckingBalance;
                checkingToChange.Status = checking.Status;

                db.Entry(checkingToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(checking);

        }
        // GET: Checking/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult ManagerEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checking @checking = db.Checkings.Find(id);
            if (@checking == null)
            {
                return HttpNotFound();
            }
            return View(@checking);
        }

        // POST: Checking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult ManagerEdit([Bind(Include = "CheckingID,CheckingName,CheckingBalance,AccountNumber,Status")] Checking checking)
        {
            if (checking.CheckingName == "" || checking.CheckingName == null)
            {
                checking.CheckingName = "Longhorn Checking";
                checking.Status = true;
            }
            if (ModelState.IsValid)
            {
                Checking checkingToChange = db.Checkings.Find(checking.CheckingID);

                checkingToChange.CheckingName = checking.CheckingName;
                checkingToChange.CheckingBalance = checking.CheckingBalance;
                checkingToChange.Status = checking.Status;

                db.Entry(checkingToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(checking);
        }

        // GET: Checking/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Checking @checking = db.Checkings.Find(id);
            if (@checking == null)
            {
                return HttpNotFound();
            }
            return View(@checking);
        }

        // POST: Checking/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Checking @checking = db.Checkings.Find(id);
            db.Checkings.Remove(@checking);
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

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
    public class StocksController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Stocks
        public ActionResult Index()
        {
            var query = from x in db.Stocks
                        select x;
            List<Stock> Stocks = query.ToList();
            List<Stock> MyStocks = new List<Stock>();
            foreach (Stock x in Stocks)
            {
                if (x.Owner.Id == User.Identity.GetUserId() && x.Status == true)
                {
                    MyStocks.Add(x);
                }
            }
            return View(MyStocks);
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        //
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockID,StockName,StockBalance,BalanceStatus,AccountNumber,SecureNumber,StockType")] Stock stock)
        {
            AppUser StockOwner = db.Users.Find(User.Identity.GetUserId());
            stock.Owner = StockOwner;
            Int32 NextAccNum = db.Accounts.ToList().Count() + 1000000001;
            stock.AccountNumber = NextAccNum;
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
            stock.SecureNumber = SecureNum;

            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                Int32 ProdID = stock.StockID;
                string ProdName = stock.StockName;
                Int32 ProdNum = stock.AccountNumber;
                string ProdString = stock.SecureNumber;
                Decimal ProdBal = stock.StockBalance;
                ProductType ProdType = ProductType.IRA;
                Account AccountToAdd = new Account { ProductID = ProdID, ProductName = ProdName, ProductType = ProdType, ProductNumber = ProdNum, ProductString = ProdString, ProductBalance = ProdBal };
                db.Accounts.Add(AccountToAdd);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockID,StockName,StockBalance,BalanceStatus,AccountNumber,SecureNumber,StockType")] Stock stock)
        {
            Stock stockToChange = db.Stocks.Find(stock.StockID);

            stockToChange.StockName = stock.StockName;
            stockToChange.StockBalance = stock.StockBalance;
            stockToChange.Status = stock.Status;

            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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

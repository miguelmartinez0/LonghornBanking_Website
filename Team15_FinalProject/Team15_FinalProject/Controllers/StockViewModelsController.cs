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
    public class StockViewModelsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: StockViewModels
        public ActionResult Index()
        {
            Int32 CurrentSVM = 1;

            if (db.StockViewModels.Find(CurrentSVM) == null)
            {
                StockViewModel svm = new StockViewModel();
                var query = from x in db.Portfolios
                            select x;
                foreach (var x in query)
                {
                    if (svm.portfolio==null)
                    {
                        svm.portfolio = new List<Portfolio>();
                    }
                    svm.portfolio.Add(x);
                }
                var query2 = from x in db.Stocks
                             select x;
                foreach (var x in query2)
                {
                    if (svm.stock==null)
                    {
                        svm.stock = new Stock();
                    }
                    svm.stock = x;
                }
                db.StockViewModels.Add(svm);
            }
            else
            {
                StockViewModel svm = db.StockViewModels.Find(CurrentSVM);
                ClearSVM(svm);
                var query = from x in db.Portfolios
                            select x;
                foreach (var x in query)
                {
                    if(!svm.portfolio.Contains(x) && x.Owner.Id==User.Identity.GetUserId())
                    {
                        svm.portfolio.Add(x);
                    }
                }
                var query2 = from x in db.Stocks
                             select x;
                foreach (var y in query2)
                {
                    if (svm.stock!=y && y.Owner.Id==User.Identity.GetUserId() && y.Status==true)
                    {
                        svm.stock = y;
                    }
                }
                db.SaveChanges();
            }
            return View(db.StockViewModels.ToList());
        }

        // GET: StockViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockViewModel stockViewModel = db.StockViewModels.Find(id);
            if (stockViewModel == null)
            {
                return HttpNotFound();
            }
            return View(stockViewModel);
        }

        // GET: StockViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockViewModelID")] StockViewModel stockViewModel)
        {
            if (ModelState.IsValid)
            {
                db.StockViewModels.Add(stockViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockViewModel);
        }

        // GET: StockViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockViewModel stockViewModel = db.StockViewModels.Find(id);
            if (stockViewModel == null)
            {
                return HttpNotFound();
            }
            return View(stockViewModel);
        }

        // POST: StockViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockViewModelID")] StockViewModel stockViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockViewModel);
        }

        // GET: StockViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockViewModel stockViewModel = db.StockViewModels.Find(id);
            if (stockViewModel == null)
            {
                return HttpNotFound();
            }
            return View(stockViewModel);
        }

        // POST: StockViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockViewModel stockViewModel = db.StockViewModels.Find(id);
            db.StockViewModels.Remove(stockViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ClearSVM(StockViewModel svm)
        {
            svm.portfolio.Clear();
            svm.stock = null;
            db.StockViewModels.Find(svm.StockViewModelID).portfolio.Clear();
            db.StockViewModels.Find(svm.StockViewModelID).stock = null;
            db.SaveChanges();
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

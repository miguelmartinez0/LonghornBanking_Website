using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team15_FinalProject.Models;

namespace Team15_FinalProject.Controllers
{
    public class DisputesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Disputes
        public ActionResult Index()
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
            return View(dispute);
        }

        // GET: Disputes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disputes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisputeID,Status,DisputeComment,CorrectAmount,DeleteTransaction")] Dispute dispute)
        {
            if (ModelState.IsValid)
            {
                db.Disputes.Add(dispute);
                db.SaveChanges();
                //Todo: Send a message to all managers asking them to review the dispute
                return RedirectToAction("Index");
            }
            
            //Todo: Create an error message if all of the values have not been passed for a create form
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
        public ActionResult Edit([Bind(Include = "DisputeID,Status,DisputeComment,CorrectAmount,DeleteTransaction")] Dispute dispute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dispute).State = EntityState.Modified;
                db.SaveChanges();
                //Todo: Send back to the respective details page of the transaction for which a claim has been made
                return RedirectToAction("Details", "Transaction");
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

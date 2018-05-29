using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team15_FinalProject.Models;
using Team15_FinalProject.StockUtilities;

namespace Team15_FinalProject.Controllers
{
    public class StockQuoteController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Home
        public ActionResult Index()
        {
            List<StockQuote> quotes = new List<StockQuote>();
            var query = from x in db.StockQuotes
                        select x;
            foreach (var x in query)
            {
                quotes.Add(x);
            }
            return View(quotes);
        }

        [Authorize (Roles ="Manager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockQuoteID,Symbol,StockType,Name,PreviousClose,LastTradePrice,Volume,PurchasePrice,PurchaseFee")] StockQuote stockquote)
        {
            if (ModelState.IsValid)
            {
                db.StockQuotes.Add(stockquote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockquote);
        }

    }
}
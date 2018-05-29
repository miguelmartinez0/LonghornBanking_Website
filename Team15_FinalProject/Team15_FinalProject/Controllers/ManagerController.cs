using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Team15_FinalProject.Models;

namespace Team15_FinalProject.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // Create the different search methods
        public enum SearchMethod {ViewAll}
      
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        //Allow the manager to resolved any disputes that they want to
        public ActionResult ResolveDisputes(SearchMethod SelectedSearch)
        {
            var query = from c in db.Disputes
                        select c;

            //Default showing of only disputes that have yet to be decided on
            if (SelectedSearch == 0) //they didn't select anything, ONLY Not resolved disputes
            {
                return RedirectToAction("Index", "Disputes");
            }

            else //they didn't pick show all and should only see the disputes yet to be decided on 
            {
                query = query.Where(c => c.Status == 0);
                return RedirectToAction("Index", "Disputes");
            }
        }

        //Allow the manager to create employees
        public ActionResult ManageEmployee()
        {
            return View();
        }

        //Allow the manager to add the balance portfolio balance
        public void AddPortfolioBonus()
        {
            Int32 NextTransNum = db.Transactions.Count() + 1;
            foreach (var x in db.Stocks)
                {
                if (x.BalanceStatus == true)
                    {
                    x.Bonus = x.StockBalance/10;
                    AppUser owner = db.Users.Find(User.Identity.GetUserId());
                    Transaction Gains = new Transaction { Description = "Balanced Portfolio Bonus", TransactionType = TransactionType.deposit, Amount = x.Bonus, Date = DateTime.Now, TransactionNumber = NextTransNum, Comment = " ", OriginID = x.StockID, Owner = owner };
                    RedirectToAction("ApplyBonus", "Confirmations");
                }
            }
        }
        //Allow the manager to add new stocks
        public ActionResult AddNewStock()
        {
            return View();
        }
    }
}
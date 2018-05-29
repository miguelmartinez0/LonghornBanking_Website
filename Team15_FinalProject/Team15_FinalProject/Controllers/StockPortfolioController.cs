using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Team15_FinalProject.Models;

namespace Team15_FinalProject.Controllers
{
    public class StockPortfolioController : Controller
    {
        //Int32 CurrentSVM = 1;
        public static int myId = 0;
        private AppDbContext db = new AppDbContext();
        // GET: StockPortfolio
        public ActionResult Index()
        {
            StockViewModel vm = new StockViewModel();
            var query = from x in db.StockViewModels
                        select x;
            query = query.Where(x => x.Owner.Id == User.Identity.GetUserId());
            List<StockViewModel> StockViewModels = query.ToList();
            foreach (StockViewModel x in StockViewModels)
            {
                vm = x;
            }
            ClearSVM(vm);
            Merge();
            List<Portfolio > PortfoliosToAdd = new List<Portfolio>();
            var quer = from c in db.Portfolios
                        select c;
            foreach (var y in quer)
            {
                if (vm.portfolio == null)
                {
                    vm.portfolio = new List<Portfolio>();
                }
                vm.portfolio.Add(y);

            }
            var query2 = from i in db.Stocks
                            select i;
            foreach (var z in query2)
            {
                if (vm.stock == null)
                {
                    vm.stock = new Stock();
                }
                vm.stock = z;
            }
            CalculateTBalance(vm);
            IsBalanced(vm);
            db.StockViewModels.Add(vm);
            db.SaveChanges();

            return View(db.StockViewModels);
        }
        public void ClearSVM(StockViewModel vm)
        {
            vm.portfolio.Clear();
            vm.stock = null;
        }
        public SelectList GetSomeProducts()
        {
            var query = from y in db.Accounts
                        select y;
            query = query.Where(x => x.ProductType != ProductType.IRA);
            List<Account> someProducts = new List<Account>();
            foreach (var x in query)
            {
                if (x.Owner.Id == User.Identity.GetUserId())
                {
                    someProducts.Add(x);
                }

            }

            SelectList someProductslist = new SelectList(someProducts, "ProductNumber", "ProductName");

            return someProductslist;
        }
        public void CalculateTBalance(StockViewModel vm)
        {
            Decimal Total;
            Total = vm.stock.Bonus + vm.stock.StockBalance + vm.stock.Gains - vm.stock.Fees;
            foreach (var item in vm.portfolio)
            {
                Total += item.StockPrice * item.Shares;
            }
            vm.stock.TotalBalance = Total;
            db.Stocks.Find(vm.StockViewModelID).TotalBalance = Total;
            db.SaveChanges();
        }
        public void IsBalanced(StockViewModel vm)
        {
            int a;
            int b;
            int c;
            a = 0;
            b = 0;
            c = 0;
            foreach (var item in vm.portfolio)
            {
                if (item.StockType == StockType.Ordinary_Stocks)
                {
                    a += 1;
                }
                if (item.StockType == StockType.Index_Funds)
                {
                    b += 1;
                }
                if (item.StockType == StockType.Mutual_Funds)
                {
                    c += 1;
                }
                if (a >= 2 && b >= 1 && c >= 1)
                {
                    vm.stock.BalanceStatus = true;
                    break;
                }
            }
        }
        public ActionResult BuyStock(int id)
        {
            myId = id;
            ViewBag.SomeProducts = GetSomeProducts();
            return View();
        }
        [HttpPost]
        public ActionResult BuyStock([Bind(Include = "ProductID,ProductName,ProductType,ProductNumber,ProductBalance")] Account product, int? DesProductID, int Shares)
        {         
            int DesProductId = Convert.ToInt32(DesProductID);
            Double Amounts = Convert.ToDouble(Shares) * db.StockQuotes.Find(myId).PreviousClose;
            Decimal Amount = Convert.ToDecimal(Amounts) + db.StockQuotes.Find(myId).PurchaseFee;
            int DesAmounts = Convert.ToInt32(Shares);

            if (Shares <= 0)
            {
                ViewBag.ErrorMessage = "Invalid amount entered. Please enter a positive number of stocks.";
                ViewBag.SomeProducts = GetSomeProducts();
                return View();
            }

            product.ProductNumber = DesProductId;

            if (SearchChecking(product.ProductNumber) == true)
            {
                product.ProductType = ProductType.checking;
                Checking check = GetChecking(product.ProductNumber);
                if (Amount > check.CheckingBalance)
                {
                    ViewBag.ErrorMessage = "Withdrawal amount exceeds account balance.";
                    ViewBag.SomeProducts = GetSomeProducts();
                    return View();
                }
            }
            if (SearchSaving(product.ProductNumber) == true)
            {
                product.ProductType = ProductType.saving;
                Saving save = GetSaving(product.ProductNumber);
                if (Amount > save.SavingBalance)
                {
                    ViewBag.ErrorMessage = "Withdrawal amount exceeds account balance.";
                    ViewBag.SomeProducts = GetSomeProducts();
                    return View();
                }
            }
            if (SearchStock(product.ProductNumber) == true)
            {
                product.ProductType = ProductType.stock;
                Stock stock = GetStock(product.ProductNumber);
                if (Amount > stock.StockBalance)
                {
                    ViewBag.ErrorMessage = "Withdrawal amount exceeds account balance.";
                    ViewBag.SomeProducts = GetSomeProducts();
                    return View();
                }
            }

            if (ModelState.IsValid)
            {
                if (product.ProductType == ProductType.checking)
                {
                    Checking check = GetChecking(product.ProductNumber);
                    check.CheckingBalance -= Amount;
                    db.Portfolios.Find(myId).Shares += DesAmounts;
                    db.SaveChanges();
                    product.ProductName = check.CheckingName;
                }
                if (product.ProductType == ProductType.saving)
                {
                    Saving save = GetSaving(product.ProductNumber);
                    save.SavingBalance -= Amount;
                    db.Portfolios.Find(myId).Shares += DesAmounts;
                    db.SaveChanges();
                    product.ProductName = save.SavingName;
                }
                if (product.ProductType == ProductType.stock)
                {
                    Stock stock = GetStock(product.ProductNumber);
                    stock.StockBalance -= Amount;
                    db.Portfolios.Find(myId).Shares += DesAmounts;
                    db.SaveChanges();
                    product.ProductName = stock.StockName;

                }
                Int32 NextTransNum = NextNum();
                AppUser owner = db.Users.Find(User.Identity.GetUserId());
                Transaction fee = new Transaction { Description = "Fee for purchase of" + product.ProductName + ".", TransactionType = TransactionType.fee, Amount = db.StockQuotes.Find(myId).PurchaseFee, Date = DateTime.Now, TransactionNumber = NextTransNum, Comment = " ", OriginID = product.ProductNumber, Owner = owner };
                db.Transactions.Add(fee);
                Transaction withdrawals = new Transaction { Description = "“Stock Purchase – Account" + product.ProductID, TransactionType = TransactionType.withdrawal, Amount = DesAmounts, Date = DateTime.Now, TransactionNumber = NextTransNum, Comment = " ", OriginID = product.ProductNumber, Owner = owner };
                db.Transactions.Add(withdrawals);
                db.SaveChanges();
                ViewBag.SomeProducts = GetSomeProducts();
                return View();
            }
            return View();
        }
        public void Merge()
        {
            Portfolio port = new Portfolio();
            foreach (var x in db.StockQuotes) 
            {
                port.StockPrice = Convert.ToDecimal(x.LastTradePrice);
                port.TickerSymbol = x.Symbol;
                port.StockType = x.StockType;
                db.Portfolios.Add(port);
                db.SaveChanges();
            }
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

        public Boolean SearchStock(Int32 Number)
        {
            var query = from x in db.Stocks
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

        public Stock GetStock(Int32 Number)
        {
            var query = from x in db.Stocks
                        select x;
            List<Stock> Stocks = query.ToList();
            foreach (Stock i in Stocks)
            {
                if (i.AccountNumber == Number)
                {
                    return i;
                }
            }
            return null;
        }

        public Int32 NextNum()
        {
            List<Transaction> transactions = new List<Transaction>();
            var query = from x in db.Transactions
                        select x;
            foreach (var x in query)
            {
                if (x.Owner.Id == User.Identity.GetUserId())
                {
                    transactions.Add(x);
                }
            }
            Int32 Count = transactions.Count() + 1;
            return Count;
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


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

public enum SearchAmount { All, ZeroToOne, OnetoTwo, TwoToThree, ThreePlus, Custom }
public enum SearchDate { AllAvailable, Last15, Last30, Last60, Custom }
public enum Sort { NumAsc, NumDesc, TypeAsc, TypeDesc, DescAsc, DescDesc, AmountAsc, AmountDesc, DateAsc, DateDesc }

namespace Team15_FinalProject.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            ViewBag.AllTransactionTypes = GetAllTransactionTypes();
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
            return View(transactions);
        }

        // GET: Transactions
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeIndex()
        {
            ViewBag.AllTransactionTypes = GetAllTransactionTypes();
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
            return View(transactions);
        }

        // GET: Transactions/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            foreach (var x in transaction.Disputes)
            {
                if (x.DisputeComment==null||x.DisputeComment=="")
                {
                    ViewBag.DisputeComment = "No dispute/No comment.";
                }
                ViewBag.DisputeComment = x.DisputeComment;
            }
            List<Transaction> transactions = new List<Transaction>();
            var query = from x in db.Transactions
                        select x;
            foreach (var x in query)
            {
                if (x.TransactionType==transaction.TransactionType && x.Owner.Id==User.Identity.GetUserId())
                {
                    transactions.Add(x);
                }
            }
            if (transactions.Count() > 5)
            {
                var count = transactions.Count();
                transactions.Skip(count - 5);
            }
            ViewBag.transactions = transactions;
            return View(transaction);
        }

        public ActionResult SearchResults(String DescString, TransactionType? SelectedType, SearchAmount? SelectedAmount, String MinAmount, String MaxAmount, String NumberString, SearchDate? SelectedDate, String Early, String Late, Sort? SelectedSort)
        {
            var query = from t in db.Transactions
                        select t;
            if (DescString == null || DescString == "")
            {
                ViewBag.DescString = "";
            }
            else
            {
                query = query.Where(t => t.Description.Contains(DescString));
            }
            
            switch (SelectedType)
            {
                case TransactionType.deposit:
                    query = query.Where(t => t.TransactionType == TransactionType.deposit);
                    break;
                case TransactionType.transfer:
                    query = query.Where(t => t.TransactionType == TransactionType.transfer);
                    break;
                case TransactionType.withdrawal:
                    query = query.Where(t => t.TransactionType == TransactionType.withdrawal);
                    break;
                default:
                    break;
            }
            
            switch (SelectedAmount)
            {
                case SearchAmount.ZeroToOne:
                    query = query.Where(t => t.Amount >= 0 && t.Amount <= 100);
                    break;
                case SearchAmount.OnetoTwo:
                    query = query.Where(t => t.Amount >= 100 && t.Amount <= 200);
                    break;
                case SearchAmount.TwoToThree:
                    query = query.Where(t => t.Amount >= 200 && t.Amount <= 300);
                    break;
                case SearchAmount.ThreePlus:
                    query = query.Where(t => t.Amount >= 300);
                    break;
                case SearchAmount.Custom:
                    if ((MinAmount == null || MinAmount == "") && (MaxAmount == null || MaxAmount == ""))
                    {
                        ViewBag.MinMessage = "";
                        ViewBag.MaxMessage = "";
                    }
                    if (!(MinAmount == null || MinAmount == "") && (MaxAmount == null || MaxAmount == ""))
                    {
                        ViewBag.MaxMessage = "";
                        var Min = Convert.ToDecimal(MinAmount);
                        query = query.Where(t => t.Amount >= Min);
                    }
                    if ((MinAmount == null || MinAmount == "") && !(MaxAmount == null || MaxAmount == ""))
                    {
                        ViewBag.MinMessage = "";
                        var Max = Convert.ToDecimal(MaxAmount);
                        query = query.Where(t => t.Amount <= Max);
                    }
                    else
                    {
                        var Min = Convert.ToDecimal(MinAmount);
                        var Max = Convert.ToDecimal(MaxAmount);
                        query = query.Where(t => t.Amount >= Min);
                        query = query.Where(t => t.Amount <= Max);
                    }
                    break;
                default:
                    ViewBag.SelectedType = "No search amount selected";
                    break;
            }
            
            switch (SelectedDate)
            {
                case SearchDate.Last15:
                    var Fifteen = (DateTime.Today).AddDays(-15);
                    query = query.Where(t => t.Date >= Fifteen);
                    break;
                case SearchDate.Last30:
                    var Thirty = (DateTime.Today).AddDays(-30);
                    query = query.Where(t => t.Date >= Thirty);
                    break;
                case SearchDate.Last60:
                    var Sixty = (DateTime.Today).AddDays(-60);
                    query = query.Where(t => t.Date >= Sixty);
                    break;
                case SearchDate.Custom:
                    if ((Early == null || Early == "") && (Late == null || Late == ""))
                    {
                        ViewBag.EarlyMessage = "";
                        ViewBag.LateMessage = "";
                    }
                    if (!(Early == null || Early == "") && (Late == null || Late == ""))
                    {
                        ViewBag.LateMessage = "";
                        var Min = Convert.ToDouble(Early);
                        var First = (DateTime.Today).AddDays(Min * -1);
                        query = query.Where(t => t.Date <= First);
                    }
                    if ((Early == null || Early == "") && !(Late == null || Late == ""))
                    {
                        ViewBag.EarlyMessage = "";
                        var Max = Convert.ToDouble(Late);
                        var Last = (DateTime.Today).AddDays(Max * -1);
                        query = query.Where(t => t.Date >= Last);
                    }
                    else
                    {
                        var Small = Convert.ToDecimal(Early);
                        var Large = Convert.ToDecimal(Late);
                        var Min = Convert.ToDouble(Small);
                        var Max = Convert.ToDouble(Large);
                        var First = (DateTime.Today).AddDays(Min * -1);
                        var Last = (DateTime.Today).AddDays(Max * -1);
                        query = query.Where(t => t.Date <= First);
                        query = query.Where(t => t.Date >= Last);
                    }
                    break;
                default:
                    ViewBag.SelectedAmount = "No search amount selected";
                    break;
            }
            if (NumberString == null || NumberString == "")
            {
                ViewBag.NumberString = "";
            }
            else
            {
                query = query.Where(t => t.TransactionNumber.Equals(NumberString));
            }
            switch (SelectedSort)
            {
                case Sort.NumAsc:
                    query = query.OrderBy(t => t.TransactionNumber);
                    break;
                case Sort.NumDesc:
                    query = query.OrderByDescending(t => t.TransactionNumber);
                    break;
                case Sort.TypeAsc:
                    query = query.OrderBy(t => t.TransactionType);
                    break;
                case Sort.TypeDesc:
                    query = query.OrderByDescending(t => t.TransactionType);
                    break;
                case Sort.DescAsc:
                    query = query.OrderBy(t => t.Description);
                    break;
                case Sort.DescDesc:
                    query = query.OrderByDescending(t => t.Description);
                    break;
                case Sort.AmountAsc:
                    query = query.OrderBy(t => t.Amount);
                    break;
                case Sort.AmountDesc:
                    query = query.OrderByDescending(t => t.Amount);
                    break;
                case Sort.DateAsc:
                    query = query.OrderBy(t => t.Date);
                    break;
                case Sort.DateDesc:
                    query = query.OrderByDescending(t => t.Date);
                    break;
                default:
                    query = query.OrderBy(t => t.TransactionNumber).ThenBy(t => t.TransactionType).ThenBy(t => t.Description).ThenBy(t => t.Amount).ThenBy(t => t.Date);
                    break;
            }
            List<Transaction> SelectedTransactions = query.ToList();
            List<Transaction> TotalTransactions = new List<Transaction>();
            TotalTransactions = db.Transactions.ToList();
            ViewBag.Show = "Showing " + SelectedTransactions.Count() + " of " + TotalTransactions.Count() + " Transactions";
            ViewBag.AllTransactionTypes = GetAllTransactionTypes();
            return View(SelectedTransactions);
        }

        public SelectList GetAllTransactionTypes()
        {
            List<TransactionKind> TransactionKinds = db.TransactionKinds.ToList();
            TransactionKind SelectNone = new Models.TransactionKind() { TransactionKindID = 0, Name = "all transaction types" };
            TransactionKinds.Add(SelectNone);
            db.SaveChanges();
            SelectList AllTransactions = new SelectList(TransactionKinds, "TransactionKindID", "Name", 0);
            return AllTransactions;
        }

        // GET: Transactions/Deposit
        [Authorize]
        public ActionResult Deposit()
        {
            ViewBag.AllProducts = GetAllProducts();
            return View();
        }

        //POST: Transactions/Deposit
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind(Include = "ProductID,ProductName,ProductType,ProductNumber,ProductString,ProductBalance")] Account product, int DesProductID, decimal DesAmount, string DesComment, DateTime desdate)
        {
            if (ModelState.IsValid)
            {
                if (DesAmount <= 0)
                {
                    ViewBag.ErrorMessage = "Invalid amount entered. Please enter a positive amount to deposit.";
                    ViewBag.AllProducts = GetAllProducts();
                    return View();
                }

                product.ProductNumber = DesProductID;
                if (SearchChecking(product.ProductNumber)==true)
                {
                    product.ProductType = ProductType.checking;
                }
                if (SearchSaving(product.ProductNumber) == true)
                {
                    product.ProductType = ProductType.saving;
                }
                if (SearchIRA(product.ProductNumber) == true)
                {
                    product.ProductType = ProductType.IRA;
                }

                if (product.ProductType == ProductType.checking)
                {
                    Checking check = GetChecking(product.ProductNumber);
                    check.CheckingBalance += DesAmount;
                    db.SaveChanges();
                    product.ProductName = check.CheckingName;
                }
                if (product.ProductType == ProductType.saving)
                {
                    Saving save = GetSaving(product.ProductNumber);
                    save.SavingBalance += DesAmount;
                    db.SaveChanges();
                    product.ProductName = save.SavingName;
                }
                if (product.ProductType==ProductType.IRA)
                {
                    IRA ira = GetIRA(product.ProductNumber);
                    var today = DateTime.Today;
                    var age = today.Year - ira.Owner.Birthday.Year;
                    Decimal NewCumulative = ira.IRACumulative + DesAmount;
                    if (age>70||NewCumulative>=5000)
                    {
                        return RedirectToAction("IRATooOld", "Errors");
                    }
                    else
                    {
                        ira.IRABalance += DesAmount;
                        ira.IRACumulative += DesAmount;
                        db.SaveChanges();
                        product.ProductName = ira.IRAName;
                    }
                }
                Int32 NextTransNum = NextNum();
                if (DesComment == null || DesComment == "")
                {
                    DesComment = "No comment entered.";
                }
                AppUser owner = db.Users.Find(User.Identity.GetUserId());
                Transaction trans = new Transaction { Description = "Deposit of $" + DesAmount + " into " + product.ProductName + ".", TransactionType = TransactionType.deposit, Amount = DesAmount, Date = desdate, TransactionNumber = NextTransNum, Comment = DesComment, OriginID=product.ProductNumber, Owner = owner };
                if (product.ProductType==ProductType.checking)
                {
                    Checking check = GetChecking(product.ProductNumber);
                    trans.Checkings.Add(check);
                }
                if (product.ProductType==ProductType.saving)
                {
                    Saving save = GetSaving(product.ProductNumber);
                    trans.Savings.Add(save);
                }
                if (product.ProductType == ProductType.IRA)
                {
                    IRA ira = GetIRA(product.ProductNumber);
                    trans.IRA = ira;
                }
                db.Transactions.Add(trans);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AllProducts = GetAllProducts();
            return View();
        }

        // GET: Transactions/Withdrawal
        [Authorize]
        public ActionResult Withdrawal()
        {
            ViewBag.AllProducts = GetAllProducts();
            return View();
        }

        // GET: Transactions/Withdrawal
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Withdrawal([Bind(Include = "ProductID,ProductName,ProductType,ProductNumber,ProductBalance")] Account product, int DesProductID, decimal DesAmount, string DesComment, DateTime desdate)
        {
            if (DesAmount <= 0)
            {
                ViewBag.ErrorMessage = "Invalid amount entered. Please enter a positive amount to withdraw.";
                ViewBag.AllProducts = GetAllProducts();
                return View();
            }

            product.ProductNumber = DesProductID;
            if (SearchIRA(product.ProductNumber)==true)
            {
                product.ProductType = ProductType.IRA;
                IRA ira = GetIRA(product.ProductNumber);
                if (DesAmount > ira.IRABalance)
                {
                    ViewBag.ErrorMessage = "Withdrawal amount exceeds account balance.";
                    ViewBag.AllProducts = GetAllProducts();
                    return View();
                }
            }
            if (SearchChecking(product.ProductNumber)==true)
            {
                product.ProductType = ProductType.checking;
                Checking check = GetChecking(product.ProductNumber);
                if (DesAmount > check.CheckingBalance)
                {
                    ViewBag.ErrorMessage = "Withdrawal amount exceeds account balance.";
                    ViewBag.AllProducts = GetAllProducts();
                    return View();
                }
            }
            if (SearchSaving(product.ProductNumber)==true)
            {
                product.ProductType = ProductType.saving;
                Saving save = GetSaving(product.ProductNumber);
                if (DesAmount > save.SavingBalance)
                {
                    ViewBag.ErrorMessage = "Withdrawal amount exceeds account balance.";
                    ViewBag.AllProducts = GetAllProducts();
                    return View();
                }
            }

            if (ModelState.IsValid)
            {
                if (product.ProductType == ProductType.checking)
                {
                    Checking check = GetChecking(product.ProductNumber);
                    check.CheckingBalance -= DesAmount;
                    db.SaveChanges();
                    product.ProductName = check.CheckingName;
                }
                if (product.ProductType == ProductType.saving)
                {
                    Saving save = GetSaving(product.ProductNumber);
                    save.SavingBalance -= DesAmount;
                    db.SaveChanges();
                    product.ProductName = save.SavingName;
                }
                if (product.ProductType == ProductType.IRA)
                {
                    IRA ira = GetIRA(product.ProductNumber);
                    var today = DateTime.Today;
                    var age = today.Year - ira.Owner.Birthday.Year;
                    if (age > 65)
                    {
                        ira.IRABalance -= DesAmount;
                        db.SaveChanges();
                        product.ProductName = ira.IRAName;
                        Int32 NextTransNumber = NextNum()
                            ;
                        if (DesComment == null || DesComment == "")
                        {
                            DesComment = "No comment entered.";
                        }
                        AppUser person = db.Users.Find(User.Identity.GetUserId());
                        Transaction transact = new Transaction { Description = "Withdrawal of $" + DesAmount + " from " + product.ProductName + ".", TransactionType = TransactionType.withdrawal, Amount = DesAmount, Date = desdate, TransactionNumber = NextTransNumber, Comment = DesComment, OriginID = product.ProductNumber, Owner = person };
                        db.Transactions.Add(transact);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    Int32 origin = 0;
                    Int32 destination = product.ProductNumber;
                    Decimal amount = Convert.ToDecimal(DesAmount);
                    DateTime date = Convert.ToDateTime(desdate);

                    if (amount>3000)
                    {
                        return RedirectToAction("UnqualifyMax", "Errors");
                    }

                    if (db.TransferViewModels.Find(1) == null)
                    {
                        TransferViewModel TVM = new TransferViewModel { OriginID = origin, DestinationID = destination, Amount = amount, Comment = DesComment, Date = date };
                        db.TransferViewModels.Add(TVM);
                        db.SaveChanges();
                        return RedirectToAction("IRAWithdraw", "Transactions");
                    }
                    else
                    {
                        TransferViewModel TVM = db.TransferViewModels.Find(1);
                        TVM.OriginID = origin;
                        TVM.DestinationID = destination;
                        TVM.Amount = amount;
                        TVM.Comment = DesComment;
                        TVM.Date = date;
                        db.SaveChanges();
                        return RedirectToAction("IRAWithdraw", "Transactions");
                    }
                }
                Int32 NextTransNum = NextNum();
                if (DesComment == null || DesComment == "")
                {
                    DesComment = "No comment entered.";
                }
                AppUser owner = db.Users.Find(User.Identity.GetUserId());
                Transaction trans = new Transaction { Description = "Withdrawal of $" + DesAmount + " into " + product.ProductName + ".", TransactionType = TransactionType.withdrawal, Amount = DesAmount, Date = desdate, TransactionNumber = NextTransNum, Comment = DesComment, OriginID = product.ProductNumber, Owner = owner };
                if (product.ProductType == ProductType.checking)
                {
                    Checking check = GetChecking(product.ProductNumber);
                    trans.Checkings.Add(check);
                }
                if (product.ProductType == ProductType.saving)
                {
                    Saving save = GetSaving(product.ProductNumber);
                    trans.Savings.Add(save);
                }
                if (product.ProductType == ProductType.IRA)
                {
                    IRA ira = GetIRA(product.ProductNumber);
                    trans.IRA = ira;
                }
                db.Transactions.Add(trans);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AllProducts = GetAllProducts();
            return View();
        }

        public ActionResult IRAWithdraw()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            return View(TVM);
        }

        [Authorize]
        public ActionResult IRAWithdrawInclude()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            Account destination = new Account();
            destination.ProductNumber = TVM.DestinationID;
            destination.ProductType = ProductType.IRA;
            IRA ira = GetIRA(destination.ProductNumber);
            ira.IRABalance -= TVM.Amount;
            db.SaveChanges();
            destination.ProductName = ira.IRAName;
            Int32 NextTransNum = NextNum();
            if (TVM.Comment == null || TVM.Comment == "")
            {
                TVM.Comment = "No comment entered.";
            }
            TVM.Amount -= 30;
            AppUser owner = db.Users.Find(User.Identity.GetUserId());
            Transaction trans = new Transaction { Description = "Withdrawal of $" + TVM.Amount + " from " + destination.ProductName + ".", TransactionType = TransactionType.withdrawal, Amount = TVM.Amount, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = TVM.Comment, OriginID = destination.ProductNumber, Owner = owner };
            Int32 NextTransNum2 = NextTransNum + 1;
            trans.IRA = ira;
            Transaction fee = new Transaction { Description = "Fee of $30 incurred during withdrawal from " + destination.ProductName + ".", TransactionType = TransactionType.fee, Amount = 30, Date = TVM.Date, TransactionNumber = NextTransNum2, Comment = TVM.Comment, OriginID = destination.ProductNumber, Owner = owner};
            fee.IRA = ira;
            db.Transactions.Add(trans);
            db.Transactions.Add(fee);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult IRAWithdrawExclude()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            Account destination = new Account();
            destination.ProductNumber = TVM.DestinationID;
            destination.ProductType = ProductType.IRA;
            IRA ira = GetIRA(destination.ProductNumber);
            ira.IRABalance -= TVM.Amount;
            ira.IRABalance -= 30;
            db.SaveChanges();
            destination.ProductName = ira.IRAName;
            Int32 NextTransNum = NextNum();
            if (TVM.Comment == null || TVM.Comment == "")
            {
                TVM.Comment = "No comment entered.";
            }
            AppUser owner = db.Users.Find(User.Identity.GetUserId());
            Transaction trans = new Transaction { Description = "Withdrawal of $" + TVM.Amount + " from " + destination.ProductName + ".", TransactionType = TransactionType.withdrawal, Amount = TVM.Amount, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = TVM.Comment, OriginID=destination.ProductNumber, Owner = owner };
            Int32 NextTransNum2 = NextTransNum + 1;
            trans.IRA = ira;
            Transaction fee = new Transaction { Description = "Fee of $30 incurred during withdrawal from " + destination.ProductName + ".", TransactionType = TransactionType.fee, Amount = 30, Date = TVM.Date, TransactionNumber = NextTransNum2, Comment = TVM.Comment, OriginID = destination.ProductNumber, Owner = owner };
            fee.IRA = ira;
            db.Transactions.Add(trans);
            db.Transactions.Add(fee);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        Decimal NewOverdraftAmount;
        
        // GET: Transactions/Transfer
        [Authorize]
        public ActionResult Transfer()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            TVM.Excessive = false;
            TVM.Overdraft = false;
            db.SaveChanges();
            ViewBag.AllProducts = GetAllProducts();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(int? OriginID, int? DestinationID, decimal? DesAmount, string DesComment, DateTime? desdate)
        {
            if (OriginID==null || DestinationID==null || DesAmount==null || desdate==null)
            {
                ViewBag.ErrorMessage = "Please fill all required fields.";
                ViewBag.AllProducts = GetAllProducts();
                return View();
            }
            if (DesAmount <= 0)
            {
                ViewBag.ErrorMessage = "Invalid amount entered. Please enter a positive amount to transfer.";
                ViewBag.AllProducts = GetAllProducts();
                return View();
            }
            Int32 origin = Convert.ToInt32(OriginID);
            Int32 destination = Convert.ToInt32(DestinationID);
            Decimal amount = Convert.ToDecimal(DesAmount);
            DateTime date = Convert.ToDateTime(desdate);

            if (SearchIRA(destination) == true)
            {
                IRA ira = GetIRA(destination);
                var today = DateTime.Today;
                var age = today.Year - ira.Owner.Birthday.Year;
                Decimal NewCumulative = ira.IRACumulative + amount;
                if (age > 70)
                {
                    return RedirectToAction("IRATooOld", "Errors");
                }
                if (NewCumulative > 5000)
                {
                    if (SearchIRA(origin) == true)
                    {
                        IRA start = GetIRA(origin);
                        if (start.IRABalance < 0)
                        {
                            return RedirectToAction("InsufficientFunds", "Errors");
                        }
                        var day = DateTime.Today;
                        if (age < 65)
                        {
                            if (db.TransferViewModels.Find(1) == null)
                            {
                                TransferViewModel TVM = new TransferViewModel { OriginID = origin, DestinationID = destination, Amount = amount, Comment = DesComment, Date = date };
                                if (start.IRABalance - amount < 0 && start.IRABalance - amount >= -50)
                                {
                                    TVM.Overdraft = true;
                                    db.SaveChanges();
                                }
                                if (start.IRABalance - amount < -50)
                                {
                                    return RedirectToAction("ExcessiveOverdraft", "Errors");
                                }
                                db.TransferViewModels.Add(TVM);
                                db.SaveChanges();
                            }
                            else
                            {
                                TransferViewModel TVM = db.TransferViewModels.Find(1);
                                TVM.OriginID = origin;
                                TVM.DestinationID = destination;
                                TVM.Amount = amount;
                                TVM.Comment = DesComment;
                                TVM.Date = date;
                                if (start.IRABalance - amount < 0 && start.IRABalance - amount >= -50)
                                {
                                    TVM.Overdraft = true;
                                    db.SaveChanges();
                                }
                                if (start.IRABalance - amount < -50)
                                {
                                    return RedirectToAction("ExcessiveOverdraft", "Errors");
                                }
                                db.SaveChanges();
                            }
                        }
                    }
                    NewOverdraftAmount = 5000 - ira.IRACumulative;
                    if (db.TransferViewModels.Find(1) == null)
                    {
                        TransferViewModel TVM = new TransferViewModel { OriginID = origin, DestinationID = destination, Amount = NewOverdraftAmount, Comment = DesComment, Date = date };
                        db.TransferViewModels.Add(TVM);
                        if(SearchIRA(origin)==false)
                        {
                            db.TransferViewModels.Find(1).Excessive = true;
                            db.SaveChanges();
                            return RedirectToAction("Confirmation", "Transactions");
                        }
                        else
                        {
                            db.TransferViewModels.Find(1).Excessive = true;
                            db.SaveChanges();
                            return RedirectToAction("IRATransfer", "Transactions");
                        }
                    }
                    else
                    {
                        TransferViewModel TVM = db.TransferViewModels.Find(1);
                        TVM.OriginID = origin;
                        TVM.DestinationID = destination;
                        TVM.Amount = NewOverdraftAmount;
                        TVM.Comment = DesComment;
                        TVM.Date = date;
                        db.SaveChanges();
                        if (SearchIRA(origin) == false)
                        {
                            db.TransferViewModels.Find(1).Excessive = true;
                            db.SaveChanges();
                            return RedirectToAction("Confirmation", "Transactions");
                        }
                        else
                        {
                            db.TransferViewModels.Find(1).Excessive = true;
                            db.SaveChanges();
                            return RedirectToAction("IRATransfer", "Transactions");
                        }
                    }
                }
            }

            if (SearchIRA(origin)==true)
            {
                IRA ira = GetIRA(origin);
                if (ira.IRABalance < 0)
                {
                    return RedirectToAction("InsufficientFunds", "Errors");
                }
                var today = DateTime.Today;
                var age = today.Year - ira.Owner.Birthday.Year;
                if (age < 65)
                {
                    if (db.TransferViewModels.Find(1) == null)
                    {
                        TransferViewModel TVM = new TransferViewModel { OriginID = origin, DestinationID = destination, Amount = amount, Comment = DesComment, Date = date };
                        if (ira.IRABalance - amount < 0 && ira.IRABalance - amount >= -50)
                        {
                            TVM.Overdraft = true;
                            db.SaveChanges();
                        }
                        if (ira.IRABalance - amount < -50)
                        {
                            return RedirectToAction("ExcessiveOverdraft", "Errors");
                        }
                        db.TransferViewModels.Add(TVM);
                        db.SaveChanges();
                        return RedirectToAction("IRATransfer", "Transactions");
                    }
                    else
                    {
                        TransferViewModel TVM = db.TransferViewModels.Find(1);
                        TVM.OriginID = origin;
                        TVM.DestinationID = destination;
                        TVM.Amount = amount;
                        TVM.Comment = DesComment;
                        TVM.Date = date;
                        if (ira.IRABalance - amount < 0 && ira.IRABalance - amount >= -50)
                        {
                            TVM.Overdraft = true;
                            db.SaveChanges();
                        }
                        if (ira.IRABalance - amount < -50)
                        {
                            return RedirectToAction("ExcessiveOverdraft", "Errors");
                        }
                        db.SaveChanges();
                        return RedirectToAction("IRATransfer", "Transactions");
                    }
                }
            }

            if (db.TransferViewModels.Find(1)==null)
            {
                TransferViewModel TVM = new TransferViewModel { OriginID = origin, DestinationID = destination, Amount = amount, Comment = DesComment, Date = date };
                db.TransferViewModels.Add(TVM);
                db.SaveChanges();
                Account start = new Account();
                start.ProductNumber = TVM.OriginID;
                if (SearchSaving(origin)==true)
                {
                    start.ProductType = ProductType.saving;
                    Saving save = GetSaving(start.ProductNumber);
                    if (save.SavingBalance < 0)
                    {
                        return RedirectToAction("InsufficientFunds", "Errors");
                    }
                    if (save.SavingBalance - amount < 0 && save.SavingBalance - amount >= -50)
                    {
                        TVM.Overdraft = true;
                        db.SaveChanges();
                    }
                    if (save.SavingBalance - amount < -50)
                    {
                        return RedirectToAction("ExcessOverdraft", "Errors");
                    }
                }
                if (SearchIRA(origin))
                {
                    start.ProductType = ProductType.IRA;
                    IRA ira = GetIRA(start.ProductNumber);
                    if (ira.IRABalance < 0)
                    {
                        return RedirectToAction("InsufficientFunds", "Errors");
                    }
                    if (ira.IRABalance-amount<0)
                    {
                        TVM.Overdraft = true;
                        db.SaveChanges();
                    }
                }
                if (SearchChecking(origin))
                {
                    start.ProductType = ProductType.checking;
                    Checking check = GetChecking(start.ProductNumber);
                    if (check.CheckingBalance < 0)
                    {
                        return RedirectToAction("InsufficientFunds", "Errors");
                    }
                    if (check.CheckingBalance - amount < 0 && check.CheckingBalance - amount >= -50)
                    {
                        TVM.Overdraft = true;
                        db.SaveChanges();
                    }
                    if (check.CheckingBalance - amount < -50)
                    {
                        return RedirectToAction("ExcessOverdraft", "Errors");
                    }
                }
                return RedirectToAction("Confirmation", "Transactions");
            }
            else
            {
                TransferViewModel TVM = db.TransferViewModels.Find(1);
                TVM.OriginID = origin;
                TVM.DestinationID = destination;
                TVM.Amount = amount;
                TVM.Comment = DesComment;
                TVM.Date = date;
                db.SaveChanges();
                if (SearchSaving(origin) == true)
                {
                    Saving save = GetSaving(TVM.OriginID);
                    if (save.SavingBalance < 0)
                    {
                        return RedirectToAction("InsufficientFunds", "Errors");
                    }
                    if (save.SavingBalance - amount < 0 && save.SavingBalance - amount >= -50)
                    {
                        TVM.Overdraft = true;
                        db.SaveChanges();
                    }
                    if (save.SavingBalance - amount < -50)
                    {
                        return RedirectToAction("ExcessiveOverdraft", "Errors");
                    }
                }
                if (SearchIRA(origin))
                {
                    IRA ira = GetIRA(TVM.OriginID);
                    if (ira.IRABalance < 0)
                    {
                        return RedirectToAction("InsufficientFunds", "Errors");
                    }
                    if (ira.IRABalance - amount < 0 && ira.IRABalance - amount >= -50)
                    {
                        TVM.Overdraft = true;
                        db.SaveChanges();
                    }
                    if (ira.IRABalance - amount < -50)
                    {
                        return RedirectToAction("ExcessiveOverdraft", "Errors");
                    }
                }
                if (SearchChecking(origin))
                {
                    Checking check = GetChecking(TVM.OriginID);
                    if (check.CheckingBalance < 0)
                    {
                        return RedirectToAction("InsufficientFunds", "Errors");
                    }
                    if (check.CheckingBalance - amount < 0 && check.CheckingBalance - amount >= -50)
                    {
                        TVM.Overdraft = true;
                        db.SaveChanges();
                    }
                    if (check.CheckingBalance - amount < -50)
                    {
                        return RedirectToAction("ExcessiveOverdraft", "Errors");
                    }
                }
                return RedirectToAction("Confirmation", "Transactions");
            }
        }

        public ActionResult Confirmation()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            if (TVM.Overdraft == true)
            {
                ViewBag.Overdraft = "Overdraft fee of $30 will apply.";
            }
            if (TVM.Excessive == true)
            {
                ViewBag.Message = "IRA transfer amount adjusted to eligible amount.";
            }
            return View(TVM);
        }

        [Authorize]
        public ActionResult Confirmed()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            Account origin = new Account();
            origin.ProductNumber = TVM.OriginID;
            Account destination = new Account();
            destination.ProductNumber = TVM.DestinationID;
            if (SearchSaving(origin.ProductNumber))
            {
                origin.ProductType = ProductType.saving;
            }
            if (SearchIRA(origin.ProductNumber))
            {
                origin.ProductType = ProductType.IRA;
            }
            if (SearchChecking(origin.ProductNumber))
            {
                origin.ProductType = ProductType.checking;
            }
            if (SearchSaving(destination.ProductNumber))
            {
                destination.ProductType = ProductType.saving;
            }
            if (SearchIRA(destination.ProductNumber))
            {
                destination.ProductType = ProductType.IRA;
            }
            if (SearchChecking(destination.ProductNumber))
            {
                destination.ProductType = ProductType.checking;
            }
            
            if (origin.ProductType == ProductType.checking)
            {
                Checking check = GetChecking(origin.ProductNumber);
                if (TVM.Overdraft==true)
                {
                    check.CheckingBalance -= 30;
                }
                check.CheckingBalance -= TVM.Amount;
                db.SaveChanges();
                origin.ProductName = check.CheckingName;
            }
            if (origin.ProductType == ProductType.saving)
            {
                Saving save = GetSaving(origin.ProductNumber);
                if (TVM.Overdraft == true)
                {
                    save.SavingBalance -= 30;
                }
                save.SavingBalance -= TVM.Amount;
                db.SaveChanges();
                origin.ProductName = save.SavingName;
            }
            if (destination.ProductType == ProductType.checking)
            {
                Checking check = GetChecking(destination.ProductNumber);
                check.CheckingBalance += TVM.Amount;
                db.SaveChanges();
                destination.ProductName = check.CheckingName;
            }
            if (destination.ProductType == ProductType.saving)
            {
                Saving save = GetSaving(destination.ProductNumber);
                save.SavingBalance += TVM.Amount;
                db.SaveChanges();
                destination.ProductName = save.SavingName;
            }
            if (destination.ProductType == ProductType.IRA)
            {
                IRA ira = GetIRA(destination.ProductNumber);
                ira.IRABalance += TVM.Amount;
                ira.IRACumulative += TVM.Amount;
                db.SaveChanges();
                destination.ProductName = ira.IRAName;
            }
            Int32 NextTransNum = NextNum();
            if (TVM.Comment == null || TVM.Comment == "")
            {
                TVM.Comment = "No comment entered.";
            }
            AppUser owner = db.Users.Find(User.Identity.GetUserId());
            Transaction trans = new Transaction { Description = "Transfer of $" + TVM.Amount + " from " + origin.ProductName + " into " + destination.ProductName + ".", TransactionType = TransactionType.transfer, Amount = TVM.Amount, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = TVM.Comment, OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
            if (TVM.Overdraft == true)
            {
                NextTransNum += 1;
                Transaction OverdraftFee = new Transaction { Description = "Overdraft Fee.", TransactionType = TransactionType.fee, Amount = 30, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = "", OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
                db.Transactions.Add(OverdraftFee);
            }
            if (origin.ProductType==ProductType.checking)
            {
                Checking check = GetChecking(origin.ProductNumber);
                trans.Checkings.Add(check);
            }
            if (origin.ProductType==ProductType.saving)
            {
                Saving save = GetSaving(origin.ProductNumber);
                trans.Savings.Add(save);
            }
            if (destination.ProductType==ProductType.checking)
            {
                Checking check = GetChecking(origin.ProductNumber);
                trans.Checkings.Add(check);
            }
            if (destination.ProductType == ProductType.saving)
            {
                Saving save = GetSaving(destination.ProductNumber);
                trans.Savings.Add(save);
            }
            if (destination.ProductType == ProductType.IRA)
            {
                IRA ira = GetIRA(destination.ProductNumber);
                trans.IRA = ira;
            }
            db.Transactions.Add(trans);
            db.SaveChanges();
            return RedirectToAction("TransferSuccess", "Confirmations");
        }

        public ActionResult IRATransfer()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            if (TVM.Excessive == true)
            {
                ViewBag.Message = "IRA transfer amount adjusted to eligible amount.";
            }
            return View(TVM);
        }

        public ActionResult IRATransferInclude()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            Account origin = new Account();
            origin.ProductNumber = TVM.OriginID;
            origin.ProductType = ProductType.IRA;
            Account destination = new Account();
            destination.ProductNumber = TVM.DestinationID;
            IRA ira = GetIRA(origin.ProductNumber);
            if (TVM.Overdraft == true)
            {
                ira.IRABalance -= 30;
            }
            ira.IRABalance -= TVM.Amount;
            db.SaveChanges();
            origin.ProductName = ira.IRAName;
            if (SearchSaving(destination.ProductNumber))
            {
                destination.ProductType = ProductType.saving;
            }
            if (SearchIRA(destination.ProductNumber))
            {
                destination.ProductType = ProductType.IRA;
            }
            if (SearchChecking(destination.ProductNumber))
            {
                destination.ProductType = ProductType.checking;
            }
            if (destination.ProductType == ProductType.checking)
            {
                Checking check = GetChecking(destination.ProductNumber);
                check.CheckingBalance += TVM.Amount;
                check.CheckingBalance -= 30;
                db.SaveChanges();
                destination.ProductName = check.CheckingName;
            }
            if (destination.ProductType == ProductType.saving)
            {
                Saving save = GetSaving(destination.ProductNumber);
                save.SavingBalance += TVM.Amount;
                save.SavingBalance -= 30;
                db.SaveChanges();
                destination.ProductName = save.SavingName;
            }
            if (destination.ProductType == ProductType.IRA)
            {
                IRA ira2 = GetIRA(destination.ProductNumber);
                ira2.IRABalance += TVM.Amount;
                ira2.IRABalance -= 30;
                db.SaveChanges();
                destination.ProductName = ira2.IRAName;
            }
            Int32 NextTransNum = NextNum();
            if (TVM.Comment == null || TVM.Comment == "")
            {
                TVM.Comment = "No comment entered.";
            }
            TVM.Amount -= 30;
            AppUser owner = db.Users.Find(User.Identity.GetUserId());
            Transaction trans = new Transaction { Description = "Transfer of $" + TVM.Amount + " from " + origin.ProductName + " into " + destination.ProductName + ".", TransactionType = TransactionType.transfer, Amount = TVM.Amount, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = TVM.Comment, OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
            trans.IRA = ira;
            if (destination.ProductType == ProductType.checking)
            {
                Checking check = GetChecking(origin.ProductNumber);
                trans.Checkings.Add(check);
            }
            if (destination.ProductType == ProductType.saving)
            {
                Saving save = GetSaving(destination.ProductNumber);
                trans.Savings.Add(save);
            }
            NextTransNum += 1;
            Transaction fee = new Transaction { Description = "Fee of $30 incurred during transfer from " + origin.ProductName + " to " + destination.ProductName + ".", TransactionType = TransactionType.fee, Amount = 30, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = TVM.Comment, OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
            db.Transactions.Add(trans);
            fee.IRA = ira;
            db.Transactions.Add(fee);
            if (TVM.Overdraft == true)
            {
                NextTransNum += 1;
                Transaction OverdraftFee = new Transaction { Description = "Overdraft Fee.", TransactionType = TransactionType.fee, Amount = 30, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = "", OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
                OverdraftFee.IRA = ira;
                db.Transactions.Add(OverdraftFee);
            }
            db.SaveChanges();
            return RedirectToAction("TransferSuccess", "Confirmations");
        }

        public ActionResult IRATransferExclude()
        {
            TransferViewModel TVM = db.TransferViewModels.Find(1);
            Account origin = new Account();
            origin.ProductNumber = TVM.OriginID;
            origin.ProductType = ProductType.IRA;
            Account destination = new Account();
            destination.ProductNumber = TVM.DestinationID;
            IRA ira = GetIRA(origin.ProductNumber);
            if (TVM.Overdraft == true)
            {
                ira.IRABalance -= 30;
            }
            ira.IRABalance -= TVM.Amount;
            ira.IRABalance -= 30;
            db.SaveChanges();
            origin.ProductName = ira.IRAName;
            if (SearchSaving(destination.ProductNumber))
            {
                destination.ProductType = ProductType.saving;
            }
            if (SearchIRA(destination.ProductNumber))
            {
                destination.ProductType = ProductType.IRA;
            }
            if (SearchChecking(destination.ProductNumber))
            {
                destination.ProductType = ProductType.checking;
            }

            if (destination.ProductType == ProductType.checking)
            {
                Checking check = GetChecking(destination.ProductNumber);
                check.CheckingBalance += TVM.Amount;
                db.SaveChanges();
                destination.ProductName = check.CheckingName;
            }
            if (destination.ProductType == ProductType.saving)
            {
                Saving save = GetSaving(destination.ProductNumber);
                save.SavingBalance += TVM.Amount;
                db.SaveChanges();
                destination.ProductName = save.SavingName;
            }
            if (destination.ProductType == ProductType.IRA)
            {
                IRA ira2 = GetIRA(destination.ProductNumber);
                ira2.IRABalance += TVM.Amount;
                ira2.IRACumulative += TVM.Amount;
                db.SaveChanges();
                destination.ProductName = ira2.IRAName;
            }
            Int32 NextTransNum = NextNum();
            if (TVM.Comment == null || TVM.Comment == "")
            {
                TVM.Comment = "No comment entered.";
            }
            AppUser owner = db.Users.Find(User.Identity.GetUserId());
            Transaction trans = new Transaction { Description = "Transfer of $" + TVM.Amount + " from " + origin.ProductName + " into " + destination.ProductName + ".", TransactionType = TransactionType.transfer, Amount = TVM.Amount, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = TVM.Comment, OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
            trans.IRA = ira;
            if (destination.ProductType == ProductType.checking)
            {
                Checking check = GetChecking(origin.ProductNumber);
                trans.Checkings.Add(check);
            }
            if (destination.ProductType == ProductType.saving)
            {
                Saving save = GetSaving(destination.ProductNumber);
                trans.Savings.Add(save);
            }
            NextTransNum += 1;
            Transaction fee = new Transaction { Description = "Fee of $30 incurred during transfer from " + origin.ProductName + " to " + destination.ProductName + ".", TransactionType = TransactionType.fee, Amount = 30, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = TVM.Comment, OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
            fee.IRA = ira;
            db.Transactions.Add(trans);
            db.Transactions.Add(fee);
            if (TVM.Overdraft==true)
            {
                NextTransNum += 1;
                Transaction OverdraftFee = new Transaction { Description = "Overdraft Fee.", TransactionType = TransactionType.fee, Amount = 30, Date = TVM.Date, TransactionNumber = NextTransNum, Comment = "", OriginID = origin.ProductNumber, DestinationID = destination.ProductNumber, Owner = owner };
                OverdraftFee.IRA = ira;
                db.Transactions.Add(OverdraftFee);
            }
            db.SaveChanges();
            return RedirectToAction("TransferSuccess", "Confirmations");
        }

        public SelectList GetAllProducts()
        {
            List<Account> allProducts = new List<Account>();
            var query = from x in db.Accounts
                        select x;
            foreach (var x in query)
            {
                if (x.Owner.Id==User.Identity.GetUserId())
                {
                    allProducts.Add(x);
                }
            }
            SelectList allProductslist = new SelectList(allProducts, "ProductNumber", "ProductName");

            return allProductslist;
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

        public Int32 NextNum()
        {
            List<Transaction> transactions = new List<Transaction>();
            var query = from x in db.Transactions
                        select x;
            foreach (var x in query)
            {
                if(x.Owner.Id==User.Identity.GetUserId())
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Team15_FinalProject.Models;
using System.Net;
using System.Net.Mail;

namespace Team15_FinalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Int32 CurrentHVM = 1;

        private AppDbContext db = new AppDbContext();

        public ActionResult StartIndex()
        {
            return View();
        }

        public ActionResult Index()
        {
            RefreshAccounts();
            List<Saving> SavingsToAdd = new List<Saving>();
            List<Checking> CheckingsToAdd = new List<Checking>();
            List<Int32> IDsToAdd = new List<Int32>();
            List<string> NamesToAdd = new List<string>();

            if (db.HomeViewModels.Find(CurrentHVM)==null)
            {
                HomeViewModel vm = new HomeViewModel();
                var query = from c in db.Checkings
                            select c;
                foreach (var x in query)
                {
                    if (vm.checking == null)
                    {
                        vm.checking = new List<Checking>();
                    }
                    vm.checking.Add(x);
                    vm.ProductIDs.Add(x.CheckingID);
                    vm.ProductNames.Add(x.CheckingName);
                }

                var query2 = from s in db.Savings
                             select s;
                foreach (var y in query2)
                {
                    if (vm.saving == null)
                    {
                        vm.saving = new List<Saving>();
                    }
                    vm.saving.Add(y);
                    vm.ProductIDs.Add(y.SavingID);
                    vm.ProductNames.Add(y.SavingName);
                }
                var query3 = from i in db.IRAs
                             select i;
                foreach (var z in query3)
                {
                    if (vm.ira == null)
                    {
                        vm.ira = new IRA();
                    }
                    vm.ira = z;
                }
                var query4 = from i in db.Stocks
                             select i;
                foreach (var z in query4)
                {
                    if (vm.stock == null)
                    {
                        vm.stock = new Stock();
                    }
                    vm.stock = z;
                }

                db.HomeViewModels.Add(vm);
                db.SaveChanges();

            }
            else
            {
                HomeViewModel vm = db.HomeViewModels.Find(CurrentHVM);
                ClearHVM(vm);
                var query = from c in db.Checkings
                            select c;
                foreach (var x in query)
                {
                    if (!vm.checking.Contains(x) && x.Owner.Id==User.Identity.GetUserId() && x.Status==true)
                    {
                        CheckingsToAdd.Add(x);
                        IDsToAdd.Add(x.CheckingID);
                        NamesToAdd.Add(x.CheckingName);
                    }
                }
                foreach (var z in CheckingsToAdd)
                {
                    vm.checking.Add(z);
                }
                foreach (var z in IDsToAdd)
                {
                    vm.ProductIDs.Add(z);
                }
                foreach (var z in NamesToAdd)
                {
                    vm.ProductNames.Add(z);
                }
                db.SaveChanges();

                var query2 = from s in db.Savings
                             select s;
                foreach (var y in query2)
                {
                    if (!vm.saving.Contains(y) && y.Owner.Id == User.Identity.GetUserId() && y.Status==true)
                    {
                        SavingsToAdd.Add(y);
                        IDsToAdd.Add(y.SavingID);
                        NamesToAdd.Add(y.SavingName);
                    }
                }
                foreach (var z in SavingsToAdd)
                {
                    vm.saving.Add(z);
                }
                foreach (var z in IDsToAdd)
                {
                    vm.ProductIDs.Add(z);
                }
                foreach (var z in NamesToAdd)
                {
                    vm.ProductNames.Add(z);
                }
                db.SaveChanges();

                var query3 = from i in db.IRAs
                             select i;
                foreach (var a in query3)
                {
                    if (vm.ira!= a && a.Owner.Id==User.Identity.GetUserId() && a.Status==true)
                    {
                        vm.ira = a;
                    }
                }
                db.SaveChanges();

                var query4 = from s in db.Stocks
                             select s;
                foreach (var a in query4)
                {
                    if (vm.stock != a && a.Owner.Id == User.Identity.GetUserId() && a.Status == true)
                    {
                        vm.stock = a;
                    }
                }
                db.SaveChanges();
            }
            return View(db.HomeViewModels.ToList());
        }

        public ActionResult NewIndex()
        {
            return View();
        }

        public void ClearHVM(HomeViewModel vm)
        {
            vm.ProductIDs.Clear();
            vm.ProductNames.Clear();
            vm.checking.Clear();
            vm.saving.Clear();
            vm.ira = null;
            vm.stock = null;
            db.HomeViewModels.Find(vm.HomeViewModelID).ProductIDs.Clear();
            db.HomeViewModels.Find(vm.HomeViewModelID).ProductNames.Clear();
            db.HomeViewModels.Find(vm.HomeViewModelID).checking.Clear();
            db.HomeViewModels.Find(vm.HomeViewModelID).saving.Clear();
            db.HomeViewModels.Find(vm.HomeViewModelID).ira = null;
            db.HomeViewModels.Find(vm.HomeViewModelID).stock = null;
            db.SaveChanges();
        }

        public void RefreshAccounts()
        {
            var accnums = from x in db.Accounts
                          select x.ProductNumber;
            List<Int32> numbers = accnums.ToList();
            var query = from x in db.Checkings
                        select x;
            foreach (var x in query)
            {
                AppUser owner = db.Users.Find(x.Owner.Id);
                Account account = new Account { ProductID = x.CheckingID, ProductName = x.CheckingName, ProductType = ProductType.checking, ProductNumber = x.AccountNumber, ProductString = x.SecureNumber, ProductBalance = x.CheckingBalance, Owner = owner };
                if (!numbers.Contains(x.AccountNumber))
                {
                    db.Accounts.Add(account);
                    numbers.Add(x.AccountNumber);
                }
            }
            var query2 = from x in db.Savings
                         select x;
            foreach (var x in query2)
            {
                AppUser owner = db.Users.Find(x.Owner.Id);
                Account account = new Account { ProductID = x.SavingID, ProductName = x.SavingName, ProductType = ProductType.saving, ProductNumber = x.AccountNumber, ProductString = x.SecureNumber, ProductBalance = x.SavingBalance, Owner = owner };
                if (!numbers.Contains(x.AccountNumber))
                {
                    db.Accounts.Add(account);
                    numbers.Add(x.AccountNumber);
                }
            }
            var query3 = from x in db.IRAs
                         select x;
            foreach (var x in query3)
            {
                AppUser owner = db.Users.Find(x.Owner.Id);
                Account account = new Account { ProductID = x.IRAID, ProductName = x.IRAName, ProductType = ProductType.IRA, ProductNumber = x.AccountNumber, ProductString = x.SecureNumber, ProductBalance = x.IRABalance, Owner = owner };
                if (!numbers.Contains(x.AccountNumber))
                {
                    db.Accounts.Add(account);
                    numbers.Add(x.AccountNumber);
                }
            }
            var query4 = from x in db.Stocks
                         select x;
            foreach (var x in query4)
            {
                AppUser owner = db.Users.Find(x.Owner.Id);
                Account account = new Account { ProductID = x.StockID, ProductName = x.StockName, ProductType = ProductType.stock, ProductNumber = x.AccountNumber, ProductString = x.SecureNumber, ProductBalance = x.StockBalance, Owner = owner };
                if (!numbers.Contains(x.AccountNumber))
                {
                    db.Accounts.Add(account);
                    numbers.Add(x.AccountNumber);
                }
            }
            db.SaveChanges();
        }
    }
}
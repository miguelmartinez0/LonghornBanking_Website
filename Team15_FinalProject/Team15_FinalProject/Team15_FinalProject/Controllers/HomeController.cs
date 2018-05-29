using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Team15_FinalProject.Models;

namespace Team15_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        Int32 CurrentHVM = 1;

        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
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
                    if (!vm.checking.ToList().Contains(x) && x.Owner.Id==User.Identity.GetUserId() && x.Status==true)
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
                    if (!vm.saving.ToList().Contains(y) && y.Owner.Id == User.Identity.GetUserId() && y.Status==true)
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
                    if (vm.ira!=a && a.Owner.Id==User.Identity.GetUserId() && a.Status==true)
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

        public void ClearHVM(HomeViewModel vm)
        {
            vm.ProductIDs.Clear();
            vm.ProductNames.Clear();
            vm.checking.Clear();
            vm.saving.Clear();
            vm.ira = null;
            vm.stock = null;
            db.SaveChanges();
        }
    }
}
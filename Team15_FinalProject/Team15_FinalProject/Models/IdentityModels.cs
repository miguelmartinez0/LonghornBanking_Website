using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Team15_FinalProject.Models
{
    public enum States { AL, AK, AZ, AR, CA, CO, CT, DE, FL, GA, HI, ID, IL, IN, IA, KS, KY, LA, ME, MD, MA, MI, MN, MS, MO, MT, NE, NV, NH, NJ, NM, NY, NC, ND, OH, OK, OR, PA, RI, SC, SD, TN, TX, UT, VT, VA, WA, WV, WI, WY }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
        //Put any additional fields that you need for your user here
        //For instance
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Middle Initial")]
        [StringLength(1)]
        public string MInitial { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Please enter your street address")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your state")]
        public States State { get; set; }

        [Required(ErrorMessage = "Please enter your zip code")]
        [DisplayFormat(DataFormatString = "{0:#####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Zip Code")]
        public int Zip { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0:(###)###-####}", ApplyFormatInEditMode = true)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter your birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        //navigational properties
        public virtual List<Checking> Checkings { get; set; }
        public virtual List<Saving> Savings { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<Dispute> Disputes { get; set; }

        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //Here's your db context for the project.  All of your db sets should go in here
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //Add dbsets here, for instance there's one for books
        //Remember, Identity adds a db set for users, so you shouldn't add that one - you will get an error
        public DbSet<Checking> Checkings { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<IRA> IRAs { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockKind> StockKinds { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<StockViewModel> StockViewModels { get; set; }
        public DbSet<StockQuote> StockQuotes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<HomeViewModel> HomeViewModels { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionKind> TransactionKinds { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<TransferViewModel> TransferViewModels { get; set; }
        public DbSet<PayBillsViewModel> PayBillsViewModels { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        //Make sure that your connection string name is correct here.
        public AppDbContext()
            : base("MyDBConnection", throwIfV1Schema: false)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
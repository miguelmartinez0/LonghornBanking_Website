namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetupNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(),
                        ProductNumber = c.Int(nullable: false),
                        ProductString = c.String(),
                        ProductBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductType = c.Int(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        MInitial = c.String(maxLength: 1),
                        LName = c.String(nullable: false),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Payee_PayeeID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payees", t => t.Payee_PayeeID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Payee_PayeeID);
            
            CreateTable(
                "dbo.Checkings",
                c => new
                    {
                        CheckingID = c.Int(nullable: false, identity: true),
                        CheckingName = c.String(),
                        CheckingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        SecureNumber = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                        HomeViewModel_HomeViewModelID = c.Int(),
                    })
                .PrimaryKey(t => t.CheckingID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.HomeViewModels", t => t.HomeViewModel_HomeViewModelID)
                .Index(t => t.Owner_Id)
                .Index(t => t.HomeViewModel_HomeViewModelID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        TransactionNumber = c.Int(nullable: false),
                        OriginID = c.Int(nullable: false),
                        DestinationID = c.Int(nullable: false),
                        Comment = c.String(),
                        IRA_IRAID = c.Int(),
                        Owner_Id = c.String(maxLength: 128),
                        Portfolio_PortfolioID = c.Int(),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.IRAs", t => t.IRA_IRAID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_PortfolioID)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.IRA_IRAID)
                .Index(t => t.Owner_Id)
                .Index(t => t.Portfolio_PortfolioID)
                .Index(t => t.Stock_StockID);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        DisputeID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        DisputeComment = c.String(nullable: false),
                        CorrectAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeleteTransaction = c.Int(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        Transactions_TransactionID = c.Int(),
                    })
                .PrimaryKey(t => t.DisputeID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Transactions", t => t.Transactions_TransactionID)
                .Index(t => t.Owner_Id)
                .Index(t => t.Transactions_TransactionID);
            
            CreateTable(
                "dbo.IRAs",
                c => new
                    {
                        IRAID = c.Int(nullable: false, identity: true),
                        IRAName = c.String(nullable: false),
                        IRABalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IRACumulative = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        SecureNumber = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IRAID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Savings",
                c => new
                    {
                        SavingID = c.Int(nullable: false, identity: true),
                        SavingName = c.String(),
                        SavingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        SecureNumber = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                        HomeViewModel_HomeViewModelID = c.Int(),
                    })
                .PrimaryKey(t => t.SavingID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.HomeViewModels", t => t.HomeViewModel_HomeViewModelID)
                .Index(t => t.Owner_Id)
                .Index(t => t.HomeViewModel_HomeViewModelID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        StockName = c.String(nullable: false),
                        StockBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gains = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        BalanceStatus = c.Boolean(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        SecureNumber = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        PortfolioID = c.Int(nullable: false, identity: true),
                        TickerSymbol = c.String(nullable: false),
                        StockPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockType = c.Int(nullable: false),
                        Shares = c.Int(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        stock_StockID = c.Int(),
                        StockViewModel_StockViewModelID = c.Int(),
                    })
                .PrimaryKey(t => t.PortfolioID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Stocks", t => t.stock_StockID)
                .ForeignKey("dbo.StockViewModels", t => t.StockViewModel_StockViewModelID)
                .Index(t => t.Owner_Id)
                .Index(t => t.stock_StockID)
                .Index(t => t.StockViewModel_StockViewModelID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.HomeViewModels",
                c => new
                    {
                        HomeViewModelID = c.Int(nullable: false, identity: true),
                        ira_IRAID = c.Int(),
                        Owner_Id = c.String(maxLength: 128),
                        stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.HomeViewModelID)
                .ForeignKey("dbo.IRAs", t => t.ira_IRAID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Stocks", t => t.stock_StockID)
                .Index(t => t.ira_IRAID)
                .Index(t => t.Owner_Id)
                .Index(t => t.stock_StockID);
            
            CreateTable(
                "dbo.PayBillsViewModels",
                c => new
                    {
                        PayBillsViewModelID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayBillsViewModelID);
            
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StreeAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayeeID);
            
            CreateTable(
                "dbo.StockKinds",
                c => new
                    {
                        StockKindID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StockKindID);
            
            CreateTable(
                "dbo.StockQuotes",
                c => new
                    {
                        StockQuoteID = c.Int(nullable: false, identity: true),
                        Symbol = c.String(nullable: false),
                        StockType = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        PreviousClose = c.Double(nullable: false),
                        LastTradePrice = c.Double(nullable: false),
                        Volume = c.Double(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StockQuoteID);
            
            CreateTable(
                "dbo.StockViewModels",
                c => new
                    {
                        StockViewModelID = c.Int(nullable: false, identity: true),
                        Owner_Id = c.String(maxLength: 128),
                        stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.StockViewModelID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Stocks", t => t.stock_StockID)
                .Index(t => t.Owner_Id)
                .Index(t => t.stock_StockID);
            
            CreateTable(
                "dbo.TransactionKinds",
                c => new
                    {
                        TransactionKindID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TransactionKindID);
            
            CreateTable(
                "dbo.TransferViewModels",
                c => new
                    {
                        TransferViewModelID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OriginID = c.Int(nullable: false),
                        DestinationID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Overdraft = c.Boolean(nullable: false),
                        Excessive = c.Boolean(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.TransferViewModelID);
            
            CreateTable(
                "dbo.TransactionCheckings",
                c => new
                    {
                        Transaction_TransactionID = c.Int(nullable: false),
                        Checking_CheckingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Transaction_TransactionID, t.Checking_CheckingID })
                .ForeignKey("dbo.Transactions", t => t.Transaction_TransactionID, cascadeDelete: true)
                .ForeignKey("dbo.Checkings", t => t.Checking_CheckingID, cascadeDelete: true)
                .Index(t => t.Transaction_TransactionID)
                .Index(t => t.Checking_CheckingID);
            
            CreateTable(
                "dbo.SavingTransactions",
                c => new
                    {
                        Saving_SavingID = c.Int(nullable: false),
                        Transaction_TransactionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Saving_SavingID, t.Transaction_TransactionID })
                .ForeignKey("dbo.Savings", t => t.Saving_SavingID, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.Transaction_TransactionID, cascadeDelete: true)
                .Index(t => t.Saving_SavingID)
                .Index(t => t.Transaction_TransactionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockViewModels", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Portfolios", "StockViewModel_StockViewModelID", "dbo.StockViewModels");
            DropForeignKey("dbo.StockViewModels", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.HomeViewModels", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Savings", "HomeViewModel_HomeViewModelID", "dbo.HomeViewModels");
            DropForeignKey("dbo.HomeViewModels", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HomeViewModels", "ira_IRAID", "dbo.IRAs");
            DropForeignKey("dbo.Checkings", "HomeViewModel_HomeViewModelID", "dbo.HomeViewModels");
            DropForeignKey("dbo.Accounts", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "Portfolio_PortfolioID", "dbo.Portfolios");
            DropForeignKey("dbo.Portfolios", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Portfolios", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stocks", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SavingTransactions", "Transaction_TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.SavingTransactions", "Saving_SavingID", "dbo.Savings");
            DropForeignKey("dbo.Savings", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "IRA_IRAID", "dbo.IRAs");
            DropForeignKey("dbo.IRAs", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disputes", "Transactions_TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Disputes", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransactionCheckings", "Checking_CheckingID", "dbo.Checkings");
            DropForeignKey("dbo.TransactionCheckings", "Transaction_TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Checkings", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SavingTransactions", new[] { "Transaction_TransactionID" });
            DropIndex("dbo.SavingTransactions", new[] { "Saving_SavingID" });
            DropIndex("dbo.TransactionCheckings", new[] { "Checking_CheckingID" });
            DropIndex("dbo.TransactionCheckings", new[] { "Transaction_TransactionID" });
            DropIndex("dbo.StockViewModels", new[] { "stock_StockID" });
            DropIndex("dbo.StockViewModels", new[] { "Owner_Id" });
            DropIndex("dbo.HomeViewModels", new[] { "stock_StockID" });
            DropIndex("dbo.HomeViewModels", new[] { "Owner_Id" });
            DropIndex("dbo.HomeViewModels", new[] { "ira_IRAID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Portfolios", new[] { "StockViewModel_StockViewModelID" });
            DropIndex("dbo.Portfolios", new[] { "stock_StockID" });
            DropIndex("dbo.Portfolios", new[] { "Owner_Id" });
            DropIndex("dbo.Stocks", new[] { "Owner_Id" });
            DropIndex("dbo.Savings", new[] { "HomeViewModel_HomeViewModelID" });
            DropIndex("dbo.Savings", new[] { "Owner_Id" });
            DropIndex("dbo.IRAs", new[] { "Owner_Id" });
            DropIndex("dbo.Disputes", new[] { "Transactions_TransactionID" });
            DropIndex("dbo.Disputes", new[] { "Owner_Id" });
            DropIndex("dbo.Transactions", new[] { "Stock_StockID" });
            DropIndex("dbo.Transactions", new[] { "Portfolio_PortfolioID" });
            DropIndex("dbo.Transactions", new[] { "Owner_Id" });
            DropIndex("dbo.Transactions", new[] { "IRA_IRAID" });
            DropIndex("dbo.Checkings", new[] { "HomeViewModel_HomeViewModelID" });
            DropIndex("dbo.Checkings", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Payee_PayeeID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Accounts", new[] { "Owner_Id" });
            DropTable("dbo.SavingTransactions");
            DropTable("dbo.TransactionCheckings");
            DropTable("dbo.TransferViewModels");
            DropTable("dbo.TransactionKinds");
            DropTable("dbo.StockViewModels");
            DropTable("dbo.StockQuotes");
            DropTable("dbo.StockKinds");
            DropTable("dbo.Payees");
            DropTable("dbo.PayBillsViewModels");
            DropTable("dbo.HomeViewModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Portfolios");
            DropTable("dbo.Stocks");
            DropTable("dbo.Savings");
            DropTable("dbo.IRAs");
            DropTable("dbo.Disputes");
            DropTable("dbo.Transactions");
            DropTable("dbo.Checkings");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Accounts");
        }
    }
}

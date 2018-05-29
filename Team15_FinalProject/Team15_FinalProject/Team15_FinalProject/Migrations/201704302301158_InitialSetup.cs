namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
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
                    })
                .PrimaryKey(t => t.AccountID);
            
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
                "dbo.Checkings",
                c => new
                    {
                        CheckingID = c.Int(nullable: false, identity: true),
                        CheckingName = c.String(nullable: false),
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
                "dbo.Savings",
                c => new
                    {
                        SavingID = c.Int(nullable: false, identity: true),
                        SavingName = c.String(nullable: false),
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
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        TransactionNumber = c.Int(nullable: false),
                        Comment = c.String(),
                        Saving_SavingID = c.Int(),
                        Checking_CheckingID = c.Int(),
                        IRA_IRAID = c.Int(),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Savings", t => t.Saving_SavingID)
                .ForeignKey("dbo.Checkings", t => t.Checking_CheckingID)
                .ForeignKey("dbo.IRAs", t => t.IRA_IRAID)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.Saving_SavingID)
                .Index(t => t.Checking_CheckingID)
                .Index(t => t.IRA_IRAID)
                .Index(t => t.Stock_StockID);
            
            CreateTable(
                "dbo.HomeViewModels",
                c => new
                    {
                        HomeViewModelID = c.Int(nullable: false, identity: true),
                        ira_IRAID = c.Int(),
                        stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.HomeViewModelID)
                .ForeignKey("dbo.IRAs", t => t.ira_IRAID)
                .ForeignKey("dbo.Stocks", t => t.stock_StockID)
                .Index(t => t.ira_IRAID)
                .Index(t => t.stock_StockID);
            
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
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        StockName = c.String(nullable: false),
                        StockBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BalanceStatus = c.Boolean(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        SecureNumber = c.String(),
                        StockType = c.Int(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
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
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.TransferViewModelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.HomeViewModels", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Savings", "HomeViewModel_HomeViewModelID", "dbo.HomeViewModels");
            DropForeignKey("dbo.HomeViewModels", "ira_IRAID", "dbo.IRAs");
            DropForeignKey("dbo.Transactions", "IRA_IRAID", "dbo.IRAs");
            DropForeignKey("dbo.IRAs", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Checkings", "HomeViewModel_HomeViewModelID", "dbo.HomeViewModels");
            DropForeignKey("dbo.Transactions", "Checking_CheckingID", "dbo.Checkings");
            DropForeignKey("dbo.Transactions", "Saving_SavingID", "dbo.Savings");
            DropForeignKey("dbo.Savings", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Checkings", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Stocks", new[] { "Owner_Id" });
            DropIndex("dbo.IRAs", new[] { "Owner_Id" });
            DropIndex("dbo.HomeViewModels", new[] { "stock_StockID" });
            DropIndex("dbo.HomeViewModels", new[] { "ira_IRAID" });
            DropIndex("dbo.Transactions", new[] { "Stock_StockID" });
            DropIndex("dbo.Transactions", new[] { "IRA_IRAID" });
            DropIndex("dbo.Transactions", new[] { "Checking_CheckingID" });
            DropIndex("dbo.Transactions", new[] { "Saving_SavingID" });
            DropIndex("dbo.Savings", new[] { "HomeViewModel_HomeViewModelID" });
            DropIndex("dbo.Savings", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Payee_PayeeID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Checkings", new[] { "HomeViewModel_HomeViewModelID" });
            DropIndex("dbo.Checkings", new[] { "Owner_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.TransferViewModels");
            DropTable("dbo.TransactionKinds");
            DropTable("dbo.Payees");
            DropTable("dbo.PayBillsViewModels");
            DropTable("dbo.Stocks");
            DropTable("dbo.IRAs");
            DropTable("dbo.HomeViewModels");
            DropTable("dbo.Transactions");
            DropTable("dbo.Savings");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Checkings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Accounts");
        }
    }
}

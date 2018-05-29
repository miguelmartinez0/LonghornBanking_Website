namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Portfolios", "StockViewModel_StockViewModelID", "dbo.StockViewModels");
            DropForeignKey("dbo.StockViewModels", "stock_StockID", "dbo.Stocks");
            DropIndex("dbo.Portfolios", new[] { "StockViewModel_StockViewModelID" });
            DropIndex("dbo.StockViewModels", new[] { "stock_StockID" });
            DropColumn("dbo.Stocks", "Fees");
            DropColumn("dbo.Stocks", "Gains");
            DropColumn("dbo.Stocks", "Bonus");
            DropColumn("dbo.Stocks", "TotalBalance");
            DropColumn("dbo.Portfolios", "StockViewModel_StockViewModelID");
            DropTable("dbo.StockViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StockViewModels",
                c => new
                    {
                        StockViewModelID = c.Int(nullable: false, identity: true),
                        stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.StockViewModelID);
            
            AddColumn("dbo.Portfolios", "StockViewModel_StockViewModelID", c => c.Int());
            AddColumn("dbo.Stocks", "TotalBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "Bonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "Gains", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "Fees", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.StockViewModels", "stock_StockID");
            CreateIndex("dbo.Portfolios", "StockViewModel_StockViewModelID");
            AddForeignKey("dbo.StockViewModels", "stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.Portfolios", "StockViewModel_StockViewModelID", "dbo.StockViewModels", "StockViewModelID");
        }
    }
}

namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockPortfolios : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Stock_StockID", "dbo.Stocks");
            DropIndex("dbo.Transactions", new[] { "Stock_StockID" });
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        PortfolioID = c.Int(nullable: false, identity: true),
                        TickerSymbol = c.String(nullable: false),
                        StockPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockType = c.Int(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.PortfolioID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.Owner_Id)
                .Index(t => t.Stock_StockID);
            
            AddColumn("dbo.Transactions", "Portfolio_PortfolioID", c => c.Int());
            AddColumn("dbo.Stocks", "Status", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Transactions", "Portfolio_PortfolioID");
            AddForeignKey("dbo.Transactions", "Portfolio_PortfolioID", "dbo.Portfolios", "PortfolioID");
            DropColumn("dbo.Transactions", "Stock_StockID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Stock_StockID", c => c.Int());
            DropForeignKey("dbo.Portfolios", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "Portfolio_PortfolioID", "dbo.Portfolios");
            DropForeignKey("dbo.Portfolios", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Portfolios", new[] { "Stock_StockID" });
            DropIndex("dbo.Portfolios", new[] { "Owner_Id" });
            DropIndex("dbo.Transactions", new[] { "Portfolio_PortfolioID" });
            DropColumn("dbo.Stocks", "Status");
            DropColumn("dbo.Transactions", "Portfolio_PortfolioID");
            DropTable("dbo.Portfolios");
            CreateIndex("dbo.Transactions", "Stock_StockID");
            AddForeignKey("dbo.Transactions", "Stock_StockID", "dbo.Stocks", "StockID");
        }
    }
}

namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Portfoliio : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Portfolios", new[] { "Stock_StockID" });
            CreateTable(
                "dbo.StockKinds",
                c => new
                    {
                        StockKindID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StockKindID);
            
            CreateIndex("dbo.Portfolios", "stock_StockID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Portfolios", new[] { "stock_StockID" });
            DropTable("dbo.StockKinds");
            CreateIndex("dbo.Portfolios", "Stock_StockID");
        }
    }
}

namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class STOCkIRA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IRAs",
                c => new
                    {
                        IRAID = c.Int(nullable: false, identity: true),
                        IRAName = c.String(nullable: false),
                        IRABalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
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
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            AddColumn("dbo.HomeViewModels", "ira_IRAID", c => c.Int());
            AddColumn("dbo.HomeViewModels", "stock_StockID", c => c.Int());
            CreateIndex("dbo.HomeViewModels", "ira_IRAID");
            CreateIndex("dbo.HomeViewModels", "stock_StockID");
            AddForeignKey("dbo.HomeViewModels", "ira_IRAID", "dbo.IRAs", "IRAID");
            AddForeignKey("dbo.HomeViewModels", "stock_StockID", "dbo.Stocks", "StockID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HomeViewModels", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HomeViewModels", "ira_IRAID", "dbo.IRAs");
            DropForeignKey("dbo.IRAs", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Stocks", new[] { "Owner_Id" });
            DropIndex("dbo.IRAs", new[] { "Owner_Id" });
            DropIndex("dbo.HomeViewModels", new[] { "stock_StockID" });
            DropIndex("dbo.HomeViewModels", new[] { "ira_IRAID" });
            DropColumn("dbo.HomeViewModels", "stock_StockID");
            DropColumn("dbo.HomeViewModels", "ira_IRAID");
            DropTable("dbo.Stocks");
            DropTable("dbo.IRAs");
        }
    }
}

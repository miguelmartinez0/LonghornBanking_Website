namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Stocks", "StockType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "StockType", c => c.Int(nullable: false));
        }
    }
}

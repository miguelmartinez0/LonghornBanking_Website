namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dispute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Portfolios", "Shares", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Portfolios", "Shares");
        }
    }
}

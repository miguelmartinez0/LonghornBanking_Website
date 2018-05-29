namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Overdraft : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransferViewModels", "Overdraft", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransferViewModels", "Overdraft");
        }
    }
}

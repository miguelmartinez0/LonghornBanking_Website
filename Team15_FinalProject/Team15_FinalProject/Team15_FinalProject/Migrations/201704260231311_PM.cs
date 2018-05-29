namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.Int(nullable: false, identity: true),
                        PayeeName = c.String(nullable: false),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        PayeeType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayeeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payees");
        }
    }
}

namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class P2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        StreeAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayeeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payees");
        }
    }
}

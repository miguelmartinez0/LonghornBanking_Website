namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dasdadjlasd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Payees", newName: "Payees1");
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeesID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StreeAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayeesID);
            
            AddColumn("dbo.AspNetUsers", "Payees_PayeesID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Payees_PayeesID");
            AddForeignKey("dbo.AspNetUsers", "Payees_PayeesID", "dbo.Payees", "PayeesID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Payees_PayeesID", "dbo.Payees");
            DropIndex("dbo.AspNetUsers", new[] { "Payees_PayeesID" });
            DropColumn("dbo.AspNetUsers", "Payees_PayeesID");
            DropTable("dbo.Payees");
            RenameTable(name: "dbo.Payees1", newName: "Payees");
        }
    }
}

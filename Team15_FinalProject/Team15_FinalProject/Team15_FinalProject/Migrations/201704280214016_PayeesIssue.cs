namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayeesIssue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Payee_PayeeID", "dbo.Payees1");
            DropForeignKey("dbo.AspNetUsers", "Payees_PayeesID", "dbo.Payees");
            DropIndex("dbo.AspNetUsers", new[] { "Payee_PayeeID" });
            DropIndex("dbo.AspNetUsers", new[] { "Payees_PayeesID" });
            AddColumn("dbo.Payees", "PayeesName", c => c.String(nullable: false));
            AddColumn("dbo.Payees", "PayeesZip", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Payee_PayeeID");
            DropColumn("dbo.AspNetUsers", "Payees_PayeesID");
            DropColumn("dbo.Payees", "Name");
            DropColumn("dbo.Payees", "Zip");
            DropTable("dbo.Payees1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payees1",
                c => new
                    {
                        PayeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StreeAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayeeID);
            
            AddColumn("dbo.Payees", "Zip", c => c.Int(nullable: false));
            AddColumn("dbo.Payees", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Payees_PayeesID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Payee_PayeeID", c => c.Int());
            DropColumn("dbo.Payees", "PayeesZip");
            DropColumn("dbo.Payees", "PayeesName");
            CreateIndex("dbo.AspNetUsers", "Payees_PayeesID");
            CreateIndex("dbo.AspNetUsers", "Payee_PayeeID");
            AddForeignKey("dbo.AspNetUsers", "Payees_PayeesID", "dbo.Payees", "PayeesID");
            AddForeignKey("dbo.AspNetUsers", "Payee_PayeeID", "dbo.Payees1", "PayeeID");
        }
    }
}

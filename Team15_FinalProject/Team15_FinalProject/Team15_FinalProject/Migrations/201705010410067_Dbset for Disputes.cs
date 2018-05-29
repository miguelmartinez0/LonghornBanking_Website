namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbsetforDisputes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        DisputeID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        DisputeComment = c.String(nullable: false),
                        CorrectAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeleteTransaction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DisputeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Disputes");
        }
    }
}

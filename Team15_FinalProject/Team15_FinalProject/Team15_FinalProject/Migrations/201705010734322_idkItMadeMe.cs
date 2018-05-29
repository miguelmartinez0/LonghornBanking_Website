namespace Team15_FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idkItMadeMe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disputes", "Transactions_TransactionID", c => c.Int());
            AlterColumn("dbo.Disputes", "DeleteTransaction", c => c.Int(nullable: false));
            CreateIndex("dbo.Disputes", "Transactions_TransactionID");
            AddForeignKey("dbo.Disputes", "Transactions_TransactionID", "dbo.Transactions", "TransactionID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Disputes", "Transactions_TransactionID", "dbo.Transactions");
            DropIndex("dbo.Disputes", new[] { "Transactions_TransactionID" });
            AlterColumn("dbo.Disputes", "DeleteTransaction", c => c.Boolean(nullable: false));
            DropColumn("dbo.Disputes", "Transactions_TransactionID");
        }
    }
}

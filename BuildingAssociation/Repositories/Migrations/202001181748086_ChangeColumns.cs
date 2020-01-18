namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProviderBills", "ProviderUnitPrice", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProviderBills", "ProviderUnitPrice", c => c.Double(nullable: false));
        }
    }
}

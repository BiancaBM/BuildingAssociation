namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumns1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProviderBills", "Units", c => c.Double(nullable: false));
            AlterColumn("dbo.ProviderBills", "Other", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProviderBills", "Other", c => c.Int(nullable: false));
            AlterColumn("dbo.ProviderBills", "Units", c => c.Int(nullable: false));
        }
    }
}

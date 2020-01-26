namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProviderBillAndWaterUnits1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Providers", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.ProviderBills", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProviderBills", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Providers", "Type");
        }
    }
}

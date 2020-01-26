namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProviderBillAndWaterUnits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProviderBills", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.WaterConsumptions", "KitchenUnits", c => c.Double(nullable: false));
            AddColumn("dbo.WaterConsumptions", "BathroomUnits", c => c.Double(nullable: false));
            DropColumn("dbo.WaterConsumptions", "HotWaterUnits");
            DropColumn("dbo.WaterConsumptions", "ColdWaterUnits");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WaterConsumptions", "ColdWaterUnits", c => c.Double(nullable: false));
            AddColumn("dbo.WaterConsumptions", "HotWaterUnits", c => c.Double(nullable: false));
            DropColumn("dbo.WaterConsumptions", "BathroomUnits");
            DropColumn("dbo.WaterConsumptions", "KitchenUnits");
            DropColumn("dbo.ProviderBills", "Type");
        }
    }
}

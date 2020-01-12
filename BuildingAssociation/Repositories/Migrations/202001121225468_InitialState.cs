namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialState : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WaterConsumptions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Apartments", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBillItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProviderBills", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.UserBillItems", "ConsumptionType_ConsumptionTypeId", "dbo.ConsumptionTypes");
            RenameColumn(table: "dbo.UserBillItems", name: "ConsumptionType_ConsumptionTypeId", newName: "ConsumptionType_UniqueId");
            RenameIndex(table: "dbo.UserBillItems", name: "IX_ConsumptionType_ConsumptionTypeId", newName: "IX_ConsumptionType_UniqueId");
            DropPrimaryKey("dbo.Apartments");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.WaterConsumptions");
            DropPrimaryKey("dbo.ProviderBills");
            DropPrimaryKey("dbo.Providers");
            DropPrimaryKey("dbo.ConsumptionTypes");
            DropPrimaryKey("dbo.UserBillItems");
            AddColumn("dbo.Apartments", "UniqueId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Users", "UniqueId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.WaterConsumptions", "UniqueId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.ProviderBills", "UniqueId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Providers", "UniqueId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.ConsumptionTypes", "UniqueId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.UserBillItems", "UniqueId", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Apartments", "UniqueId");
            AddPrimaryKey("dbo.Users", "UniqueId");
            AddPrimaryKey("dbo.WaterConsumptions", "UniqueId");
            AddPrimaryKey("dbo.ProviderBills", "UniqueId");
            AddPrimaryKey("dbo.Providers", "UniqueId");
            AddPrimaryKey("dbo.ConsumptionTypes", "UniqueId");
            AddPrimaryKey("dbo.UserBillItems", "UniqueId");
            AddForeignKey("dbo.WaterConsumptions", "UserId", "dbo.Users", "UniqueId");
            AddForeignKey("dbo.Apartments", "UserId", "dbo.Users", "UniqueId");
            AddForeignKey("dbo.UserBillItems", "UserId", "dbo.Users", "UniqueId");
            AddForeignKey("dbo.ProviderBills", "ProviderId", "dbo.Providers", "UniqueId");
            AddForeignKey("dbo.UserBillItems", "ConsumptionType_UniqueId", "dbo.ConsumptionTypes", "UniqueId", cascadeDelete: true);
            DropColumn("dbo.Apartments", "ApartmentId");
            DropColumn("dbo.Apartments", "Guid");
            DropColumn("dbo.Users", "UserId");
            DropColumn("dbo.Users", "Guid");
            DropColumn("dbo.WaterConsumptions", "WaterConsumptionId");
            DropColumn("dbo.WaterConsumptions", "Guid");
            DropColumn("dbo.ProviderBills", "BillId");
            DropColumn("dbo.ProviderBills", "Guid");
            DropColumn("dbo.Providers", "ProviderId");
            DropColumn("dbo.Providers", "Guid");
            DropColumn("dbo.ConsumptionTypes", "ConsumptionTypeId");
            DropColumn("dbo.ConsumptionTypes", "Guid");
            DropColumn("dbo.UserBillItems", "UserBillItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserBillItems", "UserBillItemId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.ConsumptionTypes", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.ConsumptionTypes", "ConsumptionTypeId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Providers", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.Providers", "ProviderId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.ProviderBills", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.ProviderBills", "BillId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.WaterConsumptions", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.WaterConsumptions", "WaterConsumptionId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Users", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "UserId", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Apartments", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.Apartments", "ApartmentId", c => c.Long(nullable: false, identity: true));
            DropForeignKey("dbo.UserBillItems", "ConsumptionType_UniqueId", "dbo.ConsumptionTypes");
            DropForeignKey("dbo.ProviderBills", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.UserBillItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.Apartments", "UserId", "dbo.Users");
            DropForeignKey("dbo.WaterConsumptions", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.UserBillItems");
            DropPrimaryKey("dbo.ConsumptionTypes");
            DropPrimaryKey("dbo.Providers");
            DropPrimaryKey("dbo.ProviderBills");
            DropPrimaryKey("dbo.WaterConsumptions");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Apartments");
            DropColumn("dbo.UserBillItems", "UniqueId");
            DropColumn("dbo.ConsumptionTypes", "UniqueId");
            DropColumn("dbo.Providers", "UniqueId");
            DropColumn("dbo.ProviderBills", "UniqueId");
            DropColumn("dbo.WaterConsumptions", "UniqueId");
            DropColumn("dbo.Users", "UniqueId");
            DropColumn("dbo.Apartments", "UniqueId");
            AddPrimaryKey("dbo.UserBillItems", "UserBillItemId");
            AddPrimaryKey("dbo.ConsumptionTypes", "ConsumptionTypeId");
            AddPrimaryKey("dbo.Providers", "ProviderId");
            AddPrimaryKey("dbo.ProviderBills", "BillId");
            AddPrimaryKey("dbo.WaterConsumptions", "WaterConsumptionId");
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.Apartments", "ApartmentId");
            RenameIndex(table: "dbo.UserBillItems", name: "IX_ConsumptionType_UniqueId", newName: "IX_ConsumptionType_ConsumptionTypeId");
            RenameColumn(table: "dbo.UserBillItems", name: "ConsumptionType_UniqueId", newName: "ConsumptionType_ConsumptionTypeId");
            AddForeignKey("dbo.UserBillItems", "ConsumptionType_ConsumptionTypeId", "dbo.ConsumptionTypes", "ConsumptionTypeId", cascadeDelete: true);
            AddForeignKey("dbo.ProviderBills", "ProviderId", "dbo.Providers", "ProviderId");
            AddForeignKey("dbo.UserBillItems", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Apartments", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.WaterConsumptions", "UserId", "dbo.Users", "UserId");
        }
    }
}

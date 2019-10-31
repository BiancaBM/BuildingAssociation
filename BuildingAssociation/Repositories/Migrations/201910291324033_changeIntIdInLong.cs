namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIntIdInLong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Apartments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Consumptions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Consumptions", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Bills", "ProviderId", "dbo.Providers");
            DropIndex("dbo.Apartments", new[] { "UserId" });
            DropIndex("dbo.Consumptions", new[] { "ProviderId" });
            DropIndex("dbo.Consumptions", new[] { "UserId" });
            DropIndex("dbo.Bills", new[] { "ProviderId" });
            DropPrimaryKey("dbo.Apartments");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Consumptions");
            DropPrimaryKey("dbo.Providers");
            DropPrimaryKey("dbo.Bills");
            AlterColumn("dbo.Apartments", "ApartmentId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Apartments", "UserId", c => c.Long());
            AlterColumn("dbo.Users", "UserId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Consumptions", "ConsumptionId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Consumptions", "ProviderId", c => c.Long());
            AlterColumn("dbo.Consumptions", "UserId", c => c.Long());
            AlterColumn("dbo.Providers", "ProviderId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Bills", "BillId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Bills", "ProviderId", c => c.Long());
            AddPrimaryKey("dbo.Apartments", "ApartmentId");
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.Consumptions", "ConsumptionId");
            AddPrimaryKey("dbo.Providers", "ProviderId");
            AddPrimaryKey("dbo.Bills", "BillId");
            CreateIndex("dbo.Apartments", "UserId");
            CreateIndex("dbo.Consumptions", "ProviderId");
            CreateIndex("dbo.Consumptions", "UserId");
            CreateIndex("dbo.Bills", "ProviderId");
            AddForeignKey("dbo.Apartments", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Consumptions", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Consumptions", "ProviderId", "dbo.Providers", "ProviderId");
            AddForeignKey("dbo.Bills", "ProviderId", "dbo.Providers", "ProviderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Consumptions", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Consumptions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Apartments", "UserId", "dbo.Users");
            DropIndex("dbo.Bills", new[] { "ProviderId" });
            DropIndex("dbo.Consumptions", new[] { "UserId" });
            DropIndex("dbo.Consumptions", new[] { "ProviderId" });
            DropIndex("dbo.Apartments", new[] { "UserId" });
            DropPrimaryKey("dbo.Bills");
            DropPrimaryKey("dbo.Providers");
            DropPrimaryKey("dbo.Consumptions");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Apartments");
            AlterColumn("dbo.Bills", "ProviderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bills", "BillId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Providers", "ProviderId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Consumptions", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Consumptions", "ProviderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Consumptions", "ConsumptionId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Apartments", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Apartments", "ApartmentId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Bills", "BillId");
            AddPrimaryKey("dbo.Providers", "ProviderId");
            AddPrimaryKey("dbo.Consumptions", "ConsumptionId");
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.Apartments", "ApartmentId");
            CreateIndex("dbo.Bills", "ProviderId");
            CreateIndex("dbo.Consumptions", "UserId");
            CreateIndex("dbo.Consumptions", "ProviderId");
            CreateIndex("dbo.Apartments", "UserId");
            AddForeignKey("dbo.Bills", "ProviderId", "dbo.Providers", "ProviderId", cascadeDelete: true);
            AddForeignKey("dbo.Consumptions", "ProviderId", "dbo.Providers", "ProviderId", cascadeDelete: true);
            AddForeignKey("dbo.Consumptions", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Apartments", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}

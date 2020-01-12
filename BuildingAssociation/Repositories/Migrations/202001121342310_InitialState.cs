namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        Surface = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        UserId = c.Long(),
                    })
                .PrimaryKey(t => t.UniqueId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        MembersCount = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.UniqueId);
            
            CreateTable(
                "dbo.WaterConsumptions",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        HotWaterUnits = c.Double(nullable: false),
                        ColdWaterUnits = c.Double(nullable: false),
                        UserId = c.Long(),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UniqueId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProviderBills",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        ProviderId = c.Long(),
                        Units = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueId)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        UnitPrice = c.Double(nullable: false),
                        Name = c.String(),
                        CUI = c.String(),
                        BankAccount = c.String(),
                    })
                .PrimaryKey(t => t.UniqueId);
            
            CreateTable(
                "dbo.ConsumptionTypes",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        CalculationType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueId);
            
            CreateTable(
                "dbo.UserBillItems",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(),
                        Price = c.Double(nullable: false),
                        CreationDate = c.DateTime(),
                        ConsumptionType_UniqueId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueId)
                .ForeignKey("dbo.ConsumptionTypes", t => t.ConsumptionType_UniqueId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ConsumptionType_UniqueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBillItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBillItems", "ConsumptionType_UniqueId", "dbo.ConsumptionTypes");
            DropForeignKey("dbo.ProviderBills", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Apartments", "UserId", "dbo.Users");
            DropForeignKey("dbo.WaterConsumptions", "UserId", "dbo.Users");
            DropIndex("dbo.UserBillItems", new[] { "ConsumptionType_UniqueId" });
            DropIndex("dbo.UserBillItems", new[] { "UserId" });
            DropIndex("dbo.ProviderBills", new[] { "ProviderId" });
            DropIndex("dbo.WaterConsumptions", new[] { "UserId" });
            DropIndex("dbo.Apartments", new[] { "UserId" });
            DropTable("dbo.UserBillItems");
            DropTable("dbo.ConsumptionTypes");
            DropTable("dbo.Providers");
            DropTable("dbo.ProviderBills");
            DropTable("dbo.WaterConsumptions");
            DropTable("dbo.Users");
            DropTable("dbo.Apartments");
        }
    }
}

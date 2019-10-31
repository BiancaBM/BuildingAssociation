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
                        ApartmentId = c.Int(nullable: false, identity: true),
                        Surface = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApartmentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        MembersCount = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        Guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Consumptions",
                c => new
                    {
                        ConsumptionId = c.Int(nullable: false, identity: true),
                        ProviderId = c.Int(nullable: false),
                        Units = c.Double(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ConsumptionId)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProviderId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderId = c.Int(nullable: false, identity: true),
                        UnitPrice = c.Double(nullable: false),
                        Name = c.String(),
                        CUI = c.String(),
                        BankAccount = c.String(),
                    })
                .PrimaryKey(t => t.ProviderId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        ProviderId = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.ProviderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Apartments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Consumptions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Consumptions", "ProviderId", "dbo.Providers");
            DropIndex("dbo.Bills", new[] { "ProviderId" });
            DropIndex("dbo.Consumptions", new[] { "UserId" });
            DropIndex("dbo.Consumptions", new[] { "ProviderId" });
            DropIndex("dbo.Apartments", new[] { "UserId" });
            DropTable("dbo.Bills");
            DropTable("dbo.Providers");
            DropTable("dbo.Consumptions");
            DropTable("dbo.Users");
            DropTable("dbo.Apartments");
        }
    }
}

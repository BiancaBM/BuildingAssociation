namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserBill : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserBillItems", "ConsumptionType_UniqueId", "dbo.OtherConsumptions");
            DropForeignKey("dbo.UserBillItems", "UserId", "dbo.Users");
            DropIndex("dbo.UserBillItems", new[] { "UserId" });
            DropIndex("dbo.UserBillItems", new[] { "ConsumptionType_UniqueId" });
            DropTable("dbo.UserBillItems");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.UniqueId);
            
            CreateIndex("dbo.UserBillItems", "ConsumptionType_UniqueId");
            CreateIndex("dbo.UserBillItems", "UserId");
            AddForeignKey("dbo.UserBillItems", "UserId", "dbo.Users", "UniqueId");
            AddForeignKey("dbo.UserBillItems", "ConsumptionType_UniqueId", "dbo.OtherConsumptions", "UniqueId", cascadeDelete: true);
        }
    }
}

namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectMansionToConsumptionType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumptionTypes", "MansionId", c => c.Long());
            CreateIndex("dbo.ConsumptionTypes", "MansionId");
            AddForeignKey("dbo.ConsumptionTypes", "MansionId", "dbo.Mansions", "UniqueId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConsumptionTypes", "MansionId", "dbo.Mansions");
            DropIndex("dbo.ConsumptionTypes", new[] { "MansionId" });
            DropColumn("dbo.ConsumptionTypes", "MansionId");
        }
    }
}

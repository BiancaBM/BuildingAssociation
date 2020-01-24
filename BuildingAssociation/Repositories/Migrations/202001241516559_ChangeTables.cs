namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsumptionTypes", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConsumptionTypes", "Date");
        }
    }
}

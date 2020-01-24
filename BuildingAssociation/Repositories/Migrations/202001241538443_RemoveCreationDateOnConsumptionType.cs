namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCreationDateOnConsumptionType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConsumptionTypes", "CreationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConsumptionTypes", "CreationDate", c => c.DateTime());
        }
    }
}

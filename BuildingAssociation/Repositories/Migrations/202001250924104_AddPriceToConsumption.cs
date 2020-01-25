namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceToConsumption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OtherConsumptions", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OtherConsumptions", "Price");
        }
    }
}

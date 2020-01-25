namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ConsumptionTypes", newName: "OtherConsumptions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.OtherConsumptions", newName: "ConsumptionTypes");
        }
    }
}

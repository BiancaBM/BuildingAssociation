namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_guidColum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.Consumptions", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.Providers", "Guid", c => c.Guid(nullable: false));
            AddColumn("dbo.Bills", "Guid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "Guid");
            DropColumn("dbo.Providers", "Guid");
            DropColumn("dbo.Consumptions", "Guid");
            DropColumn("dbo.Apartments", "Guid");
        }
    }
}

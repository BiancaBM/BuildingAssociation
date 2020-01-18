namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumns2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Roles", c => c.String());
            DropColumn("dbo.Users", "IsAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Roles");
        }
    }
}

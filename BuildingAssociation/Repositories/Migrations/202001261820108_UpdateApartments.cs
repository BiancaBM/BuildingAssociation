namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApartments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "MembersCount", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "MembersCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "MembersCount", c => c.Int(nullable: false));
            DropColumn("dbo.Apartments", "MembersCount");
        }
    }
}

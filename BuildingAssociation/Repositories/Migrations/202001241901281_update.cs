namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Apartments", "Surface", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Apartments", "Surface", c => c.Int(nullable: false));
        }
    }
}

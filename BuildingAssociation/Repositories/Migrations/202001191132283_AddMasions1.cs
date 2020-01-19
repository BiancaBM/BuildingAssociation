namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMasions1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mansions", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Mansions", "TotalFunds", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mansions", "TotalFunds", c => c.Int(nullable: false));
            AlterColumn("dbo.Mansions", "Address", c => c.Int(nullable: false));
        }
    }
}

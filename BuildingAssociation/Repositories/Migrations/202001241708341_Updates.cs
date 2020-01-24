namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "IndividualQuota", c => c.Double(nullable: false));
            AddColumn("dbo.Apartments", "MansionId", c => c.Long());
            CreateIndex("dbo.Apartments", "MansionId");
            AddForeignKey("dbo.Apartments", "MansionId", "dbo.Mansions", "UniqueId");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "MansionId", "dbo.Mansions");
            DropIndex("dbo.Apartments", new[] { "MansionId" });
            DropColumn("dbo.Apartments", "MansionId");
            DropColumn("dbo.Apartments", "IndividualQuota");
        }
    }
}

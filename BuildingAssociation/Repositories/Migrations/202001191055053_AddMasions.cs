namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMasions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mansions",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        Address = c.Int(nullable: false),
                        TotalFunds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UniqueId);
            
            AddColumn("dbo.Users", "MansionId", c => c.Long());
            AddColumn("dbo.ProviderBills", "MansionId", c => c.Long());
            CreateIndex("dbo.Users", "MansionId");
            CreateIndex("dbo.ProviderBills", "MansionId");
            AddForeignKey("dbo.ProviderBills", "MansionId", "dbo.Mansions", "UniqueId");
            AddForeignKey("dbo.Users", "MansionId", "dbo.Mansions", "UniqueId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "MansionId", "dbo.Mansions");
            DropForeignKey("dbo.ProviderBills", "MansionId", "dbo.Mansions");
            DropIndex("dbo.ProviderBills", new[] { "MansionId" });
            DropIndex("dbo.Users", new[] { "MansionId" });
            DropColumn("dbo.ProviderBills", "MansionId");
            DropColumn("dbo.Users", "MansionId");
            DropTable("dbo.Mansions");
        }
    }
}

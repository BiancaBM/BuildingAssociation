namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeneratedBillEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeneratedBills",
                c => new
                    {
                        UniqueId = c.Long(nullable: false, identity: true),
                        CSV = c.String(),
                        Date = c.DateTime(nullable: false),
                        MansionId = c.Long(),
                    })
                .PrimaryKey(t => t.UniqueId)
                .ForeignKey("dbo.Mansions", t => t.MansionId)
                .Index(t => t.MansionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeneratedBills", "MansionId", "dbo.Mansions");
            DropIndex("dbo.GeneratedBills", new[] { "MansionId" });
            DropTable("dbo.GeneratedBills");
        }
    }
}

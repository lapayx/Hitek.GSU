namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_history : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestHistories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Result = c.Int(nullable: false),
                        TestId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestHistories", "TestId", "dbo.Tests");
            DropIndex("dbo.TestHistories", new[] { "TestId" });
            DropTable("dbo.TestHistories");
        }
    }
}

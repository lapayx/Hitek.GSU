namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_test_subject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestSubjects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ParentId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tests", "TestSubjectId", c => c.Long());
            CreateIndex("dbo.Tests", "TestSubjectId");
            AddForeignKey("dbo.Tests", "TestSubjectId", "dbo.TestSubjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "TestSubjectId", "dbo.TestSubjects");
            DropIndex("dbo.Tests", new[] { "TestSubjectId" });
            DropColumn("dbo.Tests", "TestSubjectId");
            DropTable("dbo.TestSubjects");
        }
    }
}

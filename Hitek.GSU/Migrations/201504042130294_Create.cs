namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestQuestions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Text = c.String(nullable: false),
                        TestId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.TestAnswers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 200),
                        IsRight = c.String(),
                        TestQuestionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestQuestions", t => t.TestQuestionId, cascadeDelete: true)
                .Index(t => t.TestQuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestAnswers", "TestQuestionId", "dbo.TestQuestions");
            DropForeignKey("dbo.TestQuestions", "TestId", "dbo.Tests");
            DropIndex("dbo.TestAnswers", new[] { "TestQuestionId" });
            DropIndex("dbo.TestQuestions", new[] { "TestId" });
            DropTable("dbo.TestAnswers");
            DropTable("dbo.TestQuestions");
            DropTable("dbo.Tests");
        }
    }
}

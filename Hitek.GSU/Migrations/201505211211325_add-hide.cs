namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addhide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "IsHide", c => c.Boolean(nullable: false));
            AddColumn("dbo.TestQuestions", "IsHide", c => c.Boolean(nullable: false));
            AddColumn("dbo.TestAnswers", "IsHide", c => c.Boolean(nullable: false));
            AddColumn("dbo.TestSubjects", "IsHide", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestSubjects", "IsHide");
            DropColumn("dbo.TestAnswers", "IsHide");
            DropColumn("dbo.TestQuestions", "IsHide");
            DropColumn("dbo.Tests", "IsHide");
        }
    }
}

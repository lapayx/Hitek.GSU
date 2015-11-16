namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcountquestionshow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "CountQuestionForShow", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "CountQuestionForShow");
        }
    }
}

namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfilds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestHistories", "AccountId", c => c.Long(nullable: false));
            AddColumn("dbo.TestAnswers", "AccountId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestAnswers", "AccountId");
            DropColumn("dbo.TestHistories", "AccountId");
        }
    }
}

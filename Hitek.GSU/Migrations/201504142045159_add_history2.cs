namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_history2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestHistories", "Result", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestHistories", "Result", c => c.Int(nullable: false));
        }
    }
}

namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestHistories", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestHistories", "Date");
        }
    }
}

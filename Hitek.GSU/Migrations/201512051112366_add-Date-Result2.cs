namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateResult2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestHistories", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestHistories", "Date", c => c.DateTime());
        }
    }
}

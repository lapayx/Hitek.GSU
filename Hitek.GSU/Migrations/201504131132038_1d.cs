namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestAnswers", "IsRight", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestAnswers", "IsRight", c => c.String());
        }
    }
}

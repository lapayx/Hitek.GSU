namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "SecurityStamp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "SecurityStamp");
        }
    }
}

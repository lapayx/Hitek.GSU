namespace Hitek.GSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test23 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            AlterColumn("dbo.Accounts", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Accounts", "Name", c => c.String(nullable: false, maxLength: 200));
            AddPrimaryKey("dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Roles", new[] { "AccountId" });
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Accounts", "Id", c => c.String(nullable: false, maxLength: 200));
            DropTable("dbo.Roles");
            AddPrimaryKey("dbo.Accounts", "Id");
        }
    }
}

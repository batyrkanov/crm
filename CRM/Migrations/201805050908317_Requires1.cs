namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requires1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Companies", "CompanyName", c => c.String(maxLength: 250));
            AlterColumn("dbo.TaskStatus", "StatusName", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskStatus", "StatusName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Companies", "CompanyName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 250));
        }
    }
}

namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requires : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Taskas", "TaskName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Taskas", "MarketerName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Companies", "CompanyName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.TaskStatus", "StatusName", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskStatus", "StatusName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Companies", "CompanyName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Taskas", "MarketerName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Taskas", "TaskName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(maxLength: 250));
        }
    }
}

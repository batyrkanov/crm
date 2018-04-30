namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTasksTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taskas", "CompanyName", c => c.String(maxLength: 250));
            AddColumn("dbo.Taskas", "CategoryName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Taskas", "ManagerName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Taskas", "TaskStatus", c => c.String(maxLength: 250));
            DropColumn("dbo.Taskas", "CompanyId");
            DropColumn("dbo.Taskas", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taskas", "CategoryId", c => c.String(maxLength: 128));
            AddColumn("dbo.Taskas", "CompanyId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Taskas", "TaskStatus", c => c.String(maxLength: 128));
            AlterColumn("dbo.Taskas", "ManagerName", c => c.String(maxLength: 128));
            DropColumn("dbo.Taskas", "CategoryName");
            DropColumn("dbo.Taskas", "CompanyName");
        }
    }
}

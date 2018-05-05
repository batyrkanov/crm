namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Taskas", "CompanyName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Taskas", "MarketerPhone", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Taskas", "CategoryName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Taskas", "TaskDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Taskas", "StatusName", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Taskas", "StatusName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Taskas", "TaskDate", c => c.DateTime());
            AlterColumn("dbo.Taskas", "CategoryName", c => c.String(maxLength: 250));
            AlterColumn("dbo.Taskas", "MarketerPhone", c => c.String(maxLength: 100));
            AlterColumn("dbo.Taskas", "CompanyName", c => c.String(maxLength: 250));
        }
    }
}

namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taskas", "StatusName", c => c.String(maxLength: 250));
            DropColumn("dbo.Taskas", "TaskStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taskas", "TaskStatus", c => c.String(maxLength: 250));
            DropColumn("dbo.Taskas", "StatusName");
        }
    }
}

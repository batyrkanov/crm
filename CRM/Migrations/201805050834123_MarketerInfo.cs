namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarketerInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taskas", "MarketerName", c => c.String(maxLength: 100));
            AddColumn("dbo.Taskas", "MarketerEmail", c => c.String(maxLength: 150));
            AddColumn("dbo.Taskas", "MarketerPhone", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Taskas", "MarketerPhone");
            DropColumn("dbo.Taskas", "MarketerEmail");
            DropColumn("dbo.Taskas", "MarketerName");
        }
    }
}

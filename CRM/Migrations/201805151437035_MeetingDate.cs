namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taskas", "MeetingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Taskas", "MeetingDate");
        }
    }
}

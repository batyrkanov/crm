namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeetingDate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Taskas", "LastMeetingDate", c => c.DateTime());
            DropColumn("dbo.Taskas", "MeetingDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Taskas", "MeetingDate", c => c.DateTime());
            DropColumn("dbo.Taskas", "LastMeetingDate");
        }
    }
}

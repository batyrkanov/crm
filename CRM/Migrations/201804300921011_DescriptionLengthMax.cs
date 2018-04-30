namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionLengthMax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Taskas", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Taskas", "Description", c => c.String(unicode: false, storeType: "text"));
        }
    }
}

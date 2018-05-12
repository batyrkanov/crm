namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Categories", "CategoryName", unique: true);
            CreateIndex("dbo.Companies", "CompanyName", unique: true);
            CreateIndex("dbo.TaskStatus", "StatusName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TaskStatus", new[] { "StatusName" });
            DropIndex("dbo.Companies", new[] { "CompanyName" });
            DropIndex("dbo.Categories", new[] { "CategoryName" });
        }
    }
}

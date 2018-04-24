namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false),
                        CategoryName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Taskas",
                c => new
                    {
                        TaskId = c.Guid(nullable: false),
                        TaskName = c.String(maxLength: 50),
                        CompanyId = c.String(maxLength: 128),
                        CategoryId = c.String(maxLength: 128),
                        TaskDate = c.DateTime(),
                        ManagerName = c.String(maxLength: 128),
                        TaskStatus = c.String(maxLength: 128),
                        Description = c.String(unicode: false, storeType: "text"),
                        Categories_CategoryId = c.Guid(),
                        Companies_CompanyId = c.Guid(),
                        TaskStatuses_StatusId = c.Guid(),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Categories", t => t.Categories_CategoryId)
                .ForeignKey("dbo.Companies", t => t.Companies_CompanyId)
                .ForeignKey("dbo.TaskStatus", t => t.TaskStatuses_StatusId)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.Categories_CategoryId)
                .Index(t => t.Companies_CompanyId)
                .Index(t => t.TaskStatuses_StatusId)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Guid(nullable: false),
                        CompanyName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.TaskStatus",
                c => new
                    {
                        StatusId = c.Guid(nullable: false),
                        StatusName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.StatusId);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Taskas", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Taskas", "TaskStatuses_StatusId", "dbo.TaskStatus");
            DropForeignKey("dbo.Taskas", "Companies_CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Taskas", "Categories_CategoryId", "dbo.Categories");
            DropIndex("dbo.Taskas", new[] { "Users_Id" });
            DropIndex("dbo.Taskas", new[] { "TaskStatuses_StatusId" });
            DropIndex("dbo.Taskas", new[] { "Companies_CompanyId" });
            DropIndex("dbo.Taskas", new[] { "Categories_CategoryId" });
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.TaskStatus");
            DropTable("dbo.Companies");
            DropTable("dbo.Taskas");
            DropTable("dbo.Categories");
        }
    }
}

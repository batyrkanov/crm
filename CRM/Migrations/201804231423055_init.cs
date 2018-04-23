namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.String(nullable: false, maxLength: 128),
                        CategoryName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.String(nullable: false, maxLength: 128),
                        TaskName = c.String(maxLength: 50),
                        CompanyId = c.String(maxLength: 128),
                        CategoryId = c.String(maxLength: 128),
                        TaskDate = c.DateTime(),
                        ManagerName = c.String(maxLength: 128),
                        TaskStatus = c.String(maxLength: 128),
                        Description = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.TaskStatuses", t => t.TaskStatus)
                .ForeignKey("dbo.Users", t => t.ManagerName)
                .Index(t => t.CompanyId)
                .Index(t => t.CategoryId)
                .Index(t => t.ManagerName)
                .Index(t => t.TaskStatus);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.TaskStatuses",
                c => new
                    {
                        StatusId = c.String(nullable: false, maxLength: 128),
                        StatusName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        PasswordHash = c.String(),
                        PhoneNumber = c.String(),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "ManagerName", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Tasks", "TaskStatus", "dbo.TaskStatuses");
            DropForeignKey("dbo.Tasks", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Tasks", "CategoryId", "dbo.Categories");
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Tasks", new[] { "TaskStatus" });
            DropIndex("dbo.Tasks", new[] { "ManagerName" });
            DropIndex("dbo.Tasks", new[] { "CategoryId" });
            DropIndex("dbo.Tasks", new[] { "CompanyId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.TaskStatuses");
            DropTable("dbo.Companies");
            DropTable("dbo.Tasks");
            DropTable("dbo.Categories");
        }
    }
}

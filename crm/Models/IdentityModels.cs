using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using crm.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace crm.Models
{
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        [Required]
        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {

        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Entities.TaskStatus> TaskStatuses { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CRM");

            

            modelBuilder.Entity<ApplicationUserLogin>().Map(c =>
            {
                c.ToTable("UserLogin");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey
                });
            }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            // Mapping for ApiRole
            modelBuilder.Entity<ApplicationRole>().Map(c =>
            {
                c.ToTable("Role");
                c.Property(p => p.Id).HasColumnName("RoleId");
                c.Properties(p => new
                {
                    p.Name
                });
            }).HasKey(p => p.Id);

            modelBuilder.Entity<Category>().Map(c =>
            {
                c.ToTable("Category");
                c.Property(p => p.CategoryId).HasColumnName("CategoryId");
                c.Properties(p => new
                {
                    p.CategoryName
                });
            }).HasKey(p => p.CategoryId);

            modelBuilder.Entity<Company>().Map(c =>
            {
                c.ToTable("Company");
                c.Property(p => p.CompanyId).HasColumnName("CompanyId");
                c.Properties(p => new
                {
                    p.CompanyName
                });
            }).HasKey(p => p.CompanyId);

            modelBuilder.Entity<Entities.TaskStatus>().Map(c =>
            {
                c.ToTable("TaskStatus");
                c.Property(p => p.StatusId).HasColumnName("StatusId");
                c.Properties(p => new
                {
                    p.StatusName
                });
            }).HasKey(p => p.StatusId);

            modelBuilder.Entity<Tasks>().Map(c =>
            {
                c.ToTable("Tasks");
                c.Property(p => p.TaskId).HasColumnName("TaskId");
                c.Properties(p => new
                {
                    p.TaskName,
                    p.CompanyName,
                    p.CategoryName,
                    p.UserId,
                    p.ManagerName,
                    p.TaskStatusName,

                });
            }).HasKey(p => p.TaskId);
            modelBuilder.Entity<Tasks>().HasMany(c => c.Companies).WithOptional(e=>e.Tasks).HasForeignKey(c => c.CompanyId);
            modelBuilder.Entity<Tasks>().HasMany(c => c.Categories).WithOptional(e => e.Tasks).HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<Tasks>().HasMany(c => c.TaskStatuses).WithRequired(e => e.Tasks).HasForeignKey(c => c.StatusId);
            modelBuilder.Entity<Tasks>().HasMany(c => c.ApplicationUser).WithRequired(e => e.Tasks).HasForeignKey(c => c.Id);

            modelBuilder.Entity<ApplicationRole>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<ApplicationUser>().Map(c =>
            {
                c.ToTable("User");
                c.Property(p => p.Id).HasColumnName("UserId");
                c.Properties(p => new
                {
                    p.AccessFailedCount,
                    p.Email,
                    p.EmailConfirmed,
                    p.PasswordHash,
                    p.PhoneNumber,
                    p.PhoneNumberConfirmed,
                    p.TwoFactorEnabled,
                    p.SecurityStamp,
                    p.LockoutEnabled,
                    p.LockoutEndDateUtc,
                    p.UserName
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);
          

            modelBuilder.Entity<ApplicationUserRole>().Map(c =>
            {
                c.ToTable("UserRole");
                c.Properties(p => new
                {
                    p.UserId,
                    p.RoleId
                });
            })
            .HasKey(c => new { c.UserId, c.RoleId });

            modelBuilder.Entity<ApplicationUserClaim>().Map(c =>
            {
                c.ToTable("UserClaim");
                c.Property(p => p.Id).HasColumnName("UserClaimId");
                c.Properties(p => new
                {
                    p.UserId,
                    p.ClaimValue,
                    p.ClaimType
                });
            }).HasKey(c => c.Id);
        }
    }
}
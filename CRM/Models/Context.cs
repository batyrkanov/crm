using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Context : DbContext
    {
        public Context()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<TaskStatuses> TaskStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Tasks>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TaskStatuses>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.TaskStatuses)
                .HasForeignKey(e => e.TaskStatus);
            
        }
    }
}
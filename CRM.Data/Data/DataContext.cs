using CRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(string connectionString) : base(connectionString) { }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Taska> Tasks { get; set; }
        public virtual DbSet<Core.Entities.TaskStatus> TaskStatuses { get; set; }

        

    }
}

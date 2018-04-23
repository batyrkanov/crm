using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crm.Models.Entities
{
    public class Tasks
    {
        public Tasks()
        {
            TaskId = Guid.NewGuid().ToString();
        }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }

        public string UserId { get; set; }

        public string ManagerName { get; set; }
        public string TaskStatusName { get; set; }


        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<TaskStatus> TaskStatuses { get; set; }
    }
}
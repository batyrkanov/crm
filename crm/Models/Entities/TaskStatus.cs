using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crm.Models.Entities
{
    public class TaskStatus
    {
        public TaskStatus()
        {
            StatusId = Guid.NewGuid().ToString();
        }

        public string StatusId { get; set; }

        public string StatusName { get; set; }

        public virtual Tasks Tasks { get; set; }
    }
}
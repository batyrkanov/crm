using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class TaskStatus
    {
        public TaskStatus()
        {
            Tasks = new HashSet<Taska>();
        }

        [Key]
        public Guid StatusId { get; set; }

        [StringLength(250)]
        public string StatusName { get; set; }

        public virtual ICollection<Taska> Tasks { get; set; }
    }
}
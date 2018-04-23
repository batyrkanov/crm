namespace CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TaskStatuses
    {
        public TaskStatuses()
        {
            StatusId = Guid.NewGuid().ToString();
            Tasks = new HashSet<Tasks>();
        }

        [Key]
        public string StatusId { get; set; }

        [StringLength(250)]
        public string StatusName { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}

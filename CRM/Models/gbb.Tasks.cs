using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Tasks
    {
        public Tasks()
        {
            TaskId = Guid.NewGuid().ToString();
        }
        [Key]
        public string TaskId { get; set; }

        [StringLength(50)]
        public string TaskName { get; set; }

        [StringLength(128)]
        public string CompanyId { get; set; }

        [StringLength(128)]
        public string CategoryId { get; set; }

        public DateTime? TaskDate { get; set; }

        [StringLength(128)]
        public string ManagerName { get; set; }

        [StringLength(128)]
        public string TaskStatus { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public virtual Categories Categories { get; set; }

        public virtual Companies Companies { get; set; }

        public virtual TaskStatuses TaskStatuses { get; set; }

        public virtual ApplicationUser Users { get; set; }
    }
}
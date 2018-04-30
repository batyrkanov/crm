using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Taska
    {
        [Key]
        public Guid TaskId { get; set; }

        [StringLength(50)]
        public string TaskName { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }

        public DateTime? TaskDate { get; set; }

        [StringLength(250)]
        public string ManagerName { get; set; }

        [StringLength(250)]
        public string TaskStatus { get; set; }

        public string Description { get; set; }

        public virtual Category Categories { get; set; }

        public virtual Company Companies { get; set; }

        public virtual TaskStatus TaskStatuses { get; set; }

        public virtual ApplicationUser Users { get; set; }
    }
}
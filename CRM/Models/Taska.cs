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

        public virtual Category Categories { get; set; }

        public virtual Company Companies { get; set; }

        public virtual TaskStatus TaskStatuses { get; set; }

        public virtual ApplicationUser Users { get; set; }
    }
}
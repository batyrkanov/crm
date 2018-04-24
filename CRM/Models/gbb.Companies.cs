using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Companies
    {
        public Companies()
        {
            CompanyId = Guid.NewGuid().ToString();
            Tasks = new HashSet<Tasks>();
        }

        [Key]
        public string CompanyId { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Company
    {
        public Company()
        {
            Tasks = new HashSet<Taska>();
        }

        [Key]
        public Guid CompanyId { get; set; }

        [StringLength(250)]
        public string CompanyName { get; set; }
        
        public virtual ICollection<Taska> Tasks { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Category
    {
        public Category()
        {
            Tasks = new HashSet<Taska>();
        }

        [Key]
        public Guid CategoryId { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }

        public virtual ICollection<Taska> Tasks { get; set; }
        

    }
}
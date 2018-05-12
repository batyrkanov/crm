using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        
        [StringLength(250, ErrorMessage = "Длина строки не должна превышать 250 символов")]
        [Index(IsUnique = true)]
        public string CategoryName { get; set; }

        public virtual ICollection<Taska> Tasks { get; set; }
        

    }
}
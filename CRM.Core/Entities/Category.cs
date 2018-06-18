using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Core.Entities
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
        public string CategoryName { get; set; }

        public virtual ICollection<Taska> Tasks { get; set; }


    }
}

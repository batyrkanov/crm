namespace CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categories
    {
        public Categories()
        {
            CategoryId = Guid.NewGuid().ToString();
            Tasks = new HashSet<Tasks>();
        }

        [Key]
        public string CategoryId { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crm.Models.Entities
{
    public class Category
    {
        public Category()
        {
            CategoryId = Guid.NewGuid().ToString();
        }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual Tasks Tasks { get; set; }
    }
}
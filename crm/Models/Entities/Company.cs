using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crm.Models.Entities
{
    public class Company
    {
        public Company()
        {
            CompanyId = Guid.NewGuid().ToString();
        }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public virtual Tasks Tasks { get; set; }

    }
}
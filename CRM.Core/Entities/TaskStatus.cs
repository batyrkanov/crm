using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities
{
    public class TaskStatus
    {
        public TaskStatus()
        {
            Tasks = new HashSet<Taska>();
        }

        [Key]
        public Guid StatusId { get; set; }


        [StringLength(250, ErrorMessage = "Длина строки не должна превышать 250 символов")]
        public string StatusName { get; set; }

        public virtual ICollection<Taska> Tasks { get; set; }
    }
}

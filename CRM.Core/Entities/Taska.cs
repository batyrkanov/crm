using CRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities
{
    public class Taska
    {
        [Key]
        public Guid TaskId { get; set; }


        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"^[a-zA-ZЁёӨөҮүҢңА-Яа-я -]+$", ErrorMessage = "Ввод цифр запрещен")]
        [StringLength(50, ErrorMessage = "Длина строки не должна превышать 50 символов")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [StringLength(250)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"^[a-zA-ZЁёӨөҮүҢңА-Яа-я -]+$", ErrorMessage = "Ввод цифр запрещен")]
        [StringLength(100, ErrorMessage = "Длина строки не должна превышать 100 символов")]
        public string MarketerName { get; set; }

        [EmailAddress]
        [StringLength(150)]
        public string MarketerEmail { get; set; }


        [Required(ErrorMessage = "Заполните это поле")]
        [StringLength(100)]
        public string MarketerPhone { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [StringLength(250)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public DateTime? TaskDate { get; set; }


        public DateTime? LastMeetingDate { get; set; }

        [StringLength(250)]
        public string ManagerName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [StringLength(250)]
        public string StatusName { get; set; }

        public string Description { get; set; }

        public virtual Category Categories { get; set; }

        public virtual Company Companies { get; set; }

        public virtual TaskStatus TaskStatuses { get; set; }
        
    }
}

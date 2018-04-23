namespace CRM.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Id = Guid.NewGuid().ToString();
            Tasks = new HashSet<Tasks>();
            UserClaims = new HashSet<UserClaims>();
            UserLogins = new HashSet<UserLogins>();
            Roles = new HashSet<Roles>();
        }

        public string Id { get; set; }

        [StringLength(256)]
        [Display(Name = "E-mail")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage ="Неправильный E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage ="")]
        [StringLength(256)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "Пароль"), Required]
        [StringLength(128, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        [Display(Name="Телефон")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(50)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }

        public virtual ICollection<UserClaims> UserClaims { get; set; }

        public virtual ICollection<UserLogins> UserLogins { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}

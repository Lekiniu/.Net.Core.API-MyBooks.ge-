using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBooks.Data.Entities
{
    public class UserRolesEntity 
    {
        [ScaffoldColumn(false)]
        public int UserRoleId { get; set; }


        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }


        [MaxLength(50)]
        public string Title { get; set; }
  
        public bool IsActive { get; set; }

        public virtual ICollection<Users> Users { get; set; }

     

    }
}

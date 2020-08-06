using MyBooks.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Entities
{
    public class UserEntity
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        public DateTime Registration_Date { get; set; }

        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public int? AddressId { get; set; }

       
        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRolesEntity UserRole { get; set; }

        public virtual AddressEntity Address { get; set; }

        public virtual ICollection<Shopping_Cart_Entity> Carts { get; set; }

        public virtual ICollection<BooksEntity> Books { get; set; }
    }
}

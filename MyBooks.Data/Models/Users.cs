using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }


        [MaxLength(50)]
        public string Email { get; set; }

        public DateTime Registration_Date { get; set; }
      
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int? AddressId { get; set; }

        
        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRoles UserRole { get; set; }

        public virtual Addresses Address{ get; set; }

        public virtual ICollection<Shopping_Carts> Shop_Carts { get; set; }

        public virtual ICollection<WishList> WishList { get; set; }

        public virtual ICollection<Files> Files { get; set; }

        public virtual ICollection<Books> Books { get; set; }

    }
}

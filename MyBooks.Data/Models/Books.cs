using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime Publication_Date { get; set; }

        public float Price { get; set; }

        public Boolean InStock { get; set; }

        
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public virtual ICollection<WishList> WishList { get; set; }

        public virtual ICollection<Cart_items> Cart_Items { get; set; }

        public virtual ICollection<Catalogs_Books> Catalogs_Books { get; set; }

        public virtual ICollection<Files> Files { get; set; }
    }
}

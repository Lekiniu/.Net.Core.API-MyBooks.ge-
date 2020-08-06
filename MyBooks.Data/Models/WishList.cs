using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

       
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Books Book { get; set; }

        public DateTime Date { get; set; }
    }
}

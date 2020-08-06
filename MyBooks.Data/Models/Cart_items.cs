using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class Cart_items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        //[Required]
        public int? BookId { get; set; }

        //[Required]
        public int? CartId { get; set; }

        [ForeignKey("BookId")]
        public virtual Books Book { get; set; }

        [ForeignKey("CartId")]
        public virtual Shopping_Carts Shop_Carts { get; set; }
    }
}

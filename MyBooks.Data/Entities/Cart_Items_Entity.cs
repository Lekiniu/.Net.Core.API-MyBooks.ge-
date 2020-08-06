using MyBooks.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace MyBooks.Data.Entities
{
    public class Cart_Items_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        //[Required]
        //[JsonIgnore]
        public int BookId { get; set; }

        //[Required]
        //[JsonIgnore]
        public int CartId { get; set; }

        [ForeignKey("BookId")]
        public virtual BooksEntity Book { get; set; }

        [ForeignKey("CartId")]
        public virtual Shopping_Cart_Entity Shop_Carts { get; set; }
    }
}

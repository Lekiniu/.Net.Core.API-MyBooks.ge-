using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class Shopping_Carts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        public DateTime Date { get; set; }

        public Boolean IsOrder { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public virtual ICollection<Cart_items> Cart_Items { get; set; }
    }
}

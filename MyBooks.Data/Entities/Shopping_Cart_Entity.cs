using MyBooks.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Entities
{
    public class Shopping_Cart_Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        public DateTime Date { get; set; }

        public Boolean IsOrder { get; set; }

        [Required]
        [JsonIgnore]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }

        public virtual ICollection<Cart_Items_Entity> Cart_Items { get; set; }
    }
}

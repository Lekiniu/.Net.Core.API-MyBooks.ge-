using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class Addresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

       
        public int UsesId { get; set; }

        [ForeignKey("UsesId")]
        public virtual Users User { get; set; }

    }
}

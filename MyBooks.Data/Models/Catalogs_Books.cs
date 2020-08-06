using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class Catalogs_Books
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int CatalogId { get; set; }

  
        public int BookId { get; set; }


        [ForeignKey("CatalogId")]
        public virtual Catalogs Catalog { get; set; }

        [ForeignKey("BookId")]
        public virtual Books Book { get; set; }
    }
}

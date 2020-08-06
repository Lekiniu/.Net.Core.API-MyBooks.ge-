using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
  public  class Catalogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatalogId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Catalogs_Books> Catalogs_Books { get; set; }
    }
}

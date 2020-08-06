using MyBooks.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBooks.Data.Entities
{
   public  class CatalogsEntity
    {
        [ScaffoldColumn(false)]
        public int CatalogId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
       
        public IEnumerable<BooksEntity> Books { get; set; }
    }
}

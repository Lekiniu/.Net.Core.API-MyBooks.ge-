using MyBooks.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBooks.Data.Entities
{
   public class BooksEntity
    {
        [ScaffoldColumn(false)]
        public int BookId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [MaxLength(50)]
        [Required]
        public string Author { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }

        public DateTime Publication_Date { get; set; }

        public float Price { get; set; }

        [Required]
        public Boolean InStock { get; set; }

        //public int SupplierId { get; set; }

        //public IEnumerable<int> CatalogIds { get; set; }

        public UserEntity User { get; set; }

        public IEnumerable<CatalogsEntity> Catalogs { get; set; }

    }
}

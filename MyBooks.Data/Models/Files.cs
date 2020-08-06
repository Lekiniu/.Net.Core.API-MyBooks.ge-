using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyBooks.Data.Models
{
    public class Files
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

 
        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

 
        public int? UsesId { get; set; }

        [ForeignKey("UsesId")]
        public virtual Users User { get; set; }

   
        public int? BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Books Book { get; set; }
    }
}

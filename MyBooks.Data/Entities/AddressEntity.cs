using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBooks.Data.Entities
{
    public class AddressEntity
    {
        [ScaffoldColumn(false)]
        public int AddressId { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [JsonIgnore]
        public int? UsesId { get; set; }

        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}

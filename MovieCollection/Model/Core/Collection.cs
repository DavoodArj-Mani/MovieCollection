using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCollection.Model.Core
{
    public class Collection
    {
        [Key]
        public Guid CollectionId { get; set; }
        public string CollectionName { get; set; }
        public Guid CreatedBy { get; set; }

        [NotMapped]
        public ICollection<Movie> Movies { get; set; }

    }
}

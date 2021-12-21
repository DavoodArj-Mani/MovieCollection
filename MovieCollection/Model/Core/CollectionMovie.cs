using System;
using System.ComponentModel.DataAnnotations;
namespace MovieCollection.Model.Core
{
    public class CollectionMovie
    {
        [Key]
        public Guid MovieCollectionId { get; set; }
        public Guid MovieId { get; set; }
        public Guid CollectionId { get; set; }
    }
}

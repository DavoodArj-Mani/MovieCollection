using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCollection.Model.Core
{
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }
        public string MovieName { get; set; }
        public string Director { get; set; }
        public string Writers { get; set; }
        public string Stars { get; set; }
        public string IMDB_Score { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public ICollection<MovieType> MovieTypes { get; set; }
    }
}
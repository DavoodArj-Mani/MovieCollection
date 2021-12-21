using System;
using System.ComponentModel.DataAnnotations;
namespace MovieCollection.Model.Core
{
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }
        public string MovieName { get; set; }      
    }
}

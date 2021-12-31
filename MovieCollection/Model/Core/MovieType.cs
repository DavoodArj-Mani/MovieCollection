using System;
using System.ComponentModel.DataAnnotations;
namespace MovieCollection.Model.Core
{
    public class MovieType
    {
        [Key]
        public Guid MovieTypeId { get; set; }
        public string MovieTypeName { get; set; }
        public Guid MovieId { get; set; }
    }
}
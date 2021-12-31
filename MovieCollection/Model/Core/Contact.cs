using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCollection.Model.Core
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public Guid UserId { get; set; }

        [NotMapped]
        public string UserName { get; set; }
    }
}
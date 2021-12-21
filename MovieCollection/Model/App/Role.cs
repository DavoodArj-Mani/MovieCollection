using System;
using System.ComponentModel.DataAnnotations;
namespace MovieCollection.Model.App
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}

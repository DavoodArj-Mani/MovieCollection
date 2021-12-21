using System;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Model.App;
using MovieCollection.Model.Core;

namespace MovieCollection.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionMovie> CollectionMovies { get; set; }

    }
}

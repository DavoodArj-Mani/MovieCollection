using System;
using System.Collections.Generic;
using System.Linq;
using MovieCollection.Model;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.Core.MovieServices
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _db;

        public MovieService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Movie> QueryAllMovies()
        {
            var movies = _db.Movies;
            return movies;
        }
        public Movie QueryMovie(Guid movieId)
        {
            if (_db.Movies.Any(a => a.MovieId == movieId))
            {
                Movie movie = _db.Movies.Where(a => a.MovieId == movieId).Single();
                return movie;
            }
            return null;
        }
        public Movie QueryOneByMovieName(string movieName)
        {
            if (_db.Movies.Any(a => a.MovieName == movieName))
            {
                Movie movie = _db.Movies.Where(a => a.MovieName == movieName).Single();
                return movie;
            }
            return null;
           
        }

        public IEnumerable<Movie> QueryByMovieName(string movieName)
        {
            if (_db.Movies.Any(a => a.MovieName == movieName))
            {
                var movies = _db.Movies.Where(a => a.MovieName == movieName);
                return movies;
            }
            return null;
        }
        public Movie CreateMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
            return movie;
        }

        public Boolean DeleteMovie(Guid movieId)
        {
            if(_db.CollectionMovies.Any(a => a.MovieId == movieId))
            {
                return false;
            }
            else
            {
                var movie = _db.Movies.Where(a => a.MovieId == movieId).Single();
                if (movie != null)
                {
                    _db.Movies.Remove(movie);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
    }
}

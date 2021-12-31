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
            if (_db.Movies.Any(a => a.MovieName.Contains(movieName)))
            {
                var movies = _db.Movies.Where(a => a.MovieName.Contains(movieName));
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

        public Movie UpdateMovie(Movie movie)
        {
            var _movie = _db.Movies.Where(a => a.MovieId == movie.MovieId).Single();
            if (_movie != null)
            {
                if (movie.MovieName != null)
                    _movie.MovieName = movie.MovieName;
                if (movie.Director != null)
                    _movie.Director = movie.Director;
                if (movie.Writers != null)
                    _movie.Writers = movie.Writers;
                if (movie.Stars != null)
                    _movie.Stars = movie.Stars;
                if (movie.IMDB_Score != null)
                    _movie.IMDB_Score = movie.IMDB_Score;
                if (movie.Image != null)
                    _movie.Image = movie.Image;


                _db.Movies.Update(_movie);
                _db.SaveChanges();
                return _movie;
            }
            else
            {
                return null;
            }
        }

        public Boolean DeleteMovie(Guid movieId)
        {
            if(!_db.CollectionMovies.Any(a => a.MovieId == movieId))
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

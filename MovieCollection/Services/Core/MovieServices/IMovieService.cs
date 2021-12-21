using System;
using System.Collections.Generic;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.Core.MovieServices
{
    public interface IMovieService
    {
        IEnumerable<Movie> QueryAllMovies();

        Movie QueryMovie(Guid movieId);

        Movie QueryOneByMovieName(string movieName);

        IEnumerable<Movie> QueryByMovieName(string movieName);

        Movie CreateMovie(Movie movie);

        Boolean DeleteMovie(Guid movieId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Model;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.Core.CollectionServices
{
    public class CollectionService : ICollectionService
    {
        private readonly ApplicationDbContext _db;

        public CollectionService(ApplicationDbContext db)
        {
            _db = db;
        }


        public IEnumerable<Collection> QueryAllCollections()
        {

            var collections = _db.Collections
                .Join(
                    _db.Users,
                    collection => collection.CreatedBy,
                    user => user.UserId,
                    (collection, user) => new Collection
                    {
                        CollectionId = collection.CollectionId,
                        CollectionName = collection.CollectionName,
                        CreatedBy = collection.CreatedBy,
                        Description = collection.Description,
                        UserName = user.UserName,
                    }
                ).ToList();
            return getCollectionsMovies(collections);
        }
       

        public Collection QueryCollection(Guid collectionId)
        {
            if(_db.Collections.Any(a => a.CollectionId == collectionId))
            {
                Collection collection = _db.Collections.Where(a => a.CollectionId == collectionId).Join(
                    _db.Users,
                    collection => collection.CreatedBy,
                    user => user.UserId,
                    (collection, user) => new Collection
                    {
                        CollectionId = collection.CollectionId,
                        CollectionName = collection.CollectionName,
                        CreatedBy = collection.CreatedBy,
                        Description = collection.Description,
                        UserName = user.UserName,
                    }
                ).Single();
                return getCollectionMovies(collection);
            }
            return null;
        }
        public Collection QueryOneByCollectionName(string collectionName)
        {
            if (_db.Collections.Any(a => a.CollectionName == collectionName))
            {
                Collection collection = _db.Collections.Where(a => a.CollectionName == collectionName).Join(
                    _db.Users,
                    collection => collection.CreatedBy,
                    user => user.UserId,
                    (collection, user) => new Collection
                    {
                        CollectionId = collection.CollectionId,
                        CollectionName = collection.CollectionName,
                        CreatedBy = collection.CreatedBy,
                        Description = collection.Description,
                        UserName = user.UserName,
                    }
                ).Single();
                return getCollectionMovies(collection);
            }
            return null;
        }

        public IEnumerable<Collection> QueryByCollectionName(string collectionName)
        {
            if (_db.Collections.Any(a => a.CollectionName == collectionName))
            {
                var collections = _db.Collections.Where(a => a.CollectionName == collectionName).Join(
                    _db.Users,
                    collection => collection.CreatedBy,
                    user => user.UserId,
                    (collection, user) => new Collection
                    {
                        CollectionId = collection.CollectionId,
                        CollectionName = collection.CollectionName,
                        CreatedBy = collection.CreatedBy,
                        Description = collection.Description,
                        UserName = user.UserName,
                    }
                ).ToList();
                return getCollectionsMovies(collections);
            }
            return null;
        }

        public Collection CreateCollection(Collection collection)
        {
            _db.Collections.Add(collection);
            _db.SaveChanges();
            return collection;
        }
        public Boolean DeleteCollection(Guid collectionId, Guid userId)
        {
            if (_db.Collections.Any(a => a.CollectionId == collectionId && a.CreatedBy == userId))
            {
                var collection = _db.Collections.Where(a => a.CollectionId == collectionId && a.CreatedBy == userId).Single();
                if(_db.CollectionMovies.Any(a=>a.CollectionId == collection.CollectionId))
                {
                    return false;
                }
                else
                {
                    _db.Collections.Remove(collection);
                    _db.SaveChanges();
                    return true;
                }
                
            }
            else
            {
                return false;
            }

        }
        //-----------------------------------------------------------
        public IEnumerable<Collection> QueryMyCollections(Guid userId)
        {
            if(_db.Collections.Any(a => a.CreatedBy == userId))
            {
                var collections = _db.Collections.Where(a => a.CreatedBy == userId);
                return getCollectionsMovies(collections);
            }
            return null;
            
        }
        public IEnumerable<Collection> QueryMyCollectionByName(Guid userId , string collectionName)
        {
            if (_db.Collections.Any(a => a.CollectionName == collectionName && a.CollectionName == collectionName))
            {
                var collections = _db.Collections.Where(a => a.CollectionName == collectionName && a.CollectionName == collectionName);
                return getCollectionsMovies(collections);
            }
            return null;
        } 

        //-----------------------------------------------------------

        public CollectionMovie CreateCollectionMovie (CollectionMovie collectionMovie , Guid userId)
        {
            if(_db.Collections.Any(a=>a.CreatedBy == userId))
            {
                _db.CollectionMovies.Add(collectionMovie);
                _db.SaveChanges();
                return collectionMovie;
            }
            else
            {
                return null;
            }
            
        }

        public Boolean QueryCollectionMovie(Guid movieId, Guid collectionId)
        {
            if(_db.CollectionMovies.Any(a => a.CollectionId == collectionId && a.MovieId == movieId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<Movie> QueryCollectionMovies(Guid collectionId)
        {
            List<Movie> movies = new List<Movie>();
            var collectionMovies = _db.CollectionMovies.Where(a => a.CollectionId == collectionId);
            if(collectionMovies != null)
            {
                foreach(var collectionMovie in collectionMovies)
                {
                    Movie movie = _db.Movies.Where(a => a.MovieId == collectionMovie.MovieId).Single();
                    movies.Add(movie);
                }
            }
            return movies;
        }

        public Boolean DeleteCollectionMovie(Guid collectionMovieId)
        {
            if(_db.CollectionMovies.Any(a => a.MovieCollectionId == collectionMovieId))
            {
                var collectionMovie = _db.CollectionMovies.Where(a => a.MovieCollectionId == collectionMovieId).Single();
                _db.CollectionMovies.Remove(collectionMovie);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //-----------------------------------------------------------
        private IEnumerable<Collection> getCollectionsMovies(IEnumerable<Collection> collections)
        {
            
            foreach (var collection in collections)
            {
                List<Movie> movies = new List<Movie>();
                var collectionMovies = _db.CollectionMovies.Where(a => a.CollectionId == collection.CollectionId);
                foreach (var collectionMovie in collectionMovies)
                {
                    if(_db.Movies.Any(a => a.MovieId == collectionMovie.MovieId))
                    {
                        movies.Add(_db.Movies.Where(a => a.MovieId == collectionMovie.MovieId).Single());
                    }
                }
                collection.Movies = movies;
            }
            return collections;
        }
        private Collection getCollectionMovies(Collection collection)
        {
            var collectionMovies = _db.CollectionMovies.Where(a => a.CollectionId == collection.CollectionId);
            List<Movie> movies = new List<Movie>();
            foreach (var collectionMovie in collectionMovies)
            {
                if (_db.Movies.Any(a => a.MovieId == collectionMovie.MovieId))
                {
                    movies.Add(_db.Movies.Where(a => a.MovieId == collectionMovie.MovieId).Single());
                }
            }
            collection.Movies = movies;
            return collection;
        }
    }
}

using System;
using System.Collections.Generic;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.Core.CollectionServices
{
    public interface ICollectionService
    {
        IEnumerable<Collection> QueryAllCollections();

        Collection QueryCollection(Guid collectionId);

        Collection QueryOneByCollectionName(string collectionName);

        IEnumerable<Collection> QueryByCollectionName(string collectionName);

        Collection CreateCollection(Collection collection);

        IEnumerable<Collection> QueryMyCollections(Guid userId);

        IEnumerable<Collection> QueryMyCollectionByName(Guid userId, string collectionName);

        CollectionMovie CreateCollectionMovie(CollectionMovie collectionMovie, Guid userId);

        Boolean DeleteCollection(Guid collectionId, Guid userId);

        Boolean QueryCollectionMovie (Guid movieId, Guid collectionId);

        IEnumerable<Movie> QueryCollectionMovies(Guid collectionId);

        Boolean DeleteCollectionMovie(Guid collectionMovieId);
    }
}

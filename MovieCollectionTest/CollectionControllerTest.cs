using MovieCollection.Services.App.AuthenticationServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieCollection.Controllers.App;
using MovieCollection.Services.App.UserServices;
using MovieCollection.ViewEntity.App.AuthenticationServicesViewEntity;
using Xunit;
using MovieCollection.Model.Core;
using Microsoft.EntityFrameworkCore;
using System;
using MovieCollection.Model;
using System.Linq;
using MovieCollection.Services.App.RoleServices;
using MovieCollection.Services.App;
using System.Collections.Generic;
using MovieCollection.Services.Core.CollectionServices;
using MovieCollection.Controllers.Core;

namespace MovieCollectionTest
{
    public class CollectionControllerTest
    {
        private Mock<ICollectionService> _collection;
        private Mock<IAuthenticationService> _authenticationService;
        private ApplicationDbContext _db;

        public CollectionControllerTest()
        {
            _collection = new Mock<ICollectionService>();
            _authenticationService = new Mock<IAuthenticationService>();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _db = new ApplicationDbContext(builder.Options);
        }

        [Fact]
        public void QueryAll()
        {
            List<Collection> returnCollections = new List<Collection>();
            returnCollections.Add(new Collection() { CollectionName = "collection1" });

            _collection.Setup(x => x.QueryAllCollections()).Returns(returnCollections);
            var controller = new CollectionController(_collection.Object, _authenticationService.Object);

            var result = controller.QueryAll();
            Assert.NotNull(result);
        }

        //QueryById
        [Fact]
        public void QueryById()
        {
            Guid guid = new Guid();
            _collection.Setup(x => x.QueryCollection(guid)).Returns(new Collection() { CollectionName = "collection1" });
            var controller = new CollectionController(_collection.Object, _authenticationService.Object);

            var result = controller.QueryById(guid);
            Assert.NotNull(result);
        }
        /*
        //QueryOneByName
        [Fact]
        public void QueryOneByName()
        {

        }
        //QueryByName
        [Fact]
        public void QueryByName()
        {

        }
        //CreateCollection
        [Fact]
        public void CreateCollection()
        {

        }
        //DeleteCollection
        [Fact]
        public void DeleteCollection()
        {

        }
        //QueryMyCollections
        [Fact]
        public void QueryMyCollections()
        {

        }
        //QueryMyCollectionsByName
        [Fact]
        public void QueryMyCollectionsByName()
        {

        }
        //CreateCollectionMovie
        [Fact]
        public void CreateCollectionMovie()
        {

        }
        //QueryCollectionMovies
        [Fact]
        public void QueryCollectionMovies()
        {

        }
        //DeleteCollectionMovie
        [Fact]
        public void DeleteCollectionMovie()
        {

        }
        */
    }
}

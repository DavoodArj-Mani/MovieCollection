using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MovieCollection.Model.Core;
using MovieCollection.Services.App.AuthenticationServices;
using MovieCollection.Services.Core.CollectionServices;

namespace MovieCollection.Controllers.Core
{
    [Authorize]
    [Authorize(Roles = "User")]
    [Route("api/{controller}")]
    [ApiController]
    public class CollectionController : Controller
    {
        public ICollectionService _collectionService;
        public IAuthenticationService _authenticationService;

        public CollectionController(ICollectionService collectionService, IAuthenticationService authenticationService)
        {
            _collectionService = collectionService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("QueryAll")]
        public IActionResult QueryAll()
        {
            var result = _collectionService.QueryAllCollections();
            return Ok(result);
        }

        [HttpGet]
        [Route("QueryById/{collectionId}")]
        public IActionResult QueryById([FromRoute] Guid collectionId)
        {
            var result = _collectionService.QueryCollection(collectionId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("QueryOneByName/{collectionName}")]
        public IActionResult QueryOneByName([FromRoute] string collectionName)
        {
            var result = _collectionService.QueryOneByCollectionName(collectionName);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("QueryByName/{collectionName}")]
        public IActionResult QueryByName([FromRoute] string collectionName)
        {
            var result = _collectionService.QueryByCollectionName(collectionName);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCollection([FromBody] Collection _collection)
        {
            var collectionIsExist = _collectionService.QueryOneByCollectionName(_collection.CollectionName);
            if (collectionIsExist != null)
            {
                return BadRequest();
            }


            _collection.CreatedBy = _authenticationService.UserInfo(Request).UserId;
            var result = _collectionService.CreateCollection(_collection);
            return Ok(result);
        }
        [HttpDelete]
        [Route("DeleteCollection/{collectionId}")]
        public IActionResult DeleteCollection([FromRoute] Guid collectionId)
        {
            var result = _collectionService.DeleteCollection(collectionId);
            if (!result)
                return NotFound();
            else
                return Ok(result);
        }
        //--------------------------------------------------------------------------------------
        [HttpGet]
        [Route("QueryMyCollections")]
        public IActionResult QueryMyCollections()
        {
            var userId = _authenticationService.UserInfo(Request).UserId;
            var result = _collectionService.QueryMyCollections(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("QueryMyCollectionsByName/{Name}")]
        public IActionResult QueryMyCollectionsByName( [FromRoute] string collectionName)
        {
            var userId = _authenticationService.UserInfo(Request).UserId;
            var result = _collectionService.QueryMyCollectionByName(userId, collectionName);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
        

        //--------------------------------------------------------------------------------------
        [HttpPost]
        [Route("CreateCollectionMovie")]
        public IActionResult CreateCollectionMovie([FromBody] CollectionMovie _collectionMovie)
        {
            var collectionIsExist = _collectionService.QueryCollectionMovie(_collectionMovie.CollectionId, _collectionMovie.MovieId);
            if (collectionIsExist)
            {
                return BadRequest();
            }
            else
            {
                var result = _collectionService.CreateCollectionMovie(_collectionMovie);
                return Ok(result);
            }
        }
        [HttpGet]
        [Route("QueryCollectionMovies/{collectionId}")]
        public IActionResult QueryCollectionMovies([FromRoute] Guid collectionId)
        {
            var result = _collectionService.QueryCollection(collectionId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCollectionMovie/{collectionMovieId}")]
        public IActionResult DeleteCollectionMovie([FromRoute] Guid collectionMovieId)
        {
            var result = _collectionService.DeleteCollectionMovie(collectionMovieId);
            if (!result)
                return NotFound();
            else
                return Ok(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.Model.Core;
using MovieCollection.Services.Core.CollectionServices;
using MovieCollection.Services.Core.MovieServices;

namespace MovieCollection.Controllers.Core
{
    [Authorize]
    [Authorize(Roles = "User")]
    [Route("api/{controller}")]
    [ApiController]
    public class MovieController : Controller
    {

        public IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("QueryAll")]
        public IActionResult QueryAll()
        {
            var result = _movieService.QueryAllMovies();
            return Ok(result);
        }

        [HttpGet]
        [Route("QueryById/{movieId}")]
        public IActionResult QueryById([FromRoute] Guid movieId)
        {
            var result = _movieService.QueryMovie(movieId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("QueryOneByName/{movieName}")]
        public IActionResult QueryOneByName([FromRoute] string movieName)
        {
            var result = _movieService.QueryOneByMovieName(movieName);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("QueryByName/{movieName}")]
        public IActionResult QueryByName([FromRoute] string movieName)
        {
            var result = _movieService.QueryByMovieName(movieName);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateMovie([FromBody] Movie _movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var movieIsExist = _movieService.QueryOneByMovieName(_movie.MovieName);
            if (movieIsExist != null)
            {
                return BadRequest();
            }
            var result = _movieService.CreateMovie(_movie);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update/{movieId}")]
        public IActionResult UpdateMovie(Guid movieId, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var movieIsExist = _movieService.QueryMovie(movieId);
            if (movieIsExist == null)
            {
                return NotFound();
            }
            else
            {
                var result = _movieService.UpdateMovie(movie);
                return Ok(result);
            }
            
        }


        [HttpDelete]
        [Route("Delete/{movieId}")]
        public IActionResult DeleteMovie([FromRoute] Guid movieId)
        {
            var result = _movieService.DeleteMovie(movieId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}

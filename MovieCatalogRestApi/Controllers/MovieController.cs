using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Catalog_REST_API.Dto;
using Movie_Catalog_REST_API.Models;
using Movie_Catalog_REST_API.Services;
using System.Net;

namespace Movie_Catalog_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }
        /// <summary>
        /// Retrieves the last added movie
        /// </summary>
        /// <returns>Last added movie in collection</returns>
        /// <response code="404">Item hasn't been found.</response>
        [HttpGet]
        public IActionResult GetLastAdded()
        {
            try
            {
                var result = _movieService.GetLastAddedMovie();

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"FAILED: something went wrong inside GetLastAdded action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
           
        }
        /// <summary>
        /// Retrieves movies from a given genre.
        /// </summary>
        /// <returns>Movies belonging to given genre</returns>
        /// <param name="genre">Single genre name</param>
        /// <response code="404">Item hasn't been found.</response>
        [HttpGet("genre/{genre}")]
        public IActionResult GetByGenre(string genre)
        {
            try
            {
                var result = _movieService.GetMoviesByGenre(genre);

                if (result is null || !result.Any())
                {
                    _logger.LogError($"Movies containing genre: {genre}, haven't been found.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"FAILED: something went wrong inside GetByGenre action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Retrieves movies from a given year.
        /// </summary>
        /// <param name="year">Only movie world premiere year</param>
        /// <returns>Movies which had their world premiere in given year.</returns>
        /// <response code="404">Item hasn't been found.</response>
        [HttpGet("year/{year}")]
        public IActionResult GetByYear(int year)
        {
            try 
            { 
                var result = _movieService.GetMoviesByYear(year);

                if (result == null || !result.Any())
                {
                    _logger.LogError($"Movies from year: {year}, haven't been found.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"FAILED: something went wrong inside GetByYear action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }       
        }
        /// <summary>
        /// Allows to add a movie to the catalog.
        /// </summary>
        /// <remarks>
        /// "worldPremiereDate" is date only format (yyyy-mm-dd), "genre" and "director" can contain more than one field
        /// </remarks>
        /// <response code="204">Movie was successfully added</response>
        /// <response code="400">Data validation error</response>

        [HttpPost]
        public IActionResult Add(MovieDTO movie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client.");
                    return BadRequest("Invalid model object");
                }
               
                _movieService.AddMovie(movie);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"FAILED: something went wrong inside Add action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            } 
        }

    }
}

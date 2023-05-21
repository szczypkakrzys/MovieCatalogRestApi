using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Movie_Catalog_REST_API.Controllers;
using Movie_Catalog_REST_API.Dto;
using Movie_Catalog_REST_API.Models;
using Movie_Catalog_REST_API.Services;

namespace MovieCatalogTests
{
    [TestClass]
    public class MovieControllerUnitTests
    {
        private readonly Mock<IMovieService> _mockMovieService = new();
        private readonly Mock<ILogger<MovieController>> _mockLogger = new();
        
        [TestMethod]
        public void PostValidMovieReturnsNoContentResult()
        {
            MovieDTO testMovie = new MovieDTO()
            {
                Title = "Test",
                WorldPremiereDate = new DateTime(2001 - 12 - 12),
                Genre = { "Test Genre" },
                Director = { "Test Director" },
                Duration = 444

            };

            var controller = new MovieController(_mockMovieService.Object, _mockLogger.Object);
            var response = controller.Add(testMovie);
            Assert.IsInstanceOfType(response, typeof(NoContentResult));
        }
        [TestMethod]
        public void GetByGenreReturnsNotFound()
        {
            var controller = new MovieController(_mockMovieService.Object, _mockLogger.Object);
            var response = controller.GetByGenre("132");
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}
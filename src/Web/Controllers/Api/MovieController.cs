using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.Api
{
    public class MovieController : BaseApiController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Query(string title, string genre, int? yearOfRelease = null)
        {
            if (string.IsNullOrWhiteSpace(title) &&
                string.IsNullOrWhiteSpace(genre) &&
                yearOfRelease == null)
            {
                return BadRequest("Invalid criteria.");
            }

            var movieRatings = await _movieService.QueryMoviesAsync(title, genre, yearOfRelease);
            if (movieRatings.Count <= 0)
            {
                return NotFound("No movies were found.");
            }

            return Ok(movieRatings);
        }

        [HttpGet]
        public async Task<IActionResult> TopMovies()
        {
            var topFiveRatings = await _movieService.GetTopMoviesBasedOnTotalUserAverageRatings(5);
            if (topFiveRatings.Count <= 0)
            {
                return NotFound("No movies were found.");
            }

            return Ok(topFiveRatings);
        }

        [HttpGet]
        public async Task<IActionResult> TopUserMovies(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user id.");
            }

            var topFiveUserMovies = await _movieService.GetTopMoviesBasedOnHighestUserRating(5, userId);
            if (topFiveUserMovies.Count <= 0)
            {
                return NotFound("No movies were found.");
            }

            return Ok(topFiveUserMovies);
        }
    }
}

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
            var topFiveRatings = await _movieService.GetTopMoviesBasedOnTotalUserAverageRatingsAsync(5);
            if (topFiveRatings.Count <= 0)
            {
                return NotFound("No movies were found.");
            }

            return Ok(topFiveRatings);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> TopMovies(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user id.");
            }

            var topFiveUserMovies = await _movieService.GetTopMoviesBasedOnHighestUserRatingAsync(5, userId);
            if (topFiveUserMovies.Count <= 0)
            {
                return NotFound("No movies were found.");
            }

            return Ok(topFiveUserMovies);
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(int movieId, int userId, int rating)
        {
            if (movieId <= 0)
            {
                return BadRequest("Invalid movie id.");
            }

            if (userId <= 0)
            {
                return BadRequest("Invalid user id.");
            }

            if (rating < 1 || rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5.");
            }

            bool ratingAdded = await _movieService.AddRatingAsync(movieId, userId, rating);
            if (!ratingAdded)
            {
                return NotFound("Could not add the rating.");
            }

            return Ok();
        }
    }
}

using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!dbContext.Genres.Any())
                {
                    dbContext.Genres.AddRange(GetDefaultGenres());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Movies.Any())
                {
                    dbContext.Movies.AddRange(GetDefaultMovies());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.MovieGenres.Any())
                {
                    dbContext.MovieGenres.AddRange(GetMovieGenres());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.AddRange(GetDefaultUsers());
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Ratings.Any())
                {
                    dbContext.Ratings.AddRange(GetDefaultRatings());
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(ex.Message);
            }
        }

        private static IEnumerable<Movie> GetDefaultMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Title = "The Shawshank Redemption", YearOfRelease = 1994, RunningTimeInMinutes = 180 },
                new Movie { Id = 2, Title = "The Godfather", YearOfRelease = 1972, RunningTimeInMinutes = 120 },
                new Movie { Id = 3, Title = "The Dark Knight", YearOfRelease = 2008, RunningTimeInMinutes = 120 },
                new Movie { Id = 4, Title = "Pulp Fiction", YearOfRelease = 1994, RunningTimeInMinutes = 160 },
                new Movie { Id = 5, Title = "Fight Club", YearOfRelease = 1999, RunningTimeInMinutes = 90 },
                new Movie { Id = 6, Title = "Inception", YearOfRelease = 2010, RunningTimeInMinutes = 120 },
                new Movie { Id = 7, Title = "The Matrix", YearOfRelease = 1999, RunningTimeInMinutes = 90 },
                new Movie { Id = 8, Title = "Interstellar", YearOfRelease = 2014, RunningTimeInMinutes = 120 }
            };
        }

        private static IEnumerable<User> GetDefaultUsers()
        {
            return new List<User>
            {
                new User { Id = 1, FirstName = "John", LastName = "Smith" },
                new User { Id = 2, FirstName = "Paul", LastName = "Wise" },
                new User { Id = 3, FirstName = "Andrew", LastName = "Adams" },
                new User { Id = 4, FirstName = "Peter", LastName = "Mendez" },
                new User { Id = 5, FirstName = "Daniel", LastName = "Richards" }
            };
        }

        private static IEnumerable<Genre> GetDefaultGenres()
        {
            return new List<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Sci-Fi" },
                new Genre { Id = 4, Name = "Drama" },
                new Genre { Id = 5, Name = "Thriller" },
                new Genre { Id = 6, Name = "Horror" },
                new Genre { Id = 7, Name = "Romance" }
            };
        }

        private static IEnumerable<MovieGenre> GetMovieGenres()
        {
            return new List<MovieGenre>
            {
                new MovieGenre { MovieId = 1, GenreId = 4 },
                new MovieGenre { MovieId = 2, GenreId = 1 },
                new MovieGenre { MovieId = 3, GenreId = 1 },
                new MovieGenre { MovieId = 4, GenreId = 1 },
                new MovieGenre { MovieId = 5, GenreId = 1 },
                new MovieGenre { MovieId = 6, GenreId = 1 },
                new MovieGenre { MovieId = 7, GenreId = 1 },
                new MovieGenre { MovieId = 7, GenreId = 3 },
                new MovieGenre { MovieId = 8, GenreId = 1 },
                new MovieGenre { MovieId = 8, GenreId = 4 },
                new MovieGenre { MovieId = 8, GenreId = 3 }
            };
        }

        private static IEnumerable<MovieUserRating> GetDefaultRatings()
        {
            return new List<MovieUserRating>
            {
                new MovieUserRating(1, 1, 3),
                new MovieUserRating(2, 1, 5),
                new MovieUserRating(3, 1, 2),
                new MovieUserRating(3, 2, 1),
                new MovieUserRating(4, 2, 4),
                new MovieUserRating(5, 2, 2),
                new MovieUserRating(7, 2, 5),
                new MovieUserRating(1, 3, 1),
                new MovieUserRating(2, 3, 4),
                new MovieUserRating(3, 3, 2),
                new MovieUserRating(4, 3, 5),
                new MovieUserRating(5, 3, 1),
                new MovieUserRating(6, 3, 4),
                new MovieUserRating(7, 3, 2),
                new MovieUserRating(8, 3, 5),
                new MovieUserRating(4, 4, 5),
                new MovieUserRating(5, 4, 5),
                new MovieUserRating(6, 4, 5),
                new MovieUserRating(7, 4, 5),
                new MovieUserRating(8, 4, 5),
                new MovieUserRating(1, 5, 2),
                new MovieUserRating(2, 5, 3),
                new MovieUserRating(3, 5, 1),
                new MovieUserRating(4, 5, 4),
                new MovieUserRating(5, 5, 5),
                new MovieUserRating(6, 5, 3),
                new MovieUserRating(7, 5, 3),
                new MovieUserRating(8, 5, 2)
            };
        }
    }
}

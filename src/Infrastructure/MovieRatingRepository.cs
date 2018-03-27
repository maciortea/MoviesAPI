using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MovieRatingRepository : EfRepository<MovieUserRating>, IMovieRatingRepository
    {
        public MovieRatingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<MovieDto>> QueryMoviesAsync(string title, string genre, int? yearOfRelease = null)
        {
            var query = _dbContext.Ratings
                .Include(r => r.Movie)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(r => r.Movie.Title.ToLower().Contains(title.ToLower()));
            }

            if (yearOfRelease.HasValue)
            {
                query = query.Where(r => r.Movie.YearOfRelease == yearOfRelease.Value);
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(r => r.Movie.MovieGenres.Any(x => x.Genre.Name.ToLower() == genre.ToLower()));
            }

            return await query
                .GroupBy(r => new
                {
                    r.MovieId,
                    r.Movie.Title,
                    r.Movie.YearOfRelease,
                    r.Movie.RunningTimeInMinutes
                })
                .Select(r => new MovieDto
                {
                    Id = r.Key.MovieId,
                    Title = r.Key.Title,
                    YearOfRelease = r.Key.YearOfRelease,
                    RunningTime = r.Key.RunningTimeInMinutes,
                    AverageRatingInternal = r.Average(x => x.Rating)
                })
                .OrderBy(r => r.YearOfRelease)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatings(int takeCount)
        {
            return await _dbContext.Ratings
                .Include(r => r.Movie)
                .GroupBy(r => new
                {
                    r.MovieId,
                    r.Movie.Title,
                    r.Movie.YearOfRelease,
                    r.Movie.RunningTimeInMinutes
                })
                .Select(r => new MovieDto
                {
                    Id = r.Key.MovieId,
                    Title = r.Key.Title,
                    YearOfRelease = r.Key.YearOfRelease,
                    RunningTime = r.Key.RunningTimeInMinutes,
                    AverageRatingInternal = r.Average(x => x.Rating)
                })
                .OrderByDescending(r => r.AverageRatingInternal)
                .ThenBy(r => r.Title)
                .Take(takeCount)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRating(int takeCount, int userId)
        {
            return await _dbContext.Ratings
                .Include(r => r.Movie)
                .Where(r => r.UserId == userId)
                .GroupBy(r => new
                {
                    r.MovieId,
                    r.Movie.Title,
                    r.Movie.YearOfRelease,
                    r.Movie.RunningTimeInMinutes
                })
                .Select(r => new MovieDto
                {
                    Id = r.Key.MovieId,
                    Title = r.Key.Title,
                    YearOfRelease = r.Key.YearOfRelease,
                    RunningTime = r.Key.RunningTimeInMinutes,
                    AverageRatingInternal = r.Average(x => x.Rating)
                })
                .OrderByDescending(r => r.AverageRatingInternal)
                .ThenBy(r => r.Title)
                .Take(takeCount)
                .ToListAsync();
        }
    }
}

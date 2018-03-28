using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MovieRatingRepository : BaseRepository, IMovieRatingRepository
    {
        public MovieRatingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<MovieDto>> QueryMoviesAsync(string title, string genre, int? yearOfRelease = null)
        {
            var query = _dbContext.Ratings.AsQueryable();

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

            return await ProjectMoviesQuery(query)
                .OrderBy(r => r.Title)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatingsAsync(int takeCount)
        {
            var query = _dbContext.Ratings.AsQueryable();
            return await ProjectMoviesQuery(query)
                .OrderByDescending(r => r.AverageRatingInternal)
                .ThenBy(r => r.Title)
                .Take(takeCount)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRatingAsync(int takeCount, int userId)
        {
            var query = _dbContext.Ratings.Where(r => r.UserId == userId).AsQueryable();
            return await ProjectMoviesQuery(query)
                .OrderByDescending(r => r.AverageRatingInternal)
                .ThenBy(r => r.Title)
                .Take(takeCount)
                .ToListAsync();
        }

        public async Task<MovieUserRating> GetByIdAsync(int movieId, int userId)
        {
            return await _dbContext.Ratings.FindAsync(movieId, userId);
        }

        public async Task CreateAsync(MovieUserRating movieUserRating)
        {
            _dbContext.Ratings.Add(movieUserRating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MovieUserRating movieUserRating)
        {
            _dbContext.Entry(movieUserRating).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<MovieDto> ProjectMoviesQuery(IQueryable<MovieUserRating> query)
        {
            return query
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
                });
        }
    }
}

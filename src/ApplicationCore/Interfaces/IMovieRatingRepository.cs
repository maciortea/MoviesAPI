using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMovieRatingRepository
    {
        Task<IReadOnlyList<MovieDto>> QueryMoviesAsync(string title, string genre, int? yearOfRelease = null);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatingsAsync(int takeCount);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRatingAsync(int takeCount, int userId);
        Task<MovieUserRating> GetByIdAsync(int movieId, int userId);
        Task CreateAsync(MovieUserRating movieUserRating);
        Task UpdateAsync(MovieUserRating movieUserRating);
    }
}

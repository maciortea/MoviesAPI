using ApplicationCore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMovieService
    {
        Task<IReadOnlyList<MovieDto>> QueryMoviesAsync(string title, string genre, int? yearOfRelease = null);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatingsAsync(int takeCount);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRatingAsync(int takeCount, int userId);
        Task<bool> AddRatingAsync(int movieId, int userId, int rating);
    }
}

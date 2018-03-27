using ApplicationCore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMovieService
    {
        Task<IReadOnlyList<MovieDto>> QueryMoviesAsync(string title, string genre, int? yearOfRelease = null);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatings(int takeCount);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRating(int takeCount, int userId);
    }
}

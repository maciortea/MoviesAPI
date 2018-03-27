using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMovieRatingRepository : IAsyncRepository<MovieUserRating>
    {
        Task<IReadOnlyList<MovieDto>> QueryMoviesAsync(string title, string genre, int? yearOfRelease = null);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatings(int takeCount);
        Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRating(int takeCount, int userId);
    }
}

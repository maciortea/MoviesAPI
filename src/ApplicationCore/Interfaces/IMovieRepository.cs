using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMovieRepository
    {
        Task<bool> ExistsAsync(int movieId);
    }
}

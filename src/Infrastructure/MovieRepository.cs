using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(int movieId)
        {
            var movie = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == movieId);
            if (movie != null)
            {
                return true;
            }
            return false;
        }
    }
}

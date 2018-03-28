using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(m => m.Id == userId);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}

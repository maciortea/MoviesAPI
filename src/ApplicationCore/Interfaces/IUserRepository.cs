using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(int userId);
    }
}

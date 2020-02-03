using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IUserEntityRepository : IBaseEntityRepository<User, int>
    {
        bool UserExists(int id);
    }
}
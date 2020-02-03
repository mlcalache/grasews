using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class UserEntityRepository : BaseEntityRepository<User, int>, IUserEntityRepository
    {
        public bool UserExists(int id)
        {
            return _context.Users.Count(x => x.Id == id) > 0;
        }
    }
}
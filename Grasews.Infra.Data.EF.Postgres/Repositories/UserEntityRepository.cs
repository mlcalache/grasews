using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class UserEntityRepository : BaseEntityRepository<User, int>, IUserEntityRepository
    {
        #region IUserEntityRepository methods

        public bool UserExists(int id)
        {
            return _context.Users.Count(x => x.Id == id) > 0;
        }

        #endregion IUserEntityRepository methods
    }
}
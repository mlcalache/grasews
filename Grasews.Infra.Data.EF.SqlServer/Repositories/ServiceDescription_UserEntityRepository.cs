using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class ServiceDescription_UserEntityRepository : BaseEntityRepository<ServiceDescription_User, int>, IServiceDescription_UserEntityRepository
    {
        #region IServiceDescription_UserEntityRepository public methods

        public IQueryable<ServiceDescription_User> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true)
        {
            return @readonly
                ? _context.ServiceDescription_Users.AsNoTracking()
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .Where(x => x.IdServiceDescription == idServiceDescription)
                : _context.ServiceDescription_Users
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .Where(x => x.IdServiceDescription == idServiceDescription);
        }

        public IQueryable<ServiceDescription_User> GetAllBySharedUser(int idUser, bool @readonly = true)
        {
            return @readonly
                ? _context.ServiceDescription_Users.AsNoTracking()
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .Where(x => x.IdSharedUser == idUser)
                : _context.ServiceDescription_Users
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .Where(x => x.IdSharedUser == idUser);
        }

        public ServiceDescription_User GetAllByServiceDescriptionAndSharedUser(int idServiceDescription, int idUser, bool @readonly = true)
        {
            return @readonly
                ? _context.ServiceDescription_Users.AsNoTracking()
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .FirstOrDefault(x => x.IdServiceDescription == idServiceDescription && x.IdSharedUser == idUser)
                : _context.ServiceDescription_Users
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .FirstOrDefault(x => x.IdServiceDescription == idServiceDescription && x.IdSharedUser == idUser);
        }

        #endregion IServiceDescription_UserEntityRepository public methods

        #region Overrides

        public override ServiceDescription_User Get(int id, bool @readonly = true)
        {
            return @readonly
                ? _context.ServiceDescription_Users.AsNoTracking()
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .FirstOrDefault(x => x.Id == id)
                : _context.ServiceDescription_Users
                    .Include(nameof(ServiceDescription_User.ServiceDescription))
                    .Include(nameof(ServiceDescription_User.SharedUser))
                    .FirstOrDefault(x => x.Id == id);
        }

        #endregion Overrides
    }
}
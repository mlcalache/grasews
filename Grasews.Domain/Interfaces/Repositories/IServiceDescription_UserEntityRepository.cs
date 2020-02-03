using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IServiceDescription_UserEntityRepository : IBaseEntityRepository<ServiceDescription_User, int>
    {
        IQueryable<ServiceDescription_User> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true);

        ServiceDescription_User GetAllByServiceDescriptionAndSharedUser(int idServiceDescription, int idUser, bool @readonly = true);

        IQueryable<ServiceDescription_User> GetAllBySharedUser(int idUser, bool @readonly = true);
    }
}
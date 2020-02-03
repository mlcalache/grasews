using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface ITaskEntityRepository : IBaseEntityRepository<Task, int>
    {
        IQueryable<Task> GetAllByServiceDescription(int idServiceDescription, bool @readonly);

        IQueryable<Task> GetAllByUser(int idUser);
    }
}
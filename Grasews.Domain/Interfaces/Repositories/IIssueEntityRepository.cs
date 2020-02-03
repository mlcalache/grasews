using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IIssueEntityRepository : IBaseEntityRepository<Issue, int>
    {
        IQueryable<Issue> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true);

        IQueryable<Issue> GetAllCompleteByServiceDescription(int idServiceDescription, bool @readonly = true);

        IQueryable<Issue> GetAllByUser(int idUser, bool @readonly = true);

        IQueryable<Issue> GetAllCompleteByUser(int idUser, bool @readonly = true);
    }
}
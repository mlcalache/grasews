using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IIssueAnswerEntityRepository : IBaseEntityRepository<IssueAnswer, int>
    {
        IQueryable<IssueAnswer> GetAllByUser(int userId);
    }
}
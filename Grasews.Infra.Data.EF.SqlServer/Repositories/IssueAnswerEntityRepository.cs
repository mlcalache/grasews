using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class IssueAnswerEntityRepository : BaseEntityRepository<IssueAnswer, int>, IIssueAnswerEntityRepository
    {
        public IQueryable<IssueAnswer> GetAllByUser(int userId)
        {
            return GetAll().Where(x => x.IdOwnerUser == userId);
        }

        public override IQueryable<IssueAnswer> GetAll(bool @readonly = true)
        {
            var query = base.GetAll(@readonly).Include(nameof(TaskComment.OwnerUser));

            return query;
        }
    }
}
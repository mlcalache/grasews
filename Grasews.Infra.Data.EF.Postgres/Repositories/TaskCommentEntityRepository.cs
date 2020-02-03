using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class TaskCommentEntityRepository : BaseEntityRepository<TaskComment, int>, ITaskCommentEntityRepository
    {
        #region ITaskCommentEntityRepository methods

        public IQueryable<TaskComment> GetAllByUser(int userId)
        {
            return GetAll().Where(x => x.IdOwnerUser == userId);
        }

        public override IQueryable<TaskComment> GetAll(bool @readonly = true)
        {
            var query = base.GetAll(@readonly).Include(nameof(TaskComment.OwnerUser));

            return query;
        }

        #endregion ITaskCommentEntityRepository methods
    }
}
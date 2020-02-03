using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class TaskEntityRepository : BaseEntityRepository<Task, int>, ITaskEntityRepository
    {
        #region ITaskEntityRepository methods

        public IQueryable<Task> GetAllByServiceDescription(int idServiceDescription, bool @readonly)
        {
            return base.GetAll(@readonly)
                .Include(nameof(Task.TaskComments))
                .Where(x => x.IdServiceDescription == idServiceDescription);
        }

        public IQueryable<Task> GetAllByUser(int userId)
        {
            return GetAll().Where(x => x.IdOwnerUser == userId);
        }

        #endregion ITaskEntityRepository methods

        #region Overrides

        public override void Remove(int id)
        {
            var task = GetComplete(id, @readonly: false);

            _context.TaskComments.RemoveRange(task.TaskComments);

            _context.Tasks.Remove(task);
        }

        #endregion Overrides

        #region Private methods

        private Task GetComplete(int id, bool @readonly)
        {
            var query = base.GetAll(@readonly)
                .Include(nameof(Task.TaskComments));

            var task = query.FirstOrDefault(x => x.Id == id);

            return task;
        }

        #endregion Private methods
    }
}
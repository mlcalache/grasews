using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface ITaskCommentEntityRepository : IBaseEntityRepository<TaskComment, int>
    {
        IQueryable<TaskComment> GetAllByUser(int idUser);
    }
}
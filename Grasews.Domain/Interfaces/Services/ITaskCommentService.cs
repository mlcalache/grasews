using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface ITaskCommentService : IBaseService
    {
        int Create(TaskComment taskComment);

        TaskComment Get(int idTask, int id);

        List<TaskComment> GetAll(int idTask);

        List<TaskComment> GetAllByTask(int idTask);

        List<TaskComment> GetAllByUser(int iduser);

        int Remove(TaskComment taskComment);

        int Update(TaskComment taskComment);
    }
}
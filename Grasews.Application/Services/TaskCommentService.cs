using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class TaskCommentService : BaseService, ITaskCommentService
    {
        #region Private vars

        private readonly ITaskCommentEntityRepository _taskCommentRepository;

        #endregion Private vars

        #region Ctors

        public TaskCommentService(ITaskCommentEntityRepository TaskCommentRepository)
        {
            _taskCommentRepository = TaskCommentRepository;
        }

        #endregion Ctors

        #region ITaskCommentService public methods

        public int Create(TaskComment Task)
        {
            _taskCommentRepository.Create(Task);

            return _taskCommentRepository.SaveChanges();
        }

        public TaskComment Get(int idTask, int id)
        {
            return _taskCommentRepository.GetAll().FirstOrDefault(x => x.IdTask == idTask && x.Id == id);
        }

        public List<TaskComment> GetAll(int idTask)
        {
            return _taskCommentRepository.GetAll().Where(x => x.IdTask == idTask).ToList();
        }

        public List<TaskComment> GetAllByTask(int idTask)
        {
            return _taskCommentRepository.GetAll().Where(x => x.IdTask == idTask).ToList();
        }

        public List<TaskComment> GetAllByUser(int idUser)
        {
            return _taskCommentRepository.GetAllByUser(idUser).ToList();
        }

        public int Remove(TaskComment task)
        {
            _taskCommentRepository.Remove(task.Id);

            return _taskCommentRepository.SaveChanges();
        }

        public int Update(TaskComment task)
        {
            _taskCommentRepository.Create(task);

            return _taskCommentRepository.SaveChanges();
        }

        #endregion ITaskCommentService public methods
    }
}
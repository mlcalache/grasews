using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class TaskService : BaseService, ITaskService
    {
        #region Private vars

        private readonly ITaskEntityRepository _TaskRepository;

        #endregion Private vars

        #region Ctors

        public TaskService(ITaskEntityRepository taskRepository)
        {
            _TaskRepository = taskRepository;
        }

        #endregion Ctors

        #region ITaskService public methods

        public int Create(Task task)
        {
            _TaskRepository.Create(task);

            return _TaskRepository.SaveChanges();
        }

        public Task Get(int id)
        {
            return _TaskRepository.Get(id);
        }

        public List<Task> GetAll()
        {
            return _TaskRepository.GetAll().ToList();
        }

        public List<Task> GetAllByServiceDescription(int idServiceDescription)
        {
            return _TaskRepository.GetAll().Where(x => x.IdServiceDescription == idServiceDescription).ToList();
        }

        public List<Task> GetAllByUser(int idUser)
        {
            return _TaskRepository.GetAllByUser(idUser).ToList();
        }

        public int Remove(Task task)
        {
            _TaskRepository.Remove(task.Id);

            return _TaskRepository.SaveChanges();
        }

        public int Update(Task task)
        {
            var existingTask = _TaskRepository.GetAll(@readonly: false).FirstOrDefault(x => x.Id == task.Id);

            existingTask.Description = task.Description;
            existingTask.Done = task.Done;

            _TaskRepository.Update(existingTask);

            return _TaskRepository.SaveChanges();
        }

        #endregion ITaskService public methods
    }
}
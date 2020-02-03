using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface ITaskService : IBaseService
    {
        int Create(Task task);

        Task Get(int id);

        List<Task> GetAll();

        List<Task> GetAllByServiceDescription(int idServiceDescription);

        List<Task> GetAllByUser(int idUser);

        int Remove(Task task);

        int Update(Task task);
    }
}
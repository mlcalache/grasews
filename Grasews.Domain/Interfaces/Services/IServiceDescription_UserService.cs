using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IServiceDescription_UserService : IBaseService
    {
        int Create(ServiceDescription_User serviceDescription_User);

        ServiceDescription_User Get(int id);

        List<ServiceDescription_User> GetAll();

        List<ServiceDescription_User> GetAllByServiceDescription(int idServiceDescription);

        ServiceDescription_User GetAllByServiceDescriptionAndSharedUser(int idServiceDescription, int idSharedUser);

        List<ServiceDescription_User> GetAllBySharedUser(int userId);

        int Remove(ServiceDescription_User serviceDescription_User);

        int Update(ServiceDescription_User serviceDescription_User);
    }
}
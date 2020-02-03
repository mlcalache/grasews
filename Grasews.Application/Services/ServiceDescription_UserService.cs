using Grasews.Domain.Entities;
using Grasews.Domain.Events;
using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class ServiceDescription_UserService : BaseService, IServiceDescription_UserService
    {
        #region Private vars

        private readonly IEventDispatcher _eventDispatcher;
        private readonly IServiceDescription_UserEntityRepository _serviceDescription_UserRepository;
        private readonly IUserEntityRepository _userRepository;

        #endregion Private vars

        #region Ctors

        public ServiceDescription_UserService(IEventDispatcher eventDispatcher,
            IServiceDescription_UserEntityRepository serviceDescription_UserRepository,
            IUserEntityRepository userRepository)
        {
            _eventDispatcher = eventDispatcher;
            _serviceDescription_UserRepository = serviceDescription_UserRepository;
            _userRepository = userRepository;
        }

        #endregion Ctors

        #region IServiceDescription_UserService public methods

        public int Create(ServiceDescription_User serviceDescription_User)
        {
            _serviceDescription_UserRepository.Create(serviceDescription_User);

            var count = _serviceDescription_UserRepository.SaveChanges();

            var user = _userRepository.Get(serviceDescription_User.IdSharedUser);

            var invitationSecurity = Guid.NewGuid();

            _eventDispatcher.Dispatch(new SendInvitationEmailEvent(user.Email, invitationSecurity));

            return count;
        }

        public ServiceDescription_User Get(int id)
        {
            return _serviceDescription_UserRepository.Get(id);
        }

        public List<ServiceDescription_User> GetAll()
        {
            var serviceDescription_Users = _serviceDescription_UserRepository.GetAll();

            return serviceDescription_Users.ToList();
        }

        public List<ServiceDescription_User> GetAllByServiceDescription(int idServiceDescription)
        {
            return _serviceDescription_UserRepository.GetAllByServiceDescription(idServiceDescription).ToList();
        }

        public ServiceDescription_User GetAllByServiceDescriptionAndSharedUser(int idServiceDescription, int idSharedUser)
        {
            return _serviceDescription_UserRepository.GetAllByServiceDescriptionAndSharedUser(idServiceDescription, idSharedUser);
        }

        public List<ServiceDescription_User> GetAllBySharedUser(int idUser)
        {
            return _serviceDescription_UserRepository.GetAllBySharedUser(idUser).ToList();
        }

        public int Remove(ServiceDescription_User serviceDescription_User)
        {
            _serviceDescription_UserRepository.Remove(serviceDescription_User.Id);

            return _serviceDescription_UserRepository.SaveChanges();
        }

        public int Update(ServiceDescription_User serviceDescription_User)
        {
            _serviceDescription_UserRepository.Update(serviceDescription_User);

            return _serviceDescription_UserRepository.SaveChanges();
        }

        #endregion IServiceDescription_UserService public methods
    }
}
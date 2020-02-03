using Grasews.Domain.Entities;
using Grasews.Domain.Events;
using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        #region Private vars

        private readonly IEventDispatcher _eventDispatcher;
        private readonly IResetPasswordEntityRepository _resetPasswordRepository;
        private readonly IServiceDescription_UserEntityRepository _serviceDescription_UserRepository;
        private readonly IUserEntityRepository _userRepository;

        #endregion Private vars

        #region Ctors

        public UserService(IUserEntityRepository userRepository,
            IServiceDescription_UserEntityRepository serviceDescription_UserRepository,
            IResetPasswordEntityRepository resetPasswordRepository,
            IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _serviceDescription_UserRepository = serviceDescription_UserRepository;
            _resetPasswordRepository = resetPasswordRepository;
            _eventDispatcher = eventDispatcher;
        }

        #endregion Ctors

        #region IUserService public methods

        public int Create(User user)
        {
            user.Password = HashHelper.GetHash(user.Password);

            _userRepository.Create(user);

            return _userRepository.SaveChanges();
        }

        public User Get(int id, bool @readonly = true)
        {
            return _userRepository.Get(id, @readonly);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByCredentials(string username, string password)
        {
            password = HashHelper.GetHash(password);

            return _userRepository.Find(u => u.Username == username && u.Password == password).FirstOrDefault();
        }

        public User GetByEmail(string email, bool @readonly = true)
        {
            return _userRepository.Find(u => u.Email == email, @readonly).FirstOrDefault();
        }

        public ResetPassword GetResetPasswordRequest(int id, bool @readonly = true)
        {
            return _resetPasswordRepository.Get(id, @readonly);
        }

        public ResetPassword GetResetPasswordRequestByResetSecurity(Guid resetPasswordSecurity, bool @readonly = true)
        {
            return _resetPasswordRepository.GetAll(@readonly).FirstOrDefault(x => x.ResetPasswordSecurity == resetPasswordSecurity);
        }

        public IEnumerable<User> GetUsersToShare(int loggedInUser)
        {
            return _userRepository.GetAll().Where(x => x.Id != loggedInUser);
        }

        public int Remove(User user)
        {
            _userRepository.Remove(user.Id);

            return _userRepository.SaveChanges();
        }

        public ResetPassword RequestResetPassword(string email, int idUser)
        {
            var resetPasswordSecurity = Guid.NewGuid();

            var resetPassword = new ResetPassword
            {
                Email = email,
                IdUser = idUser,
                ResetPasswordSecurity = resetPasswordSecurity,
                ResetPasswordStatus = Domain.Enums.ResetPasswordStatusEnum.Requested
            };

            _resetPasswordRepository.Create(resetPassword);

            var affected = _resetPasswordRepository.SaveChanges();

            if (affected > 0)
            {
                _eventDispatcher.Dispatch(new SendResetPasswordEmailEvent(email, resetPasswordSecurity));
            }

            return resetPassword;
        }

        public ResetPassword ResetPassword(ResetPassword resetPassword)
        {
            var user = Get(resetPassword.IdUser, false);

            var newHashPassword = HashHelper.GetHash(resetPassword.NewPassword);

            user.Password = newHashPassword;

            Update(user);

            resetPassword = GetResetPasswordRequest(resetPassword.Id, false);

            resetPassword.ResetPasswordStatus = Domain.Enums.ResetPasswordStatusEnum.Processed;
            resetPassword.NewPassword = newHashPassword;

            var count = UpdateResetPasswordRequest(resetPassword);

            return resetPassword;
        }

        public void ShareServiceDescriptionWithUser(int idServiceDescription, int idUserToShare)
        {
            var notYetShared = _serviceDescription_UserRepository.GetAllBySharedUser(idUserToShare).Count(x => x.IdServiceDescription == idServiceDescription) == 0;

            if (notYetShared)
            {
                var serviceDescription_User = new ServiceDescription_User
                {
                    IdServiceDescription = idServiceDescription,
                    IdSharedUser = idUserToShare
                };

                _serviceDescription_UserRepository.Create(serviceDescription_User);

                _serviceDescription_UserRepository.SaveChanges();
            }
        }

        public int Update(User user)
        {
            _userRepository.Update(user);

            return _userRepository.SaveChanges();
        }

        public int UpdateResetPasswordRequest(ResetPassword resetPassword)
        {
            _resetPasswordRepository.Update(resetPassword);

            return _resetPasswordRepository.SaveChanges();
        }

        #endregion IUserService public methods
    }
}
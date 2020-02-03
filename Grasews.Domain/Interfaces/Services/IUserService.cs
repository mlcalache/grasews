using Grasews.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IUserService : IBaseService
    {
        int Create(User user);

        User Get(int id, bool @readonly = true);

        IEnumerable<User> GetAll();

        User GetByCredentials(string username, string password);

        User GetByEmail(string email, bool @readonly = true);

        ResetPassword GetResetPasswordRequestByResetSecurity(Guid s, bool @readonly = true);

        IEnumerable<User> GetUsersToShare(int loggedInUser);

        int Remove(User user);

        ResetPassword RequestResetPassword(string email, int idUser);

        ResetPassword ResetPassword(ResetPassword resetPassword);

        void ShareServiceDescriptionWithUser(int idServiceDescription, int idUserToShare);

        int Update(User user);
    }
}
using System;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IUserIdentityService : IBaseService
    {
        string Email { get; }
        string GivenName { get; }
        int Id { get; }
        bool IsAdmin { get; }
        bool IsAuthenticated { get; }
        string Name { get; }
        DateTime? RegistrationDateTime { get; }
        string Surname { get; }
        string Username { get; }
    }
}
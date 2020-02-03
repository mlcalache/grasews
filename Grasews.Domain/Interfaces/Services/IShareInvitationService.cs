using Grasews.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IShareInvitationService : IBaseService
    {
        void AcceptInvitationForExistingUser(ShareInvitation shareInvitation, User user);

        void AcceptInvitationForNewUser(ShareInvitation shareInvitation, User newUser);

        int Create(ShareInvitation shareInvitation);
        int Create(ShareInvitation shareInvitation, List<Ontology> ontologies);

        ShareInvitation Get(int idShareInvitation);

        List<ShareInvitation> GetAll();

        List<ShareInvitation> GetAllByServiceDescription(int idServiceDescription);

        List<ShareInvitation> GetAllBySharedEmail(string email);

        List<ShareInvitation> GetAllByUserInviter(int idUserInviter);

        ShareInvitation GetByInvitationSecurity(Guid invitationSecurity);

        int Remove(ShareInvitation shareInvitation);

        int Update(ShareInvitation shareInvitation); 
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class ShareInvitationEntityRepository : BaseEntityRepository<ShareInvitation, int>, IShareInvitationEntityRepository
    {
        #region IShareInvitationEntityRepository public methods

        public override IQueryable<ShareInvitation> GetAll(bool @readonly = true)
        {
            return @readonly
                ? _context.ShareInvitations.AsNoTracking()
                    .Include(nameof(ShareInvitation.ServiceDescription))
                    .Include(nameof(ShareInvitation.UserInviter))
                : _context.ShareInvitations
                    .Include(nameof(ShareInvitation.ServiceDescription))
                    .Include(nameof(ShareInvitation.UserInviter));
        }

        //public IQueryable<ShareInvitation> GetAllBySharedEmail(string email)
        //{
        //    return _context.ShareInvitations
        //        .Include(nameof(ShareInvitation.ServiceDescription))
        //        .Include(nameof(ShareInvitation.UserInviter))
        //        .Where(x => x.Email == email.ToUpper());
        //}

        //public ShareInvitation GetByInvitationSecurity(Guid invitationSecurity)
        //{
        //    return _context.ShareInvitations
        //        .Include(nameof(ShareInvitation.ServiceDescription))
        //        .Include(nameof(ShareInvitation.UserInviter))
        //        .FirstOrDefault(x => x.InvitationSecurity == invitationSecurity);
        //}

        //public IQueryable<ShareInvitation> GetAllByServiceDescription(int idServiceDescription)
        //{
        //    return _context.ShareInvitations
        //        .Include(nameof(ShareInvitation.ServiceDescription))
        //        .Include(nameof(ShareInvitation.UserInviter))
        //        .Where(x => x.IdServiceDescription == idServiceDescription);
        //}

        #endregion IShareInvitationEntityRepository public methods
    }
}
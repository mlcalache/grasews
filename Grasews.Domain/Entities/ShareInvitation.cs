using Grasews.Domain.Enums;
    using System;
    using System.Collections.Generic;
namespace Grasews.Domain.Entities
{

    public class ShareInvitation : BaseEntity<int>
    {
        #region Props

        public string Email { get; set; }
        public bool ExistingUser { get; set; }
        public int IdServiceDescription { get; set; }
        public int IdUserInviter { get; set; }
        public Guid InvitationSecurity { get; set; }
        public InvitationStatusEnum InvitationStatus { get; set; }
        #endregion Props

        #region Navigations

        public virtual ICollection<ShareInvitation_Ontology> ShareInvitation_Ontologies { get; set; }
        public virtual ServiceDescription ServiceDescription { get; set; }
        public virtual User UserInviter { get; set; }

        #endregion Navigations

        #region ToString

        public override string ToString()
        {
            return Email;
        }

        #endregion ToString
    }
}
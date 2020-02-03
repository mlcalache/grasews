using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class IssueEntityRepository : BaseEntityRepository<Issue, int>, IIssueEntityRepository
    {
        #region IIssueEntityRepository methods

        public IQueryable<Issue> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true)
        {
            return base.GetAll(@readonly)
                .Include(nameof(Issue.IssueAnswers))
                .Where(x => x.IdServiceDescription == idServiceDescription);
        }

        public IQueryable<Issue> GetAllByUser(int idUser, bool @readonly = true)
        {
            return GetAll(@readonly)
                .Include(nameof(Issue.IssueAnswers))
                .Where(x => x.IdOwnerUser == idUser);
        }

        public IQueryable<Issue> GetAllCompleteByServiceDescription(int idServiceDescription, bool @readonly = true)
        {
            return base.GetAll(@readonly)
                .Include(nameof(Issue.IssueAnswers))
                .Include(nameof(Issue.WsdlInterface))
                .Include(nameof(Issue.WsdlOperation))
                .Include(nameof(Issue.WsdlInput))
                .Include(nameof(Issue.WsdlOutput))
                .Include(nameof(Issue.WsdlInFault))
                .Include(nameof(Issue.WsdlOutFault))
                .Include(nameof(Issue.XsdComplexElement))
                .Include(nameof(Issue.XsdSimpleElement))
                .Include(nameof(Issue.XsdElement))
                .Where(x => x.IdServiceDescription == idServiceDescription);
        }

        public IQueryable<Issue> GetAllCompleteByUser(int idUser, bool @readonly = true)
        {
            return GetAll(@readonly)
                .Include(nameof(Issue.IssueAnswers))
                .Include(nameof(Issue.WsdlInterface))
                .Include(nameof(Issue.WsdlOperation))
                .Include(nameof(Issue.WsdlInput))
                .Include(nameof(Issue.WsdlOutput))
                .Include(nameof(Issue.WsdlInFault))
                .Include(nameof(Issue.WsdlOutFault))
                .Include(nameof(Issue.XsdComplexElement))
                .Include(nameof(Issue.XsdSimpleElement))
                .Include(nameof(Issue.XsdElement))
                .Where(x => x.IdOwnerUser == idUser);
        }

        #endregion IIssueEntityRepository methods

        #region Overrides

        public override void Remove(int id)
        {
            var issue = GetComplete(id, @readonly: false);

            _context.IssueAnswers.RemoveRange(issue.IssueAnswers);

            _context.Issues.Remove(issue);
        }

        #endregion Overrides

        #region Private methods

        private Issue GetComplete(int id, bool @readonly)
        {
            var query = base.GetAll(@readonly)
                .Include(nameof(Issue.IssueAnswers));

            var issue = query.FirstOrDefault(x => x.Id == id);

            return issue;
        }

        #endregion Private methods
    }
}
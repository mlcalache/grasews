using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IIssueAnswerService : IBaseService
    {
        int Create(IssueAnswer issueAnswer);

        IssueAnswer Get(int idIssue, int id);

        List<IssueAnswer> GetAll(int idIssue);

        int Remove(IssueAnswer issueAnswer);

        int Update(IssueAnswer issueAnswer);
    }
}
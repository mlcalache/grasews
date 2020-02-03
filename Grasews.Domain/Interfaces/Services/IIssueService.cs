using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IIssueService : IBaseService
    {
        int Create(Issue issue);

        Issue Get(int id);

        List<Issue> GetAll();

        List<Issue> GetAllByServiceDescription(int idServiceDescription);

        List<Issue> GetAllByUser(int idUser);

        List<Issue> GetAllCompleteByServiceDescription(int idServiceDescription);

        List<Issue> GetAllCompleteByUser(int idUser);

        int Remove(Issue issue);

        int Update(Issue issue);
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class IssueAnswerService : BaseService, IIssueAnswerService
    {
        #region Private vars

        private readonly IIssueAnswerEntityRepository _issueAnswerRepository;

        #endregion Private vars

        #region Ctors

        public IssueAnswerService(IIssueAnswerEntityRepository issueAnswerRepository)
        {
            _issueAnswerRepository = issueAnswerRepository;
        }

        #endregion Ctors

        #region IIssueAnswerService public methods

        public int Remove(IssueAnswer issueAnswer)
        {
            _issueAnswerRepository.Remove(issueAnswer.Id);

            return _issueAnswerRepository.SaveChanges();
        }

        public int Create(IssueAnswer issueAnswer)
        {
            _issueAnswerRepository.Create(issueAnswer);

            return _issueAnswerRepository.SaveChanges();
        }

        public IssueAnswer Get(int idIssue, int id)
        {
            return _issueAnswerRepository.GetAll().FirstOrDefault(x => x.IdIssue == idIssue && x.Id == id);
        }

        public List<IssueAnswer> GetAll(int idIssue)
        {
            return _issueAnswerRepository.GetAll().Where(x => x.IdIssue == idIssue).ToList();
        }

        public int Update(IssueAnswer issueAnswer)
        {
            var existingIssueAnswer = _issueAnswerRepository.GetAll(@readonly: false)
                .FirstOrDefault(x => x.IdIssue == issueAnswer.IdIssue && x.Id == issueAnswer.Id);

            existingIssueAnswer.Answer = issueAnswer.Answer;

            _issueAnswerRepository.Update(existingIssueAnswer);

            return _issueAnswerRepository.SaveChanges();
        }

        #endregion IIssueAnswerService public methods
    }
}
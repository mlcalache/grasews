using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class IssueService : BaseService, IIssueService
    {
        #region Private vars

        private readonly IIssueEntityRepository _issueRepository;
        private readonly IWsdlInterfaceEntityRepository _wsdlInterfaceRepository;
        private readonly IWsdlOperationEntityRepository _wsdlOperationRepository;
        private readonly IWsdlFaultEntityRepository _wsdlFaultRepository;
        private readonly IXsdComplexTypeEntityRepository _xsdComplexTypeRepository;
        private readonly IXsdSimpleTypeEntityRepository _xsdSimpleTypeRepository;

        #endregion Private vars

        #region Ctors

        public IssueService(IIssueEntityRepository issueRepository,
            IWsdlInterfaceEntityRepository wsdlInterfaceRepository,
            IWsdlOperationEntityRepository wsdlOperationRepository,
            IWsdlFaultEntityRepository wsdlFaultRepository,
            IXsdComplexTypeEntityRepository xsdComplexTypeRepository,
            IXsdSimpleTypeEntityRepository xsdSimpleTypeRepository)
        {
            _issueRepository = issueRepository;

            _wsdlInterfaceRepository = wsdlInterfaceRepository;
            _wsdlOperationRepository = wsdlOperationRepository;
            _wsdlFaultRepository = wsdlFaultRepository;
            _xsdComplexTypeRepository = xsdComplexTypeRepository;
            _xsdSimpleTypeRepository = xsdSimpleTypeRepository;
        }

        #endregion Ctors

        #region IIssueService public methods

        public int Remove(Issue issue)
        {
            _issueRepository.Remove(issue.Id);

            return _issueRepository.SaveChanges();
        }

        public int Create(Issue issue)
        {
            _issueRepository.Create(issue);

            return _issueRepository.SaveChanges();
        }

        public Issue Get(int id)
        {
            return _issueRepository.Get(id);
        }

        public List<Issue> GetAll()
        {
            return _issueRepository.GetAll().ToList();
        }

        public List<Issue> GetAllByServiceDescription(int idServiceDescription)
        {
            return _issueRepository.GetAllByServiceDescription(idServiceDescription).ToList();
        }

        public List<Issue> GetAllByUser(int idUser)
        {
            return _issueRepository.GetAllByUser(idUser).ToList();
        }

        public List<Issue> GetAllCompleteByServiceDescription(int idServiceDescription)
        {
            var issues = _issueRepository.GetAllCompleteByServiceDescription(idServiceDescription).ToList();

            foreach (var issue in issues)
            {
                if (issue.IdWsdlInterface.HasValue)
                {
                    issue.WsdlInterface = _wsdlInterfaceRepository.Get(issue.IdWsdlInterface.Value);
                }
                else if (issue.IdWsdlOperation.HasValue)
                {
                    issue.WsdlOperation = _wsdlOperationRepository.Get(issue.IdWsdlOperation.Value);
                }
                else if (issue.IdWsdlFault.HasValue)
                {
                    issue.WsdlFault = _wsdlFaultRepository.Get(issue.IdWsdlFault.Value);
                }
                else if (issue.IdXsdComplexType.HasValue)
                {
                    issue.XsdComplexType = _xsdComplexTypeRepository.Get(issue.IdXsdComplexType.Value);
                }
                else if (issue.IdXsdSimpleType.HasValue)
                {
                    issue.XsdSimpleType = _xsdSimpleTypeRepository.Get(issue.IdXsdSimpleType.Value);
                }
            }

            return issues;
        }

        public List<Issue> GetAllCompleteByUser(int idUser)
        {
            return _issueRepository.GetAllCompleteByUser(idUser).ToList();
        }

        public int Update(Issue issue)
        {
            var existingIssue = _issueRepository.GetAll(@readonly: false).FirstOrDefault(x => x.Id == issue.Id);

            existingIssue.Description = issue.Description;
            existingIssue.Solved = issue.Solved;

            _issueRepository.Update(existingIssue);

            return _issueRepository.SaveChanges();
        }

        #endregion IIssueService public methods
    }
}
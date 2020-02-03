using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.DTOs;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IOntologyService : IBaseService
    {
        int AddOntologyToSharedUsers(int idOntology, IEnumerable<int> idSharedUsers);

        void AddOntologyToSharedUsersAsync(int idOntology, int idServiceDescription);

        void AddOntologyToSharedUsersSync(int idOntology, int idServiceDescription);

        bool CheckIfOntologyAlreadyExists(string ontologyName);

        int Create(Ontology ontology);

        IEnumerable<OntologyTerm> FindPathBetweenTerms(OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2);

        OntologyTerm FindTerm(int idOntology, int idOntologyTerm);

        OntologyTerm FindTerm(Ontology ontology, int termId);

        Ontology Get(int idOntology);

        IEnumerable<Ontology> GetAll();

        IEnumerable<Ontology> GetAllByUser(int userId);

        IEnumerable<Ontology> GetAllOntologiesFromOntologyTerms(IEnumerable<int> idOntologyTerms);

        Ontology GetByOntologyName(string ontologyName);

        Ontology GetByServiceName(string serviceName, int userId);

        Ontology GetComplete(int idOntology);

        string GetHtmlTreeViewMenu(Ontology ontology);

        string GetOntologyNameFromXml(Ontology ontology);

        OntologyTerm GetOntologyTerm(int idOntologyTerm);

        void ParseXml(Ontology ontology);

        IParseOwlResponseDTO ParseXml(IParseOwlRequestDTO parseOwlRequestDTO);

        int Remove(Ontology ontology);

        int Update(Ontology ontology);
    }
}
using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IServiceDescription_OntologyService : IBaseService
    {
        int Create(ServiceDescription_Ontology serviceDescription_Ontology);

        ServiceDescription_Ontology Get(int id);

        List<ServiceDescription_Ontology> GetAll();

        List<ServiceDescription_Ontology> GetByOntologyId(int idOntology);

        List<ServiceDescription_Ontology> GetByServiceDescriptionId(int idServiceDescription, bool @readonly = true);

        int Remove(ServiceDescription_Ontology serviceDescription_Ontology);

        int RemoveRange(IEnumerable<ServiceDescription_Ontology> serviceDescription_Ontologies);
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    public class ServiceDescription_OntologyService : BaseService, IServiceDescription_OntologyService
    {
        #region Private vars

        private readonly IServiceDescription_OntologyEntityRepository _serviceDescription_OntologyEntityRepository;

        #endregion Private vars

        #region Ctors

        public ServiceDescription_OntologyService(IServiceDescription_OntologyEntityRepository serviceDescription_OntologyEntityRepository)
        {
            _serviceDescription_OntologyEntityRepository = serviceDescription_OntologyEntityRepository;
        }

        #endregion Ctors

        #region IServiceDescription_OntologyService public methods

        public int Create(ServiceDescription_Ontology serviceDescription_Ontology)
        {
            _serviceDescription_OntologyEntityRepository.Create(serviceDescription_Ontology);

            var count = _serviceDescription_OntologyEntityRepository.SaveChanges();

            return count;
        }

        public ServiceDescription_Ontology Get(int idServiceDescription_Ontology)
        {
            return _serviceDescription_OntologyEntityRepository.Get(idServiceDescription_Ontology);
        }

        public List<ServiceDescription_Ontology> GetAll()
        {
            return _serviceDescription_OntologyEntityRepository.GetAll().ToList();
        }

        public List<ServiceDescription_Ontology> GetByOntologyId(int idOntology)
        {
            return _serviceDescription_OntologyEntityRepository.GetAll().Where(x => x.IdOntology == idOntology).ToList();
        }

        public List<ServiceDescription_Ontology> GetByServiceDescriptionId(int idServiceDescription, bool @readonly = true)
        {
            return _serviceDescription_OntologyEntityRepository.GetAll(@readonly).Where(x => x.IdServiceDescription == idServiceDescription).ToList();
        }

        public int Remove(ServiceDescription_Ontology serviceDescription_Ontology)
        {
            _serviceDescription_OntologyEntityRepository.Remove(serviceDescription_Ontology.Id);

            return _serviceDescription_OntologyEntityRepository.SaveChanges();
        }

        public int RemoveRange(IEnumerable<ServiceDescription_Ontology> serviceDescription_Ontologies)
        {
            _serviceDescription_OntologyEntityRepository.RemoveRange(serviceDescription_Ontologies);

            return _serviceDescription_OntologyEntityRepository.SaveChanges();
        }

        #endregion IServiceDescription_OntologyService public methods
    }
}
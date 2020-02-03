using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class ServiceDescription_OntologyEntityRepository : BaseEntityRepository<ServiceDescription_Ontology, int>, IServiceDescription_OntologyEntityRepository
    {
        #region IServiceDescription_OntologyEntityRepository public methods

        public IQueryable<ServiceDescription_Ontology> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true)
        {
            return @readonly
                ? _context.ServiceDescription_Ontologies.AsNoTracking()
                    .Include(nameof(ServiceDescription_Ontology.ServiceDescription))
                    .Include(nameof(ServiceDescription_Ontology.Ontology))
                    .Where(x => x.IdServiceDescription == idServiceDescription)
                : _context.ServiceDescription_Ontologies
                    .Include(nameof(ServiceDescription_Ontology.ServiceDescription))
                    .Include(nameof(ServiceDescription_Ontology.Ontology))
                    .Where(x => x.IdServiceDescription == idServiceDescription);
        }

        #endregion IServiceDescription_OntologyEntityRepository public methods
    }
}
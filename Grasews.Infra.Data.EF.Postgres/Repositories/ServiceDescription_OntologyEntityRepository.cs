using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class ServiceDescription_OntologyEntityRepository : BaseEntityRepository<ServiceDescription_Ontology, int>, IServiceDescription_OntologyEntityRepository
    {
        #region IServiceDescription_OntologyEntityRepository public methods

        public IQueryable<ServiceDescription_Ontology> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.ServiceDescription_Ontologies.AsNoTracking() : _context.ServiceDescription_Ontologies;

            var query = baseQuery
                .Include(nameof(ServiceDescription_Ontology.ServiceDescription))
                .Include(nameof(ServiceDescription_Ontology.Ontology))
                .Where(x => x.IdServiceDescription == idServiceDescription);

            return query;

            //return @readonly
            //    ? _context.ServiceDescription_Ontologies.AsNoTracking()
            //        .Include(nameof(ServiceDescription_Ontology.ServiceDescription))
            //        .Include(nameof(ServiceDescription_Ontology.Ontology))
            //        .Where(x => x.IdServiceDescription == idServiceDescription)
            //    : _context.ServiceDescription_Ontologies
            //        .Include(nameof(ServiceDescription_Ontology.ServiceDescription))
            //        .Include(nameof(ServiceDescription_Ontology.Ontology))
            //        .Where(x => x.IdServiceDescription == idServiceDescription);
        }

        #endregion IServiceDescription_OntologyEntityRepository public methods
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class OntologyTermEntityRepository : BaseEntityRepository<OntologyTerm, int>, IOntologyTermEntityRepository
    {
        public IQueryable<OntologyTerm> GetWithNodePositionsByIdOntologyTerm(int idOntologyTerm, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.OntologyTerms.AsNoTracking() : _context.OntologyTerms;

            var query = baseQuery
                .Include(nameof(OntologyTerm.GraphNodePosition_OntologyTerms))
                .Where(x => x.Id == idOntologyTerm);

            return query;

            //return @readonly
            //    ? _context.OntologyTerms.AsNoTracking()
            //        .Include(nameof(OntologyTerm.GraphNodePosition_OntologyTerms))
            //        .Where(x => x.Id == idOntologyTerm)
            //    : _context.OntologyTerms
            //        .Include(nameof(OntologyTerm.GraphNodePosition_OntologyTerms))
            //        .Where(x => x.Id == idOntologyTerm);
        }
    }
}
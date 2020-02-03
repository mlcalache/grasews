using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class OntologyTermEntityRepository : BaseEntityRepository<OntologyTerm, int>, IOntologyTermEntityRepository
    {
        public IQueryable<OntologyTerm> GetWithNodePositionsByIdOntologyTerm(int idOntologyTerm, bool @readonly = true)
        {
            return @readonly
                ? _context.OntologyTerms.AsNoTracking()
                    .Include(nameof(OntologyTerm.GraphNodePosition_OntologyTerms))
                    .Where(x => x.Id == idOntologyTerm)
                : _context.OntologyTerms
                    .Include(nameof(OntologyTerm.GraphNodePosition_OntologyTerms))
                    .Where(x => x.Id == idOntologyTerm);
        }
    }
}
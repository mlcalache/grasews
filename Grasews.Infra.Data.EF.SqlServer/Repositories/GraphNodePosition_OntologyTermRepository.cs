using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class GraphNodePosition_OntologyTermRepository : BaseEntityRepository<GraphNodePosition_OntologyTerm, int>, IGraphNodePosition_OntologyTermRepository
    {
        public IQueryable<GraphNodePosition_OntologyTerm> GetAllWithOntologyTerms(bool @readonly = false)
        {
            return @readonly
                ? _context.GraphNodePosition_OntologyTerms.AsNoTracking()
                    .Include(nameof(GraphNodePosition_OntologyTerm.OntologyTerm))
                : _context.GraphNodePosition_OntologyTerms
                    .Include(nameof(GraphNodePosition_OntologyTerm.OntologyTerm));
        }

        public IQueryable<GraphNodePosition_OntologyTerm> GetOntologyTermGraphPositionsByUser(int idOwnerUser, bool @readonly = true)
        {
            return @readonly
                ? _context.GraphNodePosition_OntologyTerms.AsNoTracking()
                    .Where(x => x.IdOwnerUser == idOwnerUser)
                : _context.GraphNodePosition_OntologyTerms
                    .Where(x => x.IdOwnerUser == idOwnerUser);
        }

        public void RemoveOntologyTermGraphPositions(IEnumerable<GraphNodePosition_OntologyTerm> nodePositionsOntologyTerms)
        {
            _context.GraphNodePosition_OntologyTerms.RemoveRange(nodePositionsOntologyTerms);
        }
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class GraphNodePosition_OntologyTermRepository : BaseEntityRepository<GraphNodePosition_OntologyTerm, int>, IGraphNodePosition_OntologyTermRepository
    {
        #region IGraphNodePosition_OntologyTermRepository methods

        public IQueryable<GraphNodePosition_OntologyTerm> GetAllWithOntologyTerms(bool @readonly = false)
        {
            var baseQuery = @readonly ? _context.GraphNodePosition_OntologyTerms.AsNoTracking() : _context.GraphNodePosition_OntologyTerms;

            //var query = baseQuery.Include(nameof(GraphNodePosition_OntologyTerm.OntologyTerm));

            //return query;
            return baseQuery;
        }

        public IQueryable<GraphNodePosition_OntologyTerm> GetOntologyTermGraphPositionsByUser(int idOwnerUser, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.GraphNodePosition_OntologyTerms.AsNoTracking() : _context.GraphNodePosition_OntologyTerms;

            //var query = baseQuery.Where(x => x.IdOwnerUser == idOwnerUser);

            //return query;
            return baseQuery;
        }

        public void RemoveOntologyTermGraphPositions(IEnumerable<GraphNodePosition_OntologyTerm> nodePositionsOntologyTerms)
        {
            _context.GraphNodePosition_OntologyTerms.RemoveRange(nodePositionsOntologyTerms);
        }

        #endregion IGraphNodePosition_OntologyTermRepository methods
    }
}
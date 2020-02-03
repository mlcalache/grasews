using Grasews.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IGraphNodePosition_OntologyTermRepository : IBaseEntityRepository<GraphNodePosition_OntologyTerm, int>
    {
        IQueryable<GraphNodePosition_OntologyTerm> GetAllWithOntologyTerms(bool @readonly = false);

        IQueryable<GraphNodePosition_OntologyTerm> GetOntologyTermGraphPositionsByUser(int idOwnerUser, bool @readonly = true);

        void RemoveOntologyTermGraphPositions(IEnumerable<GraphNodePosition_OntologyTerm> nodePositionsOntologyTerms);
    }
}
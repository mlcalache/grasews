using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class GraphNodePosition_SawsdlModelReferenceRepository : BaseEntityRepository<GraphNodePosition_SawsdlModelReference, int>, IGraphNodePosition_SawsdlModelReferenceRepository
    {
        #region IGraphNodePosition_SawsdlModelReferenceRepository methods

        public IQueryable<GraphNodePosition_SawsdlModelReference> GetAllWithSawsdlModelReference(bool @readonly = false)
        {
            var baseQuery = @readonly ? _context.GraphNodePosition_SawsdlModelReferences.AsNoTracking() : _context.GraphNodePosition_SawsdlModelReferences;

            var query = baseQuery.Include(nameof(GraphNodePosition_SawsdlModelReference.SawsdlModelReference));

            return query;
            //return baseQuery;
        }

        #endregion IGraphNodePosition_SawsdlModelReferenceRepository methods
    }
}
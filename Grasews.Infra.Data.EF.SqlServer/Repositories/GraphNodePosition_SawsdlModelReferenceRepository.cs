using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class GraphNodePosition_SawsdlModelReferenceRepository : BaseEntityRepository<GraphNodePosition_SawsdlModelReference, int>, IGraphNodePosition_SawsdlModelReferenceRepository
    {
        public IQueryable<GraphNodePosition_SawsdlModelReference> GetAllWithSawsdlModelReference(bool @readonly = false)
        {
            return @readonly
                ? _context.GraphNodePosition_SawsdlModelReferences.AsNoTracking()
                    .Include(nameof(GraphNodePosition_SawsdlModelReference.SawsdlModelReference))
                : _context.GraphNodePosition_SawsdlModelReferences
                    .Include(nameof(GraphNodePosition_SawsdlModelReference.SawsdlModelReference));
        }
    }
}
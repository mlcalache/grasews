using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IGraphNodePosition_SawsdlModelReferenceRepository : IBaseEntityRepository<GraphNodePosition_SawsdlModelReference, int>
    {
        IQueryable<GraphNodePosition_SawsdlModelReference> GetAllWithSawsdlModelReference(bool @readonly = false);
    }
}
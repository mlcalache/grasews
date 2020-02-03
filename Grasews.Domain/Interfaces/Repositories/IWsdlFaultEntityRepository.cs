using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IWsdlFaultEntityRepository : IBaseEntityRepository<WsdlFault, int>
    {
        WsdlFault GetWithNodePositions(int id, bool @readonly = true);

        WsdlFault GetWithSemanticAnnotations(int id, bool @readonly = true);
    }
}
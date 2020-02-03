using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IWsdlOperationEntityRepository : IBaseEntityRepository<WsdlOperation, int>
    {
        WsdlOperation GetWithNodePositions(int id, bool @readonly = true);

        WsdlOperation GetWithSemanticAnnotations(int id, bool @readonly = true);
    }
}
using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IWsdlInterfaceEntityRepository : IBaseEntityRepository<WsdlInterface, int>
    {
        WsdlInterface GetWithNodePositions(int id, bool @readonly = true);

        WsdlInterface GetWithSemanticAnnotations(int id, bool @readonly = true);
    }
}
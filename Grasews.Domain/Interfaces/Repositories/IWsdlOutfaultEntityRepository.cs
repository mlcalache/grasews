using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IWsdlOutfaultEntityRepository : IBaseEntityRepository<WsdlOutfault, int>
    {
        WsdlOutfault GetWithNodePositions(int id, bool @readonly = false);
    }
}
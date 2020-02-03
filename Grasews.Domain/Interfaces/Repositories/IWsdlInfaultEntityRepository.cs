using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IWsdlInfaultEntityRepository : IBaseEntityRepository<WsdlInfault, int>
    {
        WsdlInfault GetWithNodePositions(int id, bool @readonly = false);
    }
}
using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IWsdlOutputEntityRepository : IBaseEntityRepository<WsdlOutput, int>
    {
        WsdlOutput GetWithNodePositions(int id, bool @readonly = false);
    }
}
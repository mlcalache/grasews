using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IWsdlInputEntityRepository : IBaseEntityRepository<WsdlInput, int>
    {
        WsdlInput GetWithNodePositions(int id, bool @readonly = false);
    }
}
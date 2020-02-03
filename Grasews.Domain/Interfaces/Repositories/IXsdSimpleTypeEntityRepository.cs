using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IXsdSimpleTypeEntityRepository : IBaseEntityRepository<XsdSimpleType, int>
    {
        XsdSimpleType GetWithNodePositions(int id, bool @readonly = false);

        XsdSimpleType GetWithSemanticAnnotations(int id, bool @readonly = false);
    }
}
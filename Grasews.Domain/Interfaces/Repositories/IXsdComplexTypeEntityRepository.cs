using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IXsdComplexTypeEntityRepository : IBaseEntityRepository<XsdComplexType, int>
    {
        XsdComplexType GetWithNodePositions(int id, bool @readonly = true);

        XsdComplexType GetWithSemanticAnnotations(int id, bool @readonly = true);

        IQueryable<XsdComplexType> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true);
    }
}
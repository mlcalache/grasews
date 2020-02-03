using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IOntologyEntityRepository : IBaseEntityRepository<Ontology, int>
    {
        IQueryable<Ontology> GetAllByUser(int userId);

        Ontology GetComplete(int id, int levels, bool @readonly = true);
    }
}
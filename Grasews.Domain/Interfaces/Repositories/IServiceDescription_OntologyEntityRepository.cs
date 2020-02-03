using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IServiceDescription_OntologyEntityRepository : IBaseEntityRepository<ServiceDescription_Ontology, int>
    {
        IQueryable<ServiceDescription_Ontology> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true);
    }
}
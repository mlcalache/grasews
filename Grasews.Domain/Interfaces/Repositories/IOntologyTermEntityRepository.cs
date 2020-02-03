using Grasews.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IOntologyTermEntityRepository : IBaseEntityRepository<OntologyTerm, int>
    {
        IQueryable<OntologyTerm> GetWithNodePositionsByIdOntologyTerm(int idOntologyTerm, bool @readonly = true);
    }
}
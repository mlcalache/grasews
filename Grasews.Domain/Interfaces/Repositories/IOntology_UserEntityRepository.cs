using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IOntology_UserEntityRepository : IBaseEntityRepository<Ontology_User, int>
    {
        IQueryable<Ontology_User> GetAllByOntology(int idServiceDescription);

        Ontology_User GetAllByOntologyAndSharedUser(int idServiceDescription, int idUser);

        IQueryable<Ontology_User> GetAllBySharedUser(int idUser);
    }
}
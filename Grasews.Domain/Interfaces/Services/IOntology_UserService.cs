using Grasews.Domain.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IOntology_UserService : IBaseService
    {
        bool CheckIfOntologyIsAlreadySharedWithUser(int idUser);

        int Create(Ontology_User ontology_User);

        int CreateBySharedServiceDescription(int idServiceDescription, int idOntologyTerm);

        int CreateBySharedServiceDescription(int idServiceDescription);

        Ontology_User Get(int id);

        List<Ontology_User> GetAll();

        int Remove(Ontology_User ontology_User);

        int Update(Ontology_User ontology_User);
    }
}
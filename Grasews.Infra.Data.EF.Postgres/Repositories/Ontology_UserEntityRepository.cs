using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class Ontology_UserEntityRepository : BaseEntityRepository<Ontology_User, int>, IOntology_UserEntityRepository
    {
        #region IOntology_UserEntityRepository public methods

        public IQueryable<Ontology_User> GetAllByOntology(int idOntology)
        {
            return _context.Ontology_Users
                .Include(nameof(Ontology_User.Ontology))
                .Include(nameof(Ontology_User.SharedUser))
                .Where(x => x.IdOntology == idOntology);
        }

        public IQueryable<Ontology_User> GetAllBySharedUser(int idUser)
        {
            return _context.Ontology_Users
                .Include(nameof(Ontology_User.Ontology))
                .Include(nameof(Ontology_User.SharedUser))
                .Where(x => x.IdSharedUser == idUser);
        }

        public Ontology_User GetAllByOntologyAndSharedUser(int idOntology, int idUser)
        {
            return _context.Ontology_Users
                .Include(nameof(Ontology_User.Ontology))
                .Include(nameof(Ontology_User.SharedUser))
                .FirstOrDefault(x => x.IdOntology == idOntology && x.IdSharedUser == idUser);
        }

        #endregion IOntology_UserEntityRepository public methods

        #region Overrides

        public override Ontology_User Get(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.Ontology_Users.AsNoTracking() : _context.Ontology_Users;

            var query = baseQuery
                .Include(nameof(Ontology_User.Ontology))
                .Include(nameof(Ontology_User.SharedUser))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //    ? _context.Ontology_Users.AsNoTracking()
            //        .Include(nameof(Ontology_User.Ontology))
            //        .Include(nameof(Ontology_User.SharedUser))
            //        .FirstOrDefault(x => x.Id == id)
            //    : _context.Ontology_Users
            //        .Include(nameof(Ontology_User.Ontology))
            //        .Include(nameof(Ontology_User.SharedUser))
            //        .FirstOrDefault(x => x.Id == id);
        }

        #endregion Overrides
    }
}
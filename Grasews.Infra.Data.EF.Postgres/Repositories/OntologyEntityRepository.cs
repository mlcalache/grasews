using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class OntologyEntityRepository : BaseEntityRepository<Ontology, int>, IOntologyEntityRepository
    {
        #region IOntologyEntityRepository methods

        public IQueryable<Ontology> GetAllByUser(int idUser)
        {
            return GetAll().Where(x => x.IdOwnerUser == idUser);
        }

        public Ontology GetComplete(int id, int levels, bool @readonly = true)
        {
            var ontologies = GetAll(@readonly);

            ontologies = ontologies.Include("OntologyTerms." + CreateIncludeForCompleteOntology(levels));

            var ontology = ontologies.FirstOrDefault(x => x.Id == id);

            return ontology;
        }

        #endregion IOntologyEntityRepository methods

        #region Overrides

        public override void Remove(int id)
        {
            var ontologyTerms = _context.OntologyTerms.Where(x => x.IdOntology == id);

            _context.OntologyTerms.RemoveRange(ontologyTerms);

            var ontology_Users = _context.Ontology_Users.Where(x => x.IdOntology == id);

            _context.Ontology_Users.RemoveRange(ontology_Users);

            base.Remove(id);
        }

        #endregion Overrides

        #region Private methods

        private static string CreateIncludeForCompleteOntology(int levels)
        {
            var includes = new List<string>();

            for (int i = 0; i < levels; i++)
            {
                includes.Add(nameof(OntologyTerm.ChildrenOntologyTerms));
            }

            return string.Join(".", includes);
        }

        #endregion Private methods
    }
}
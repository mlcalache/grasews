using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class OntologyEntityRepository : BaseEntityRepository<Ontology, int>, IOntologyEntityRepository
    {
        #region Public methods

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

        public override void Remove(int id)
        {
            var ontologyTerms = _context.OntologyTerms.Where(x => x.IdOntology == id);

            _context.OntologyTerms.RemoveRange(ontologyTerms);

            base.Remove(id);
        }

        #endregion Public methods

        #region Private methods

        private string CreateIncludeForCompleteOntology(int levels)
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
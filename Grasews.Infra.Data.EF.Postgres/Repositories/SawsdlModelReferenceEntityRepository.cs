using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class SawsdlModelReferenceEntityRepository : BaseEntityRepository<SawsdlModelReference, int>, ISawsdlModelReferenceEntityRepository
    {
        #region ISawsdlModelReferenceEntityRepository methods

        public IQueryable<SawsdlModelReference> GetAllWithWsdlFault(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.WsdlFault));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetAllWithWsdlInterface(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.WsdlInterface));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetAllWithWsdlOperation(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.WsdlOperation));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetAllWithXsdComplexType(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.XsdComplexType));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetAllWithXsdSimpleType(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.XsdSimpleType));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetWithNodePositionsByIdOntologyTermAndIdServiceDescription(int idOntologyTerm, int idServiceDescription, bool @readonly = false)
        {
            var baseQuery = @readonly ? _context.SawsdlModelReferences.AsNoTracking() : _context.SawsdlModelReferences;

            var query = baseQuery
                .Include(nameof(SawsdlModelReference.GraphNodePosition_SawsdlModelReferences))
                .Where(x => x.IdOntologyTerm == idOntologyTerm && x.IdServiceDescription == idServiceDescription);

            return query;

            //return @readonly
            //    ? _context.SawsdlModelReferences.AsNoTracking()
            //        .Include(nameof(SawsdlModelReference.GraphNodePosition_SawsdlModelReferences))
            //        .Where(x => x.IdOntologyTerm == idOntologyTerm && x.IdServiceDescription == idServiceDescription)
            //    : _context.SawsdlModelReferences
            //        .Include(nameof(SawsdlModelReference.GraphNodePosition_SawsdlModelReferences))
            //        .Where(x => x.IdOntologyTerm == idOntologyTerm && x.IdServiceDescription == idServiceDescription);
        }

        #endregion ISawsdlModelReferenceEntityRepository methods
    }
}
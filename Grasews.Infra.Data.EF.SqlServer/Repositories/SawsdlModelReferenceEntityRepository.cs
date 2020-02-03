using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class SawsdlModelReferenceEntityRepository : BaseEntityRepository<SawsdlModelReference, int>, ISawsdlModelReferenceEntityRepository
    {
        public IQueryable<SawsdlModelReference> GetAllWithWsdlInFault(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.WsdlInFault));

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

        public IQueryable<SawsdlModelReference> GetAllWithWsdlOutFault(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.WsdlOutFault));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetAllWithXsdComplexElement(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.XsdComplexElement));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetAllWithXsdElement(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.XsdElement));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetAllWithXsdSimpleElement(bool @readonly = true)
        {
            var sawsdModelReferences = GetAll(@readonly);

            sawsdModelReferences = sawsdModelReferences.Include(nameof(SawsdlModelReference.XsdSimpleElement));

            return sawsdModelReferences;
        }

        public IQueryable<SawsdlModelReference> GetWithNodePositionsByIdOntologyTermAndIdServiceDescription(int idOntologyTerm, int idServiceDescription, bool @readonly = false)
        {
            return @readonly
                ? _context.SawsdlModelReferences.AsNoTracking()
                    .Include(nameof(SawsdlModelReference.GraphNodePosition_SawsdlModelReferences))
                    .Where(x => x.IdOntologyTerm == idOntologyTerm && x.IdServiceDescription == idServiceDescription)
                : _context.SawsdlModelReferences
                    .Include(nameof(SawsdlModelReference.GraphNodePosition_SawsdlModelReferences))
                    .Where(x => x.IdOntologyTerm == idOntologyTerm && x.IdServiceDescription == idServiceDescription);
        }
    }
}
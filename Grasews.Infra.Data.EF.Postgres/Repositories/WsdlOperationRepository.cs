using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class WsdlOperationRepository : BaseEntityRepository<WsdlOperation, int>, IWsdlOperationEntityRepository
    {
        public WsdlOperation GetWithNodePositions(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlOperations.AsNoTracking() : _context.WsdlOperations;

            var query = baseQuery
                .Include(nameof(WsdlOperation.GraphNodePosition_WsdlOperations))
                .Include(nameof(WsdlOperation.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //     ? _context.WsdlOperations.AsNoTracking()
            //         .Include("GraphNodePosition_WsdlOperations")
            //        .Include(nameof(WsdlOperation.Issues))
            //         .FirstOrDefault(x => x.Id == id)
            //     : _context.WsdlOperations
            //         .Include("GraphNodePosition_WsdlOperations")
            //        .Include(nameof(WsdlOperation.Issues))
            //         .FirstOrDefault(x => x.Id == id);
        }

        public WsdlOperation GetWithSemanticAnnotations(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlOperations.AsNoTracking() : _context.WsdlOperations;

            var query = baseQuery
                .Include(nameof(WsdlOperation.SawsdlModelReferences))
                .Include($"{nameof(WsdlOperation.SawsdlModelReferences)}.{nameof(SawsdlModelReference.OntologyTerm)}")
                .Include(nameof(WsdlOperation.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //     ? _context.WsdlOperations.AsNoTracking()
            //         .Include("SawsdlModelReferences")
            //         .Include("SawsdlModelReferences.OntologyTerm")
            //        .Include(nameof(WsdlOperation.Issues))
            //         .FirstOrDefault(x => x.Id == id)
            //     : _context.WsdlOperations
            //         .Include("SawsdlModelReferences")
            //         .Include("SawsdlModelReferences.OntologyTerm")
            //        .Include(nameof(WsdlOperation.Issues))
            //         .FirstOrDefault(x => x.Id == id);
        }
    }
}
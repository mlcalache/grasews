using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class WsdlOperationRepository : BaseEntityRepository<WsdlOperation, int>, IWsdlOperationEntityRepository
    {
        public WsdlOperation GetWithNodePositions(int id, bool @readonly = true)
        {
            return @readonly
                 ? _context.WsdlOperations.AsNoTracking()
                     .Include("GraphNodePosition_WsdlOperations")
                    .Include(nameof(WsdlOperation.Issues))
                     .FirstOrDefault(x => x.Id == id)
                 : _context.WsdlOperations
                     .Include("GraphNodePosition_WsdlOperations")
                    .Include(nameof(WsdlOperation.Issues))
                     .FirstOrDefault(x => x.Id == id);
        }

        public WsdlOperation GetWithSemanticAnnotations(int id, bool @readonly = true)
        {
            return @readonly
                 ? _context.WsdlOperations.AsNoTracking()
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(WsdlOperation.Issues))
                     .FirstOrDefault(x => x.Id == id)
                 : _context.WsdlOperations
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(WsdlOperation.Issues))
                     .FirstOrDefault(x => x.Id == id);
        }
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class WsdlInterfaceRepository : BaseEntityRepository<WsdlInterface, int>, IWsdlInterfaceEntityRepository
    {
        public WsdlInterface GetWithNodePositions(int id, bool @readonly = true)
        {
           return @readonly
                ? _context.WsdlInterfaces.AsNoTracking()
                    .Include("GraphNodePosition_WsdlInterfaces")
                    .Include(nameof(WsdlInterface.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.WsdlInterfaces
                    .Include("GraphNodePosition_WsdlInterfaces")
                    .Include(nameof(WsdlInterface.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }


        public WsdlInterface GetWithSemanticAnnotations(int id, bool @readonly = true)
        {
            return @readonly
                 ? _context.WsdlInterfaces.AsNoTracking()
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(WsdlInterface.Issues))
                     .FirstOrDefault(x => x.Id == id)
                 : _context.WsdlInterfaces
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(WsdlInterface.Issues))
                     .FirstOrDefault(x => x.Id == id);
        }
    }
}
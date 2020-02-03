using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class WsdlOutFaultRepository : BaseEntityRepository<WsdlOutFault, int>, IWsdlOutFaultEntityRepository
    {
        public WsdlOutFault GetWithNodePositions(int id, bool @readonly = false)
        {
            return @readonly
                ? _context.WsdlOutFaults.AsNoTracking()
                    .Include("GraphNodePosition_WsdlOutFaults")
                    .Include(nameof(WsdlOutFault.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.WsdlOutFaults
                    .Include("GraphNodePosition_WsdlOutFaults")
                    .Include(nameof(WsdlOutFault.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
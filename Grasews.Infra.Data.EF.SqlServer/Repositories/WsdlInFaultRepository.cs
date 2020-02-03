using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class WsdlInFaultRepository : BaseEntityRepository<WsdlInFault, int>, IWsdlInFaultEntityRepository
    {
        public WsdlInFault GetWithNodePositions(int id, bool @readonly = false)
        {
            return @readonly
                ? _context.WsdlInFaults.AsNoTracking()
                    .Include("GraphNodePosition_WsdlInFaults")
                    .Include(nameof(WsdlInFault.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.WsdlInFaults
                    .Include("GraphNodePosition_WsdlInFaults")
                    .Include(nameof(WsdlInFault.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
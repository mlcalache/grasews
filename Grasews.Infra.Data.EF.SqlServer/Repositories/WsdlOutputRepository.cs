using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class WsdlOutputRepository : BaseEntityRepository<WsdlOutput, int>, IWsdlOutputEntityRepository
    {
        public WsdlOutput GetWithNodePositions(int id, bool @readonly = false)
        {
            return @readonly
                ? _context.WsdlOutputs.AsNoTracking()
                    .Include("GraphNodePosition_WsdlOutputs")
                    .Include(nameof(WsdlOutput.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.WsdlOutputs
                    .Include("GraphNodePosition_WsdlOutputs")
                    .Include(nameof(WsdlOutput.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
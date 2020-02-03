using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    public class WsdlInputRepository : BaseEntityRepository<WsdlInput, int>, IWsdlInputEntityRepository
    {
        public WsdlInput GetWithNodePositions(int id, bool @readonly = false)
        {
            return @readonly
                ? _context.WsdlInputs.AsNoTracking()
                    .Include("GraphNodePosition_WsdlInputs")
                    .Include(nameof(WsdlInput.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.WsdlInputs
                    .Include("GraphNodePosition_WsdlInputs")
                    .Include(nameof(WsdlInput.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
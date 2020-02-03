using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class WsdlOutfaultRepository : BaseEntityRepository<WsdlOutfault, int>, IWsdlOutfaultEntityRepository
    {
        public WsdlOutfault GetWithNodePositions(int id, bool @readonly = false)
        {
            var baseQuery = @readonly ? _context.WsdlOutfaults.AsNoTracking() : _context.WsdlOutfaults;

            var query = baseQuery
                .Include(nameof(WsdlOutfault.GraphNodePosition_WsdlOutfaults))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //    ? _context.WsdlOutfaults.AsNoTracking()
            //        .Include("GraphNodePosition_WsdlOutfaults")
            //        .Include(nameof(WsdlOutfault.Issues))
            //        .FirstOrDefault(x => x.Id == id)
            //    : _context.WsdlOutfaults
            //        .Include("GraphNodePosition_WsdlOutfaults")
            //        .Include(nameof(WsdlOutfault.Issues))
            //        .FirstOrDefault(x => x.Id == id);
        }
    }
}
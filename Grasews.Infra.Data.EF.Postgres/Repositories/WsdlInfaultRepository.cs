using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class WsdlInfaultRepository : BaseEntityRepository<WsdlInfault, int>, IWsdlInfaultEntityRepository
    {
        #region IWsdlInfaultEntityRepository methods

        public WsdlInfault GetWithNodePositions(int id, bool @readonly = false)
        {
            var baseQuery = @readonly ? _context.WsdlInfaults.AsNoTracking() : _context.WsdlInfaults;

            var query = baseQuery
                .Include(nameof(WsdlInfault.GraphNodePosition_WsdlInfaults))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //    ? _context.WsdlInfaults.AsNoTracking()
            //        .Include("GraphNodePosition_WsdlInfaults")
            //        .Include(nameof(WsdlInfault.Issues))
            //        .FirstOrDefault(x => x.Id == id)
            //    : _context.WsdlInfaults
            //        .Include("GraphNodePosition_WsdlInfaults")
            //        .Include(nameof(WsdlInfault.Issues))
            //        .FirstOrDefault(x => x.Id == id);
        }

        #endregion IWsdlInfaultEntityRepository methods
    }
}
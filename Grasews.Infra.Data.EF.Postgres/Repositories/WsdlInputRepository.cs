using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class WsdlInputRepository : BaseEntityRepository<WsdlInput, int>, IWsdlInputEntityRepository
    {
        #region IWsdlInputEntityRepository methods

        public WsdlInput GetWithNodePositions(int id, bool @readonly = false)
        {
            var baseQuery = @readonly ? _context.WsdlInputs.AsNoTracking() : _context.WsdlInputs;

            var query = baseQuery
                .Include(nameof(WsdlInput.GraphNodePosition_WsdlInputs))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //    ? _context.WsdlInputs.AsNoTracking()
            //        .Include("GraphNodePosition_WsdlInputs")
            //        .Include(nameof(WsdlInput.Issues))
            //        .FirstOrDefault(x => x.Id == id)
            //    : _context.WsdlInputs
            //        .Include("GraphNodePosition_WsdlInputs")
            //        .Include(nameof(WsdlInput.Issues))
            //        .FirstOrDefault(x => x.Id == id);
        }

        #endregion IWsdlInputEntityRepository methods
    }
}
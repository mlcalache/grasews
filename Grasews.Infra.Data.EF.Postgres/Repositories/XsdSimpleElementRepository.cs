using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class XsdSimpleElementRepository : BaseEntityRepository<XsdSimpleType, int>, IXsdSimpleTypeEntityRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdSimpleType GetWithNodePositions(int id, bool @readonly = false)
        {
            var baseQuery = @readonly ? _context.XsdSimpleTypes.AsNoTracking() : _context.XsdSimpleTypes;

            var query = baseQuery
                .Include(nameof(XsdSimpleType.GraphNodePosition_XsdSimpleTypes))
                .Include(nameof(XsdSimpleType.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //    ? _context.XsdSimpleTypes.AsNoTracking()
            //        .Include("GraphNodePosition_XsdSimpleElements")
            //        .Include(nameof(XsdSimpleType.Issues))
            //        .FirstOrDefault(x => x.Id == id)
            //    : _context.XsdSimpleTypes
            //        .Include("GraphNodePosition_XsdSimpleElements")
            //        .Include(nameof(XsdSimpleType.Issues))
            //        .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdSimpleType GetWithSemanticAnnotations(int id, bool @readonly = false)
        {
            return @readonly
                 ? _context.XsdSimpleTypes.AsNoTracking()
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(XsdSimpleType.Issues))
                     .FirstOrDefault(x => x.Id == id)
                 : _context.XsdSimpleTypes
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(XsdSimpleType.Issues))
                     .FirstOrDefault(x => x.Id == id);
        }
    }
}
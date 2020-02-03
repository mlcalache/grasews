using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class XsdSimpleElementRepository : BaseEntityRepository<XsdSimpleElement, int>, IXsdSimpleElementEntityRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdSimpleElement GetWithNodePositions(int id, bool @readonly = false)
        {
            return @readonly
                ? _context.XsdSimpleElements.AsNoTracking()
                    .Include("GraphNodePosition_XsdSimpleElements")
                    .Include(nameof(XsdSimpleElement.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.XsdSimpleElements
                    .Include("GraphNodePosition_XsdSimpleElements")
                    .Include(nameof(XsdSimpleElement.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdSimpleElement GetWithSemanticAnnotations(int id, bool @readonly = false)
        {
            return @readonly
                 ? _context.XsdSimpleElements.AsNoTracking()
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(XsdSimpleElement.Issues))
                     .FirstOrDefault(x => x.Id == id)
                 : _context.XsdSimpleElements
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(XsdSimpleElement.Issues))
                     .FirstOrDefault(x => x.Id == id);
        }
    }
}
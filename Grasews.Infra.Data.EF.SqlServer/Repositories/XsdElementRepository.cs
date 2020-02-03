using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class XsdElementRepository : BaseEntityRepository<XsdElement, int>, IXsdElementEntityRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdElement GetWithNodePositions(int id, bool @readonly = false)
        {
            return @readonly
                ? _context.XsdElements.AsNoTracking()
                    .Include("GraphNodePosition_XsdElements")
                    .Include(nameof(XsdElement.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.XsdElements
                    .Include("GraphNodePosition_XsdElements")
                    .Include(nameof(XsdElement.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdElement GetWithSemanticAnnotations(int id, bool @readonly = false)
        {
            return @readonly
                 ? _context.XsdElements.AsNoTracking()
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(XsdElement.Issues))
                     .FirstOrDefault(x => x.Id == id)
                 : _context.XsdElements
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(XsdElement.Issues))
                     .FirstOrDefault(x => x.Id == id);
        }
    }
}
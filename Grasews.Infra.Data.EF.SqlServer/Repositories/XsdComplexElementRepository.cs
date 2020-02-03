using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.SqlServer.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class XsdComplexElementRepository : BaseEntityRepository<XsdComplexElement, int>, IXsdComplexElementEntityRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdComplexElement GetWithNodePositions(int id, bool @readonly = false)
        {
            return @readonly
                ? _context.XsdComplexElements.AsNoTracking()
                    .Include("GraphNodePosition_XsdComplexElements")
                    .Include(nameof(XsdComplexElement.Issues))
                    .FirstOrDefault(x => x.Id == id)
                : _context.XsdComplexElements
                    .Include("GraphNodePosition_XsdComplexElements")
                    .Include(nameof(XsdComplexElement.Issues))
                    .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdComplexElement GetWithSemanticAnnotations(int id, bool @readonly = false)
        {
            return @readonly
                 ? _context.XsdComplexElements.AsNoTracking()
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(XsdComplexElement.Issues))
                     .FirstOrDefault(x => x.Id == id)
                 : _context.XsdComplexElements
                     .Include("SawsdlModelReferences")
                     .Include("SawsdlModelReferences.OntologyTerm")
                    .Include(nameof(WsdlOutput.Issues))
                     .FirstOrDefault(x => x.Id == id);
        }
    }
}
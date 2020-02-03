using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class XsdComplexElementRepository : BaseEntityRepository<XsdComplexType, int>, IXsdComplexTypeEntityRepository
    {
        #region IXsdComplexTypeEntityRepository methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdComplexType GetWithNodePositions(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.XsdComplexTypes.AsNoTracking() : _context.XsdComplexTypes;

            var query = baseQuery
                .Include(nameof(XsdComplexType.GraphNodePosition_XsdComplexTypes))
                .Include(nameof(XsdComplexType.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //? _context.XsdComplexTypes.AsNoTracking()
            //    .Include("GraphNodePosition_XsdComplexElements")
            //    .Include(nameof(XsdComplexType.Issues))
            //    .FirstOrDefault(x => x.Id == id)
            //: _context.XsdComplexTypes
            //    .Include("GraphNodePosition_XsdComplexElements")
            //    .Include(nameof(XsdComplexType.Issues))
            //    .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public XsdComplexType GetWithSemanticAnnotations(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.XsdComplexTypes.AsNoTracking() : _context.XsdComplexTypes;

            var query = baseQuery
                .Include(nameof(XsdComplexType.SawsdlModelReferences))
                .Include($"{nameof(XsdComplexType.SawsdlModelReferences)}.{nameof(SawsdlModelReference.OntologyTerm)}")
                .Include(nameof(XsdComplexType.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //     ? _context.XsdComplexTypes.AsNoTracking()
            //         .Include("SawsdlModelReferences")
            //         .Include("SawsdlModelReferences.OntologyTerm")
            //        .Include(nameof(XsdComplexType.Issues))
            //         .FirstOrDefault(x => x.Id == id)
            //     : _context.XsdComplexTypes
            //         .Include("SawsdlModelReferences")
            //         .Include("SawsdlModelReferences.OntologyTerm")
            //        .Include(nameof(WsdlOutput.Issues))
            //         .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public IQueryable<XsdComplexType> GetAllByServiceDescription(int idServiceDescription, bool @readonly = true)
        {
            var baseQuery = GetAll();

            var query = baseQuery
                .Include(ct => ct.XsdDocument)
                .Include(ct => ct.XsdDocument.ServiceDescription)
                .Include(ct => ct.ParentsXsdComplexTypes)
                .Where(ct => ct.XsdDocument.ServiceDescription.Id == idServiceDescription);

            return query;
        }

        #endregion IXsdComplexTypeEntityRepository methods
    }
}
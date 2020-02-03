using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;
using System.Linq;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class WsdlFaultRepository : BaseEntityRepository<WsdlFault, int>, IWsdlFaultEntityRepository
    {
        public WsdlFault GetWithNodePositions(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlFaults.AsNoTracking() : _context.WsdlFaults;

            var query = baseQuery
                .Include(nameof(WsdlFault.GraphNodePosition_WsdlFaults))
                .Include(nameof(WsdlFault.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;
        }

        public WsdlFault GetWithSemanticAnnotations(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlFaults.AsNoTracking() : _context.WsdlFaults;

            var query = baseQuery
                .Include(nameof(WsdlFault.SawsdlModelReferences))
                .Include($"{nameof(WsdlFault.SawsdlModelReferences)}.{nameof(SawsdlModelReference.OntologyTerm)}")
                .Include(nameof(WsdlFault.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;
        }
    }

    public class WsdlInterfaceRepository : BaseEntityRepository<WsdlInterface, int>, IWsdlInterfaceEntityRepository
    {
        public WsdlInterface GetWithNodePositions(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlInterfaces.AsNoTracking() : _context.WsdlInterfaces;

            var query = baseQuery
                .Include(nameof(WsdlInterface.GraphNodePosition_WsdlInterfaces))
                .Include(nameof(WsdlInterface.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //     ? _context.WsdlInterfaces.AsNoTracking()
            //         .Include("GraphNodePosition_WsdlInterfaces")
            //         .Include(nameof(WsdlInterface.Issues))
            //         .FirstOrDefault(x => x.Id == id)
            //     : _context.WsdlInterfaces
            //         .Include("GraphNodePosition_WsdlInterfaces")
            //         .Include(nameof(WsdlInterface.Issues))
            //         .FirstOrDefault(x => x.Id == id);
        }

        public WsdlInterface GetWithSemanticAnnotations(int id, bool @readonly = true)
        {
            var baseQuery = @readonly ? _context.WsdlInterfaces.AsNoTracking() : _context.WsdlInterfaces;

            var query = baseQuery
                .Include(nameof(WsdlInterface.SawsdlModelReferences))
                .Include($"{nameof(WsdlInterface.SawsdlModelReferences)}.{nameof(SawsdlModelReference.OntologyTerm)}")
                .Include(nameof(WsdlInterface.Issues))
                .FirstOrDefault(x => x.Id == id);

            return query;

            //return @readonly
            //     ? _context.WsdlInterfaces.AsNoTracking()
            //         .Include("SawsdlModelReferences")
            //         .Include("SawsdlModelReferences.OntologyTerm")
            //          .Include(nameof(WsdlInterface.Issues))
            //         .FirstOrDefault(x => x.Id == id)
            //     : _context.WsdlInterfaces
            //         .Include("SawsdlModelReferences")
            //         .Include("SawsdlModelReferences.OntologyTerm")
            //        .Include(nameof(WsdlInterface.Issues))
            //         .FirstOrDefault(x => x.Id == id);
        }
    }
}
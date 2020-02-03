using Grasews.Domain.Entities;
using System.Linq;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface ISawsdlModelReferenceEntityRepository : IBaseEntityRepository<SawsdlModelReference, int>
    {
        IQueryable<SawsdlModelReference> GetAllWithWsdlFault(bool @readonly = true);

        IQueryable<SawsdlModelReference> GetAllWithWsdlInterface(bool @readonly = true);

        IQueryable<SawsdlModelReference> GetAllWithWsdlOperation(bool @readonly = true);

        IQueryable<SawsdlModelReference> GetAllWithXsdComplexType(bool @readonly = true);

        IQueryable<SawsdlModelReference> GetAllWithXsdSimpleType(bool @readonly = true);

        IQueryable<SawsdlModelReference> GetWithNodePositionsByIdOntologyTermAndIdServiceDescription(int idOntologyTerm, int idServiceDescription, bool @readonly = false);
    }
}
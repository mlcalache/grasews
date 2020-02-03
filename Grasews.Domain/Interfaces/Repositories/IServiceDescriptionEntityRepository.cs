using Grasews.Domain.Entities;

namespace Grasews.Domain.Interfaces.Repositories
{
    public interface IServiceDescriptionEntityRepository : IBaseEntityRepository<ServiceDescription, int>
    {
        ServiceDescription GetComplete(int id, bool withSawsdlModelReferences = false, bool @readonly = true);

        ServiceDescription GetWithOntologies(int id, bool @readonly = true);

        #region Wsdl and Xsd

        WsdlFault GetWsdlFault(int idWsdlFault, bool @readonly = true);

        WsdlInfault GetWsdlInfault(int idWsdlInfault, bool @readonly = true);

        WsdlInput GetWsdlInput(int idWsdlInput, bool @readonly = true);

        WsdlInterface GetWsdlInterface(int idWsdlInterface, bool @readonly = true);

        WsdlOperation GetWsdlOperation(int idWsdlOperation, bool @readonly = true);

        WsdlOutfault GetWsdlOutfault(int idWsdlOutfault, bool @readonly = true);

        WsdlOutput GetWsdlOutput(int idWsdlOutput, bool @readonly = true);

        XsdComplexType GetXsdComplexType(int idXsdComplexType, bool @readonly = true);

        XsdSimpleType GetXsdSimpleType(int idXsdSimpleType, bool @readonly = true);

        void RemoveXsdComplexTypeSelfReferences(int id);

        #endregion Wsdl and Xsd
    }
}
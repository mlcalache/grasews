using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.DTOs;
using Grasews.Domain.Interfaces.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IServiceDescriptionService : IBaseService
    {
        int Create(ServiceDescription serviceDescription);

        int CreateNodePositionsForSameModelReferencesBetweenWsdlElements(int idServiceDescription, int idOwnerUser);

        ServiceDescription Get(int idServiceDescription, bool withSawsdlModelReference = false, bool @readonly = true);

        IEnumerable<ServiceDescription> GetAll();

        IEnumerable<ServiceDescription> GetAllByOwnerUser(int idOwnerUser);

        ServiceDescription GetByServiceName(string serviceName);

        ServiceDescription GetByUser(int idServiceDescription, int idUser, bool withSawsdlModelReference = false, bool @readonly = true);

        string GetHtmlTreeViewMenu(ServiceDescription serviceDescription);

        ServiceDescription GetWithOntologies(int idServiceDescription, bool @readonly = true);

        WsdlFault GetWsdlFault(int idWsdlFault);

        WsdlFault GetWsdlFaultWithSemanticAnnotations(int idWsdlFault);

        WsdlInfault GetWsdlInfault(int idWsdlInfault);

        WsdlInput GetWsdlInput(int idWsdlInput);

        WsdlInterface GetWsdlInterface(int idWsdlInterface);

        WsdlInterface GetWsdlInterfaceWithSemanticAnnotations(int idWsdlInterface);

        WsdlOperation GetWsdlOperation(int idWsdlOperation);

        WsdlOperation GetWsdlOperationWithSemanticAnnotations(int idWsdlOperation);

        WsdlOutfault GetWsdlOutfault(int idWsdlOutfault);

        WsdlOutput GetWsdlOutput(int idWsdlOutput);

        XsdComplexType GetXsdComplexType(int idXsdComplexType);

        XsdComplexType GetXsdComplexTypeWithSemanticAnnotations(int idXsdComplexType);

        XsdSimpleType GetXsdSimpleType(int idXsdSimpleType);

        XsdSimpleType GetXsdSimpleTypeWithSemanticAnnotations(int idXsdSimpleType);

        ServiceDescription ParseXml(IParseWsdlRequestDTO parseXmlRequestDTO);

        int Remove(ServiceDescription serviceDescription);

        void RemoveAllSharing(int idServiceDescription);

        void RemoveSharing(int idServiceDescription, int idSharedUser);

        void RemoveSharing(int idServiceDescription_User);

        int RemoveUnusedGraphNodePositisons(IGraphDataObject graphDataObject);

        //int RemoveUnusedGraphNodePositisonsByUser(int idOwnerUser, IGraphDataObject graphDataObject);

        string SetGraphNodesPositionsByUser(int idOwnerUser, int idServiceDescription, string originalGraphJson);

        int TransferNodePositionsFromModelReferencesToOntologyTerms(int idOwnerUser, int idServiceDescription, IEnumerable<int> idOntologyTerms);

        int TransferNodePositionsFromOntologyTermsToModelReferences(int idOwnerUser, int idServiceDescription);

        int Update(ServiceDescription serviceDescription);

        int UpdateJsonGraph(int idServiceDescription, int idOwnerUser, IGraphDataObject graphDataObject);
    }
}
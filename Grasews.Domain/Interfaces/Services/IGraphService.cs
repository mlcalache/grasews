using Grasews.Domain.Entities;
using Grasews.Domain.Enums;
using Grasews.Domain.Interfaces.Entities;
using System.Collections.Generic;

namespace Grasews.Domain.Interfaces.Services
{
    public interface IGraphService : IBaseService
    {
        #region Model Reference

        #region Add Model Reference

        IGraphDataObject AddEdgeToIntermediateNodesForOntologyTerms(IGraphDataObject cytoscapeObject, OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2);

        IGraphDataObject AddIntermediateNodesForOntologyTerms(IGraphDataObject cytoscapeObject, OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2);

        IGraphDataObject AddModelReferencesNodesToWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface, IEnumerable<SawsdlModelReference> modelReferences);

        IGraphDataObject AddModelReferencesNodesToWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdlOperation, IEnumerable<SawsdlModelReference> modelReferences);

        IGraphDataObject AddModelReferencesNodesToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType, IEnumerable<SawsdlModelReference> modelReferences);

        IGraphDataObject AddModelReferencesNodesToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType, IEnumerable<SawsdlModelReference> modelReferences);

        #endregion Add Model Reference

        #region Remove Model Reference

        IGraphDataObject RemoveModelReferencesNodesFromWsdlFault(IGraphDataObject cytoscapeObject, WsdlFault wsdlFault, IEnumerable<int> idOntologyTerms);

        IGraphDataObject RemoveModelReferencesNodesFromWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface, IEnumerable<int> idOntologyTerms);

        IGraphDataObject RemoveModelReferencesNodesFromWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdOperation, IEnumerable<int> idOntologyTerms);

        IGraphDataObject RemoveModelReferencesNodesFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType, IEnumerable<int> idOntologyTerms);

        IGraphDataObject RemoveModelReferencesNodesFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType, IEnumerable<int> idOntologyTerms);

        #endregion Remove Model Reference

        #endregion Model Reference

        #region Schema Mapping

        #region Add Schema Mapping

        IGraphDataObject AddLiftingSchemaMappingToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType);

        IGraphDataObject AddLiftingSchemaMappingToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType);

        IGraphDataObject AddLoweringSchemaMappingToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType);

        IGraphDataObject AddLoweringSchemaMappingToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType);

        #endregion Add Schema Mapping

        #region Remove Schema Mapping

        IGraphDataObject RemoveLiftingSchemaMappingFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType);

        IGraphDataObject RemoveLiftingSchemaMappingFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType);

        IGraphDataObject RemoveLoweringSchemaMappingFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType);

        IGraphDataObject RemoveLoweringSchemaMappingFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType);

        #endregion Remove Schema Mapping

        #endregion Schema Mapping

        #region Issue

        #region Add issue

        IGraphDataObject AddIssueToWsdlFault(IGraphDataObject cytoscapeObject, WsdlFault wsdlFault);

        IGraphDataObject AddIssueToWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface);

        IGraphDataObject AddIssueToWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdlOperation);

        IGraphDataObject AddIssueToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType);

        IGraphDataObject AddIssueToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType);

        #endregion Add issue

        #region Remove issue

        IGraphDataObject RemoveIssueFromWsdlFault(IGraphDataObject cytoscapeObject, WsdlFault wsdlFault);

        IGraphDataObject RemoveIssueFromWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface);

        IGraphDataObject RemoveIssueFromWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdlOperation);

        IGraphDataObject RemoveIssueFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexType);

        IGraphDataObject RemoveIssueFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType);

        #endregion Remove issue

        #endregion Issue

        #region Graph Data

        IGraphDataObject CreateGraphData(int idServiceDescription, string serviceName, ICollection<WsdlInterface> wsdlInterfaces);

        ICollection<IGraphStyleObject> CreateGraphStyles();

        IGraphEdge GetExistingEdge(ICollection<IGraphEdge> existingEdges, int idOntologyTerm, int sourceId, string sourceName, GraphNodeTypeEnum graphNodeTypeEnum);

        ICollection<IGraphEdge> GetExistingEdgesForOntologyTermNode(ICollection<IGraphEdge> existingEdges, int idOntologyTerm);

        bool IsEdgeAlreadyBetweenOntologyTerms(IGraphDataObject cytoscapeObject, OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2);

        IGraphDataObject RemoveIntermediateOntologyTermsFromGraph(IGraphDataObject graphDataObject);

        #endregion Graph Data
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Enums;
using Grasews.Domain.Interfaces.Entities;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using Grasews.Infra.ExternalService.Cytoscape.Enums;
using Grasews.Infra.ExternalService.Cytoscape.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Grasews.Infra.ExternalService.Cytoscape.Services
{
    /// <summary>
    ///
    /// </summary>
    public class CytoscapeService : IGraphService
    {
        #region Public methods

        #region Model Reference Nodes

        #region Add Model Reference

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="ontologyTerm1"></param>
        /// <param name="ontologyTerm2"></param>
        /// <returns></returns>
        public IGraphDataObject AddEdgeToIntermediateNodesForOntologyTerms(IGraphDataObject cytoscapeObject, OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2)
        {
            cytoscapeObject.Edges.Add(CreateEdgeForIntermediateOntologyTerm(ontologyTerm1, ontologyTerm2));

            return cytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="ontologyTerm1"></param>
        /// <param name="ontologyTerm2"></param>
        /// <returns></returns>
        public IGraphDataObject AddIntermediateNodesForOntologyTerms(IGraphDataObject cytoscapeObject, OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2)
        {
            var parentNode = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Document);

            var sourceNode = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdOntologyTerm == ontologyTerm1.Id);

            var targetNode = CreateSawsdlOntologyTermNode(parentNode, "ontology-node", ontologyTerm2);

            cytoscapeObject.Nodes.Add(targetNode);

            cytoscapeObject.Edges.Add(CreateEdgeForIntermediateOntologyTerm(childClass: ontologyTerm2, parentClass: ontologyTerm1));

            return cytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="wsdlFault"></param>
        /// <param name="modelReferences"></param>
        /// <returns></returns>
        public IGraphDataObject AddModelReferencesNodesToWsdlFault(IGraphDataObject cytoscapeObject, WsdlFault wsdlFault, IEnumerable<SawsdlModelReference> modelReferences)
        {
            var hasIssues = HasIssues(wsdlFault);

            return CreateSawsdlModelReferenceGraphDataObject(cytoscapeObject, wsdlFault.Id, wsdlFault.WsdlFaultName, GraphNodeTypeEnum.Fault, modelReferences, false, false, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="modelReferences"></param>
        /// <param name="wsdlInterface"></param>
        /// <returns></returns>
        public IGraphDataObject AddModelReferencesNodesToWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface, IEnumerable<SawsdlModelReference> modelReferences)
        {
            var hasIssues = HasIssues(wsdlInterface);

            return CreateSawsdlModelReferenceGraphDataObject(cytoscapeObject, wsdlInterface.Id, wsdlInterface.WsdlInterfaceName, GraphNodeTypeEnum.Interface, modelReferences, false, false, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="wsdlOperation"></param>
        /// <param name="modelReferences"></param>
        /// <returns></returns>
        public IGraphDataObject AddModelReferencesNodesToWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdlOperation, IEnumerable<SawsdlModelReference> modelReferences)
        {
            var hasIssues = HasIssues(wsdlOperation);

            return CreateSawsdlModelReferenceGraphDataObject(cytoscapeObject, wsdlOperation.Id, wsdlOperation.WsdlOperationName, GraphNodeTypeEnum.Operation, modelReferences, false, false, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdComplexElement"></param>
        /// <param name="modelReferences"></param>
        /// <returns></returns>
        public IGraphDataObject AddModelReferencesNodesToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement, IEnumerable<SawsdlModelReference> modelReferences)
        {
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);
            var hasIssues = HasIssues(xsdComplexElement);

            return CreateSawsdlModelReferenceGraphDataObject(cytoscapeObject, xsdComplexElement.Id, xsdComplexElement.XsdComplexTypeName, GraphNodeTypeEnum.ComplexType, modelReferences, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdSimpleType"></param>
        /// <param name="modelReferences"></param>
        /// <returns></returns>
        public IGraphDataObject AddModelReferencesNodesToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType, IEnumerable<SawsdlModelReference> modelReferences)
        {
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdSimpleType);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdSimpleType);
            var hasIssues = HasIssues(xsdSimpleType);

            return CreateSawsdlModelReferenceGraphDataObject(cytoscapeObject, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName, GraphNodeTypeEnum.SimpleType, modelReferences, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);
        }

        #endregion Add Model Reference

        #region Remove Model Reference

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="wsdlFault"></param>
        /// <param name="idOntologyTerms">todo: describe idOntologyTerms parameter on RemoveModelReferencesNodesFromXsdComplexElement</param>
        /// <returns></returns>
        public IGraphDataObject RemoveModelReferencesNodesFromWsdlFault(IGraphDataObject cytoscapeObject, WsdlFault wsdlFault, IEnumerable<int> idOntologyTerms)
        {
            var hasIssues = HasIssues(wsdlFault);

            return RemoveSawsdlModelReferenceNode(cytoscapeObject, wsdlFault.Id, wsdlFault.WsdlFaultName, wsdlFault.SawsdlModelReferences, GraphNodeTypeEnum.Fault, idOntologyTerms, false, false, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="wsdlInterface"></param>
        /// <param name="idOntologyTerms">todo: describe idOntologyTerms parameter on RemoveModelReferencesNodesFromWsdlInterface</param>
        /// <returns></returns>
        public IGraphDataObject RemoveModelReferencesNodesFromWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface, IEnumerable<int> idOntologyTerms)
        {
            var hasIssues = HasIssues(wsdlInterface);

            return RemoveSawsdlModelReferenceNode(cytoscapeObject, wsdlInterface.Id, wsdlInterface.WsdlInterfaceName, wsdlInterface.SawsdlModelReferences, GraphNodeTypeEnum.Interface, idOntologyTerms, false, false, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="wsdlOperation"></param>
        /// <param name="idOntologyTerms">todo: describe idOntologyTerms parameter on RemoveModelReferencesNodesFromWsdlOperation</param>
        /// <returns></returns>
        public IGraphDataObject RemoveModelReferencesNodesFromWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdlOperation, IEnumerable<int> idOntologyTerms)
        {
            var hasIssues = HasIssues(wsdlOperation);

            return RemoveSawsdlModelReferenceNode(cytoscapeObject, wsdlOperation.Id, wsdlOperation.WsdlOperationName, wsdlOperation.SawsdlModelReferences, GraphNodeTypeEnum.Operation, idOntologyTerms, false, false, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdComplexElement"></param>
        /// <param name="idOntologyTerms">todo: describe idOntologyTerms parameter on RemoveModelReferencesNodesFromXsdComplexElement</param>
        /// <returns></returns>
        public IGraphDataObject RemoveModelReferencesNodesFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement, IEnumerable<int> idOntologyTerms)
        {
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);
            var hasIssues = HasIssues(xsdComplexElement);

            return RemoveSawsdlModelReferenceNode(cytoscapeObject, xsdComplexElement.Id, xsdComplexElement.XsdComplexTypeName, xsdComplexElement.SawsdlModelReferences, GraphNodeTypeEnum.ComplexType, idOntologyTerms, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdSimpleType"></param>
        /// <param name="idOntologyTerms">todo: describe idOntologyTerms parameter on RemoveModelReferencesNodesFromXsdComplexElement</param>
        /// <returns></returns>
        public IGraphDataObject RemoveModelReferencesNodesFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType, IEnumerable<int> idOntologyTerms)
        {
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdSimpleType);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdSimpleType);
            var hasIssues = HasIssues(xsdSimpleType);

            return RemoveSawsdlModelReferenceNode(cytoscapeObject, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName, xsdSimpleType.SawsdlModelReferences, GraphNodeTypeEnum.SimpleType, idOntologyTerms, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);
        }

        #endregion Remove Model Reference

        #endregion Model Reference Nodes

        #region Schema Mapping

        #region Add Schema Mapping

        #region Add Lifting Schema Mapping

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdComplexElement">todo: describe xsdElement parameter on AddLiftingSchemaMappingToXsdElement</param>
        /// <returns></returns>
        public IGraphDataObject AddLiftingSchemaMappingToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement)
        {
            var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);
            var hasIssues = HasIssues(xsdComplexElement);

            return AddSawsdlLiftingSchemaMappingToNodeInGraphDataObject(cytoscapeObject, xsdComplexElement.Id, GraphNodeTypeEnum.ComplexType, hasModelReference, hasLoweringSchemaMapping, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdSimpleType"></param>
        /// <returns></returns>
        public IGraphDataObject AddLiftingSchemaMappingToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType)
        {
            var hasModelReference = HasSawsdlModelReference(xsdSimpleType);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdSimpleType);
            var hasIssues = HasIssues(xsdSimpleType);

            return AddSawsdlLiftingSchemaMappingToNodeInGraphDataObject(cytoscapeObject, xsdSimpleType.Id, GraphNodeTypeEnum.SimpleType, hasModelReference, hasLoweringSchemaMapping, hasIssues);
        }

        #endregion Add Lifting Schema Mapping

        #region Add Lowering Schema Mapping

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdComplexElement"></param>
        /// <returns></returns>
        public IGraphDataObject AddLoweringSchemaMappingToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement)
        {
            var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            var hasIssues = HasIssues(xsdComplexElement);

            return AddSawsdlLoweringSchemaMappingToNodeInGraphDataObject(cytoscapeObject, xsdComplexElement.Id, GraphNodeTypeEnum.ComplexType, hasModelReference, hasLiftingSchemaMapping, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdSimpleType"></param>
        /// <returns></returns>
        public IGraphDataObject AddLoweringSchemaMappingToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType)
        {
            var hasModelReference = HasSawsdlModelReference(xsdSimpleType);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdSimpleType);
            var hasIssues = HasIssues(xsdSimpleType);

            return AddSawsdlLoweringSchemaMappingToNodeInGraphDataObject(cytoscapeObject, xsdSimpleType.Id, GraphNodeTypeEnum.SimpleType, hasModelReference, hasLiftingSchemaMapping, hasIssues);
        }

        #endregion Add Lowering Schema Mapping

        #endregion Add Schema Mapping

        #region Remove Schema Mapping

        #region Remove Lifting Schema Mapping

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdComplexElement">todo: describe xsdElement parameter on RemoveLiftingSchemaMappingFromXsdComplexElement</param>
        /// <returns></returns>
        public IGraphDataObject RemoveLiftingSchemaMappingFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement)
        {
            var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);
            var hasIssues = HasIssues(xsdComplexElement);

            return RemoveSawsdlLiftingSchemaMappingFromNodeInGraphDataObject(cytoscapeObject, xsdComplexElement.Id, GraphNodeTypeEnum.ComplexType, hasModelReference, hasLoweringSchemaMapping, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdSmpleElement">todo: describe xsdElement parameter on AddLiftingSchemaMappingToXsdElement</param>
        /// <returns></returns>
        public IGraphDataObject RemoveLiftingSchemaMappingFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSmpleElement)
        {
            var hasModelReference = HasSawsdlModelReference(xsdSmpleElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdSmpleElement);
            var hasIssues = HasIssues(xsdSmpleElement);

            return RemoveSawsdlLiftingSchemaMappingFromNodeInGraphDataObject(cytoscapeObject, xsdSmpleElement.Id, GraphNodeTypeEnum.SimpleType, hasModelReference, hasLoweringSchemaMapping, hasIssues);
        }

        #endregion Remove Lifting Schema Mapping

        #region Remove Lowering Schema Mapping

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdComplexElement">todo: describe xsdElement parameter on RemoveLoweringSchemaMappingFromXsdComplexElement</param>
        /// <returns></returns>
        public IGraphDataObject RemoveLoweringSchemaMappingFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement)
        {
            var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            var hasIssues = HasIssues(xsdComplexElement);

            return RemoveSawsdlLoweringSchemaMappingFromNodeInGraphDataObject(cytoscapeObject, xsdComplexElement.Id, GraphNodeTypeEnum.ComplexType, hasModelReference, hasLiftingSchemaMapping, hasIssues);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="xsdSimpleType">todo: describe xsdElement parameter on AddLiftingSchemaMappingToXsdElement</param>
        /// <returns></returns>
        public IGraphDataObject RemoveLoweringSchemaMappingFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType)
        {
            var hasModelReference = HasSawsdlModelReference(xsdSimpleType);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdSimpleType);
            var hasIssues = HasIssues(xsdSimpleType);

            return RemoveSawsdlLoweringSchemaMappingFromNodeInGraphDataObject(cytoscapeObject, xsdSimpleType.Id, GraphNodeTypeEnum.SimpleType, hasModelReference, hasLiftingSchemaMapping, hasIssues);
        }

        #endregion Remove Lowering Schema Mapping

        #endregion Remove Schema Mapping

        #endregion Schema Mapping

        #region Issue

        #region Add issue

        public IGraphDataObject AddIssueToWsdlFault(IGraphDataObject cytoscapeObject, WsdlFault wsdlFault)
        {
            const bool hasIssues = true;
            var hasModelReference = HasSawsdlModelReference(wsdlFault);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == wsdlFault.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.Interface);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Interface);
            var nodeTypeString = GraphNodeTypeEnum.Interface.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject AddIssueToWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface)
        {
            const bool hasIssues = true;
            var hasModelReference = HasSawsdlModelReference(wsdlInterface);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == wsdlInterface.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.Interface);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Interface);
            var nodeTypeString = GraphNodeTypeEnum.Interface.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject AddIssueToWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdlOperation)
        {
            const bool hasIssues = true;
            var hasModelReference = HasSawsdlModelReference(wsdlOperation);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == wsdlOperation.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.Operation);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Operation);
            var nodeTypeString = GraphNodeTypeEnum.Operation.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject AddIssueToXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement)
        {
            const bool hasIssues = true;
            var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == xsdComplexElement.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.ComplexType);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.ComplexType);
            var nodeTypeString = GraphNodeTypeEnum.ComplexType.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject AddIssueToXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType)
        {
            const bool hasIssues = true;
            var hasModelReference = HasSawsdlModelReference(xsdSimpleType);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdSimpleType);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdSimpleType);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == xsdSimpleType.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.SimpleType);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.SimpleType);
            var nodeTypeString = GraphNodeTypeEnum.SimpleType.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);

            return cytoscapeObject;
        }

        #endregion Add issue

        #region Remove issue

        public IGraphDataObject RemoveIssueFromWsdlFault(IGraphDataObject cytoscapeObject, WsdlFault wsdlFault)
        {
            const bool hasIssues = false;
            var hasModelReference = HasSawsdlModelReference(wsdlFault);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == wsdlFault.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.Fault);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Interface);
            var nodeTypeString = GraphNodeTypeEnum.Fault.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject RemoveIssueFromWsdlInterface(IGraphDataObject cytoscapeObject, WsdlInterface wsdlInterface)
        {
            const bool hasIssues = false;
            var hasModelReference = HasSawsdlModelReference(wsdlInterface);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == wsdlInterface.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.Interface);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Interface);
            var nodeTypeString = GraphNodeTypeEnum.Interface.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject RemoveIssueFromWsdlOperation(IGraphDataObject cytoscapeObject, WsdlOperation wsdlOperation)
        {
            const bool hasIssues = false;
            var hasModelReference = HasSawsdlModelReference(wsdlOperation);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == wsdlOperation.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.Operation);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Operation);
            var nodeTypeString = GraphNodeTypeEnum.Operation.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject RemoveIssueFromXsdComplexType(IGraphDataObject cytoscapeObject, XsdComplexType xsdComplexElement)
        {
            const bool hasIssues = false;
            var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == xsdComplexElement.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.ComplexType);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.ComplexType);
            var nodeTypeString = GraphNodeTypeEnum.ComplexType.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);

            return cytoscapeObject;
        }

        public IGraphDataObject RemoveIssueFromXsdSimpleType(IGraphDataObject cytoscapeObject, XsdSimpleType xsdSimpleType)
        {
            const bool hasIssues = false;
            var hasModelReference = HasSawsdlModelReference(xsdSimpleType);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdSimpleType);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdSimpleType);

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == xsdSimpleType.Id && x.Data.NodeTypeEnum == GraphNodeTypeEnum.SimpleType);
            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.SimpleType);
            var nodeTypeString = GraphNodeTypeEnum.SimpleType.GetEnumDescription();

            node.Classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);

            return cytoscapeObject;
        }

        #endregion Remove issue

        #endregion Issue

        #region Graph Data

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="wsdlInterfaces"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on CreateGraphData</param>
        /// <returns></returns>
        public IGraphDataObject CreateGraphData(int idServiceDescription, string serviceName, ICollection<WsdlInterface> wsdlInterfaces)
        {
            var cytoscapeObject = new CytoscapeObject
            {
                Nodes = new List<CytoscapeNode>().Cast<IGraphNode>().ToList(),
                Edges = new List<CytoscapeEdge>().Cast<IGraphEdge>().ToList()
            };

            var serviceNode = CytoscapeNode.Factory.CreateServiceNode(serviceName);

            cytoscapeObject.Nodes.Add(serviceNode);

            FillGraphWithWsdlInterfaceNodes(cytoscapeObject, serviceNode, wsdlInterfaces, idServiceDescription);

            return cytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="sourceName"></param>
        /// <param name="graphNodeTypeEnum">todo: describe graphNodeTypeEnum parameter on GetExistingEdge</param>
        /// <param name="existingEdges">todo: describe edges parameter on GetExistingEdge</param>
        /// <param name="idOntologyTerm">todo: describe idOntologyTerm parameter on GetExistingEdge</param>
        /// <returns></returns>
        public IGraphEdge GetExistingEdge(ICollection<IGraphEdge> existingEdges, int idOntologyTerm, int sourceId, string sourceName, GraphNodeTypeEnum graphNodeTypeEnum)
        {
            //var nodeType = GetNodeTypeString(graphNodeTypeEnum);
            var nodeType = graphNodeTypeEnum.GetEnumDescription();

            var existingEdge = existingEdges.FirstOrDefault(x => x.Data.Source == $"{nodeType}-{sourceId}-{sourceName}" && x.Data.Target == $"ontology-term-{idOntologyTerm}");

            return existingEdge;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="existingEdges"></param>
        /// <param name="idOntologyTerm"></param>
        /// <returns></returns>
        public ICollection<IGraphEdge> GetExistingEdgesForOntologyTermNode(ICollection<IGraphEdge> existingEdges, int idOntologyTerm)
        {
            var existingEdge = existingEdges.Where(x => x.Data.Target == $"ontology-term-{idOntologyTerm}");

            return existingEdge.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="ontologyTerm1"></param>
        /// <param name="ontologyTerm2"></param>
        /// <returns></returns>
        public bool IsEdgeAlreadyBetweenOntologyTerms(IGraphDataObject cytoscapeObject, OntologyTerm ontologyTerm1, OntologyTerm ontologyTerm2)
        {
            var isEdgeAlreadyBetweenOntologyTerms = cytoscapeObject.Edges.Any(x =>
                (x.Data.Source == $"ontology-term-{ontologyTerm1.Id}" && x.Data.Target == $"ontology-term-{ontologyTerm2.Id}")
                || (x.Data.Source == $"ontology-term-{ontologyTerm2.Id}" && x.Data.Target == $"ontology-term-{ontologyTerm1.Id}"));

            return isEdgeAlreadyBetweenOntologyTerms;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphDataObject"></param>
        /// <returns></returns>
        public IGraphDataObject RemoveIntermediateOntologyTermsFromGraph(IGraphDataObject graphDataObject)
        {
            var edgesToRemove = new List<IGraphEdge>();
            var nodesToRemove = new List<IGraphNode>();

            var ontologyTermEdges = graphDataObject.Edges.Where(x => x.Data.Source.Contains("ontology-term-") && x.Data.Target.Contains("ontology-term-"));

            edgesToRemove.AddRange(ontologyTermEdges);

            var ontologyTermNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.OntologyTerm);

            nodesToRemove.AddRange(ontologyTermNodes);

            foreach (var edgeToRemove in edgesToRemove)
            {
                graphDataObject.Edges.Remove(edgeToRemove);
            }

            foreach (var nodeToRemove in nodesToRemove)
            {
                graphDataObject.Nodes.Remove(nodeToRemove);
            }

            return graphDataObject;
        }

        #endregion Graph Data

        #region Graph Styles

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ICollection<IGraphStyleObject> CreateGraphStyles()
        {
            ICollection<CytoscapeStyleObject> graphStyles = new List<CytoscapeStyleObject>();

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = "node",
                Style = new CytoscapeStyle
                {
                    Content = "data(label)",
                    Color = ColorHelper.ToHex(Color.Black),
                    Text_V_Align = "center",
                    Text_H_Align = "center",
                    Text_Outline_Width = 0.3F,
                    Text_Outline_Color = ColorHelper.ToHex(Color.White),
                    Font_Size = 3,
                    Width = 20,
                    Height = 20
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".node",
                Style = new CytoscapeStyle
                {
                    Width = 40,
                    Height = 20,
                    Shape = CytospaceNodeShapeEnum.Hexagon.GetEnumDescription(),
                    Border_Style = "solid",
                    Border_Width = 0.5F,
                    Border_Color = ColorHelper.ToHex(Color.Black),
                    Border_Opacity = 1,
                    Background_Color = ColorHelper.ToHex(Color.Black),
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".group",
                Style = new CytoscapeStyle
                {
                    Text_V_Align = "top",
                    Text_H_Align = "center",
                    Text_Margin_Y = -5,
                    Font_Size = 10
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".node-interface",
                Style = new CytoscapeStyle
                {
                    Background_Opacity = 0.7F
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".node-operation",
                Style = new CytoscapeStyle
                {
                    Background_Opacity = 0.5F
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".node-input",
                Style = new CytoscapeStyle
                {
                    Background_Opacity = 0.3F
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".node-input-element",
                Style = new CytoscapeStyle
                {
                    Background_Opacity = 0.1F
                }
            });
            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".node-output",
                Style = new CytoscapeStyle
                {
                    Background_Opacity = 0.3F
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".node-output-element",
                Style = new CytoscapeStyle
                {
                    Background_Opacity = 0.1F
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = "edge",
                Style = new CytoscapeStyle
                {
                    Width = 1
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = "edge.bezier",
                Style = new CytoscapeStyle
                {
                    Curve_Style = "bezier",
                    Line_Color = ColorHelper.ToHex(Color.Black),
                    Target_Arrow_Color = ColorHelper.ToHex(Color.Black),
                    Target_Arrow_Shape = CytospaceNodeShapeEnum.Triangle.GetEnumDescription()
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = "edge.unbundled-bezier",
                Style = new CytoscapeStyle
                {
                    Curve_Style = "unbundled-bezier",
                    Line_Color = ColorHelper.ToHex(Color.Blue),
                    Target_Arrow_Color = ColorHelper.ToHex(Color.Blue),
                    Target_Arrow_Shape = CytospaceNodeShapeEnum.Triangle.GetEnumDescription(),
                    Line_Style = "dashed"
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = "edge.multi-unbundled-bezier",
                Style = new CytoscapeStyle
                {
                    Curve_Style = "unbundled-bezier",
                    Control_Point_Distances = "-30 20",
                    Control_Point_Weights = "0.25 0.75",
                    Line_Color = ColorHelper.ToHex(Color.Blue),
                    Line_Style = "dotted"
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".multiline-manual",
                Style = new CytoscapeStyle
                {
                    Text_Wrap = "wrap"
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ".ontology-node",
                Style = new CytoscapeStyle
                {
                    Shape = "circle",
                    Background_Opacity = 0.5F,
                    Background_Color = ColorHelper.ToHex(Color.Black),
                    Border_Style = "solid",
                    Border_Width = 2,
                    Border_Color = ColorHelper.ToHex(Color.Black),
                    Border_Opacity = 0.5F
                }
            });

            graphStyles.Add(new CytoscapeStyleObject
            {
                Selector = ":parent",
                Style = new CytoscapeStyle
                {
                    Background_Color = ColorHelper.ToHex(Color.White)
                }
            });

            return graphStyles.Cast<IGraphStyleObject>().ToList();
        }

        #endregion Graph Styles

        #endregion Public methods

        #region Private methods

        #region Create Node Classes

        /// <summary>
        ///
        /// </summary>
        /// <param name="hasModelReference"></param>
        /// <param name="wsdlElementType"></param>
        /// <param name="hasIssues"></param>
        /// <returns></returns>
        private static string CreateNodeClasses(string wsdlElementType, bool hasModelReference, bool hasIssues)
        {
            var classes = "multiline-manual node";

            if (hasIssues)
            {
                classes += " node-with-issues";
            }

            if (hasModelReference)
            {
                classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-model-reference node-model-reference-only-with-model-reference-annotated";
            }
            else
            {
                classes += $" node-{wsdlElementType} node-model-reference-only";
            }

            return classes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlElementType"></param>
        /// <param name="hasIssues"></param>
        /// <returns></returns>
        private static string CreateNodeClasses(string wsdlElementType, bool hasIssues)
        {
            var classes = "multiline-manual node";

            if (hasIssues)
            {
                classes += " node-with-issues";
            }

            classes += $" node-{wsdlElementType}";

            return classes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlElementType"></param>
        /// <param name="hasModelReference"></param>
        /// <param name="hasLiftingSchemaMapping"></param>
        /// <param name="hasLoweringSchemaMapping"></param>
        /// <param name="hasIssues"></param>
        /// <returns></returns>
        private static string CreateNodeClasses(string wsdlElementType, bool hasModelReference, bool hasLiftingSchemaMapping, bool hasLoweringSchemaMapping, bool hasIssues)
        {
            var classes = "multiline-manual node";

            if (hasIssues)
            {
                classes += " node-with-issues";
            }

            if (!hasModelReference && !hasLiftingSchemaMapping && !hasLoweringSchemaMapping)
            {
                classes += $" node-{wsdlElementType} node-model-reference-and-schema-mapping";
            }
            else
            {
                //Has model-reference
                if (hasModelReference && !hasLiftingSchemaMapping && !hasLoweringSchemaMapping)
                {
                    classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-model-reference node-model-reference-and-schema-mapping-with-model-reference-annotated";
                }
                //Has model-reference + lifting-schema-mapping
                else if (hasModelReference && hasLiftingSchemaMapping && !hasLoweringSchemaMapping)
                {
                    classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-model-reference node-{wsdlElementType}-schema-mapping node-model-reference-and-schema-mapping-with-lifting-and-model-reference-annotated";
                }
                //Has model-reference + lowering-schema-mapping
                else if (hasModelReference && !hasLiftingSchemaMapping && hasLoweringSchemaMapping)
                {
                    classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-model-reference node-{wsdlElementType}-schema-mapping node-model-reference-and-schema-mapping-with-lowering-and-model-reference-annotated";
                }
                //Has lifting-schema-mapping
                else if (!hasModelReference && hasLiftingSchemaMapping && !hasLoweringSchemaMapping)
                {
                    classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-schema-mapping node-model-reference-and-schema-mapping-with-lifting-annotated";
                }
                //Has lowering-schema-mapping
                else if (!hasModelReference && !hasLiftingSchemaMapping && hasLoweringSchemaMapping)
                {
                    classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-schema-mapping node-model-reference-and-schema-mapping-with-lowering-annotated";
                }
                //Has lifting-schema-mapping + lowering-schema-mapping
                else if (!hasModelReference && hasLiftingSchemaMapping && hasLoweringSchemaMapping)
                {
                    classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-schema-mapping node-model-reference-and-schema-mapping-with-lifting-and-lowering-annotated";
                }
                //Has model-reference + lifting-schema-mapping + lowering-schema-mapping
                else if (hasModelReference && hasLiftingSchemaMapping && hasLoweringSchemaMapping)
                {
                    classes += $" node-{wsdlElementType}-annotated node-{wsdlElementType}-model-reference node-{wsdlElementType}-schema-mapping node-model-reference-and-schema-mapping-with-lifting-and-lowering-and-model-reference-annotated";
                }
            }

            return classes;
        }

        #endregion Create Node Classes

        #region Create Graph Edge Objects

        /// <summary>
        ///
        /// </summary>
        /// <param name="sourceType">todo: describe sourceType parameter on CreateEdge</param>
        /// <param name="sourceId">todo: describe sourceId parameter on CreateEdge</param>
        /// <param name="sourceName">todo: describe sourceName parameter on CreateEdge</param>
        /// <param name="targetType">todo: describe targetType parameter on CreateEdge</param>
        /// <param name="targetId">todo: describe targetId parameter on CreateEdge</param>
        /// <param name="targetName">todo: describe targetName parameter on CreateEdge</param>
        /// <returns></returns>
        private static CytoscapeEdge CreateEdge(GraphNodeTypeEnum sourceType, int sourceId, string sourceName, GraphNodeTypeEnum targetType, int targetId, string targetName)
        {
            return new CytoscapeEdge
            {
                Data = new CytoscapeEdgeData
                {
                    Source = $"{sourceType.GetEnumDescription()}-{sourceId}-{sourceName}",
                    Target = $"{targetType.GetEnumDescription()}-{targetId}-{targetName}"
                },
                Classes = "edge-service-description-nodes"
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="childClass"></param>
        /// <param name="parentClass"></param>
        /// <returns></returns>
        private static CytoscapeEdge CreateEdgeForIntermediateOntologyTerm(OntologyTerm childClass, OntologyTerm parentClass)
        {
            return new CytoscapeEdge
            {
                Data = new CytoscapeEdgeData
                {
                    Source = $"ontology-term-{childClass.Id}",
                    Target = $"ontology-term-{parentClass.Id}"
                },
                Classes = "edge-intermediate-ontology-term-nodes"
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelReference"></param>
        /// <param name="obj"></param>
        /// <param name="sourceId">todo: describe sourceId parameter on CreateEdgeForSawsdlModelReferenceNode</param>
        /// <param name="sourceName">todo: describe sourceName parameter on CreateEdgeForSawsdlModelReferenceNode</param>
        /// <param name="graphNodeTypeEnum">todo: describe graphNodeTypeEnum parameter on CreateEdgeForSawsdlModelReferenceNode</param>
        /// <returns></returns>
        private static CytoscapeEdge CreateEdgeForSawsdlModelReferenceNode(SawsdlModelReference modelReference, int sourceId, string sourceName, GraphNodeTypeEnum graphNodeTypeEnum)
        {
            //var nodeType = GetNodeTypeString(graphNodeTypeEnum);

            var nodeType = graphNodeTypeEnum.GetEnumDescription();

            return new CytoscapeEdge
            {
                Data = new CytoscapeEdgeData
                {
                    Source = $"{nodeType}-{sourceId}-{sourceName}",
                    Target = $"ontology-term-{modelReference.IdOntologyTerm}",
                    Label = "Model Reference"
                },
                Classes = "edge-model-reference-nodes"
            };
        }

        #endregion Create Graph Edge Objects

        #region Create Graph Node Objects

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="idElement"></param>
        /// <param name="elementType"></param>
        /// <param name="hasModelReference"></param>
        /// <param name="hasLoweringSchemaMapping"></param>
        /// <param name="hasIssues">todo: describe hasIssues parameter on AddSawsdlLiftingSchemaMappingToNodeInGraphDataObject</param>
        /// <returns></returns>
        private static IGraphDataObject AddSawsdlLiftingSchemaMappingToNodeInGraphDataObject(IGraphDataObject cytoscapeObject, int idElement, GraphNodeTypeEnum elementType, bool hasModelReference, bool hasLoweringSchemaMapping, bool hasIssues)
        {
            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == idElement && x.Data.NodeTypeEnum == elementType);
            //var nodeTypeString = GetNodeTypeString(elementType);
            var nodeTypeString = elementType.GetEnumDescription();

            var nodeClasses = CreateNodeClasses(nodeTypeString, hasModelReference, true, hasLoweringSchemaMapping, hasIssues);

            node.Classes = nodeClasses;

            return cytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="idElement"></param>
        /// <param name="elementType"></param>
        /// <param name="hasModelReference"></param>
        /// <param name="hasLiftingSchemaMapping"></param>
        /// <param name="hasIssues">todo: describe hasIssues parameter on AddSawsdlLoweringSchemaMappingToNodeInGraphDataObject</param>
        /// <returns></returns>
        private static IGraphDataObject AddSawsdlLoweringSchemaMappingToNodeInGraphDataObject(IGraphDataObject cytoscapeObject, int idElement, GraphNodeTypeEnum elementType, bool hasModelReference, bool hasLiftingSchemaMapping, bool hasIssues)
        {
            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == idElement && x.Data.NodeTypeEnum == elementType);
            //var nodeTypeString = GetNodeTypeString(elementType);
            var nodeTypeString = elementType.GetEnumDescription();

            var nodeClasses = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, true, hasIssues);

            node.Classes = nodeClasses;

            return cytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="idElement"></param>
        /// <param name="elementName"></param>
        /// <param name="elementType"></param>
        /// <param name="modelReferences"></param>
        /// <param name="hasLiftingSchemaMapping">todo: describe hasLiftingSchemaMapping parameter on CreateSawsdlModelReferenceGraphDataObject</param>
        /// <param name="hasLoweringSchemaMapping">todo: describe hasLoweringSchemaMapping parameter on CreateSawsdlModelReferenceGraphDataObject</param>
        /// <param name="hasIssues">todo: describe hasIssues parameter on CreateSawsdlModelReferenceGraphDataObject</param>
        /// <returns></returns>
        private static IGraphDataObject CreateSawsdlModelReferenceGraphDataObject(IGraphDataObject cytoscapeObject, int idElement, string elementName, GraphNodeTypeEnum elementType, IEnumerable<SawsdlModelReference> modelReferences, bool hasLiftingSchemaMapping, bool hasLoweringSchemaMapping, bool hasIssues)
        {
            var parentNode = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Document);

            IGraphNode modelReferenceNode;

            foreach (var modelReference in modelReferences)
            {
                var existingNode = GetExistingOntologyTermNode(cytoscapeObject, modelReference.IdOntologyTerm);

                if (existingNode == null)
                {
                    modelReferenceNode = CreateSawsdlModelReferenceNode(parentNode, "ontology-node", modelReference);

                    cytoscapeObject.Nodes.Add(modelReferenceNode);
                }
                else
                {
                    if (existingNode.Data.NodeTypeEnum == GraphNodeTypeEnum.OntologyTerm)
                    {
                        existingNode.Data.NodeTypeEnum = GraphNodeTypeEnum.ModelReference;
                    }
                }

                var existingEdge = GetExistingEdge(cytoscapeObject.Edges, modelReference, idElement, elementName, elementType);

                if (existingEdge == null)
                {
                    cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, idElement, elementName, elementType));
                }
            }

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == idElement && x.Data.NodeTypeEnum == elementType);
            //var nodeTypeString = GetNodeTypeString(elementType);
            var nodeTypeString = elementType.GetEnumDescription();
            string nodeClasses;

            if (elementType != GraphNodeTypeEnum.ComplexType && elementType != GraphNodeTypeEnum.SimpleType)
            {
                nodeClasses = CreateNodeClasses(nodeTypeString, true, hasIssues);
            }
            else
            {
                nodeClasses = CreateNodeClasses(nodeTypeString, true, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);
            }

            node.Classes = nodeClasses;

            return cytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelReference"></param>
        /// <param name="parentNode">todo: describe parentNode parameter on CreateSawsdlOntologyTermNode</param>
        /// <param name="classes">todo: describe classes parameter on CreateSawsdlOntologyTermNode</param>
        /// <returns></returns>
        private static IGraphNode CreateSawsdlModelReferenceNode(IGraphNode parentNode, string classes, SawsdlModelReference modelReference)
        {
            var modelReferenceTerm = modelReference.OntologyTerm.TermName;
            //var modelReferenceTerm = UrlHelper.ExtractTermIdentifierFromUrl(modelReference.ModelReference);
            //var ontologyName = UrlHelper.ExtractOntologyNameFromUrl(modelReference.ModelReference);

            return new CytoscapeNode
            {
                Data = new CytoscapeNodeData
                {
                    Id = $"ontology-term-{modelReference.IdOntologyTerm}",
                    Label = modelReferenceTerm,
                    Name = $"ontology-term-{modelReference.IdOntologyTerm}",
                    NodeTypeEnum = GraphNodeTypeEnum.ModelReference,
                    Parent = parentNode.Data.Id,
                    TermUri = modelReference.ModelReference,
                    IdOntologyTerm = modelReference.IdOntologyTerm,
                    IdOntology = modelReference.OntologyTerm.IdOntology,
                    OntologyName = modelReference.OntologyTerm.Ontology.OntologyName
                },
                Classes = classes
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="classes"></param>
        /// <param name="ontologyTerm"></param>
        /// <returns></returns>
        private static IGraphNode CreateSawsdlOntologyTermNode(IGraphNode parentNode, string classes, OntologyTerm ontologyTerm)
        {
            var termName = UrlHelper.ExtractTermIdentifierFromUrl(ontologyTerm.TermUri);

            return new CytoscapeNode
            {
                Data = new CytoscapeNodeData
                {
                    Id = $"ontology-term-{ontologyTerm.Id}",
                    Label = termName,
                    Name = $"ontology-term-{ontologyTerm.Id}",
                    NodeTypeEnum = GraphNodeTypeEnum.OntologyTerm,
                    Parent = parentNode.Data.Id,
                    TermUri = ontologyTerm.TermUri,
                    IdOntologyTerm = ontologyTerm.Id,
                    IdOntology = ontologyTerm.IdOntology,
                    OntologyName = ontologyTerm.Ontology.OntologyName
                },
                Classes = classes
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject">todo: describe cytoscapeObject parameter on GetExistingOntologyTermNode</param>
        /// <param name="idOnntologyTerm">todo: describe idOnntologyTerm parameter on GetExistingOntologyTermNode</param>
        /// <returns></returns>
        private static IGraphNode GetExistingOntologyTermNode(IGraphDataObject cytoscapeObject, int idOnntologyTerm)
        {
            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.Id == $"ontology-term-{idOnntologyTerm}");

            return node;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="idElement"></param>
        /// <param name="elementType"></param>
        /// <param name="hasModelReference"></param>
        /// <param name="hasLoweringSchemaMapping"></param>
        /// <param name="hasIssues">todo: describe hasIssues parameter on RemoveSawsdlLiftingSchemaMappingFromNodeInGraphDataObject</param>
        /// <returns></returns>
        private static IGraphDataObject RemoveSawsdlLiftingSchemaMappingFromNodeInGraphDataObject(IGraphDataObject cytoscapeObject, int idElement, GraphNodeTypeEnum elementType, bool hasModelReference, bool hasLoweringSchemaMapping, bool hasIssues)
        {
            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == idElement && x.Data.NodeTypeEnum == elementType);
            //var nodeTypeString = GetNodeTypeString(elementType);
            var nodeTypeString = elementType.GetEnumDescription();

            var nodeClasses = CreateNodeClasses(nodeTypeString, hasModelReference, false, hasLoweringSchemaMapping, hasIssues);

            node.Classes = nodeClasses;

            return cytoscapeObject;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="idElement"></param>
        /// <param name="elementType"></param>
        /// <param name="hasModelReference"></param>
        /// <param name="hasLiftingSchemaMapping"></param>
        /// <param name="hasIssues">todo: describe hasIssues parameter on RemoveSawsdlLoweringSchemaMappingFromNodeInGraphDataObject</param>
        /// <returns></returns>
        private static IGraphDataObject RemoveSawsdlLoweringSchemaMappingFromNodeInGraphDataObject(IGraphDataObject cytoscapeObject, int idElement, GraphNodeTypeEnum elementType, bool hasModelReference, bool hasLiftingSchemaMapping, bool hasIssues)
        {
            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == idElement && x.Data.NodeTypeEnum == elementType);
            //var nodeTypeString = GetNodeTypeString(elementType);
            var nodeTypeString = elementType.GetEnumDescription();

            var nodeClasses = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, false, hasIssues);

            node.Classes = nodeClasses;

            return cytoscapeObject;
        }

        #endregion Create Graph Node Objects

        #region Get edges

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelReference"></param>
        /// <param name="sourceId"></param>
        /// <param name="sourceName"></param>
        /// <param name="graphNodeTypeEnum">todo: describe graphNodeTypeEnum parameter on GetExistingEdge</param>
        /// <param name="existingEdges">todo: describe edges parameter on GetExistingEdge</param>
        /// <returns></returns>
        private static IGraphEdge GetExistingEdge(ICollection<IGraphEdge> existingEdges, SawsdlModelReference modelReference, int sourceId, string sourceName, GraphNodeTypeEnum graphNodeTypeEnum)
        {
            //var nodeType = GetNodeTypeString(graphNodeTypeEnum);

            var nodeType = graphNodeTypeEnum.GetEnumDescription();

            existingEdges.FirstOrDefault(x => x.Data.Source == $"{nodeType}-{sourceId}-{sourceName}");

            var existingEdge = existingEdges.FirstOrDefault(x => x.Data.Source == $"{nodeType}-{sourceId}-{sourceName}" && x.Data.Target == $"ontology-term-{modelReference.IdOntologyTerm}");

            return existingEdge;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="existingEdges"></param>
        /// <param name="sourceType"></param>
        /// <param name="sourceId"></param>
        /// <param name="sourceName"></param>
        /// <param name="targetType"></param>
        /// <param name="targetId"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        private static IGraphEdge GetExistingEdge(ICollection<IGraphEdge> existingEdges, GraphNodeTypeEnum sourceType, int sourceId, string sourceName, GraphNodeTypeEnum targetType, int targetId, string targetName)
        {
            //var sourceNodeType = GetNodeTypeString(sourceType);
            //var targetNodeType = GetNodeTypeString(targetType);

            var existingEdge = existingEdges.FirstOrDefault(x => x.Data.Source == $"{sourceType.GetEnumDescription()}-{sourceId}-{sourceName}" && x.Data.Target == $"{targetType.GetEnumDescription()}-{targetId}-{targetName}");

            return existingEdge;
        }

        #endregion Get edges

        #region Remove Graph Node Objects

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="idElement"></param>
        /// <param name="elementName"></param>
        /// <param name="elementType"></param>
        /// <param name="modelReferences"></param>
        /// <param name="idOntologyTerms">todo: describe idOntologyTerms parameter on RemoveSawsdlModelReferenceNode</param>
        /// <param name="hasLiftingSchemaMapping">todo: describe hasLiftingSchemaMapping parameter on RemoveSawsdlModelReferenceNode</param>
        /// <param name="hasLoweringSchemaMapping">todo: describe hasLoweringSchemaMapping parameter on RemoveSawsdlModelReferenceNode</param>
        /// <param name="hasIssues">todo: describe hasIssues parameter on RemoveSawsdlModelReferenceNode</param>
        /// <returns></returns>
        private IGraphDataObject RemoveSawsdlModelReferenceNode(IGraphDataObject cytoscapeObject, int idElement, string elementName, IEnumerable<SawsdlModelReference> modelReferences, GraphNodeTypeEnum elementType, IEnumerable<int> idOntologyTerms, bool hasLiftingSchemaMapping, bool hasLoweringSchemaMapping, bool hasIssues)
        {
            //var nodeTypeString = GetNodeTypeString(elementType);
            var nodeType = elementType.GetEnumDescription();

            foreach (var idOntologyTerm in idOntologyTerms)
            {
                var existingNode = GetExistingOntologyTermNode(cytoscapeObject, idOntologyTerm);

                if (existingNode != null)
                {
                    var existingEdges = GetExistingEdgesForOntologyTermNode(cytoscapeObject.Edges, idOntologyTerm);

                    if (existingEdges != null && existingEdges.Any())
                    {
                        var edgeToRemove = existingEdges.FirstOrDefault(x => x.Data.Source == $"{nodeType}-{idElement}-{elementName}");
                        cytoscapeObject.Edges.Remove(edgeToRemove);

                        var thereAreOtherEdges = existingEdges.Any(x => x.Data.Source != $"{nodeType}-{idElement}-{elementName}");

                        if (!thereAreOtherEdges)
                        {
                            cytoscapeObject.Nodes.Remove(existingNode);
                        }
                    }
                }
            }

            var hasModelReference = modelReferences.Count() > 0;

            var node = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.IdWsdlElement == idElement && x.Data.NodeTypeEnum == elementType);

            string nodeClasses;

            if (elementType == GraphNodeTypeEnum.Interface || elementType == GraphNodeTypeEnum.Operation)
            {
                nodeClasses = CreateNodeClasses(nodeType, hasModelReference, hasIssues);
            }
            else
            {
                nodeClasses = CreateNodeClasses(nodeType, hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);
            }

            node.Classes = nodeClasses;

            return cytoscapeObject;
        }

        #endregion Remove Graph Node Objects

        #region Fill Graph Nodes

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="parentNodeId"></param>
        /// <param name="parentNodeText"></param>
        /// <param name="idServiceDescription"></param>
        /// <param name="xsdComplexElement"></param>
        private static void CreateXsdComplexTypeNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, GraphNodeTypeEnum parentType, int parentNodeId, string parentNodeText, int idServiceDescription, XsdComplexType xsdComplexElement)
        {
            var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);
            var hasIssues = HasIssues(xsdComplexElement);

            //var xsdComplexElementNodeName = $"{xsdComplexElement.XsdComplexTypeElementType}-{xsdComplexElement.XsdComplexTypeName}";
            var xsdComplexElementNodeName = xsdComplexElement.XsdComplexTypeName;
            string childXsdComplexElementNodeNameplexTypeNodeName;

            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.ComplexType);
            var nodeTypeString = GraphNodeTypeEnum.ComplexType.GetEnumDescription();

            var classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);

            var xsdComplexTypeNode = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.Id == $"{GraphNodeTypeEnum.ComplexType.GetEnumDescription()}-{xsdComplexElement.Id}-{parentNodeText}");

            if (xsdComplexTypeNode == null)
            {
                xsdComplexTypeNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.ComplexType, xsdComplexElement.Id, xsdComplexElementNodeName, idServiceDescription, groupNode.Data.Id, classes);

                cytoscapeObject.Nodes.Add(xsdComplexTypeNode);
            }

            var existingEdge = GetExistingEdge(cytoscapeObject.Edges, GraphNodeTypeEnum.ComplexType, parentNodeId, parentNodeText, GraphNodeTypeEnum.ComplexType, xsdComplexElement.Id, xsdComplexElementNodeName);

            if (existingEdge == null)
            {
                cytoscapeObject.Edges.Add(CreateEdge(parentType, parentNodeId, parentNodeText, GraphNodeTypeEnum.ComplexType, xsdComplexElement.Id, xsdComplexElementNodeName));
            }

            if (hasModelReference)
            {
                foreach (var modelReference in xsdComplexElement.SawsdlModelReferences)
                {
                    //var ontologyName = UrlHelper.Instance.ExtractOntologyNameFromUrl(modelReference.ModelReference);

                    cytoscapeObject.Nodes.Add(CreateSawsdlModelReferenceNode(groupNode, "ontology-node", modelReference));

                    cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, xsdComplexElement.Id, xsdComplexElementNodeName, GraphNodeTypeEnum.ComplexType));
                }
            }

            if (xsdComplexElement.ChildrenXsdComplexTypes != null && xsdComplexElement.ChildrenXsdComplexTypes.Any())
            {
                foreach (var child in xsdComplexElement.ChildrenXsdComplexTypes)
                {
                    if (child.XsdComplexTypeElementType == xsdComplexElement.XsdComplexTypeElementType)
                    {
                        //childXsdComplexElementNodeNameplexTypeNodeName = $"{child.XsdComplexTypeElementType}-{child.XsdComplexTypeName}";
                        childXsdComplexElementNodeNameplexTypeNodeName = child.XsdComplexTypeName;

                        existingEdge = GetExistingEdge(cytoscapeObject.Edges, GraphNodeTypeEnum.ComplexType, xsdComplexElement.Id, xsdComplexElementNodeName, GraphNodeTypeEnum.ComplexType, child.Id, childXsdComplexElementNodeNameplexTypeNodeName);

                        if (existingEdge == null)
                        {
                            cytoscapeObject.Edges.Add(CreateEdge(parentType, xsdComplexElement.Id, xsdComplexElementNodeName, GraphNodeTypeEnum.ComplexType, child.Id, childXsdComplexElementNodeNameplexTypeNodeName));
                        }
                    }
                    else
                    {
                        CreateXsdComplexTypeNodes(cytoscapeObject, groupNode, GraphNodeTypeEnum.ComplexType, xsdComplexElement.Id, xsdComplexElementNodeName, idServiceDescription, child);
                    }
                }
            }
        }

        private static void CreateXsdSimpleTypeNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, GraphNodeTypeEnum parentType, int parentNodeId, string parentNodeText, int idServiceDescription, XsdComplexType xsdComplexType, XsdSimpleType xsdSimpleType)
        {
            var hasModelReference = HasSawsdlModelReference(xsdSimpleType);
            var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdSimpleType);
            var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdSimpleType);
            var hasIssues = HasIssues(xsdSimpleType);

            //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.SimpleType);
            var nodeTypeString = GraphNodeTypeEnum.SimpleType.GetEnumDescription();

            //var complexTypeNodeName = $"{xsdComplexType.XsdComplexTypeElementType}-{xsdComplexType.XsdComplexTypeName}";
            var complexTypeNodeName = xsdComplexType.XsdComplexTypeName;
            //var simpleTypeNodeName = $"{xsdSimpleType.XsdSimpleTypeElementType}-{xsdSimpleType.XsdSimpleTypeName}";
            var simpleTypeNodeName = xsdSimpleType.XsdSimpleTypeName;

            var classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);

            var xsdSimpleTypeNode = cytoscapeObject.Nodes.FirstOrDefault(x => x.Data.Id == $"{GraphNodeTypeEnum.SimpleType.GetEnumDescription()}-{xsdSimpleType.Id}-{simpleTypeNodeName}");

            if (xsdSimpleTypeNode == null)
            {
                xsdSimpleTypeNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.SimpleType, xsdSimpleType.Id, simpleTypeNodeName, idServiceDescription, groupNode.Data.Id, classes);
                //CreateXsdSimpleTypeNode(groupNode, classes, xsdSimpleType, idServiceDescription);

                cytoscapeObject.Nodes.Add(xsdSimpleTypeNode);

                cytoscapeObject.Edges.Add(CreateEdge(parentType, xsdComplexType.Id, complexTypeNodeName, GraphNodeTypeEnum.SimpleType, xsdSimpleType.Id, simpleTypeNodeName));
            }

            if (hasModelReference)
            {
                foreach (var modelReference in xsdSimpleType.SawsdlModelReferences)
                {
                    //var ontologyName = UrlHelper.Instance.ExtractOntologyNameFromUrl(modelReference.ModelReference);

                    cytoscapeObject.Nodes.Add(CreateSawsdlModelReferenceNode(groupNode, "ontology-node", modelReference));

                    cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, xsdSimpleType.Id, simpleTypeNodeName, GraphNodeTypeEnum.SimpleType));
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlOperation"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillWsdlInfaultNodes</param>
        private static void FillGraphWithWsdlInfaultNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlOperation wsdlOperation, int idServiceDescription)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlOperation"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillWsdlInputNodes</param>
        private static void FillGraphWithWsdlInputNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlOperation wsdlOperation, int idServiceDescription)
        {
            //bool hasIssues;
            string classes;

            foreach (var wsdlInput in wsdlOperation.WsdlInputs)
            {
                //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Input);
                var nodeTypeString = GraphNodeTypeEnum.Input.GetEnumDescription();

                classes = CreateNodeClasses(nodeTypeString, hasIssues: false);

                var wsdlInputNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.Input, wsdlInput.Id, wsdlInput.WsdlInputName, idServiceDescription, groupNode.Data.Id, classes); ;
                //CreateWsdlInputNode(groupNode, classes, wsdlInput, idServiceDescription);

                cytoscapeObject.Nodes.Add(wsdlInputNode);

                cytoscapeObject.Edges.Add(CreateEdge(GraphNodeTypeEnum.Operation, wsdlOperation.Id, wsdlOperation.WsdlOperationName, GraphNodeTypeEnum.Input, wsdlInput.Id, wsdlInput.WsdlInputName));

                FillGraphWithXsdComplexElementNodeForWsdlInput(cytoscapeObject, groupNode, wsdlInput, idServiceDescription);

                FillGraphWithXsdSimpleTypeNodeForWsdlInput(cytoscapeObject, groupNode, wsdlInput, idServiceDescription);

                FillGraphWithXsdSimpleTypeNodeForXsdComplexTypes(cytoscapeObject, groupNode, wsdlInput.XsdComplexType, idServiceDescription);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlInterfaces"></param>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillWsdlInterfaceNodes</param>
        private static void FillGraphWithWsdlInterfaceNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, ICollection<WsdlInterface> wsdlInterfaces, int idServiceDescription)
        {
            bool hasModelReference;
            bool hasIssues;
            string classes;

            foreach (var wsdlInterface in wsdlInterfaces)
            {
                hasModelReference = HasSawsdlModelReference(wsdlInterface);
                hasIssues = HasIssues(wsdlInterface);

                //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Interface);
                var nodeTypeString = GraphNodeTypeEnum.Interface.GetEnumDescription();

                classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

                var wsdlInterfaceNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.Interface, wsdlInterface.Id, wsdlInterface.WsdlInterfaceName, idServiceDescription, groupNode.Data.Id, classes); ;
                //CreateWsdlInterfaceNode(groupNode, classes, wsdlInterface, idServiceDescription);

                cytoscapeObject.Nodes.Add(wsdlInterfaceNode);

                if (hasModelReference)
                {
                    foreach (var modelReference in wsdlInterface.SawsdlModelReferences)
                    {
                        var modelReferenceTerm = UrlHelper.ExtractTermIdentifierFromUrl(modelReference.ModelReference);

                        cytoscapeObject.Nodes.Add(CreateSawsdlModelReferenceNode(groupNode, "ontology-node", modelReference));

                        cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, wsdlInterface.Id, wsdlInterface.WsdlInterfaceName, GraphNodeTypeEnum.Interface));
                    }
                }

                FillGraphWithWsdlOperationNodes(cytoscapeObject, groupNode, wsdlInterface, idServiceDescription);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlInterface"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillWsdlOperationNodes</param>
        private static void FillGraphWithWsdlOperationNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlInterface wsdlInterface, int idServiceDescription)
        {
            bool hasModelReference;
            bool hasIssues;
            string classes;

            foreach (var wsdlOperation in wsdlInterface.WsdlOperations)
            {
                hasModelReference = HasSawsdlModelReference(wsdlOperation);
                hasIssues = HasIssues(wsdlInterface);

                //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Operation);
                var nodeTypeString = GraphNodeTypeEnum.Operation.GetEnumDescription();

                classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

                var wsdlOperationNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.Operation, wsdlOperation.Id, wsdlOperation.WsdlOperationName, idServiceDescription, groupNode.Data.Id, classes);
                //CreateWsdllOperationNode(groupNode, classes, wsdlOperation, idServiceDescription);

                cytoscapeObject.Nodes.Add(wsdlOperationNode);

                cytoscapeObject.Edges.Add(CreateEdge(GraphNodeTypeEnum.Interface, wsdlInterface.Id, wsdlInterface.WsdlInterfaceName, GraphNodeTypeEnum.Operation, wsdlOperation.Id, wsdlOperation.WsdlOperationName));

                if (hasModelReference)
                {
                    foreach (var modelReference in wsdlOperation.SawsdlModelReferences)
                    {
                        cytoscapeObject.Nodes.Add(CreateSawsdlModelReferenceNode(groupNode, "ontology-node", modelReference));

                        cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, wsdlOperation.Id, wsdlOperation.WsdlOperationName, GraphNodeTypeEnum.Operation));
                    }
                }

                FillGraphWithWsdlInputNodes(cytoscapeObject, groupNode, wsdlOperation, idServiceDescription);

                FillGraphWithWsdlOutputNodes(cytoscapeObject, groupNode, wsdlOperation, idServiceDescription);

                FillGraphWithWsdlInfaultNodes(cytoscapeObject, groupNode, wsdlOperation, idServiceDescription);

                FillGraphWithWsdlOutfaultNodes(cytoscapeObject, groupNode, wsdlOperation, idServiceDescription);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlOperation"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillWsdlOutfaultNodes</param>
        private static void FillGraphWithWsdlOutfaultNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlOperation wsdlOperation, int idServiceDescription)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlOperation"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillWsdlOutputNodes</param>
        private static void FillGraphWithWsdlOutputNodes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlOperation wsdlOperation, int idServiceDescription)
        {
            //bool hasIssues;
            string classes;

            foreach (var wsdlOutput in wsdlOperation.WsdlOutputs)
            {
                //hasIssues = HasIssues(wsdlOutput);

                //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.Output);
                var nodeTypeString = GraphNodeTypeEnum.Output.GetEnumDescription();

                classes = CreateNodeClasses(nodeTypeString, hasIssues: false);

                var wsdlOutputNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.Output, wsdlOutput.Id, wsdlOutput.WsdlOutputName, idServiceDescription, groupNode.Data.Id, classes);
                //CreateWsdlOutputNode(groupNode, classes, wsdlOutput, idServiceDescription);

                cytoscapeObject.Nodes.Add(wsdlOutputNode);

                cytoscapeObject.Edges.Add(CreateEdge(GraphNodeTypeEnum.Operation, wsdlOperation.Id, wsdlOperation.WsdlOperationName, GraphNodeTypeEnum.Output, wsdlOutput.Id, wsdlOutput.WsdlOutputName));

                FillGraphWithXsdComplexElementNodeForWsdlOutput(cytoscapeObject, groupNode, wsdlOutput, idServiceDescription);

                FillGraphWithXsdSimpleTypeNodeForWsdlOutput(cytoscapeObject, groupNode, wsdlOutput, idServiceDescription);

                FillGraphWithXsdSimpleTypeNodeForXsdComplexTypes(cytoscapeObject, groupNode, wsdlOutput.XsdComplexType, idServiceDescription);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlInput"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillXsdComplexElementNodeForWsdlInput</param>
        /// <param name="hasIssues">todo: describe hasIssues parameter on FillXsdComplexElementNodeForWsdlInput</param>
        private static void FillGraphWithXsdComplexElementNodeForWsdlInput(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlInput wsdlInput, int idServiceDescription)
        {
            if (wsdlInput.XsdComplexType != null)
            {
                var xsdComplexElement = wsdlInput.XsdComplexType;

                CreateXsdComplexTypeNodes(cytoscapeObject, groupNode, GraphNodeTypeEnum.Input, wsdlInput.Id, wsdlInput.WsdlInputName, idServiceDescription, xsdComplexElement);

                //TODO : Fill Simple Types
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlOutput"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillXsdComplexElementNodeForWsdlOutput</param>
        private static void FillGraphWithXsdComplexElementNodeForWsdlOutput(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlOutput wsdlOutput, int idServiceDescription)
        {
            if (wsdlOutput.XsdComplexType != null)
            {
                var xsdComplexElement = wsdlOutput.XsdComplexType;

                CreateXsdComplexTypeNodes(cytoscapeObject, groupNode, GraphNodeTypeEnum.Output, wsdlOutput.Id, wsdlOutput.WsdlOutputName, idServiceDescription, xsdComplexElement);

                //TODO : Fill Simple Types
            }

            //if (wsdlOutput.XsdComplexType != null)
            //{
            //    var xsdComplexElement = wsdlOutput.XsdComplexType;

            //    var hasModelReference = HasSawsdlModelReference(xsdComplexElement);
            //    var hasLiftingSchemaMapping = HasSawsdlLiftingSchemaMapping(xsdComplexElement);
            //    var hasLoweringSchemaMapping = HasSawsdlLoweringSchemaMapping(xsdComplexElement);
            //    var hasIssues = HasIssues(xsdComplexElement);

            //    var classes = CreateNodeClasses("complex-element", hasModelReference, hasLiftingSchemaMapping, hasLoweringSchemaMapping, hasIssues);

            //    var xsdComplexElementNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.ComplexType, xsdComplexElement.Id, xsdComplexElement.XsdComplexTypeName, idServiceDescription, groupNode.Data.Id, classes);
            //    //CreateXsdComplexElementNode(groupNode, classes, xsdComplexElement, idServiceDescription);

            //    cytoscapeObject.Nodes.Add(xsdComplexElementNode);

            //    cytoscapeObject.Edges.Add(CreateEdge("output", wsdlOutput.Id, wsdlOutput.WsdlOutputName, "complex-element", xsdComplexElement.Id, xsdComplexElement.XsdComplexTypeName));

            //    if (hasModelReference)
            //    {
            //        foreach (var modelReference in xsdComplexElement.SawsdlModelReferences)
            //        {
            //            //var ontologyName = UrlHelper.Instance.ExtractOntologyNameFromUrl(modelReference.ModelReference);

            //            cytoscapeObject.Nodes.Add(CreateSawsdlModelReferenceNode(groupNode, "ontology-node", modelReference));

            //            cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, xsdComplexElement.Id, xsdComplexElement.XsdComplexTypeName, GraphNodeTypeEnum.ComplexType));
            //        }
            //    }

            //    // TODO: Fill Simple Types
            //}
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlInput"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillxsdSimpleTypeNodeForWsdlInput</param>
        private static void FillGraphWithXsdSimpleTypeNodeForWsdlInput(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlInput wsdlInput, int idServiceDescription)
        {
            if (wsdlInput.XsdSimpleType != null)
            {
                var xsdSimpleType = wsdlInput.XsdSimpleType;

                bool hasModelReference;
                bool hasIssues;
                string classes;

                hasModelReference = HasSawsdlModelReference(xsdSimpleType);
                hasIssues = HasIssues(xsdSimpleType);

                //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.SimpleType);
                var nodeTypeString = GraphNodeTypeEnum.SimpleType.GetEnumDescription();

                classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

                var xsdSimpleTypeNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.SimpleType, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName, idServiceDescription, groupNode.Data.Id, classes);
                //CreateXsdSimpleTypeNode(groupNode, classes, xsdSimpleType, idServiceDescription);

                cytoscapeObject.Nodes.Add(xsdSimpleTypeNode);

                cytoscapeObject.Edges.Add(CreateEdge(GraphNodeTypeEnum.Input, wsdlInput.Id, wsdlInput.WsdlInputName, GraphNodeTypeEnum.SimpleType, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName));

                if (hasModelReference)
                {
                    foreach (var modelReference in xsdSimpleType.SawsdlModelReferences)
                    {
                        //var ontologyName = UrlHelper.Instance.ExtractOntologyNameFromUrl(modelReference.ModelReference);

                        cytoscapeObject.Nodes.Add(CreateSawsdlModelReferenceNode(groupNode, "ontology-node", modelReference));

                        cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName, GraphNodeTypeEnum.SimpleType));
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cytoscapeObject"></param>
        /// <param name="groupNode"></param>
        /// <param name="wsdlOutput"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on FillxsdSimpleTypeNodeForWsdlOutput</param>
        private static void FillGraphWithXsdSimpleTypeNodeForWsdlOutput(IGraphDataObject cytoscapeObject, IGraphNode groupNode, WsdlOutput wsdlOutput, int idServiceDescription)
        {
            if (wsdlOutput.XsdSimpleType != null)
            {
                var xsdSimpleType = wsdlOutput.XsdSimpleType;

                bool hasModelReference;
                bool hasIssues;
                string classes;

                hasModelReference = HasSawsdlModelReference(xsdSimpleType);
                hasIssues = HasIssues(xsdSimpleType);

                //var nodeTypeString = GetNodeTypeString(GraphNodeTypeEnum.SimpleType);
                var nodeTypeString = GraphNodeTypeEnum.SimpleType.GetEnumDescription();

                classes = CreateNodeClasses(nodeTypeString, hasModelReference, hasIssues);

                var xsdSimpleTypeNode = CytoscapeNode.Factory.Create(GraphNodeTypeEnum.SimpleType, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName, idServiceDescription, groupNode.Data.Id, classes);
                //CreateXsdSimpleTypeNode(groupNode, classes, xsdSimpleType, idServiceDescription);

                cytoscapeObject.Nodes.Add(xsdSimpleTypeNode);

                cytoscapeObject.Edges.Add(CreateEdge(GraphNodeTypeEnum.Output, wsdlOutput.Id, wsdlOutput.WsdlOutputName, GraphNodeTypeEnum.SimpleType, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName));

                if (hasModelReference)
                {
                    foreach (var modelReference in xsdSimpleType.SawsdlModelReferences)
                    {
                        //var ontologyName = UrlHelper.Instance.ExtractOntologyNameFromUrl(modelReference.ModelReference);

                        cytoscapeObject.Nodes.Add(CreateSawsdlModelReferenceNode(groupNode, "ontology-node", modelReference));

                        cytoscapeObject.Edges.Add(CreateEdgeForSawsdlModelReferenceNode(modelReference, xsdSimpleType.Id, xsdSimpleType.XsdSimpleTypeName, GraphNodeTypeEnum.SimpleType));
                    }
                }
            }
        }

        private static void FillGraphWithXsdSimpleTypeNodeForXsdComplexTypes(IGraphDataObject cytoscapeObject, IGraphNode groupNode, XsdComplexType xsdComplexType, int idServiceDescription)
        {
            if (xsdComplexType != null)
            {
                if (xsdComplexType.XsdSimpleTypes != null && xsdComplexType.XsdSimpleTypes.Any())
                {
                    string sourceName;

                    foreach (var xsdSimpleType in xsdComplexType.XsdSimpleTypes)
                    {
                        //sourceName = $"{xsdComplexType.XsdComplexTypeElementType}-{xsdComplexType.XsdComplexTypeName}";
                        sourceName = xsdComplexType.XsdComplexTypeName;

                        CreateXsdSimpleTypeNodes(cytoscapeObject, groupNode, GraphNodeTypeEnum.ComplexType, xsdComplexType.Id, sourceName, idServiceDescription, xsdComplexType, xsdSimpleType);
                    }
                }

                if (xsdComplexType.ChildrenXsdComplexTypes != null && xsdComplexType.ChildrenXsdComplexTypes.Any())
                {
                    foreach (var child in xsdComplexType.ChildrenXsdComplexTypes)
                    {
                        if (child.XsdComplexTypeElementType != xsdComplexType.XsdComplexTypeElementType)
                        {
                            FillGraphWithXsdSimpleTypeNodeForXsdComplexTypes(cytoscapeObject, groupNode, child, idServiceDescription);
                        }
                    }
                }
            }
        }

        #endregion Fill Graph Nodes

        #region Has Sawsdl?

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool HasSawsdlLiftingSchemaMapping(ICanBeAnnotatedWithSchemaMapping obj) => !string.IsNullOrEmpty(obj.LiftingSchemaMapping);

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool HasSawsdlLoweringSchemaMapping(ICanBeAnnotatedWithSchemaMapping obj) => !string.IsNullOrEmpty(obj.LoweringSchemaMapping);

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool HasSawsdlModelReference(ICanBeAnnotatedWithModelReference obj) => obj.SawsdlModelReferences != null && obj.SawsdlModelReferences.Count > 0;

        #endregion Has Sawsdl?

        #region Has Issues?

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool HasIssues(ICanHaveIssues obj) => obj.Issues != null && obj.Issues.Count > 0 && obj.Issues.Any(x => !x.Solved);

        #endregion Has Issues?

        #endregion Private methods

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
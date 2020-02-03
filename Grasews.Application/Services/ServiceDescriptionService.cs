using Grasews.Domain.Entities;
using Grasews.Domain.Enums;
using Grasews.Domain.Exceptions;
using Grasews.Domain.Interfaces.DTOs;
using Grasews.Domain.Interfaces.Entities;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Email;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.CrossCutting.Helpers.Extensions;
using Grasews.Infra.ExternalService.Cytoscape.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;

namespace Grasews.Application.Services
{
    public class ServiceDescriptionService : BaseService, IServiceDescriptionService
    {
        #region Private consts

        private const string WSDL_NAMESPACE = "http://www.w3.org/ns/wsdl";
        private const string XSD_NAMESPACE = "http://www.w3.org/2001/XMLSchema";

        #endregion Private consts

        #region Private vars

        #region External services

        private readonly IGraphService _graphService;

        #endregion External services

        #region Repositories

        private readonly IGraphNodePosition_OntologyTermRepository _graphNodePosition_OntologyTermRepository;
        private readonly IGraphNodePosition_SawsdlModelReferenceRepository _graphNodePosition_SawsdlModelReferenceRepository;
        private readonly IIssueAnswerEntityRepository _issueAnswerEntityRepository;
        private readonly IIssueEntityRepository _issueEntityRepository;
        private readonly IOntologyTermEntityRepository _ontologyTermRepository;
        private readonly ISawsdlModelReferenceEntityRepository _sawsdlModelReferenceRepository;
        private readonly IServiceDescription_OntologyEntityRepository _serviceDescription_OntologyRepository;
        private readonly IServiceDescription_UserEntityRepository _serviceDescription_UserRepository;
        private readonly IServiceDescriptionEntityRepository _serviceDescriptionRepository;
        private readonly IShareInvitationEntityRepository _shareInvitationRepository;
        private readonly ITaskCommentEntityRepository _taskCommentEntityRepository;
        private readonly ITaskEntityRepository _taskEntityRepository;
        private readonly IWsdlFaultEntityRepository _wsdlFaultRepository;
        private readonly IWsdlInfaultEntityRepository _wsdlInfaultRepository;
        private readonly IWsdlInputEntityRepository _wsdlInputRepository;
        private readonly IWsdlInterfaceEntityRepository _wsdlInterfaceRepository;
        private readonly IWsdlOperationEntityRepository _wsdlOperationRepository;
        private readonly IWsdlOutfaultEntityRepository _wsdlOutfaultRepository;
        private readonly IWsdlOutputEntityRepository _wsdlOutputRepository;
        private readonly IXsdComplexTypeEntityRepository _xsdComplexTypeRepository;
        private readonly IXsdSimpleTypeEntityRepository _xsdSimpleTypeRepository;

        #endregion Repositories

        #region Xml namespaces

        private readonly XNamespace wsdlNamespace = XNamespace.Get(WSDL_NAMESPACE);
        private readonly XNamespace xsdNamespace = XNamespace.Get(XSD_NAMESPACE);

        #endregion Xml namespaces

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription_UserRepository"></param>
        /// <param name="serviceDescriptionRepository"></param>
        /// <param name="shareInvitationRepository"></param>
        /// <param name="graphService"></param>
        /// <param name="wsdlInterfaceRepository"></param>
        /// <param name="wsdlOperationRepository"></param>
        /// <param name="wsdlInputRepository"></param>
        /// <param name="wsdlOutputRepository"></param>
        /// <param name="wsdlInfaultRepository"></param>
        /// <param name="wsdlOutfaultRepository"></param>
        /// <param name="xsdComplexTypeRepository"></param>
        /// <param name="xsdSimpleTypeRepository"></param>
        /// <param name="sawsdlModelReferenceRepository"></param>
        /// <param name="ontologyTermRepository"></param>
        /// <param name="graphNodePosition_OntologyTermRepository"></param>
        /// <param name="graphNodePosition_SawsdlModelReferenceRepository"></param>
        /// <param name="issueEntityRepository"></param>
        /// <param name="issueAnswerEntityRepository"></param>
        /// <param name="taskEntityRepository"></param>
        /// <param name="taskCommentEntityRepository"></param>
        /// <param name="serviceDescription_OntologyRepository"></param>
        public ServiceDescriptionService(IServiceDescription_UserEntityRepository serviceDescription_UserRepository,
            IServiceDescriptionEntityRepository serviceDescriptionRepository,
            IShareInvitationEntityRepository shareInvitationRepository,
            IGraphService graphService,
            IWsdlInterfaceEntityRepository wsdlInterfaceRepository,
            IWsdlOperationEntityRepository wsdlOperationRepository,
            IWsdlInputEntityRepository wsdlInputRepository,
            IWsdlOutputEntityRepository wsdlOutputRepository,
            IWsdlFaultEntityRepository wsdlFaultRepository,
            IWsdlInfaultEntityRepository wsdlInfaultRepository,
            IWsdlOutfaultEntityRepository wsdlOutfaultRepository,
            IXsdComplexTypeEntityRepository xsdComplexTypeRepository,
            IXsdSimpleTypeEntityRepository xsdSimpleTypeRepository,
            ISawsdlModelReferenceEntityRepository sawsdlModelReferenceRepository,
            IOntologyTermEntityRepository ontologyTermRepository,
            IGraphNodePosition_OntologyTermRepository graphNodePosition_OntologyTermRepository,
            IGraphNodePosition_SawsdlModelReferenceRepository graphNodePosition_SawsdlModelReferenceRepository,
            IIssueEntityRepository issueEntityRepository,
            IIssueAnswerEntityRepository issueAnswerEntityRepository,
            ITaskEntityRepository taskEntityRepository,
            ITaskCommentEntityRepository taskCommentEntityRepository,
            IServiceDescription_OntologyEntityRepository serviceDescription_OntologyRepository)
        {
            _graphService = graphService;

            _serviceDescriptionRepository = serviceDescriptionRepository;

            _serviceDescription_UserRepository = serviceDescription_UserRepository;
            _shareInvitationRepository = shareInvitationRepository;

            _wsdlInterfaceRepository = wsdlInterfaceRepository;
            _wsdlOperationRepository = wsdlOperationRepository;
            _wsdlFaultRepository = wsdlFaultRepository;
            _wsdlInputRepository = wsdlInputRepository;
            _wsdlOutputRepository = wsdlOutputRepository;
            _wsdlInfaultRepository = wsdlInfaultRepository;
            _wsdlOutfaultRepository = wsdlOutfaultRepository;
            _xsdComplexTypeRepository = xsdComplexTypeRepository;
            _xsdSimpleTypeRepository = xsdSimpleTypeRepository;

            _sawsdlModelReferenceRepository = sawsdlModelReferenceRepository;

            _ontologyTermRepository = ontologyTermRepository;

            _graphNodePosition_OntologyTermRepository = graphNodePosition_OntologyTermRepository;
            _graphNodePosition_SawsdlModelReferenceRepository = graphNodePosition_SawsdlModelReferenceRepository;

            _issueEntityRepository = issueEntityRepository;
            _issueAnswerEntityRepository = issueAnswerEntityRepository;
            _taskEntityRepository = taskEntityRepository;
            _taskCommentEntityRepository = taskCommentEntityRepository;

            _serviceDescription_OntologyRepository = serviceDescription_OntologyRepository;
        }

        #endregion Ctors

        #region IServiceDescriptionService public methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <returns></returns>
        public int Create(ServiceDescription serviceDescription)
        {
            //Identify the objects from the Wsdl (Xml)
            ParseXml(serviceDescription);

            //Beautify the xml code
            var wsdl = XDocument.Parse(serviceDescription.Xml);
            serviceDescription.Xml = wsdl.ToString();

            //Create the service description in the EF context
            _serviceDescriptionRepository.Create(serviceDescription);

            //Save the EF context in the DB
            var count = _serviceDescriptionRepository.SaveChanges();

            //Generate the graph
            var graphJson = _graphService.CreateGraphData(serviceDescription.Id, serviceDescription.ServiceName, serviceDescription.WsdlInterfaces);

            var htmlMenu = GetHtmlTreeViewMenu(serviceDescription);

            //Set the graph to the service description
            serviceDescription.GraphJson = JsonConvert.SerializeObject(graphJson);

            //Update the service description in the EF context
            _serviceDescriptionRepository.Update(serviceDescription);

            //Save the EF context in the DB
            count = _serviceDescriptionRepository.SaveChanges();

            return count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="idOwnerUser"></param>
        /// <returns></returns>
        public int CreateNodePositionsForSameModelReferencesBetweenWsdlElements(int idServiceDescription, int idOwnerUser)
        {
            //Get all model references by the owner id and the service description id
            var modelReferences = _sawsdlModelReferenceRepository.GetAll().Where(x => x.IdOwnerUser == idOwnerUser && x.IdServiceDescription == idServiceDescription).ToList();

            var idModelReferences = modelReferences.Select(x => x.Id).ToList();

            //Get all the node positions by the owner id and that are in the range of the existing model references for the service description
            var modelReferenceNodePositions = _graphNodePosition_SawsdlModelReferenceRepository.GetAllWithSawsdlModelReference(@readonly: false).Where(x => x.IdOwnerUser == idOwnerUser && idModelReferences.Contains(x.IdSawsdlModelReference)).ToList();

            if (modelReferenceNodePositions.Any())
            {
                //Check if there are any model reference without a node position
                if (idModelReferences.Count() != modelReferenceNodePositions.Count())
                {
                    //Get all the model reference ids that don't have a position
                    var idModelReferencesToCreateNodePositions = idModelReferences.Where(x => !modelReferenceNodePositions.Select(y => y.IdSawsdlModelReference).Contains(x)).ToList();

                    //For each model reference that does not have a node position yet
                    foreach (var idModelReferenceToCreateNodePosition in idModelReferencesToCreateNodePositions)
                    {
                        //Get the model reference according to the id you want to create a position for
                        var modelReferenceToCreateNodePosition = modelReferences.FirstOrDefault(x => x.Id == idModelReferenceToCreateNodePosition);

                        //Get the existing node position for the same ontology term that already have a model reference with a node position set
                        var existingPosition = modelReferenceNodePositions.FirstOrDefault(x => x.SawsdlModelReference.IdOntologyTerm == modelReferenceToCreateNodePosition.IdOntologyTerm);

                        //Check if exist the position
                        if (existingPosition != null)
                        {
                            //Create the new position copying the X and the Y values from the same node position that already exists for the same ontology term node
                            _graphNodePosition_SawsdlModelReferenceRepository.Create(new GraphNodePosition_SawsdlModelReference
                            {
                                IdOwnerUser = idOwnerUser,
                                RegistrationDateTime = DateTime.Now,
                                IdSawsdlModelReference = idModelReferenceToCreateNodePosition,
                                X = existingPosition.X,
                                Y = existingPosition.Y
                            });
                        }
                    }
                }

                return _graphNodePosition_SawsdlModelReferenceRepository.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="withSawsdlModelReference">todo: describe withSawsdlModelReference parameter on Get</param>
        /// <param name="readonly">todo: describe readonly parameter on Get</param>
        /// <returns></returns>
        public ServiceDescription Get(int idServiceDescription, bool withSawsdlModelReference = false, bool @readonly = true)
        {
            //var xsdComplexTypes = _xsdComplexTypeRepository.GetAllByServiceDescription(idServiceDescription).ToList();

            //ArrangeXsdComplexTypes(xsdComplexTypes);

            //var xsdComplexTypes = _xsdComplexTypeQueryRepository.GetByServiceDescription(idServiceDescription);

            return _serviceDescriptionRepository.GetComplete(idServiceDescription, withSawsdlModelReference, @readonly);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ServiceDescription> GetAll()
        {
            return _serviceDescriptionRepository.GetAll().ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public IEnumerable<ServiceDescription> GetAllByOwnerUser(int idUser)
        {
            var serviceDescriptions = new List<ServiceDescription>();

            //Get all service descriptions created by the user
            var ownedServiceDescriptions = _serviceDescriptionRepository.GetAll().Where(x => x.IdOwnerUser == idUser).ToList();
            //Get all service descriptions shared with the user
            var sharedServiceDescriptions = _serviceDescription_UserRepository.GetAllBySharedUser(idUser).Select(x => x.ServiceDescription).ToList();

            //Add both lists of service descriptions into a single list
            serviceDescriptions.AddRange(ownedServiceDescriptions);
            serviceDescriptions.AddRange(sharedServiceDescriptions);

            return serviceDescriptions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public ServiceDescription GetByServiceName(string serviceName)
        {
            return _serviceDescriptionRepository.GetAll().FirstOrDefault(x => x.ServiceName == serviceName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="idUser"></param>
        /// <param name="withSawsdlModelReference"></param>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public ServiceDescription GetByUser(int idServiceDescription, int idUser, bool withSawsdlModelReference = false, bool @readonly = true)
        {
            var serviceDescription = _serviceDescriptionRepository.GetComplete(idServiceDescription, withSawsdlModelReference, @readonly);

            RemoveSelfReference(serviceDescription);

            return serviceDescription;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <returns></returns>
        public string GetHtmlTreeViewMenu(ServiceDescription serviceDescription)
        {
            ////Identify the objects from the Wsdl (Xml)
            //ParseXml(serviceDescription);

            var htmlBuilder = new StringBuilder();

            var serviceDisplayName = serviceDescription.ServiceName;

            //if (serviceDescription.ServiceName.Length > 20)
            //{
            //    serviceDisplayName = serviceDescription.ServiceName.Substring(0, 19) + "...";
            //}

            //Opening LI for Web Service
            htmlBuilder.Append($"<li class='treeview' id='li-service-description-{serviceDescription.Id}' service-name='{serviceDescription.ServiceName}' id-service-description={serviceDescription.Id}>".Trim());
            htmlBuilder.Append($"   <a href='#' class='webserviceContextMenu'>".Trim());
            htmlBuilder.Append($"       <i class='fa fa-file-code-o' title='Web Service [{serviceDescription.ServiceName}]'></i>".Trim());
            htmlBuilder.Append($"       <span data-placement='right' title='Web Service [{serviceDescription.ServiceName}]'>{serviceDisplayName}</span>".Trim());
            htmlBuilder.Append($"       <span class='pull-right-container'>".Trim());
            htmlBuilder.Append($"           <i class='fa fa-eye pull-right service-description-eye' id-service-description='{serviceDescription.Id}'></i>".Trim());
            htmlBuilder.Append($"           <i class='fa fa-angle-right pull-left'></i>".Trim());
            htmlBuilder.Append($"       </span>".Trim());
            htmlBuilder.Append($"   </a>".Trim());

            var wsdlIntefacesHtmlTreeViewMenu = CreateHtmlTreeViewMenuForWsdlInterfaces(serviceDescription.WsdlInterfaces, serviceDescription.Id);

            htmlBuilder.Append(wsdlIntefacesHtmlTreeViewMenu);

            //Closing LI for Web Service
            htmlBuilder.Append($"</li>");

            return htmlBuilder.ToString();
        }

        public ServiceDescription GetWithOntologies(int idServiceDescription, bool @readonly = true)
        {
            return _serviceDescriptionRepository.GetWithOntologies(idServiceDescription, @readonly);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlFault"></param>
        /// <returns></returns>
        public WsdlFault GetWsdlFault(int idWsdlFault)
        {
            return _wsdlFaultRepository.Get(idWsdlFault);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlFault"></param>
        /// <returns></returns>
        public WsdlFault GetWsdlFaultWithSemanticAnnotations(int idWsdlFault)
        {
            return _wsdlFaultRepository.GetWithSemanticAnnotations(idWsdlFault);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlInfault"></param>
        /// <returns></returns>
        public WsdlInfault GetWsdlInfault(int idWsdlInfault)
        {
            return _wsdlInfaultRepository.Get(idWsdlInfault);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlInput"></param>
        /// <returns></returns>
        public WsdlInput GetWsdlInput(int idWsdlInput)
        {
            return _wsdlInputRepository.Get(idWsdlInput);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlInterface"></param>
        /// <returns></returns>
        public WsdlInterface GetWsdlInterface(int idWsdlInterface)
        {
            return _wsdlInterfaceRepository.Get(idWsdlInterface);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlInterface"></param>
        /// <returns></returns>
        public WsdlInterface GetWsdlInterfaceWithSemanticAnnotations(int idWsdlInterface)
        {
            return _wsdlInterfaceRepository.GetWithSemanticAnnotations(idWsdlInterface);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlOperation"></param>
        /// <returns></returns>
        public WsdlOperation GetWsdlOperation(int idWsdlOperation)
        {
            return _wsdlOperationRepository.Get(idWsdlOperation);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlOperation"></param>
        /// <returns></returns>
        public WsdlOperation GetWsdlOperationWithSemanticAnnotations(int idWsdlOperation)
        {
            return _wsdlOperationRepository.GetWithSemanticAnnotations(idWsdlOperation);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlOutfault"></param>
        /// <returns></returns>
        public WsdlOutfault GetWsdlOutfault(int idWsdlOutfault)
        {
            return _wsdlOutfaultRepository.Get(idWsdlOutfault);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlOutput"></param>
        /// <returns></returns>
        public WsdlOutput GetWsdlOutput(int idWsdlOutput)
        {
            return _wsdlOutputRepository.Get(idWsdlOutput);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        public XsdComplexType GetXsdComplexType(int idXsdComplexType)
        {
            return _xsdComplexTypeRepository.Get(idXsdComplexType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idXsdComplexType"></param>
        /// <returns></returns>
        public XsdComplexType GetXsdComplexTypeWithSemanticAnnotations(int idXsdComplexType)
        {
            return _xsdComplexTypeRepository.GetWithSemanticAnnotations(idXsdComplexType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idXsdSimpleType"></param>
        /// <returns></returns>
        public XsdSimpleType GetXsdSimpleType(int idXsdSimpleType)
        {
            return _xsdSimpleTypeRepository.Get(idXsdSimpleType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idXsdSimpleType"></param>
        /// <returns></returns>
        public XsdSimpleType GetXsdSimpleTypeWithSemanticAnnotations(int idXsdSimpleType)
        {
            return _xsdSimpleTypeRepository.GetWithSemanticAnnotations(idXsdSimpleType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parseXmlRequestDTO"></param>
        /// <returns></returns>
        public ServiceDescription ParseXml(IParseWsdlRequestDTO parseXmlRequestDTO)
        {
            var xDocument = XDocument.Parse(parseXmlRequestDTO.Xml);

            var serviceDescription = new ServiceDescription();

            ParseXDocumentToServiceDescription(serviceDescription, xDocument);

            return serviceDescription;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <returns></returns>
        public int Remove(ServiceDescription serviceDescription)
        {
            //RemoveIssuesByServiceDescription(serviceDescription.Id);

            //RemoveTasksByServiceDescription(serviceDescription.Id);

            //RemoveShareInvitations(serviceDescription.Id);

            //RemoveSharedServiceByServiceDescription(serviceDescription.Id);

            //RemoveServiceDescriptionOntologiesLinkByServiceDescription(serviceDescription.Id);

            _serviceDescriptionRepository.RemoveXsdComplexTypeSelfReferences(serviceDescription.Id);

            //var count = _serviceDescriptionRepository.SaveChanges();

            _serviceDescriptionRepository.Remove(serviceDescription.Id);

            return _serviceDescriptionRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        public void RemoveAllSharing(int idServiceDescription)
        {
            RemoveSharedServiceByServiceDescription(idServiceDescription);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="idSharedUser"></param>
        public void RemoveSharing(int idServiceDescription, int idSharedUser)
        {
            RemoveSharedServiceByServiceDescriptionAndSharedUser(idServiceDescription, idSharedUser);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription_User"></param>
        public void RemoveSharing(int idServiceDescription_User)
        {
            RemoveSharedService(idServiceDescription_User);
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="graphDataObject"></param>
        ///// <param name="idOwnerUser"></param>
        ///// <returns></returns>
        //public int RemoveUnusedGraphNodePositisonsByUser(int idOwnerUser, IGraphDataObject graphDataObject)
        //{
        //    var idOntologyTermsPresentInGraph = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.OntologyTerm).Select(x => x.Data.IdOntologyTerm);

        //    var ontologyTermNodePositions = _ontologyTermRepository.GetOntologyTermGraphPositionsByUser(idOwnerUser, @readonly: false);

        //    var positionsToBeRemoved = ontologyTermNodePositions.Where(x => !idOntologyTermsPresentInGraph.Contains(x.IdOntologyTerm));

        //    _ontologyTermRepository.RemoveOntologyTermGraphPositions(positionsToBeRemoved);

        //    return _ontologyTermRepository.SaveChanges();
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphDataObject"></param>
        /// <returns></returns>
        public int RemoveUnusedGraphNodePositisons(IGraphDataObject graphDataObject)
        {
            var idOntologyTermsPresentInGraph = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.OntologyTerm).Select(x => x.Data.IdOntologyTerm);

            var ontologyTermNodePositions = _graphNodePosition_OntologyTermRepository.GetAll(@readonly: false);

            var positionsToBeRemoved = ontologyTermNodePositions.Where(x => !idOntologyTermsPresentInGraph.Contains(x.IdOntologyTerm));

            _graphNodePosition_OntologyTermRepository.RemoveOntologyTermGraphPositions(positionsToBeRemoved);

            return _ontologyTermRepository.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="originalGraphJson"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on SetGraphNodesLocationsByUser</param>
        /// <returns></returns>
        public string SetGraphNodesPositionsByUser(int idOwnerUser, int idServiceDescription, string originalGraphJson)
        {
            var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(originalGraphJson);

            var wsdlInterfaceNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Interface);
            SetExistingPositionsForWsdlInterfaceNodes(idOwnerUser, wsdlInterfaceNodes);

            var wsdlOperationNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Operation);
            SetExistingPositionsForWsdlOperationNodes(idOwnerUser, wsdlOperationNodes);

            var wsdlInputNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Input);
            SetExistingPositionsForWsdlInputNodes(idOwnerUser, wsdlInputNodes);

            var wsdlOutputNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Output);
            SetExistingPositionsForWsdlOutputNodes(idOwnerUser, wsdlOutputNodes);

            var wsdlInfaultNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Infault);
            SetExistingPositionsForWsdlInfaultNodes(idOwnerUser, wsdlInfaultNodes);

            var wsdlOutfaultNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Outfault);
            SetExistingPositionsForWsdlOutfaultNodes(idOwnerUser, wsdlOutfaultNodes);

            var xsdComplexTypeNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.ComplexType);
            SetExistingPositionsForXsdComplexTypeNodes(idOwnerUser, xsdComplexTypeNodes);

            var xsdSimpleTypeNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.SimpleType);
            SetExistingPositionsForXsdSimpleTypeNodes(idOwnerUser, xsdSimpleTypeNodes);

            var sawsdModelReferenceNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.ModelReference);
            SetExistingPositionsForSawsdlModelReferenceNodes(idOwnerUser, idServiceDescription, sawsdModelReferenceNodes);

            var ontologyTermNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.OntologyTerm);
            SetExistingPositionsForOntologyTermNodes(idOwnerUser, idServiceDescription, ontologyTermNodes);

            //TODO: [Node Position] Set position nodes for wsdl outfaults and wsdl infaults

            return JsonConvert.SerializeObject(graphDataObject);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="idServiceDescription"></param>
        /// <param name="idOntologyTerms"></param>
        /// <returns></returns>
        public int TransferNodePositionsFromModelReferencesToOntologyTerms(int idOwnerUser, int idServiceDescription, IEnumerable<int> idOntologyTerms)
        {
            var modelReferencePositionsToBeMovedToOntologyTermPositions = _graphNodePosition_SawsdlModelReferenceRepository.GetAllWithSawsdlModelReference(@readonly: false).Where(x => idOntologyTerms.Contains(x.SawsdlModelReference.IdOntologyTerm)).ToList();

            if (modelReferencePositionsToBeMovedToOntologyTermPositions.Any())
            {
                GraphNodePosition_OntologyTerm ontologyTermNodePosition;

                foreach (var modelReferenceNodePosition in modelReferencePositionsToBeMovedToOntologyTermPositions)
                {
                    ontologyTermNodePosition = new GraphNodePosition_OntologyTerm
                    {
                        IdOwnerUser = idOwnerUser,
                        IdOntologyTerm = modelReferenceNodePosition.SawsdlModelReference.IdOntologyTerm,
                        RegistrationDateTime = DateTime.Now,
                        X = modelReferenceNodePosition.X,
                        Y = modelReferenceNodePosition.Y
                    };

                    _graphNodePosition_OntologyTermRepository.Create(ontologyTermNodePosition);
                }
            }

            var countModelReferenceNodePositions = _graphNodePosition_SawsdlModelReferenceRepository.SaveChanges();
            var countOntologyTermNodePositions = _graphNodePosition_OntologyTermRepository.SaveChanges();

            return countModelReferenceNodePositions + countOntologyTermNodePositions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="idServiceDescription"></param>
        public int TransferNodePositionsFromOntologyTermsToModelReferences(int idOwnerUser, int idServiceDescription)
        {
            var modelReferences = _sawsdlModelReferenceRepository.GetAll().Where(x => x.IdOwnerUser == idOwnerUser && x.IdServiceDescription == idServiceDescription).ToList();

            var idOntologyTermsUsedInModelReferences = modelReferences.Select(x => x.IdOntologyTerm).ToList();

            var ontologyTermPositionsToBeMovedToModelReferencePositions = _graphNodePosition_OntologyTermRepository.GetAll(@readonly: false).Where(x => idOntologyTermsUsedInModelReferences.Contains(x.IdOntologyTerm)).ToList();

            if (ontologyTermPositionsToBeMovedToModelReferencePositions.Any())
            {
                SawsdlModelReference sawsdlModelReference;
                GraphNodePosition_SawsdlModelReference modelReferenceNodePosition;

                foreach (var ontologyTermNodePosition in ontologyTermPositionsToBeMovedToModelReferencePositions)
                {
                    sawsdlModelReference = modelReferences.FirstOrDefault(x => x.IdOntologyTerm == ontologyTermNodePosition.IdOntologyTerm);

                    modelReferenceNodePosition = new GraphNodePosition_SawsdlModelReference
                    {
                        IdOwnerUser = ontologyTermNodePosition.IdOwnerUser,
                        IdSawsdlModelReference = sawsdlModelReference.Id,
                        RegistrationDateTime = DateTime.Now,
                        X = ontologyTermNodePosition.X,
                        Y = ontologyTermNodePosition.Y
                    };

                    _graphNodePosition_SawsdlModelReferenceRepository.Create(modelReferenceNodePosition);
                }
            }

            _graphNodePosition_OntologyTermRepository.RemoveRange(ontologyTermPositionsToBeMovedToModelReferencePositions);

            var countModelReferenceNodePositions = _graphNodePosition_SawsdlModelReferenceRepository.SaveChanges();
            var countOntologyTermNodePositions = _graphNodePosition_OntologyTermRepository.SaveChanges();

            return countModelReferenceNodePositions + countOntologyTermNodePositions;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <returns></returns>
        public int Update(ServiceDescription serviceDescription)
        {
            var existingServiceDescription = _serviceDescriptionRepository.GetWithOntologies(serviceDescription.Id, false);

            if (!string.IsNullOrEmpty(serviceDescription.GraphJson))
            {
                existingServiceDescription.GraphJson = serviceDescription.GraphJson;
            }

            if (!string.IsNullOrEmpty(serviceDescription.ServiceName))
            {
                existingServiceDescription.ServiceName = serviceDescription.ServiceName;
            }

            if (!string.IsNullOrEmpty(serviceDescription.Xml))
            {
                existingServiceDescription.Xml = serviceDescription.Xml;
            }

            if (serviceDescription.ServiceDescription_Ontologies != null && serviceDescription.ServiceDescription_Ontologies.Any())
            {
                if (existingServiceDescription.ServiceDescription_Ontologies == null)
                {
                    existingServiceDescription.ServiceDescription_Ontologies = new List<ServiceDescription_Ontology>();
                }

                var ontologiesAlreadyLinkedToServiceDescripton = existingServiceDescription.ServiceDescription_Ontologies.Select(x => x.IdOntology);

                var serviceDescription_OntologiesNotUsedYet = serviceDescription.ServiceDescription_Ontologies.Where(x => !ontologiesAlreadyLinkedToServiceDescripton.Contains(x.IdOntology));

                foreach (var serviceDescription_Ontology in serviceDescription_OntologiesNotUsedYet)
                {
                    existingServiceDescription.ServiceDescription_Ontologies.Add(serviceDescription_Ontology);
                }
            }

            //TODO: Update Wsdl and Xsd registers in database after parsing the XML.
            //ParseXml(existingServiceDescription);

            _serviceDescriptionRepository.Update(existingServiceDescription);

            //_serviceDescriptionRepository.Update(serviceDescription);

            var count = _serviceDescriptionRepository.SaveChanges();

            RemoveSelfReference(serviceDescription);

            return count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="idOwnerUser"></param>
        /// <param name="graphDataObject"></param>
        /// <returns></returns>
        public int UpdateJsonGraph(int idServiceDescription, int idOwnerUser, IGraphDataObject graphDataObject)
        {
            WsdlInterface wsdlInteface;
            WsdlOperation wsdlOperation;
            WsdlInput wsdlInput;
            WsdlOutput wsdlOutput;
            //WsdlInfault wsdlInfault;
            //WsdlOutfault wsdlOutfault;
            XsdComplexType xsdComplexType;
            XsdSimpleType xsdSimpleType;
            List<SawsdlModelReference> sawsdlModeReferences;
            List<OntologyTerm> ontologyTerms;

            #region Interface Nodes

            var wsdlInterfaceNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Interface);

            foreach (var wsdlInterfaceNode in wsdlInterfaceNodes)
            {
                wsdlInteface = UpdateWsdlInterfaceNodePosition(idOwnerUser, wsdlInterfaceNode);

                _wsdlInterfaceRepository.Update(wsdlInteface);
            }

            #endregion Interface Nodes

            #region Operation Nodes

            var wsdlOperationNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Operation);

            foreach (var wsdlOpertionNode in wsdlOperationNodes)
            {
                wsdlOperation = UpdateWsdlOperationNodePosition(idOwnerUser, wsdlOpertionNode);

                _wsdlOperationRepository.Update(wsdlOperation);
            }

            #endregion Operation Nodes

            #region Input Nodes

            var wsdlInputNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Input);

            foreach (var wsdlInputNode in wsdlInputNodes)
            {
                wsdlInput = UpdateWsdlInputNodePosition(idOwnerUser, wsdlInputNode);

                _wsdlInputRepository.Update(wsdlInput);
            }

            #endregion Input Nodes

            #region Output Nodes

            var wsdlOutputNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Output);

            foreach (var wsdlOutputNode in wsdlOutputNodes)
            {
                wsdlOutput = UpdateWsdlOutputNodePosition(idOwnerUser, wsdlOutputNode);

                _wsdlOutputRepository.Update(wsdlOutput);
            }

            #endregion Output Nodes

            //TODO: [Node Position] Develop wsdl infault nodes' positions
            //var wsdlInfaultNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Infault);
            //TODO: [Node Position] Develop wsdl outfault nodes' positions
            //var wsdlOutfaultNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.Outfault);

            #region Complex Element Nodes

            var xsdComplexTypeNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.ComplexType);

            foreach (var xsdComplexTypeNode in xsdComplexTypeNodes)
            {
                xsdComplexType = UpdateXsdComplexTypeNodePosition(idOwnerUser, xsdComplexTypeNode);

                _xsdComplexTypeRepository.Update(xsdComplexType);
            }

            #endregion Complex Element Nodes

            #region Simple Type Nodes

            var xsdSimpleTypeNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.SimpleType);

            foreach (var xsdSimpleTypeNode in xsdSimpleTypeNodes)
            {
                xsdSimpleType = UpdateXsdSimpleTypeNodePosition(idOwnerUser, xsdSimpleTypeNode);

                _xsdSimpleTypeRepository.Update(xsdSimpleType);
            }

            #endregion Simple Type Nodes

            #region Model References

            var sawsdlModelReferenceNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.ModelReference);

            foreach (var sawsdlModelReferenceNode in sawsdlModelReferenceNodes)
            {
                sawsdlModeReferences = UpdateSawsdlModelReferenceNodePosition(idOwnerUser, idServiceDescription, sawsdlModelReferenceNode);

                foreach (var sawsdlModeReference in sawsdlModeReferences)
                {
                    _sawsdlModelReferenceRepository.Update(sawsdlModeReference);
                }
            }

            #endregion Model References

            #region Ontology Terms (intermediate nodes / ontology hierarchies)

            var ontologyTermNodes = graphDataObject.Nodes.Where(x => x.Data.NodeTypeEnum == GraphNodeTypeEnum.OntologyTerm);

            foreach (var ontologyTermNode in ontologyTermNodes)
            {
                ontologyTerms = UpdateOntologyTermNodePosition(idOwnerUser, idServiceDescription, ontologyTermNode);

                foreach (var ontologyTerm in ontologyTerms)
                {
                    _ontologyTermRepository.Update(ontologyTerm);
                }
            }

            #endregion Ontology Terms (intermediate nodes / ontology hierarchies)

            var countIntefaces = _wsdlInterfaceRepository.SaveChanges();
            var countOperations = _wsdlOperationRepository.SaveChanges();
            var countInputs = _wsdlInputRepository.SaveChanges();
            var countOutputs = _wsdlOutputRepository.SaveChanges();
            var countComplexTypes = _xsdComplexTypeRepository.SaveChanges();
            var countSimpleTypes = _xsdSimpleTypeRepository.SaveChanges();
            var countModelReferences = _sawsdlModelReferenceRepository.SaveChanges();
            var countOntologyTerms = _ontologyTermRepository.SaveChanges();

            return countIntefaces + countOperations + countInputs + countOutputs + countComplexTypes + countSimpleTypes + countModelReferences + countOntologyTerms;
        }

        #endregion IServiceDescriptionService public methods

        #region Private methods

        #region Html for Wsdl Elements

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlInfaults"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on CreateHtmlTreeViewMenuForWsdlInfaults</param>
        /// <returns></returns>
        private string CreateHtmlTreeViewMenuForWsdlInfaults(ICollection<WsdlInfault> wsdlInfaults, int idServiceDescription)
        {
            var htmlBuilder = new StringBuilder();

            return htmlBuilder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlInputs"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on CreateHtmlTreeViewMenuForWsdlInputs</param>
        /// <returns></returns>
        private string CreateHtmlTreeViewMenuForWsdlInputs(ICollection<WsdlInput> wsdlInputs, int idServiceDescription)
        {
            var htmlBuilder = new StringBuilder();

            foreach (var wsdlInput in wsdlInputs)
            {
                var inputDisplayName = wsdlInput.WsdlInputName;

                //if (wsdlInput.WsdlInputName.Length > 16)
                //{
                //    inputDisplayName = $"{wsdlInput.WsdlInputName.Substring(0, 15)}...";
                //}

                inputDisplayName = $"[in] {inputDisplayName}";

                //Opening LI for Input
                htmlBuilder.Append($"<li class='treeview' id-wsdl-element='{wsdlInput.Id}' wsdl-element-name='{wsdlInput.WsdlInputName}' ");
                htmlBuilder.Append($" wsdl-element-type='{ServiceDescriptionElementTypeEnum.WsdlInput.GetEnumDescription()}' id-wsdl-element-type='{(int)ServiceDescriptionElementTypeEnum.WsdlInput}' ");
                htmlBuilder.Append($" id-service-description='{idServiceDescription}'>");
                htmlBuilder.Append($"   <a class='tree-view-menu-wsdl-input' href='#'>".Trim());
                htmlBuilder.Append($"       <i class='fa fa-circle-o fa-cicle-o-colored'></i>".Trim());
                htmlBuilder.Append($"       <span data-placement='right' title='Input [{wsdlInput.WsdlInputName}]'>{inputDisplayName}</span>".Trim());
                htmlBuilder.Append($"       <span class='pull-right-container'>".Trim());
                htmlBuilder.Append($"           <i class='fa fa-angle-right pull-left'></i>".Trim());
                htmlBuilder.Append($"       </span>".Trim());
                htmlBuilder.Append($"   </a>".Trim());

                //Opening UL for Complex Elements and Simple Elements
                htmlBuilder.Append($"   <ul class='treeview-menu'>".Trim());

                if (wsdlInput.XsdComplexType != null)
                {
                    var xsdComplexTypesHtmlTreeViewMenu = CreateHtmlTreeViewMenuForXsdComplexType(wsdlInput.XsdComplexType, idServiceDescription);

                    htmlBuilder.Append(xsdComplexTypesHtmlTreeViewMenu);
                }

                if (wsdlInput.XsdSimpleType != null)
                {
                    var xsdSimpleTypesHtmlTreeViewMenu = CreateHtmlTreeViewMenuForXsdSimpleType(new List<XsdSimpleType> { wsdlInput.XsdSimpleType }, idServiceDescription);

                    htmlBuilder.Append(xsdSimpleTypesHtmlTreeViewMenu);
                }

                //Closing UL for Complex Elements and Simple Elements
                htmlBuilder.Append($"   </ul>".Trim());

                //Closing LI for Input
                htmlBuilder.Append($"</li>");
            }

            return htmlBuilder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlInterfaces"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on CreateHtmlTreeViewMenuForWsdlInterfaces</param>
        /// <returns></returns>
        private string CreateHtmlTreeViewMenuForWsdlInterfaces(ICollection<WsdlInterface> wsdlInterfaces, int idServiceDescription)
        {
            var modelReferenceColor = "#00a65a";
            //var schemaMappingColor = "#00a65a";

            var htmlBuilder = new StringBuilder();

            //Opening UL for Web Service Interfaces
            htmlBuilder.Append($"<ul class='treeview-menu' style='display: none;'>".Trim());

            foreach (var wsdlInterface in wsdlInterfaces)
            {
                var interfaceDisplayName = wsdlInterface.WsdlInterfaceName;

                //if (wsdlInterface.WsdlInterfaceName.Length > 21)
                //{
                //    interfaceDisplayName = $"{ wsdlInterface.WsdlInterfaceName.Substring(0, 20)}...";
                //}

                interfaceDisplayName = $"[i] {interfaceDisplayName}";

                var hasSawsdlModelReference = wsdlInterface.SawsdlModelReferences != null && wsdlInterface.SawsdlModelReferences.Any();

                //Opening LI for Wsdl Interface
                htmlBuilder.Append($"<li class='treeview' id-wsdl-element='{wsdlInterface.Id}' wsdl-element-name='{wsdlInterface.WsdlInterfaceName}' ");
                htmlBuilder.Append($" wsdl-element-type='{ServiceDescriptionElementTypeEnum.WsdlInterface.GetEnumDescription()}' id-wsdl-element-type='{(int)ServiceDescriptionElementTypeEnum.WsdlInterface}' ");
                htmlBuilder.Append($" id-service-description='{idServiceDescription}'>");
                htmlBuilder.Append($"   <a href='#' class='webserviceElementContextMenu tree-view-menu-wsdl-interface' can-model-reference='true' can-schema-mapping='false' has-model-reference='{hasSawsdlModelReference.ToString().ToLower()}' has-lifting-schema-mapping='false' has-lowering-schema-mapping='false'>".Trim());
                htmlBuilder.Append($"       <i class='fa fa-circle-o fa-cicle-o-colored'></i>".Trim());
                htmlBuilder.Append($"       <span data-placement='right' title='Interface [{wsdlInterface.WsdlInterfaceName}]'>{interfaceDisplayName}</span>".Trim());
                htmlBuilder.Append($"       <span id='annotation-span-{wsdlInterface.Id}'>".Trim());

                if (hasSawsdlModelReference)
                {
                    htmlBuilder.Append($"       &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Model Reference'><i class='fa fa-check-circle' style='color: {modelReferenceColor}'></i></span>".Trim());
                }

                htmlBuilder.Append($"       </span>".Trim());
                htmlBuilder.Append($"       <span class='pull-right-container'>".Trim());
                htmlBuilder.Append($"           <i class='fa fa-angle-right pull-left'></i>".Trim());
                htmlBuilder.Append($"       </span>".Trim());
                htmlBuilder.Append($"   </a>".Trim());

                var wsdlOperationsHtmlTreeViewMenu = CreateHtmlTreeViewMenuForWsdlOperations(wsdlInterface.WsdlOperations, idServiceDescription);

                htmlBuilder.Append(wsdlOperationsHtmlTreeViewMenu);

                //Closing LI for Wsdl Interface
                htmlBuilder.Append($"</li>");
            }

            //Closing UL for Web Service Interfaces
            htmlBuilder.Append($"</ul>".Trim());

            return htmlBuilder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlOperations"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on CreateHtmlTreeViewMenuForWsdlOperations</param>
        /// <returns></returns>
        private string CreateHtmlTreeViewMenuForWsdlOperations(ICollection<WsdlOperation> wsdlOperations, int idServiceDescription)
        {
            var modelReferenceColor = "#00a65a";
            //var schemaMappingColor = "#00a65a";

            var htmlBuilder = new StringBuilder();

            //Opening UL for Operations
            htmlBuilder.Append($"<ul class='treeview-menu'>");

            //Operations
            foreach (var wsdlOperation in wsdlOperations)
            {
                var operationDisplayName = wsdlOperation.WsdlOperationName;

                //if (wsdlOperation.WsdlOperationName.Length > 20)
                //{
                //    operationDisplayName = $"{wsdlOperation.WsdlOperationName.Substring(0, 19)}...";
                //}

                operationDisplayName = $"[op] {operationDisplayName}";

                var hasSawsdlModelReference = wsdlOperation.SawsdlModelReferences != null && wsdlOperation.SawsdlModelReferences.Any();

                //Opening LI for Operation
                htmlBuilder.Append($"<li class='treeview' id-wsdl-element='{wsdlOperation.Id}' wsdl-element-name='{wsdlOperation.WsdlOperationName}' ");
                htmlBuilder.Append($" wsdl-element-type='{ServiceDescriptionElementTypeEnum.WsdlOperation.GetEnumDescription()}' id-wsdl-element-type='{(int)ServiceDescriptionElementTypeEnum.WsdlOperation}' ");
                htmlBuilder.Append($" id-service-description='{idServiceDescription}'>");
                htmlBuilder.Append($"   <a href='#' class='webserviceElementContextMenu tree-view-menu-wsdl-operation' can-model-reference='true' can-schema-mapping='false' has-model-reference='{hasSawsdlModelReference.ToString().ToLower()}' has-lifting-schema-mapping='false' has-lowering-schema-mapping='false'>".Trim());
                htmlBuilder.Append($"       <i class='fa fa-circle-o fa-cicle-o-colored'></i>".Trim());
                htmlBuilder.Append($"       <span data-placement='right' title='Operation [{wsdlOperation.WsdlOperationName}]'>{operationDisplayName}</span>".Trim());
                htmlBuilder.Append($"       <span id='annotation-span-{wsdlOperation.Id}'>".Trim());

                if (hasSawsdlModelReference)
                {
                    htmlBuilder.Append($"       &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Model Reference'><i class='fa fa-check-circle' style='color: {modelReferenceColor};'></i></span>".Trim());
                }

                htmlBuilder.Append($"       </span>".Trim());
                htmlBuilder.Append($"       <span class='pull-right-container'>".Trim());
                htmlBuilder.Append($"           <i class='fa fa-angle-right pull-left'></i>".Trim());
                htmlBuilder.Append($"       </span>".Trim());
                htmlBuilder.Append($"   </a>".Trim());

                //Opening UL for Inputs and Outputs
                htmlBuilder.Append($"   <ul class='treeview-menu'>".Trim());

                var wsdlInputsHtmlTreeViewMenu = CreateHtmlTreeViewMenuForWsdlInputs(wsdlOperation.WsdlInputs, idServiceDescription);

                htmlBuilder.Append(wsdlInputsHtmlTreeViewMenu);

                var wsdlOutputsHtmlTreeViewMenu = CreateHtmlTreeViewMenuForWsdlOutputs(wsdlOperation.WsdlOutputs, idServiceDescription);

                htmlBuilder.Append(wsdlOutputsHtmlTreeViewMenu);

                var wsdlInfaultsHtmlTreeViewMenu = CreateHtmlTreeViewMenuForWsdlInfaults(wsdlOperation.WsdlInfaults, idServiceDescription);

                htmlBuilder.Append(wsdlInfaultsHtmlTreeViewMenu);

                var wsdlOutfaultsHtmlTreeViewMenu = CreateHtmlTreeViewMenuForWsdlOutfaults(wsdlOperation.WsdlOutfaults, idServiceDescription);

                htmlBuilder.Append(wsdlOutfaultsHtmlTreeViewMenu);

                //Closing UL for Inputs and Outputs
                htmlBuilder.Append($"   </ul>".Trim());

                //Closing LI for Operation
                htmlBuilder.Append($"</li>");
            }

            //Closing UL for Operations
            htmlBuilder.Append($"</ul>");

            return htmlBuilder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlOutfaults"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on CreateHtmlTreeViewMenuForWsdlOutfaults</param>
        /// <returns></returns>
        private string CreateHtmlTreeViewMenuForWsdlOutfaults(ICollection<WsdlOutfault> wsdlOutfaults, int idServiceDescription)
        {
            var htmlBuilder = new StringBuilder();

            return htmlBuilder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wsdlOutputs"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on CreateHtmlTreeViewMenuForWsdlOutputs</param>
        /// <returns></returns>
        private string CreateHtmlTreeViewMenuForWsdlOutputs(ICollection<WsdlOutput> wsdlOutputs, int idServiceDescription)
        {
            var htmlBuilder = new StringBuilder();

            foreach (var wsdlOutput in wsdlOutputs)
            {
                var outputDisplayName = wsdlOutput.WsdlOutputName;

                //if (wsdlOutput.WsdlOutputName.Length > 11)
                //{
                //    outputDisplayName = $"{wsdlOutput.WsdlOutputName.Substring(0, 10)}...";
                //}

                outputDisplayName = $"[out] {outputDisplayName}";

                //Opening LI for Output
                htmlBuilder.Append($"<li class='treeview' id-wsdl-element='{wsdlOutput.Id}' wsdl-element-name='{wsdlOutput.WsdlOutputName}' ");
                htmlBuilder.Append($" wsdl-element-type='{ServiceDescriptionElementTypeEnum.WsdlOutput.GetEnumDescription()}' id-wsdl-element-type='{(int)ServiceDescriptionElementTypeEnum.WsdlOutput}' ");
                htmlBuilder.Append($" id-service-description='{idServiceDescription}'>");
                htmlBuilder.Append($"   <a class='tree-view-menu-wsdl-output' href='#'>".Trim());
                htmlBuilder.Append($"       <i class='fa fa-circle-o fa-cicle-o-colored'></i>".Trim());
                htmlBuilder.Append($"       <span data-placement='right' title='Output [{wsdlOutput.WsdlOutputName}]'>{outputDisplayName}</span>".Trim());
                htmlBuilder.Append($"       <span class='pull-right-container'>".Trim());
                htmlBuilder.Append($"           <i class='fa fa-angle-right pull-left'></i>".Trim());
                htmlBuilder.Append($"       </span>".Trim());
                htmlBuilder.Append($"   </a>".Trim());

                //Opening UL for Complex Elements and Simple Elements
                htmlBuilder.Append($"   <ul class='treeview-menu'>".Trim());

                if (wsdlOutput.XsdComplexType != null)
                {
                    var xsdComplexTypesHtmlTreeViewMenu = CreateHtmlTreeViewMenuForXsdComplexType(wsdlOutput.XsdComplexType, idServiceDescription);

                    htmlBuilder.Append(xsdComplexTypesHtmlTreeViewMenu);
                }

                if (wsdlOutput.XsdSimpleType != null)
                {
                    var xsdSimpleTypesHtmlTreeViewMenu = CreateHtmlTreeViewMenuForXsdSimpleType(new List<XsdSimpleType> { wsdlOutput.XsdSimpleType }, idServiceDescription);

                    htmlBuilder.Append(xsdSimpleTypesHtmlTreeViewMenu);
                }

                //Closing UL for Complex Elements and Simple Elements
                htmlBuilder.Append($"   </ul>".Trim());

                //Closing LI for Output
                htmlBuilder.Append($"</li>");
            }

            return htmlBuilder.ToString();
        }

        #endregion Html for Wsdl Elements

        #region Html for Xsd Complex Elements and Xsd Simple Elements

        private string CreateHtmlTreeViewMenuForXsdComplexType(XsdComplexType xsdComplexType, int idServiceDescription)
        {
            var modelReferenceColor = "#00a65a";
            var schemaMappingColor = "#00a65a";

            var htmlBuilder = new StringBuilder();

            var ComplexTypeDisplayName = xsdComplexType.XsdComplexTypeName;

            //if (xsdComplexType.XsdComplexTypeName.Length > 16)
            //{
            //    ComplexTypeDisplayName = $"{xsdComplexType.XsdComplexTypeName.Substring(0, 15)}...";
            //}

            ComplexTypeDisplayName = $"[c-t] {ComplexTypeDisplayName}";

            var hasSawsdlModelReference = xsdComplexType.SawsdlModelReferences != null && xsdComplexType.SawsdlModelReferences.Any();
            var hasLiftingSchemaMapping = !string.IsNullOrEmpty(xsdComplexType.LiftingSchemaMapping);
            var hasLoweringSchemaMapping = !string.IsNullOrEmpty(xsdComplexType.LoweringSchemaMapping);

            var hasXsdSimpleTypesElements = xsdComplexType.XsdSimpleTypes != null && xsdComplexType.XsdSimpleTypes.Any();
            var hasXsdComplexTypesElements = xsdComplexType.ChildrenXsdComplexTypes != null && xsdComplexType.ChildrenXsdComplexTypes.Any();

            //Opening LI for Complex Element
            htmlBuilder.Append($"<li class='treeview' id-wsdl-element='{xsdComplexType.Id}' wsdl-element-name='{xsdComplexType.XsdComplexTypeName}' ");
            htmlBuilder.Append($" wsdl-element-type='{ServiceDescriptionElementTypeEnum.XsdComplexType.GetEnumDescription()}' id-wsdl-element-type='{(int)ServiceDescriptionElementTypeEnum.XsdComplexType}' ");
            htmlBuilder.Append($" id-service-description='{idServiceDescription}'>");
            htmlBuilder.Append($"   <a href='#' class='webserviceElementContextMenu tree-view-menu-xsd-complex-type' can-model-reference='true' can-schema-mapping='true' has-model-reference='{hasSawsdlModelReference.ToString().ToLower()}' has-lifting-schema-mapping='{hasLiftingSchemaMapping.ToString().ToLower()}' has-lowering-schema-mapping='{hasLoweringSchemaMapping.ToString().ToLower()}'>".Trim());
            htmlBuilder.Append($"       <i class='fa fa-circle-o fa-cicle-o-colored'></i>".Trim());
            htmlBuilder.Append($"       <span data-placement='right' title='Complex Element [{xsdComplexType.XsdComplexTypeName}]'>{ComplexTypeDisplayName}</span>".Trim());
            htmlBuilder.Append($"       <span id='annotation-span-{xsdComplexType.Id}'>".Trim());

            if (hasSawsdlModelReference)
            {
                htmlBuilder.Append($"       &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Model Reference'><i class='fa fa-check-circle' style='color: {modelReferenceColor};'></i></span>".Trim());
            }
            if (hasLiftingSchemaMapping)
            {
                htmlBuilder.Append($"       &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Lifting Schema Mapping'><i class='fa fa-arrow-circle-up' style='color: {schemaMappingColor};'></i></span>".Trim());
            }
            if (hasLoweringSchemaMapping)
            {
                htmlBuilder.Append($"       &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Lowering Schema Mapping'><i class='fa fa-arrow-circle-down' style='color: {schemaMappingColor};'></i></span>".Trim());
            }

            htmlBuilder.Append($"       </span>".Trim());

            if (hasXsdSimpleTypesElements || hasXsdComplexTypesElements)
            {
                htmlBuilder.Append($"       <span class='pull-right-container'>".Trim());
                htmlBuilder.Append($"           <i class='fa fa-angle-right pull-left'></i>".Trim());
                htmlBuilder.Append($"       </span>".Trim());
            }

            htmlBuilder.Append($"   </a>".Trim());

            if (hasXsdComplexTypesElements)
            {
                //Opening UL for NESTED Complex Elements and Simple Elements
                htmlBuilder.Append($"   <ul class='treeview-menu'>".Trim());

                foreach (var childXsdComplexType in xsdComplexType.ChildrenXsdComplexTypes)
                {
                    if (childXsdComplexType.XsdComplexTypeElementType != xsdComplexType.XsdComplexTypeElementType)
                    {
                        htmlBuilder.Append(CreateHtmlTreeViewMenuForXsdComplexType(childXsdComplexType, idServiceDescription));
                    }
                }

                //Closing UL for NESTED Complex Elements and Simple Elements
                htmlBuilder.Append($"   </ul>".Trim());
            }

            if (hasXsdSimpleTypesElements)
            {
                //Opening UL for NESTED Complex Elements and Simple Elements
                htmlBuilder.Append($"   <ul class='treeview-menu'>".Trim());

                htmlBuilder.Append(CreateHtmlTreeViewMenuForXsdSimpleType(xsdComplexType.XsdSimpleTypes, idServiceDescription));

                //Closing UL for NESTED Complex Elements and Simple Elements
                htmlBuilder.Append($"   </ul>".Trim());
            }

            //Closing LI for Complex Element
            htmlBuilder.Append($"</li>");

            return htmlBuilder.ToString();
        }

        private string CreateHtmlTreeViewMenuForXsdSimpleType(ICollection<XsdSimpleType> xsdSimpleTypes, int idServiceDescription)
        {
            var modelReferenceColor = "#00a65a";
            var schemaMappingColor = "#00a65a";

            var htmlBuilder = new StringBuilder();

            foreach (var xsdSimpleType in xsdSimpleTypes)
            {
                var simpleTypeDisplayName = xsdSimpleType.XsdSimpleTypeName;

                //if (xsdSimpleType.XsdSimpleTypeName.Length > 11)
                //{
                //    ComplexTypeDisplayName = $"{xsdSimpleType.XsdSimpleTypeName.Substring(0, 10)}...";
                //}

                simpleTypeDisplayName = $"[s-t] {simpleTypeDisplayName}";

                var hasSawsdlModelReference = xsdSimpleType.SawsdlModelReferences != null && xsdSimpleType.SawsdlModelReferences.Any();
                var hasLiftingSchemaMapping = !string.IsNullOrEmpty(xsdSimpleType.LiftingSchemaMapping);
                var hasLoweringSchemaMapping = !string.IsNullOrEmpty(xsdSimpleType.LoweringSchemaMapping);

                //Opening LI for Simple Element
                htmlBuilder.Append($"<li class='treeview' id-wsdl-element='{xsdSimpleType.Id}' wsdl-element-name='{xsdSimpleType.XsdSimpleTypeName}' ");
                htmlBuilder.Append($" wsdl-element-type='{ServiceDescriptionElementTypeEnum.XsdSimpleType.GetEnumDescription()}' id-wsdl-element-type='{(int)ServiceDescriptionElementTypeEnum.XsdSimpleType}' ");
                htmlBuilder.Append($" id-service-description='{idServiceDescription}'>");
                htmlBuilder.Append($"   <a href='#' class='webserviceElementContextMenu tree-view-menu-xsd-simple-type' can-model-reference='true' can-schema-mapping='true' has-model-reference='{hasSawsdlModelReference.ToString().ToLower()}' has-lifting-schema-mapping='{hasLiftingSchemaMapping.ToString().ToLower()}' has-lowering-schema-mapping='{hasLoweringSchemaMapping.ToString().ToLower()}'>".Trim());
                htmlBuilder.Append($"       <i class='fa fa-circle-o fa-cicle-o-colored'></i>".Trim());
                htmlBuilder.Append($"       <span data-placement='right' title='Simple Type [{xsdSimpleType.XsdSimpleTypeName}]'>{simpleTypeDisplayName}</span>".Trim());

                if (hasSawsdlModelReference)
                {
                    htmlBuilder.Append($"   &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Model Reference'><i class='fa fa-check-circle' style='color: {modelReferenceColor};'></i></span>".Trim());
                }
                if (hasLiftingSchemaMapping)
                {
                    htmlBuilder.Append($"   &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Lifting Schema Mapping'><i class='fa fa-arrow-circle-up' style='color: {schemaMappingColor};'></i></span>");
                }
                if (hasLoweringSchemaMapping)
                {
                    htmlBuilder.Append($"   &nbsp;&nbsp;<span data-toggle='tooltip' data-placement='top' title='SAWSDL Lowering Schema Mapping'><i class='fa fa-arrow-circle-down' style='color: {schemaMappingColor};'></i></span>");
                }

                htmlBuilder.Append($"   </a>".Trim());

                //Closing LI for Complex Element
                htmlBuilder.Append($"</li>");
            }

            return htmlBuilder.ToString();
        }

        #endregion Html for Xsd Complex Elements and Xsd Simple Elements

        #region Graph Node Positions

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="idServiceDescription"></param>
        /// <param name="ontologyTermNodes"></param>
        private void SetExistingPositionsForOntologyTermNodes(int idOwnerUser, int idServiceDescription, IEnumerable<IGraphNode> ontologyTermNodes)
        {
            foreach (var sawsdlModelReferenceNode in ontologyTermNodes)
            {
                var ontologyTerms = _ontologyTermRepository.GetWithNodePositionsByIdOntologyTerm(sawsdlModelReferenceNode.Data.IdOntologyTerm, false);

                foreach (var ontologyTerm in ontologyTerms)
                {
                    var customUserPosition = ontologyTerm.GraphNodePosition_OntologyTerms.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                    if (customUserPosition != null)
                    {
                        sawsdlModelReferenceNode.Position = new CytoscapeNodePosition
                        {
                            X = customUserPosition.X,
                            Y = customUserPosition.Y
                        };
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="idServiceDescription"></param>
        /// <param name="sawsdlModelReferenceNodes"></param>
        private void SetExistingPositionsForSawsdlModelReferenceNodes(int idOwnerUser, int idServiceDescription, IEnumerable<IGraphNode> sawsdlModelReferenceNodes)
        {
            foreach (var sawsdlModelReferenceNode in sawsdlModelReferenceNodes)
            {
                var sawsdlModelReferences = _sawsdlModelReferenceRepository.GetWithNodePositionsByIdOntologyTermAndIdServiceDescription(sawsdlModelReferenceNode.Data.IdOntologyTerm, idServiceDescription, false);

                foreach (var sawsdlModelReference in sawsdlModelReferences)
                {
                    var customUserPosition = sawsdlModelReference.GraphNodePosition_SawsdlModelReferences.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                    if (customUserPosition != null)
                    {
                        sawsdlModelReferenceNode.Position = new CytoscapeNodePosition
                        {
                            X = customUserPosition.X,
                            Y = customUserPosition.Y
                        };
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="wsdlInfaultNodes"></param>
        private void SetExistingPositionsForWsdlInfaultNodes(int idOwnerUser, IEnumerable<IGraphNode> wsdlInfaultNodes)
        {
            foreach (var wsdlInfaultNode in wsdlInfaultNodes)
            {
                var wsdlInfault = _wsdlInfaultRepository.GetWithNodePositions(wsdlInfaultNode.Data.IdWsdlElement, false);
                var customUserPosition = wsdlInfault.GraphNodePosition_WsdlInfaults.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    wsdlInfaultNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="wsdlInputNodes"></param>
        private void SetExistingPositionsForWsdlInputNodes(int idOwnerUser, IEnumerable<IGraphNode> wsdlInputNodes)
        {
            foreach (var wsdlInputNode in wsdlInputNodes)
            {
                var wsdlInput = _wsdlInputRepository.GetWithNodePositions(wsdlInputNode.Data.IdWsdlElement, false);
                var customUserPosition = wsdlInput.GraphNodePosition_WsdlInputs.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    wsdlInputNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="wsdlInterfaceNodes"></param>
        private void SetExistingPositionsForWsdlInterfaceNodes(int idOwnerUser, IEnumerable<IGraphNode> wsdlInterfaceNodes)
        {
            foreach (var wsdlInterfaceNode in wsdlInterfaceNodes)
            {
                var wsdlInterface = _wsdlInterfaceRepository.GetWithNodePositions(wsdlInterfaceNode.Data.IdWsdlElement, false);
                var customUserPosition = wsdlInterface.GraphNodePosition_WsdlInterfaces.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    wsdlInterfaceNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="wsdlOperationNodes"></param>
        private void SetExistingPositionsForWsdlOperationNodes(int idOwnerUser, IEnumerable<IGraphNode> wsdlOperationNodes)
        {
            foreach (var wsdlOperationNode in wsdlOperationNodes)
            {
                var wsdlOperation = _wsdlOperationRepository.GetWithNodePositions(wsdlOperationNode.Data.IdWsdlElement, false);
                var customUserPosition = wsdlOperation.GraphNodePosition_WsdlOperations.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    wsdlOperationNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="wsdlOutfaultNodes"></param>
        private void SetExistingPositionsForWsdlOutfaultNodes(int idOwnerUser, IEnumerable<IGraphNode> wsdlOutfaultNodes)
        {
            foreach (var wsdlOutfaultNode in wsdlOutfaultNodes)
            {
                var wsdlOutfault = _wsdlOutfaultRepository.GetWithNodePositions(wsdlOutfaultNode.Data.IdWsdlElement, false);
                var customUserPosition = wsdlOutfault.GraphNodePosition_WsdlOutfaults.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    wsdlOutfaultNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="wsdlOutputNodes"></param>
        private void SetExistingPositionsForWsdlOutputNodes(int idOwnerUser, IEnumerable<IGraphNode> wsdlOutputNodes)
        {
            foreach (var wsdlOutputNode in wsdlOutputNodes)
            {
                var wsdlOutput = _wsdlOutputRepository.GetWithNodePositions(wsdlOutputNode.Data.IdWsdlElement, false);
                var customUserPosition = wsdlOutput.GraphNodePosition_WsdlOutputs.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    wsdlOutputNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="xsdComplexTypeNodes"></param>
        private void SetExistingPositionsForXsdComplexTypeNodes(int idOwnerUser, IEnumerable<IGraphNode> xsdComplexTypeNodes)
        {
            foreach (var xsdComplexTypeNode in xsdComplexTypeNodes)
            {
                var xsdComplexType = _xsdComplexTypeRepository.GetWithNodePositions(xsdComplexTypeNode.Data.IdWsdlElement, false);
                var customUserPosition = xsdComplexType.GraphNodePosition_XsdComplexTypes.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    xsdComplexTypeNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="xsdSimpleTypeNodes"></param>
        private void SetExistingPositionsForXsdSimpleTypeNodes(int idOwnerUser, IEnumerable<IGraphNode> xsdSimpleTypeNodes)
        {
            foreach (var xsdSimpleTypeNode in xsdSimpleTypeNodes)
            {
                var xsdSimpleType = _xsdSimpleTypeRepository.GetWithNodePositions(xsdSimpleTypeNode.Data.IdWsdlElement, false);
                var customUserPosition = xsdSimpleType.GraphNodePosition_XsdSimpleTypes.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (customUserPosition != null)
                {
                    xsdSimpleTypeNode.Position = new CytoscapeNodePosition
                    {
                        X = customUserPosition.X,
                        Y = customUserPosition.Y
                    };
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="idServiceDescription"></param>
        /// <param name="ontologyTermNode"></param>
        /// <returns></returns>
        private List<OntologyTerm> UpdateOntologyTermNodePosition(int idOwnerUser, int idServiceDescription, IGraphNode ontologyTermNode)
        {
            var idOntologyTerm = ontologyTermNode.Data.IdOntologyTerm;

            var ontologyTerms = _ontologyTermRepository.GetWithNodePositionsByIdOntologyTerm(idOntologyTerm, false).ToList();

            foreach (var ontologyTerm in ontologyTerms)
            {
                if (ontologyTerm.GraphNodePosition_OntologyTerms == null)
                {
                    ontologyTerm.GraphNodePosition_OntologyTerms = new List<GraphNodePosition_OntologyTerm>
                    {
                        new GraphNodePosition_OntologyTerm
                        {
                            IdOntologyTerm = ontologyTerm.Id,
                            X = ontologyTermNode.Position.X,
                            Y = ontologyTermNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
                }
                else
                {
                    var nodePosition = ontologyTerm.GraphNodePosition_OntologyTerms.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                    if (nodePosition == null)
                    {
                        ontologyTerm.GraphNodePosition_OntologyTerms.Add(new GraphNodePosition_OntologyTerm
                        {
                            IdOntologyTerm = ontologyTerm.Id,
                            X = ontologyTermNode.Position.X,
                            Y = ontologyTermNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        });
                    }
                    else
                    {
                        nodePosition.X = ontologyTermNode.Position.X;
                        nodePosition.Y = ontologyTermNode.Position.Y;
                    }
                }
            }

            return ontologyTerms;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOwnerUser"></param>
        /// <param name="idServiceDescription"></param>
        /// <param name="ontologyTermNode"></param>
        /// <returns></returns>
        private List<SawsdlModelReference> UpdateSawsdlModelReferenceNodePosition(int idOwnerUser, int idServiceDescription, IGraphNode ontologyTermNode)
        {
            var idOntologyTerm = ontologyTermNode.Data.IdOntologyTerm;

            var sawsdlModelReferences = _sawsdlModelReferenceRepository.GetWithNodePositionsByIdOntologyTermAndIdServiceDescription(idOntologyTerm, idServiceDescription, @readonly: false).ToList();

            foreach (var sawsdlModelReference in sawsdlModelReferences)
            {
                if (sawsdlModelReference.GraphNodePosition_SawsdlModelReferences == null)
                {
                    sawsdlModelReference.GraphNodePosition_SawsdlModelReferences = new List<GraphNodePosition_SawsdlModelReference>
                    {
                        new GraphNodePosition_SawsdlModelReference
                        {
                            IdSawsdlModelReference = sawsdlModelReference.Id,
                            X = ontologyTermNode.Position.X,
                            Y = ontologyTermNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
                }
                else
                {
                    var nodePosition = sawsdlModelReference.GraphNodePosition_SawsdlModelReferences.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                    if (nodePosition == null)
                    {
                        sawsdlModelReference.GraphNodePosition_SawsdlModelReferences.Add(new GraphNodePosition_SawsdlModelReference
                        {
                            IdSawsdlModelReference = sawsdlModelReference.Id,
                            X = ontologyTermNode.Position.X,
                            Y = ontologyTermNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        });
                    }
                    else
                    {
                        nodePosition.X = ontologyTermNode.Position.X;
                        nodePosition.Y = ontologyTermNode.Position.Y;
                    }
                }
            }

            return sawsdlModelReferences;
        }

        private WsdlInput UpdateWsdlInputNodePosition(int idOwnerUser, IGraphNode wsdlInputNode)
        {
            var idWsdlInput = wsdlInputNode.Data.IdWsdlElement;

            var wsdlInput = _wsdlInputRepository.GetWithNodePositions(idWsdlInput, false);

            if (wsdlInput.GraphNodePosition_WsdlInputs == null)
            {
                wsdlInput.GraphNodePosition_WsdlInputs = new List<GraphNodePosition_WsdlInput>
                    {
                        new GraphNodePosition_WsdlInput
                        {
                            IdWsdlInput = idWsdlInput,
                            X = wsdlInputNode.Position.X,
                            Y = wsdlInputNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
            }
            else
            {
                var nodePosition = wsdlInput.GraphNodePosition_WsdlInputs.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (nodePosition == null)
                {
                    wsdlInput.GraphNodePosition_WsdlInputs.Add(new GraphNodePosition_WsdlInput
                    {
                        IdWsdlInput = idWsdlInput,
                        X = wsdlInputNode.Position.X,
                        Y = wsdlInputNode.Position.Y,
                        IdOwnerUser = idOwnerUser
                    });
                }
                else
                {
                    nodePosition.X = wsdlInputNode.Position.X;
                    nodePosition.Y = wsdlInputNode.Position.Y;
                }
            }

            return wsdlInput;
        }

        private WsdlInterface UpdateWsdlInterfaceNodePosition(int idOwnerUser, IGraphNode wsdlInterfaceNode)
        {
            var idWsdlInterface = wsdlInterfaceNode.Data.IdWsdlElement;

            var wsdlInteface = _wsdlInterfaceRepository.GetWithNodePositions(idWsdlInterface, false);

            if (wsdlInteface.GraphNodePosition_WsdlInterfaces == null)
            {
                wsdlInteface.GraphNodePosition_WsdlInterfaces = new List<GraphNodePosition_WsdlInterface>
                    {
                        new GraphNodePosition_WsdlInterface
                        {
                            IdWsdlInterface = idWsdlInterface,
                            X = wsdlInterfaceNode.Position.X,
                            Y = wsdlInterfaceNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
            }
            else
            {
                var nodePosition = wsdlInteface.GraphNodePosition_WsdlInterfaces.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (nodePosition == null)
                {
                    wsdlInteface.GraphNodePosition_WsdlInterfaces.Add(new GraphNodePosition_WsdlInterface
                    {
                        IdWsdlInterface = idWsdlInterface,
                        X = wsdlInterfaceNode.Position.X,
                        Y = wsdlInterfaceNode.Position.Y,
                        IdOwnerUser = idOwnerUser
                    });
                }
                else
                {
                    nodePosition.X = wsdlInterfaceNode.Position.X;
                    nodePosition.Y = wsdlInterfaceNode.Position.Y;
                }
            }

            return wsdlInteface;
        }

        private WsdlOperation UpdateWsdlOperationNodePosition(int idOwnerUser, IGraphNode wsdlOperationNode)
        {
            var idWsdlOperation = wsdlOperationNode.Data.IdWsdlElement;

            var wsdlOperation = _wsdlOperationRepository.GetWithNodePositions(idWsdlOperation, false);

            if (wsdlOperation.GraphNodePosition_WsdlOperations == null)
            {
                wsdlOperation.GraphNodePosition_WsdlOperations = new List<GraphNodePosition_WsdlOperation>
                    {
                        new GraphNodePosition_WsdlOperation
                        {
                            IdWsdlOperation = idWsdlOperation,
                            X = wsdlOperationNode.Position.X,
                            Y = wsdlOperationNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
            }
            else
            {
                var nodePosition = wsdlOperation.GraphNodePosition_WsdlOperations.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);
                if (nodePosition == null)
                {
                    wsdlOperation.GraphNodePosition_WsdlOperations.Add(new GraphNodePosition_WsdlOperation
                    {
                        IdWsdlOperation = idWsdlOperation,
                        X = wsdlOperationNode.Position.X,
                        Y = wsdlOperationNode.Position.Y,
                        IdOwnerUser = idOwnerUser
                    });
                }
                else
                {
                    nodePosition.X = wsdlOperationNode.Position.X;
                    nodePosition.Y = wsdlOperationNode.Position.Y;
                }
            }

            return wsdlOperation;
        }

        private WsdlOutput UpdateWsdlOutputNodePosition(int idOwnerUser, IGraphNode wsdlOutputNode)
        {
            var idWsdlOutput = wsdlOutputNode.Data.IdWsdlElement;

            var wsdlOutput = _wsdlOutputRepository.GetWithNodePositions(idWsdlOutput, false);

            if (wsdlOutput.GraphNodePosition_WsdlOutputs == null)
            {
                wsdlOutput.GraphNodePosition_WsdlOutputs = new List<GraphNodePosition_WsdlOutput>
                    {
                        new GraphNodePosition_WsdlOutput
                        {
                            IdWsdlOutput = idWsdlOutput,
                            X = wsdlOutputNode.Position.X,
                            Y = wsdlOutputNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
            }
            else
            {
                var nodePosition = wsdlOutput.GraphNodePosition_WsdlOutputs.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (nodePosition == null)
                {
                    wsdlOutput.GraphNodePosition_WsdlOutputs.Add(new GraphNodePosition_WsdlOutput
                    {
                        IdWsdlOutput = idWsdlOutput,
                        X = wsdlOutputNode.Position.X,
                        Y = wsdlOutputNode.Position.Y,
                        IdOwnerUser = idOwnerUser
                    });
                }
                else
                {
                    nodePosition.X = wsdlOutputNode.Position.X;
                    nodePosition.Y = wsdlOutputNode.Position.Y;
                }
            }

            return wsdlOutput;
        }

        private XsdComplexType UpdateXsdComplexTypeNodePosition(int idOwnerUser, IGraphNode xsdComplexTypeNode)
        {
            var idComplexType = xsdComplexTypeNode.Data.IdWsdlElement;

            var xsdComplexType = _xsdComplexTypeRepository.GetWithNodePositions(idComplexType, false);

            if (xsdComplexType.GraphNodePosition_XsdComplexTypes == null)
            {
                xsdComplexType.GraphNodePosition_XsdComplexTypes = new List<GraphNodePosition_XsdComplexType>
                    {
                        new GraphNodePosition_XsdComplexType
                        {
                            IdXsdComplexType = idComplexType,
                            X = xsdComplexTypeNode.Position.X,
                            Y = xsdComplexTypeNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
            }
            else
            {
                var nodePosition = xsdComplexType.GraphNodePosition_XsdComplexTypes.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (nodePosition == null)
                {
                    xsdComplexType.GraphNodePosition_XsdComplexTypes.Add(new GraphNodePosition_XsdComplexType
                    {
                        IdXsdComplexType = idComplexType,
                        X = xsdComplexTypeNode.Position.X,
                        Y = xsdComplexTypeNode.Position.Y,
                        IdOwnerUser = idOwnerUser
                    });
                }
                else
                {
                    nodePosition.X = xsdComplexTypeNode.Position.X;
                    nodePosition.Y = xsdComplexTypeNode.Position.Y;
                }
            }

            return xsdComplexType;
        }

        private XsdSimpleType UpdateXsdSimpleTypeNodePosition(int idOwnerUser, IGraphNode xsdSimpleTypeNode)
        {
            var idSimpleType = xsdSimpleTypeNode.Data.IdWsdlElement;

            var xsdSimpleType = _xsdSimpleTypeRepository.GetWithNodePositions(idSimpleType, false);

            if (xsdSimpleType.GraphNodePosition_XsdSimpleTypes == null)
            {
                xsdSimpleType.GraphNodePosition_XsdSimpleTypes = new List<GraphNodePosition_XsdSimpleType>
                    {
                        new GraphNodePosition_XsdSimpleType
                        {
                            IdXsdSimpleType = idSimpleType,
                            X = xsdSimpleTypeNode.Position.X,
                            Y = xsdSimpleTypeNode.Position.Y,
                            IdOwnerUser = idOwnerUser
                        }
                    };
            }
            else
            {
                var nodePosition = xsdSimpleType.GraphNodePosition_XsdSimpleTypes.FirstOrDefault(x => x.IdOwnerUser == idOwnerUser);

                if (nodePosition == null)
                {
                    xsdSimpleType.GraphNodePosition_XsdSimpleTypes.Add(new GraphNodePosition_XsdSimpleType
                    {
                        IdXsdSimpleType = idSimpleType,
                        X = xsdSimpleTypeNode.Position.X,
                        Y = xsdSimpleTypeNode.Position.Y,
                        IdOwnerUser = idOwnerUser
                    });
                }
                else
                {
                    nodePosition.X = xsdSimpleTypeNode.Position.X;
                    nodePosition.Y = xsdSimpleTypeNode.Position.Y;
                }
            }

            return xsdSimpleType;
        }

        #endregion Graph Node Positions

        #region Sharing

        private void RemoveIssuesByServiceDescription(int idServiceDescription)
        {
            var issues = _issueEntityRepository.GetAllByServiceDescription(idServiceDescription, false).ToList();

            if (issues.Any())
            {
                var issueIds = issues.Select(x => x.Id);
                var issuesAnswers = _issueAnswerEntityRepository.GetAll(@readonly: false).Where(x => issueIds.Contains(x.IdIssue)).ToList();

                if (issuesAnswers.Any())
                {
                    _issueAnswerEntityRepository.RemoveRange(issuesAnswers);

                    _issueAnswerEntityRepository.SaveChanges();
                }

                _issueEntityRepository.RemoveRange(issues);

                _issueEntityRepository.SaveChanges();
            }
        }

        private void RemoveServiceDescriptionOntologiesLinkByServiceDescription(int idServiceDescription)
        {
            var serviceDescription_Ontologies = _serviceDescription_OntologyRepository.GetAllByServiceDescription(idServiceDescription, @readonly: false).ToList();

            if (serviceDescription_Ontologies.Any())
            {
                _serviceDescription_OntologyRepository.RemoveRange(serviceDescription_Ontologies);

                _serviceDescription_OntologyRepository.SaveChanges();
            }
        }

        private void RemoveSharedService(int idServiceDescription_User)
        {
            var shared = _serviceDescription_UserRepository.Get(idServiceDescription_User, @readonly: false);

            if (shared != null)
            {
                _serviceDescription_UserRepository.Remove(shared.Id);

                _serviceDescription_UserRepository.SaveChanges();
            }
        }

        private void RemoveSharedServiceByServiceDescription(int idServiceDescription)
        {
            var shared = _serviceDescription_UserRepository.GetAllByServiceDescription(idServiceDescription, @readonly: false).ToList();

            if (shared.Any())
            {
                _serviceDescription_UserRepository.RemoveRange(shared);

                _serviceDescription_UserRepository.SaveChanges();
            }
        }

        private void RemoveSharedServiceByServiceDescriptionAndSharedUser(int idServiceDescription, int idSharedUser)
        {
            var serviceDescription_User = _serviceDescription_UserRepository.GetAllByServiceDescriptionAndSharedUser(idServiceDescription, idSharedUser, @readonly: false);

            if (serviceDescription_User != null)
            {
                _serviceDescription_UserRepository.Remove(serviceDescription_User.Id);

                _serviceDescription_UserRepository.SaveChanges();
            }
            else
            {
                throw new ServiceDescriptionNotSharedWithUserException();
            }
        }

        private void RemoveShareInvitations(int idServiceDescription)
        {
            var shareInvitations = _shareInvitationRepository.GetAll(@readonly: false)
                .Where(x => x.IdServiceDescription == idServiceDescription).ToList();

            if (shareInvitations.Any())
            {
                var serviceName = shareInvitations.FirstOrDefault().ServiceDescription.ServiceName;

                SendShareInvitationCancelation(serviceName, shareInvitations.Select(x => x.Email).ToArray());

                _shareInvitationRepository.RemoveRange(shareInvitations);

                _shareInvitationRepository.SaveChanges();
            }
        }

        private void RemoveTasksByServiceDescription(int idServiceDescription)
        {
            var tasks = _taskEntityRepository.GetAllByServiceDescription(idServiceDescription, @readonly: false).ToList();

            if (tasks.Any())
            {
                var taskIds = tasks.Select(x => x.Id);
                var tasksComments = _taskCommentEntityRepository.GetAll(@readonly: false).Where(x => taskIds.Contains(x.IdTask)).ToList();

                if (tasksComments.Any())
                {
                    _taskCommentEntityRepository.RemoveRange(tasksComments);

                    _taskCommentEntityRepository.SaveChanges();
                }

                _taskEntityRepository.RemoveRange(tasks);

                _taskEntityRepository.SaveChanges();
            }
        }

        private void SendShareInvitationCancelation(string serviceName, string[] emails)
        {
            var html = EmailResources.EmailShareInvitationCancelation;
            //var baseUrlForAcceptInvitation = ConfigurationManagerHelper.BaseUrlForAcceptInvitation;

            var messageBody = string.Format(html, serviceName);

            var mail = new MailMessage
            {
                From = new MailAddress("matheuscalache@usp.br", "Matheus de Lara Calache"),
                Subject = "Grasews | Service description removed",
                Body = messageBody,
                IsBodyHtml = true
            };
            for (int i = 0; i < emails.Length; i++)
            {
                mail.To.Add(emails[i]);
            }

            using (var smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential("mlcalache@gmail.com", CryptHelper.Decrypt(ConfigurationManagerHelper.EmailPassword));

                smtp.SendMailAsync(mail);
            }
        }

        #endregion Sharing

        #region Gets

        private XsdComplexType FindParentComplexType(XElement xElement, ICollection<XsdComplexType> xsdComplexTypes)
        {
            if (xElement.Parent.Name == xsdNamespace + "complexType")
            {
                return xsdComplexTypes.FirstOrDefault(ct => ct.XsdComplexTypeName == (string)xElement.Parent.Attribute("name"));
            }
            else
            {
                return FindParentComplexType(xElement.Parent, xsdComplexTypes);
            }
        }

        private void GetWsdlInputs(XDocument xDocument, ServiceDescription serviceDescription, WsdlInterface wsdlInterface, WsdlOperation wsdlOperation)
        {
            var inputsNames = xDocument.Descendants(wsdlNamespace + "input")
                .Where(x => (string)x.Parent.Attribute("name") == wsdlOperation.WsdlOperationName && x.Parent.Name == wsdlNamespace + "operation")
                .Where(x => (string)x.Parent.Parent.Attribute("name") == wsdlInterface.WsdlInterfaceName && x.Parent.Parent.Name == wsdlNamespace + "interface")
                .Select(x => x.Attribute("element").Value.IndexOf(':') != -1 ? x.Attribute("element").Value.Remove(0, x.Attribute("element").Value.IndexOf(':') + 1) : x.Attribute("element").Value);

            inputsNames = inputsNames.Where(x => x != "#none");

            var wsdlInputs = new List<WsdlInput>();
            WsdlInput wsdlInput = null;

            foreach (var inputName in inputsNames)
            {
                wsdlInput = new WsdlInput { WsdlOperation = wsdlOperation, WsdlInputName = inputName };

                var complexTypeFound = serviceDescription.XsdDocument.XsdComplexTypes.FirstOrDefault(w => w.XsdComplexTypeName == inputName);

                if (complexTypeFound != null)
                {
                    complexTypeFound.WsdlInput = wsdlInput;
                    //complexTypeFound.XsdToWsdlRelationType = XsdToWsdlRelationTypeEnum.WsdlInput;
                    wsdlInput.XsdComplexType = complexTypeFound;
                }
                else
                {
                    var simpleTypeFound = serviceDescription.XsdDocument.XsdSimpleTypes.FirstOrDefault(w => w.XsdSimpleTypeName == inputName);

                    if (simpleTypeFound != null)
                    {
                        simpleTypeFound.WsdlInput = wsdlInput;
                        //simpleTypeFound.XsdToWsdlRelationType = XsdToWsdlRelationTypeEnum.WsdlInput;
                        wsdlInput.XsdSimpleType = simpleTypeFound;
                    }
                }

                wsdlInputs.Add(wsdlInput);
            }

            wsdlOperation.WsdlInputs = wsdlInputs;
        }

        private void GetWsdlInterfaces(XDocument xDocument, ServiceDescription serviceDescription)
        {
            var interfaces = xDocument.Descendants(wsdlNamespace + "interface")
                .Select(x => new WsdlInterface { WsdlInterfaceName = (string)x.Attribute("name"), ServiceDescription = serviceDescription })
                .ToList();

            serviceDescription.WsdlInterfaces = interfaces;
        }

        private void GetWsdlOperations(XDocument xDocument, WsdlInterface wsdlInterface)
        {
            var operations = xDocument.Descendants(wsdlNamespace + "operation")
                .Where(x => (string)x.Parent.Attribute("name") == wsdlInterface.WsdlInterfaceName && x.Parent.Name == wsdlNamespace + "interface")
                .Select(x => new WsdlOperation { WsdlOperationName = (string)x.Attribute("name"), WsdlInterface = wsdlInterface })
                .ToList();

            wsdlInterface.WsdlOperations = operations;
        }

        private void GetWsdlOutputs(XDocument xDocument, ServiceDescription serviceDescription, WsdlInterface wsdlInterface, WsdlOperation wsdlOperation)
        {
            var outputsNames = xDocument.Descendants(wsdlNamespace + "output")
                .Where(x => (string)x.Parent.Attribute("name") == wsdlOperation.WsdlOperationName && x.Parent.Name == wsdlNamespace + "operation")
                .Where(x => (string)x.Parent.Parent.Attribute("name") == wsdlInterface.WsdlInterfaceName && x.Parent.Parent.Name == wsdlNamespace + "interface")
                .Select(x => x.Attribute("element").Value.IndexOf(':') != -1 ? x.Attribute("element").Value.Remove(0, x.Attribute("element").Value.IndexOf(':') + 1) : x.Attribute("element").Value);

            outputsNames = outputsNames.Where(x => x != "#none");

            var wsdlOutputs = new List<WsdlOutput>();
            WsdlOutput wsdlOutput = null;

            foreach (var outputName in outputsNames)
            {
                wsdlOutput = new WsdlOutput { WsdlOperation = wsdlOperation, WsdlOutputName = outputName };

                var complexTypeFound = serviceDescription.XsdDocument.XsdComplexTypes.FirstOrDefault(w => w.XsdComplexTypeName == outputName);

                if (complexTypeFound != null)
                {
                    complexTypeFound.WsdlOutput = wsdlOutput;
                    //complexTypeFound.XsdToWsdlRelationType = XsdToWsdlRelationTypeEnum.WsdlOutput;
                    wsdlOutput.XsdComplexType = complexTypeFound;
                }
                else
                {
                    var simpleTypeFound = serviceDescription.XsdDocument.XsdSimpleTypes.FirstOrDefault(w => w.XsdSimpleTypeName == outputName);

                    if (simpleTypeFound != null)
                    {
                        simpleTypeFound.WsdlOutput = wsdlOutput;
                        //simpleTypeFound.XsdToWsdlRelationType = XsdToWsdlRelationTypeEnum.WsdlOutput;
                        wsdlOutput.XsdSimpleType = simpleTypeFound;
                    }
                }

                wsdlOutputs.Add(wsdlOutput);
            }

            wsdlOperation.WsdlOutputs = wsdlOutputs;
        }

        private List<XsdComplexType> GetXsdComplexTypesFromRoot(XDocument xDocument)
        {
            var xsdComplexTypes = new List<XsdComplexType>();
            XsdComplexType xsdComplexType;

            var complexTypeXElements = xDocument.Descendants(xsdNamespace + "complexType")
                .Where(x => x.Parent.Name == xsdNamespace + "schema")
                .Distinct()
                .ToList();

            var elementComplexTypeXElements = xDocument.Descendants(xsdNamespace + "complexType")
                .Where(x => x.Parent.Name == xsdNamespace + "element")
                .Distinct()
                .ToList();

            foreach (var complexTypeXElement in complexTypeXElements)
            {
                xsdComplexType = new XsdComplexType
                {
                    XsdComplexTypeName = (string)complexTypeXElement.Attribute("name"),
                    XsdComplexTypeElementType = (string)complexTypeXElement.Attribute("name")
                };

                xsdComplexTypes.Add(xsdComplexType);
            }

            foreach (var complexTypeXElement in elementComplexTypeXElements)
            {
                xsdComplexType = new XsdComplexType
                {
                    XsdComplexTypeName = (string)complexTypeXElement.Parent.Attribute("name"),
                    XsdComplexTypeElementType = (string)complexTypeXElement.Parent.Attribute("name")
                };

                xsdComplexTypes.Add(xsdComplexType);
            }

            foreach (var item in xsdComplexTypes)
            {
                List<XElement> elementsInsideComplexType;

                if (complexTypeXElements.FirstOrDefault(x => (string)x.Attribute("name") == item.XsdComplexTypeName) != null)
                {
                    elementsInsideComplexType = complexTypeXElements
                        .FirstOrDefault(x => (string)x.Attribute("name") == item.XsdComplexTypeName)
                        .Descendants(xsdNamespace + "element")
                        .Where(x => x.Parent.Name == xsdNamespace + "sequence")
                        .Distinct()
                        .ToList();
                }
                else
                {
                    elementsInsideComplexType = elementComplexTypeXElements
                        .FirstOrDefault(x => (string)x.Parent.Attribute("name") == item.XsdComplexTypeName)
                        .Descendants(xsdNamespace + "element")
                        .Where(x => x.Parent.Name == xsdNamespace + "sequence")
                        .Distinct()
                        .ToList();
                }

                if (elementsInsideComplexType != null && elementsInsideComplexType.Any())
                {
                    foreach (var elementInsideComplexType in elementsInsideComplexType)
                    {
                        string nameAttribute;
                        string typeAttribute;
                        string namespacePrefixTypeAttribute;

                        typeAttribute = (string)elementInsideComplexType.Attribute("type");

                        var namespacePositionToSkip = typeAttribute.Contains(":")
                            ? typeAttribute.IndexOf(":") + 1
                            : 0;

                        typeAttribute = typeAttribute.Substring(namespacePositionToSkip);
                        namespacePrefixTypeAttribute = ((string)elementInsideComplexType.Attribute("type")).Substring(0, namespacePositionToSkip - 1);

                        if (xsdNamespace.NamespaceName == elementInsideComplexType.GetNamespaceOfPrefix(namespacePrefixTypeAttribute).NamespaceName)
                        //TODO
                        //|| typeAttribute == item.XsdComplexTypeName)
                        {
                            nameAttribute = (string)elementInsideComplexType.Attribute("name");
                        }
                        else
                        {
                            nameAttribute = typeAttribute;
                        }

                        if (xsdComplexTypes.Any(x => x.XsdComplexTypeName == nameAttribute))
                        {
                            if (item.ChildrenXsdComplexTypes == null)
                            {
                                item.ChildrenXsdComplexTypes = new List<XsdComplexType>();
                            }

                            if (!item.ChildrenXsdComplexTypes.Any(x => x.XsdComplexTypeName == nameAttribute))
                            {
                                item.ChildrenXsdComplexTypes.Add(xsdComplexTypes.FirstOrDefault(x => x.XsdComplexTypeName == nameAttribute));
                            }
                        }
                        else
                        {
                            //TODO
                            /*
                            if (xsdComplexTypes.Any(x => x.XsdComplexTypeElementType == typeAttribute))
                            {
                                if (item.ChildrenXsdComplexTypes == null)
                                {
                                    item.ChildrenXsdComplexTypes = new List<XsdComplexType>();
                                }

                                item.ChildrenXsdComplexTypes.Add(xsdComplexTypes.FirstOrDefault(x => x.XsdComplexTypeElementType == typeAttribute));
                            }
                            else
                            {
                            */
                            if (item.XsdSimpleTypes == null)
                            {
                                item.XsdSimpleTypes = new List<XsdSimpleType>();
                            }

                            item.XsdSimpleTypes.Add(new XsdSimpleType
                            {
                                XsdSimpleTypeName = nameAttribute,
                                XsdSimpleTypeElementType = typeAttribute,
                                XsdComplexType = item
                            });
                            /*}*/
                        }
                    }
                }
            }

            return xsdComplexTypes;
        }

        private void GetXsdDocument(XDocument xDocument, ServiceDescription serviceDescription)
        {
            var xsdDocument = new XsdDocument
            {
                Id = serviceDescription.Id,
                ServiceDescription = serviceDescription,
                XsdComplexTypes = GetXsdComplexTypesFromRoot(xDocument),
                XsdSimpleTypes = GetXsdSimpleTypesFromRoot(xDocument)
            };

            var simpleTypesInsideComplexTypes = GetXsdSimpleTypesFromComplexTypes(xDocument, xsdDocument.XsdComplexTypes);

            //simpleTypesInsideComplexTypes.ForEach(x => xsdDocument.XsdSimpleTypes.Add(x));

            //var SimpleTypesFromComplexTypes = xsdDocument.XsdComplexTypes.Where(x => x.XsdSimpleTypes != null && x.XsdSimpleTypes.Any()).SelectMany(x => x.XsdSimpleTypes);

            //SimpleTypesFromComplexTypes.ToList().ForEach(x => xsdDocument.XsdSimpleTypes.Add(x));

            serviceDescription.XsdDocument = xsdDocument;
        }

        private List<XsdSimpleType> GetXsdSimpleTypesFromComplexTypes(XDocument xDocument, ICollection<XsdComplexType> xsdComplexTypes)
        {
            var xsdSimpleTypes = xDocument.Descendants(xsdNamespace + "complexType")
                .Where(x => x.Parent.Name == xsdNamespace + "schema")
                .Descendants(xsdNamespace + "element")
                .Where(x => x.Attribute("type").Value.StartsWith($"{x.GetPrefixOfNamespace(xsdNamespace)}:"))
                .Select(x => new XsdSimpleType
                {
                    XsdSimpleTypeName = (string)x.Attribute("name"),
                    XsdSimpleTypeElementType = (string)x.Attribute("type"),
                    XsdComplexType = FindParentComplexType(x, xsdComplexTypes)
                })
                .Distinct()
                .ToList();

            return xsdSimpleTypes;
        }

        private List<XsdSimpleType> GetXsdSimpleTypesFromRoot(XDocument xDocument)
        {
            var xsdSimpleTypes = xDocument.Descendants(xsdNamespace + "simpleType")
                                    .Select(x => new XsdSimpleType
                                    {
                                        XsdSimpleTypeName = (string)x.Attribute("name"),
                                        XsdSimpleTypeElementType = (string)x.Attribute("name")
                                    })
                                    .Distinct()
                                    .ToList();

            return xsdSimpleTypes;
        }

        #endregion Gets

        #region Parses

        private static void ArrangeXsdComplexTypes(List<XsdComplexType> xsdComplexTypes)
        {
            foreach (var ct in xsdComplexTypes)
            {
                if (ct.ParentsXsdComplexTypes != null && ct.ParentsXsdComplexTypes.Any())
                {
                    foreach (var parent in ct.ParentsXsdComplexTypes)
                    {
                        if (parent.ChildrenXsdComplexTypes == null)
                        {
                            parent.ChildrenXsdComplexTypes = new List<XsdComplexType>();
                        }
                        parent.ChildrenXsdComplexTypes.Add(ct);
                    }
                }
            }
        }

        private void ParseXDocumentToServiceDescription(ServiceDescription serviceDescription, XDocument xDocument)
        {
            GetXsdDocument(xDocument, serviceDescription);

            GetWsdlInterfaces(xDocument, serviceDescription);

            if (serviceDescription.WsdlInterfaces != null)
            {
                foreach (var wsdlInterface in serviceDescription.WsdlInterfaces)
                {
                    GetWsdlOperations(xDocument, wsdlInterface);

                    if (wsdlInterface.WsdlOperations != null)
                    {
                        foreach (var wsdlOperation in wsdlInterface.WsdlOperations)
                        {
                            GetWsdlInputs(xDocument, serviceDescription, wsdlInterface, wsdlOperation);

                            GetWsdlOutputs(xDocument, serviceDescription, wsdlInterface, wsdlOperation);
                        }
                    }
                }
            }
        }

        private void ParseXml(ServiceDescription serviceDescription)
        {
            var xDocument = XDocument.Parse(serviceDescription.Xml);
            ParseXDocumentToServiceDescription(serviceDescription, xDocument);
        }

        private void RemoveSelfReference(ServiceDescription serviceDescription)
        {
            foreach (var wsdlInterface in serviceDescription.WsdlInterfaces)
            {
                foreach (var wsdlOperation in wsdlInterface.WsdlOperations)
                {
                    foreach (var wsdlInput in wsdlOperation.WsdlInputs)
                    {
                        var xsdComplexType = wsdlInput.XsdComplexType;

                        var toRemove = new List<XsdComplexType>();

                        foreach (var childXsdComplexTypes in xsdComplexType.ChildrenXsdComplexTypes)
                        {
                            if (childXsdComplexTypes.ChildrenXsdComplexTypes.Any(x => x.Id == childXsdComplexTypes.Id))
                            {
                                toRemove.AddRange(childXsdComplexTypes.ChildrenXsdComplexTypes.Where(x => x.Id == childXsdComplexTypes.Id));

                                foreach (var remove in toRemove)
                                {
                                    childXsdComplexTypes.ChildrenXsdComplexTypes.Remove(remove);
                                }
                            }
                        }
                    }

                    foreach (var wsdlOutput in wsdlOperation.WsdlOutputs)
                    {
                        var xsdComplexType = wsdlOutput.XsdComplexType;

                        var toRemove = new List<XsdComplexType>();

                        foreach (var childXsdComplexTypes in xsdComplexType.ChildrenXsdComplexTypes)
                        {
                            if (childXsdComplexTypes.ChildrenXsdComplexTypes.Any(x => x.Id == childXsdComplexTypes.Id))
                            {
                                toRemove.AddRange(childXsdComplexTypes.ChildrenXsdComplexTypes.Where(x => x.Id == childXsdComplexTypes.Id));

                                foreach (var remove in toRemove)
                                {
                                    childXsdComplexTypes.ChildrenXsdComplexTypes.Remove(remove);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var xsdComplexType in serviceDescription.XsdDocument.XsdComplexTypes)
            {
                var toRemove = new List<XsdComplexType>();

                foreach (var childXsdComplexTypes in xsdComplexType.ChildrenXsdComplexTypes)
                {
                    if (childXsdComplexTypes.ChildrenXsdComplexTypes.Any(x => x.Id == childXsdComplexTypes.Id))
                    {
                        toRemove.AddRange(childXsdComplexTypes.ChildrenXsdComplexTypes.Where(x => x.Id == childXsdComplexTypes.Id));

                        foreach (var remove in toRemove)
                        {
                            childXsdComplexTypes.ChildrenXsdComplexTypes.Remove(remove);
                        }
                    }
                }
            }
        }

        #endregion Parses

        #endregion Private methods
    }
}
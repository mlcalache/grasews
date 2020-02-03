using AutoMapper;
using Grasews.API.Models;
using Grasews.Application.DTOs;
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.ExternalService.Cytoscape.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Grasews.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [RoutePrefix("api/service-descriptions")]
    [DisplayName("Service Descriptions")]
    public class ServiceDescriptionsApiController : BaseApiController
    {
        #region Private vars

        /// <summary>
        ///
        /// </summary>
        private readonly IGraphService _graphService;

        /// <summary>
        ///
        /// </summary>
        private readonly IServiceDescriptionService _serviceDescriptionService;

        /// <summary>
        ///
        /// </summary>
        private readonly IShareInvitationService _shareInvitationService;

        /// <summary>
        ///
        /// </summary>
        private readonly IServiceDescription_UserService _serviceDescription_UserService;

        /// <summary>
        ///
        /// </summary>
        private readonly ITaskService _taskService;

        /// <summary>
        ///
        /// </summary>
        private readonly IIssueService _issueService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="serviceDescriptionService"></param>
        /// <param name="graphService"></param>
        /// <param name="userIdentityService"></param>
        /// <param name="shareInvitationService"></param>
        /// <param name="serviceDescription_UserService"></param>
        /// <param name="taskService"></param>
        /// <param name="issueService"></param>
        public ServiceDescriptionsApiController(IMapper mapper,
            IServiceDescriptionService serviceDescriptionService,
            IGraphService graphService,
            IUserIdentityService userIdentityService,
            IShareInvitationService shareInvitationService,
            IServiceDescription_UserService serviceDescription_UserService,
            ITaskService taskService,
            IIssueService issueService)
            : base(mapper, userIdentityService)
        {
            _serviceDescriptionService = serviceDescriptionService;
            _graphService = graphService;
            _shareInvitationService = shareInvitationService;
            _serviceDescription_UserService = serviceDescription_UserService;
            _taskService = taskService;
            _issueService = issueService;
        }

        #endregion Ctors

        #region Actions

        #region GRAPH

        // POST: api/service-descriptions/1/graph/data
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/graph/data")]
        [ResponseType(typeof(CytoscapeObject))]
        public IHttpActionResult CreateGraphJsonData(int id)
        {
            var serviceDescription = _serviceDescriptionService.Get(id, withSawsdlModelReference: true);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            var response = _graphService.CreateGraphData(serviceDescription.Id, serviceDescription.ServiceName, serviceDescription.WsdlInterfaces);

            return Ok(response);
        }

        //// POST: api/service-descriptions/graph/data
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("graph/data")]
        //[ResponseType(typeof(CytoscapeObject))]
        //public IHttpActionResult CreateGraphJsonDataFromXml(GraphJsonRequestCreateModel request)
        //{
        //    var parseXmlRequestViewModel = Mapper.Map<ParseWsdlRequestViewModel>(request);

        //    var parseXmlResponseViewModel = ConvertWsdlToServiceDescription(parseXmlRequestViewModel);

        //    var wsdlInterfaces = Mapper.Map<ICollection<WsdlInterface>>(parseXmlResponseViewModel.WsdlInterfaces);

        //    var response = _graphService.CreateGraphData(request.ServiceName, wsdlInterfaces);

        //    return Ok(response);
        //}

        // POST: api/service-descriptions/graph/styles
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("graph/styles")]
        [ResponseType(typeof(CytoscapeObject))]
        public IHttpActionResult CreateGraphJsonStyles()
        {
            var response = _graphService.CreateGraphStyles();

            return Ok(response);
        }

        // GET: api/service-descriptions/5/graph
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/graph")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetServiceDescriptionGraphJson(int id)
        {
            var serviceDescription = _serviceDescriptionService.Get(id, withSawsdlModelReference: true);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            return Ok(serviceDescription.GraphJson);
        }

        // PUT: api/service-descriptions/5/graph
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/graph")]
        public IHttpActionResult UpdateJsonGraph(int id, UpdateGraphJson_ApiRequestUpdateModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var graphDataObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CytoscapeObject>(requestModel.GraphJson);

            var affectedRegisters = _serviceDescriptionService.UpdateJsonGraph(id, idUser, graphDataObject);

            return Ok(affectedRegisters);
        }

        // GET: api/service-descriptions/5/graph/nodes-positions
        /// <summary>
        ///
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id:int}/graph/nodes-positions")]
        public IHttpActionResult SetGraphNodesPositionsByUser(GraphNodesLocationsByUser_ApiRequestViewModel requestModel)
        {
            var idUser = _userIdentityService.Id;

            var updatedGraphJson = _serviceDescriptionService.SetGraphNodesPositionsByUser(idUser, requestModel.IdServiceDescription, requestModel.OriginalGraphJson);

            return Ok(updatedGraphJson);
        }

        // PUT: api/service-descriptions/1/graph/node-positions
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/graph/node-positions")]
        public IHttpActionResult TransferNodePositionsFromOntologyTermsToModelReferences(int id)
        {
            var idUser = _userIdentityService.Id;

            var count = _serviceDescriptionService.TransferNodePositionsFromOntologyTermsToModelReferences(idUser, id);

            return Ok(count);
        }

        #endregion GRAPH

        #region HTML

        // GET: api/service-descriptions/5/html/tree-view-menu
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/html/tree-view-menu")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetHtmlTreeViewMenu(int id)
        {
            var serviceDescription = _serviceDescriptionService.Get(id, withSawsdlModelReference: true);

            if (serviceDescription == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, "Service description with given ID not found");
            }

            var html = _serviceDescriptionService.GetHtmlTreeViewMenu(serviceDescription);

            return Ok(html);
        }

        #endregion HTML

        #region XML

        // GET: api/service-descriptions
        // GET: api/service-descriptions/5/xml
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/xml")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetXmlFromServiceDescription(int id)
        {
            var serviceDescription = _serviceDescriptionService.Get(id, withSawsdlModelReference: true);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            return Ok(serviceDescription.Xml);
        }

        // POST: api/service-descriptions/xml
        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("elements")]
        [ResponseType(typeof(ParseWsdl_ApiResponseViewModel))]
        public IHttpActionResult GetElementsFromXml(ParseWsdl_ApiRequestViewModel request)
        {
            var response = ConvertXmlToServiceDescriptionElements(request);

            return Ok(response);
        }

        // POST: api/service-descriptions/1/xml
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on ParseXmlByServiceDescription</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/elements")]
        [ResponseType(typeof(ParseWsdl_ApiResponseViewModel))]
        public IHttpActionResult GetElementsFromServiceDescription(int id)
        {
            var serviceDescription = _serviceDescriptionService.Get(id);

            var response = _mapper.Map<ParseWsdl_ApiResponseViewModel>(serviceDescription);

            return Ok(response);
        }

        #endregion XML

        #region CRUD

        // DELETE: api/service-descriptions/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult DeleteServiceDescription(int id)
        {
            var serviceDescription = _serviceDescriptionService.Get(id, withSawsdlModelReference: true, @readonly: false);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            var count = _serviceDescriptionService.Remove(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        // GET: api/service-descriptions/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult GetServiceDescription(int id)
        {
            var idUser = _userIdentityService.Id;

            var serviceDescription = _serviceDescriptionService.GetByUser(id, idUser, withSawsdlModelReference: true);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        // GET: api/service-descriptions
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(ServiceDescriptionCollection_ApiResponseViewModel))]
        public IHttpActionResult GetServiceDescriptions()
        {
            var idUser = _userIdentityService.Id;

            var serviceDescriptions = _serviceDescriptionService.GetAllByOwnerUser(idUser);

            var serviceDescriptionsResponseModel = _mapper.Map<ServiceDescriptionCollection_ApiResponseViewModel>(serviceDescriptions);

            return Ok(serviceDescriptionsResponseModel);
        }

        // POST: api/service-descriptions
        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescriptionRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult PostServiceDescription(ServiceDescription_ApiRequestCreateModel serviceDescriptionRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUser = _userIdentityService.Id;

            var serviceDescription = _mapper.Map<ServiceDescription>(serviceDescriptionRequestModel);

            serviceDescription.Xml = serviceDescription.Xml.Replace("%26", "&");
            serviceDescription.Xml = serviceDescription.Xml.UnescapeXml();

            serviceDescription.IdOwnerUser = idUser;

            var count = _serviceDescriptionService.Create(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        // PUT: api/service-descriptions/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serviceDescriptionRequestModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult PutServiceDescription(int id, ServiceDescription_ApiRequestUpdateModel serviceDescriptionRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceDescriptionRequestModel.Id)
            {
                return BadRequest();
            }

            var serviceDescription = _mapper.Map<ServiceDescription>(serviceDescriptionRequestModel);

            var count = _serviceDescriptionService.Update(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescriptionResponseModel);
        }

        #endregion CRUD

        #region Share invitations

        // GET: api/service-descriptions/1/share-invitations/
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}/share-invitations")]
        [HttpGet]
        [ResponseType(typeof(List<ShareInvitation_ApiResponseViewModel>))]
        public IHttpActionResult GetShareInvitationsByServiceDescription(int id)
        {
            var idUser = _userIdentityService.Id;

            var serviceDescription = _serviceDescriptionService.Get(id);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            if (serviceDescription.IdOwnerUser != idUser)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, "The user is not the owner of the service description.");
            }

            var shareInvitations = _shareInvitationService.GetAllByServiceDescription(id);

            if (!shareInvitations.Any())
            {
                return NotFound();
            }

            var shareInvitationResponseModel = _mapper.Map<List<ShareInvitation_ApiResponseViewModel>>(shareInvitations);

            return Ok(shareInvitationResponseModel);
        }

        #endregion Share invitations

        #region Service description users

        // GET: api/service-descriptions/1/share-invitations/
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}/service-description-users")]
        [HttpGet]
        [ResponseType(typeof(List<ServiceDescription_User_ApiResponseViewModel>))]
        public IHttpActionResult GetServiceDescriptionUsersByServiceDescription(int id)
        {
            var serviceDescription_Users = _serviceDescription_UserService.GetAllByServiceDescription(id);

            if (serviceDescription_Users.Any())
            {
                var serviceDescription_UsersResponseModel = _mapper.Map<List<ServiceDescription_User_ApiResponseViewModel>>(serviceDescription_Users);

                return Ok(serviceDescription_UsersResponseModel);
            }

            return Ok();
        }

        #endregion Service description users

        #region Tasks

        // GET: api/service-descriptions/1/tasks
        /// <summary>
        ///
        /// </summary>
        /// <param name="id">todo: describe id parameter on GetTasks</param>
        /// <returns></returns>
        [Route("{id:int}/tasks")]
        [ResponseType(typeof(IEnumerable<Task_ApiResponseViewModel>))]
        public IHttpActionResult GetTasks(int id)
        {
            var tasks = _taskService.GetAllByServiceDescription(id);

            if (tasks.Any())
            {
                var tasksResponseModel = _mapper.Map<List<Task_ApiResponseViewModel>>(tasks);

                return Ok(tasksResponseModel);
            }

            return NotFound();
        }

        #endregion Tasks

        #region Issues

        // GET: api/service-descriptions/1/issues
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}/issues")]
        [ResponseType(typeof(IEnumerable<Issue_ApiResponseViewModel>))]
        public IHttpActionResult GetIssues(int id)
        {
            var issues = _issueService.GetAllCompleteByServiceDescription(id);

            if (issues.Any())
            {
                var issuesResponseModel = _mapper.Map<List<Issue_ApiResponseViewModel>>(issues);

                return Ok(issuesResponseModel);
            }

            return NotFound();
        }

        #endregion Issues

        #endregion Actions

        #region Private methods

        private ParseWsdl_ApiResponseViewModel ConvertXmlToServiceDescriptionElements(ParseWsdl_ApiRequestViewModel request)
        {
            var requestDTO = _mapper.Map<ParseWsdlRequestDTO>(request);

            var serviceDescription = _serviceDescriptionService.ParseXml(requestDTO);

            var response = _mapper.Map<ParseWsdl_ApiResponseViewModel>(serviceDescription);

            return response;
        }

        #endregion Private methods
    }
}
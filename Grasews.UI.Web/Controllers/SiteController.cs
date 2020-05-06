using Grasews.API.Models;
using Grasews.Domain.Enums;
using Grasews.Domain.Exceptions;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class SiteController : BaseController
    {
        #region Private vars

        private readonly IServiceDescriptionService _serviceDescriptionService;
        private readonly IUserService _userService;

        #endregion Private vars

        #region Ctors

        public SiteController(IServiceDescriptionService serviceDescriptionService,
            IUserService userService,
            IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
            _serviceDescriptionService = serviceDescriptionService;
            _userService = userService;
        }

        #endregion Ctors

        #region Actions

        #region MVC Actions

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            ViewBag.UserName = Username;
            ViewBag.UserFullName = UserFullName;
            ViewBag.UserRegistrationDate = UserRegistrationDate.ToString("yyyy MMM ");

            return View();
        }

        #endregion MVC Actions

        #region Partial Views

        #region Service description and ontology

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalOpenOntology()
        {
            OntologyCollection_ApiResponseViewModel ontologies = null;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get ontologies' details from API

            var ontologiesRequest = CreateApiRequest($"api/ontologies", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var ontologiesResponse = await _apiRestClient.ExecuteAsync<OntologyCollection_ApiResponseViewModel>(ontologiesRequest);

            if (ontologiesResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ontologies = ontologiesResponse.Data;
            }

            #endregion Get ontologies' details from API

            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalOpenOntology.cshtml", ontologies);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalOpenServiceDescription()
        {
            ServiceDescriptionCollection_ApiResponseViewModel serviceDescriptions = null;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get service description's details from API

            var serviceDescriptionsRequest = CreateApiRequest($"api/service-descriptions", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var serviceDescriptionsResponse = await _apiRestClient.ExecuteAsync<ServiceDescriptionCollection_ApiResponseViewModel>(serviceDescriptionsRequest);

            if (serviceDescriptionsResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                serviceDescriptions = serviceDescriptionsResponse.Data;
            }

            #endregion Get service description's details from API

            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalOpenServiceDescription.cshtml", serviceDescriptions);
        }

        #endregion Service description and ontology

        #region Semantic Annotation

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _ModalAddLiftingSchemaMapping()
        {
            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalAddLiftingSchemaMapping.cshtml");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _ModalAddLoweringSchemaMapping()
        {
            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalAddLoweringSchemaMapping.cshtml");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalAddModelReference()
        {
            var ontologies = new OntologyCollection_ApiResponseViewModel();

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get Ontologies Cookie

            string[] ontologiesCookie = null;

            var cookie = GetCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE);

            #endregion Get Ontologies Cookie

            if (cookie != null)
            {
                ontologiesCookie = cookie.Value.Split(',');

                ontologies.OntologyViewModels = new List<OntologyCollection_ApiResponseViewModel.OntologyViewModel>();

                #region Get ontologies' details from API

                foreach (var ontologyId in ontologiesCookie)
                {
                    var ontologyRequest = CreateApiRequest($"api/ontologies/{ontologyId}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                    var ontologyResponse = await _apiRestClient.ExecuteAsync<OntologyCollection_ApiResponseViewModel.OntologyViewModel>(ontologyRequest);

                    if (ontologyResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ontology = ontologyResponse.Data;

                        ontologies.OntologyViewModels.Add(ontology);
                    }
                }

                #endregion Get ontologies' details from API
            }

            if (ontologies.OntologyViewModels != null && ontologies.OntologyViewModels.Count > 0)
            {
                return PartialView("~/Views/Site/_PartialViews/Modals/_ModalAddModelReference.cshtml", ontologies);
            }

            return null;
        }

        [HttpGet]
        public async Task<ActionResult> _ModalAddModelReferenceFromOntologyTerm()
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get service description in the view from the cookie

            var serviceDescriptionCookie = GetCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

            #endregion Get service description in the view from the cookie

            if (serviceDescriptionCookie != null)
            {
                int idServiceDescription;

                if (int.TryParse(serviceDescriptionCookie.Value, out idServiceDescription))
                {
                    #region Get service description from the API

                    var serviceDescriptionElementsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/elements", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                    var serviceDescriptionElementsResponse = await _apiRestClient.ExecuteAsync<ParseWsdl_ApiResponseViewModel>(serviceDescriptionElementsRequest);

                    #endregion Get service description from the API

                    if (serviceDescriptionElementsResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var serviceDescriptionElements = serviceDescriptionElementsResponse.Data;

                        return PartialView("~/Views/Site/_PartialViews/Modals/_ModalAddModelReferenceFromOntology.cshtml", serviceDescriptionElements);
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology">todo: describe ontologyId parameter on _ModalAddModelReferenceOntologyTerms</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalAddModelReferenceOntologyTerms(int idOntology)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get ontologies' details from API

            var ontologyRequest = CreateApiRequest($"api/ontologies/{idOntology}?complete=true", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var ontologyResponse = await _apiRestClient.ExecuteAsync<Ontology_ApiResponseViewModel>(ontologyRequest);

            #endregion Get ontologies' details from API

            if (ontologyResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var ontology = ontologyResponse.Data;

                if (ontology.OntologyTerms != null && ontology.OntologyTerms.Count > 0)
                {
                    return PartialView("~/Views/Site/_PartialViews/Modals/_ModalAddModelReferenceOntologyTerms.cshtml", ontology.OntologyTerms);
                }
            }

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on _ModalRemoveModelReference</param>
        /// <param name="idWsdlElement">todo: describe idWsdlElement parameter on _ModalRemoveModelReference</param>
        /// <param name="idWsdlElementType">todo: describe idWsdlElementType parameter on _ModalRemoveModelReference</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalRemoveLiftingSchemaMapping(int idServiceDescription, int idWsdlElement, int idWsdlElementType)
        {
            SawsdlLiftingSchemaMapping_ApiRequestViewModel liftingSchemaMapping = null;

            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get lifting schema mapping from API

            var endpoint = DefineEndpoint(wsdlElementType);

            var liftingSchemaMappingRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/lifting-schema-mapping", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var liftingSchemaMappingResponse = await _apiRestClient.ExecuteAsync<SawsdlLiftingSchemaMapping_ApiRequestViewModel>(liftingSchemaMappingRequest);

            if (liftingSchemaMappingResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                liftingSchemaMapping = liftingSchemaMappingResponse.Data;
            }

            #endregion Get lifting schema mapping from API

            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalRemoveLiftingSchemaMapping.cshtml", liftingSchemaMapping);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on _ModalRemoveModelReference</param>
        /// <param name="idWsdlElement">todo: describe idWsdlElement parameter on _ModalRemoveModelReference</param>
        /// <param name="idWsdlElementType">todo: describe idWsdlElementType parameter on _ModalRemoveModelReference</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalRemoveLoweringSchemaMapping(int idServiceDescription, int idWsdlElement, int idWsdlElementType)
        {
            SawsdlLoweringSchemaMapping_ApiRequestViewModel loweringSchemaMapping = null;

            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get lifting schema mapping from API

            var endpoint = DefineEndpoint(wsdlElementType);

            var loweringSchemaMappingRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/lowering-schema-mapping", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var loweringSchemaMappingResponse = await _apiRestClient.ExecuteAsync<SawsdlLoweringSchemaMapping_ApiRequestViewModel>(loweringSchemaMappingRequest);

            if (loweringSchemaMappingResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                loweringSchemaMapping = loweringSchemaMappingResponse.Data;
            }

            #endregion Get lifting schema mapping from API

            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalRemoveLoweringSchemaMapping.cshtml", loweringSchemaMapping);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on _ModalRemoveModelReference</param>
        /// <param name="idWsdlElement">todo: describe idWsdlElement parameter on _ModalRemoveModelReference</param>
        /// <param name="idWsdlElementType">todo: describe idWsdlElementType parameter on _ModalRemoveModelReference</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalRemoveModelReference(int idServiceDescription, int idWsdlElement, int idWsdlElementType)
        {
            SawsdlModelReferenceCollection_ApiRequestViewModel modelReferences = null;

            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get model references from API

            var endpoint = DefineEndpoint(wsdlElementType);

            var modelReferencesRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/model-reference", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var modelReferencesResponse = await _apiRestClient.ExecuteAsync<SawsdlModelReferenceCollection_ApiRequestViewModel>(modelReferencesRequest);

            if (modelReferencesResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                modelReferences = modelReferencesResponse.Data;
            }

            #endregion Get model references from API

            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalRemoveModelReference.cshtml", modelReferences);
        }

        #endregion Semantic Annotation

        #region Sharing

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalAddIssue()
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get service description in the view from the cookie

            var serviceDescriptionCookie = GetCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

            #endregion Get service description in the view from the cookie

            if (serviceDescriptionCookie != null)
            {
                int idServiceDescription;

                if (int.TryParse(serviceDescriptionCookie.Value, out idServiceDescription))
                {
                    #region Get service description from the API

                    var serviceDescriptionElementsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/elements", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                    var serviceDescriptionElementsResponse = await _apiRestClient.ExecuteAsync<ParseWsdl_ApiResponseViewModel>(serviceDescriptionElementsRequest);

                    #endregion Get service description from the API

                    if (serviceDescriptionElementsResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var serviceDescriptionElements = serviceDescriptionElementsResponse.Data;

                        return PartialView("~/Views/Site/_PartialViews/Modals/_ModalAddIssue.cshtml", serviceDescriptionElements);
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalAddTask()
        {
            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalAddTask.cshtml");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on _ModalIssueList</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalIssueList(int idServiceDescription)
        {
            List<Issue_ApiResponseViewModel> issues = null;
            List<IssueAnswer_ApiResponseViewModel> issueAnswers = null;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the issue list

            var getIssuesRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/issues", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getIssuesResponse = await _apiRestClient.ExecuteAsync<List<Issue_ApiResponseViewModel>>(getIssuesRequest);

            if (getIssuesResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                issues = getIssuesResponse.Data;

                var issuesViewModel = Mapper.Map<List<Issue_MvcViewModel>>(issues);

                #region Get the answer list for every issue

                if (issuesViewModel != null && issuesViewModel.Any())
                {
                    foreach (var issue in issuesViewModel)
                    {
                        var getIssueAnswersRequest = CreateApiRequest($"api/issues/{issue.Id}/answers", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                        var getIssuAnswersResponse = await _apiRestClient.ExecuteAsync<List<IssueAnswer_ApiResponseViewModel>>(getIssueAnswersRequest);

                        if (getIssuAnswersResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            issueAnswers = getIssuAnswersResponse.Data;
                            issue.Answers = Mapper.Map<List<Issue_Answer_MvcViewModel>>(issueAnswers);
                        }
                    }
                }

                #endregion Get the answer list for every issue

                ViewBag.IdUser = IdUser;

                return PartialView("~/Views/Site/_PartialViews/Modals/_ModalIssueList.cshtml", issuesViewModel);
            }

            #endregion Get the issue list

            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalShareServiceDescription()
        {
            List<User_ApiResponseViewModel> users = null;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get users from API

            var usersRequest = CreateApiRequest($"api/users", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var usersResponse = await _apiRestClient.ExecuteAsync<List<User_ApiResponseViewModel>>(usersRequest);

            if (usersResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                users = usersResponse.Data;

                users = users.Where(x => x.Id != IdUser).ToList();
            }

            #endregion Get users from API

            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalShare.cshtml", users);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on _ModalTaskList</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> _ModalTaskList(int idServiceDescription)
        {
            List<Task_ApiResponseViewModel> tasks = null;
            List<TaskComment_ApiResponseViewModel> taskComments = null;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the task list

            var getTasksRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/tasks", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getTasksResponse = await _apiRestClient.ExecuteAsync<List<Task_ApiResponseViewModel>>(getTasksRequest);

            if (getTasksResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tasks = getTasksResponse.Data;
            }

            #endregion Get the task list

            var tasksViewModel = Mapper.Map<List<Task_MvcViewModel>>(tasks);

            #region Get the comment list for every task

            if (tasksViewModel != null && tasksViewModel.Any())
            {
                foreach (var task in tasksViewModel)
                {
                    var getTaskCommentsRequest = CreateApiRequest($"api/tasks/{task.Id}/comments", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                    var getTaskCommentsResponse = await _apiRestClient.ExecuteAsync<List<TaskComment_ApiResponseViewModel>>(getTaskCommentsRequest);

                    if (getTaskCommentsResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        taskComments = getTaskCommentsResponse.Data;
                        task.Comments = Mapper.Map<List<Task_Comment_MvcViewModel>>(taskComments);
                    }
                }
            }

            #endregion Get the comment list for every task

            ViewBag.IdUser = IdUser;

            return PartialView("~/Views/Site/_PartialViews/Modals/_ModalTaskList.cshtml", tasksViewModel);
        }

        #endregion Sharing

        #endregion Partial Views

        #region AJAX Actions

        #region Semantic annotations

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlElement"></param>
        /// <param name="idWsdlElementType"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on AddSawsdlModelReference</param>
        /// <param name="liftingSchemaMappingUrl">todo: describe liftingSchemaMappingUrl parameter on AddSawsdlLiftingSchemaMapping</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddSawsdlLiftingSchemaMapping(int idWsdlElement, int idWsdlElementType, int idServiceDescription, string liftingSchemaMappingUrl)
        {
            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var endpoint = DefineEndpoint(wsdlElementType);

            var addLiftingSchemaMappingRequestModel = new SawsdlLiftingSchemaMapping_ApiRequestCreateModel
            {
                LiftingSchemaMappingUrl = liftingSchemaMappingUrl
            };

            var addLiftingSchemaMappingRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/lifting-schema-mapping", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            addLiftingSchemaMappingRequest.AddRequestBodyParameter(addLiftingSchemaMappingRequestModel);
            var addLiftingSchemaMappingResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(addLiftingSchemaMappingRequest);

            if (addLiftingSchemaMappingResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = addLiftingSchemaMappingResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                return Json(new { ServiceDescription = serviceDescription, Message = $"SAWSDL Lifting Schema Mapping added successfuly." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Exception = JsonConvert.DeserializeObject<SawsdlLiftingSchemaMappingAlreadyAddedException>(addLiftingSchemaMappingResponse.Content) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlElement"></param>
        /// <param name="idWsdlElementType"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on AddSawsdlModelReference</param>
        /// <param name="loweringSchemaMappingUrl">todo: describe liftingSchemaMappingUrl parameter on AddSawsdlLiftingSchemaMapping</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddSawsdlLoweringSchemaMapping(int idWsdlElement, int idWsdlElementType, int idServiceDescription, string loweringSchemaMappingUrl)
        {
            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var endpoint = DefineEndpoint(wsdlElementType);

            var addLoweringSchemaMappingRequestModel = new SawsdlLoweringSchemaMapping_ApiRequestCreateModel
            {
                LoweringSchemaMappingUrl = loweringSchemaMappingUrl
            };

            var addLoweringSchemaMappingRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/lowering-schema-mapping", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            addLoweringSchemaMappingRequest.AddRequestBodyParameter(addLoweringSchemaMappingRequestModel);
            var addLoweringSchemaMappingResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(addLoweringSchemaMappingRequest);

            if (addLoweringSchemaMappingResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = addLoweringSchemaMappingResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = addLoweringSchemaMappingResponse.Content.Replace("\"", string.Empty) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlElement"></param>
        /// <param name="idWsdlElementType"></param>
        /// <param name="idOntologyTerm"></param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on AddSawsdlModelReference</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddSawsdlModelReference(int idWsdlElement, int idWsdlElementType, int idServiceDescription, int idOntologyTerm)
        {
            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var endpoint = DefineEndpoint(wsdlElementType);

            var addModelReferenceRequestModel = new SawsdlModelReference_ApiRequestCreateModel
            {
                IdOntologyTerm = idOntologyTerm
            };

            #region Add model reference via API

            var addModelReferenceRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/model-reference", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            addModelReferenceRequest.AddRequestBodyParameter(addModelReferenceRequestModel);
            var addModelReferenceResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(addModelReferenceRequest);

            #endregion Add model reference via API

            if (addModelReferenceResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = addModelReferenceResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = addModelReferenceResponse.Content.Replace("\"", string.Empty) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlElement"></param>
        /// <param name="idWsdlElementType"></param>
        /// <param name="idServiceDescription"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RemoveLiftingSchemaMapping(int idWsdlElement, int idWsdlElementType, int idServiceDescription)
        {
            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            var endpoint = DefineEndpoint(wsdlElementType);

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var removeLiftingSchemaMappingRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/lifting-schema-mapping", HttpMethodENUM.DELETE, "application/x-www-form-urlencoded");
            var removeLiftingSchemaMappingResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(removeLiftingSchemaMappingRequest);

            if (removeLiftingSchemaMappingResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = removeLiftingSchemaMappingResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlElement"></param>
        /// <param name="idWsdlElementType"></param>
        /// <param name="idServiceDescription"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RemoveLoweringSchemaMapping(int idWsdlElement, int idWsdlElementType, int idServiceDescription)
        {
            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            var endpoint = DefineEndpoint(wsdlElementType);

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var removeLoweringSchemaMappingRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/lowering-schema-mapping", HttpMethodENUM.DELETE, "application/x-www-form-urlencoded");
            var removeLoweringSchemaMappingResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(removeLoweringSchemaMappingRequest);

            if (removeLoweringSchemaMappingResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = removeLoweringSchemaMappingResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idWsdlElement"></param>
        /// <param name="idWsdlElementType"></param>
        /// <param name="idServiceDescription"></param>
        /// <param name="idOntologyTermsToRemove">todo: describe idOntologyTermsToRemove parameter on RemoveSawsdlModelReference</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RemoveSawsdlModelReference(int idWsdlElement, int idWsdlElementType, int idServiceDescription, string idOntologyTermsToRemove)
        {
            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            var endpoint = DefineEndpoint(wsdlElementType);

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var removeModelReferenceRequestModel = new SawsdlModelReference_ApiRequestRemoveModel { IdOntologyTerms = idOntologyTermsToRemove };

            var removeModelReferenceRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/{endpoint}/{idWsdlElement}/sawsdl/model-reference", HttpMethodENUM.DELETE, "application/x-www-form-urlencoded");
            removeModelReferenceRequest.AddRequestBodyParameter(removeModelReferenceRequestModel);
            var removeModelReferenceResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(removeModelReferenceRequest);

            if (removeModelReferenceResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = removeModelReferenceResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        #endregion Semantic annotations

        #region Sharing

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="issueDescription"></param>
        /// <param name="issueTitle">todo: describe issueTitle parameter on AddIssue</param>
        /// <param name="idWsdlElement">todo: describe idWsdlElement parameter on AddIssue</param>
        /// <param name="idWsdlElementType">todo: describe idWsdlElementType parameter on AddIssue</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddIssue(int idServiceDescription, string issueTitle, string issueDescription, int idWsdlElement, int idWsdlElementType)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

            var addIssueRequestModel = new Issue_ApiRequestCreateModel
            {
                Title = issueTitle,
                Description = issueDescription,
                IdServiceDescription = idServiceDescription
            };

            SetIdWsdlElementToAddIssueRequest(idWsdlElement, wsdlElementType, addIssueRequestModel);

            var addIssueRequest = CreateApiRequest($"api/issues", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            addIssueRequest.AddRequestBodyParameter(addIssueRequestModel);
            var addIssueResponse = await _apiRestClient.ExecuteAsync<Issue_ApiResponseViewModel>(addIssueRequest);

            if (addIssueResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var issue = addIssueResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = issue.ServiceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    issue.ServiceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                return Json(new { Issue = issue, Message = WebResource.IssueCreatedSuccessfuly }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idIssue"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddIssueAnswer(int idIssue, string answer)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var addIssueAnswerRequestModel = new IssueAnswer_ApiRequestCreateModel
            {
                Answer = answer,
                IdIssue = idIssue
            };

            var addIssueAnswerRequest = CreateApiRequest($"api/issues/{idIssue}/answers", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            addIssueAnswerRequest.AddRequestBodyParameter(addIssueAnswerRequestModel);
            var addIssueAnswerResponse = await _apiRestClient.ExecuteAsync<IssueAnswer_ApiResponseViewModel>(addIssueAnswerRequest);

            if (addIssueAnswerResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var issueAnswer = addIssueAnswerResponse.Data;

                return Json(new { IssueAnswer = issueAnswer.Answer, UserEmail = UserEmail, Message = WebResource.IssueAnswerCreatedSuccessfully }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="taskDescription"></param>
        /// <param name="taskTitle">todo: describe taskTitle parameter on AddTask</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddTask(int idServiceDescription, string taskTitle, string taskDescription)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var addTaskRequestModel = new Task_ApiRequestCreateModel
            {
                Title = taskTitle,
                Description = taskDescription,
                IdServiceDescription = idServiceDescription
            };

            var addTaskRequest = CreateApiRequest($"api/tasks", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            addTaskRequest.AddRequestBodyParameter(addTaskRequestModel);
            var addTaskResponse = await _apiRestClient.ExecuteAsync<Task_ApiResponseViewModel>(addTaskRequest);

            if (addTaskResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var task = addTaskResponse.Data;

                return Json(new { Task = task, Message = WebResource.TaskCreatedSuccessfuly }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddTaskComment(int idTask, string comment)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var addTaskCommentRequestModel = new TaskComment_ApiRequestCreateModel
            {
                Comment = comment,
                IdTask = idTask
            };

            var addTaskCommentRequest = CreateApiRequest($"api/tasks/{idTask}/comments", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            addTaskCommentRequest.AddRequestBodyParameter(addTaskCommentRequestModel);
            var addTaskCommentResponse = await _apiRestClient.ExecuteAsync<TaskComment_ApiResponseViewModel>(addTaskCommentRequest);

            if (addTaskCommentResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var taskComment = addTaskCommentResponse.Data;

                return Json(new { TaskComment = taskComment.Comment, UserEmail = UserEmail, Message = WebResource.TaskCommentCreatedSuccessfuly }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idIssue"></param>
        /// <param name="solved"></param>
        /// <param name="idWsdlElement"></param>
        /// <param name="idWsdlElementType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> MarkIssueAsSolved(int idIssue, bool solved, int idWsdlElement, int idWsdlElementType)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the issue from the API

            var getIssueRequest = CreateApiRequest($"api/issues/{idIssue}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getIssueResponse = await _apiRestClient.ExecuteAsync<Issue_ApiResponseViewModel>(getIssueRequest);

            #endregion Get the issue from the API

            if (getIssueResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var issue = getIssueResponse.Data;

                var updateIssueRequestBody = new Issue_ApiRequestUpdateModel
                {
                    Solved = solved,
                    Description = issue.Description,
                    Id = issue.Id,
                    IdServiceDescription = issue.IdServiceDescription
                };

                var wsdlElementType = (ServiceDescriptionElementTypeEnum)idWsdlElementType;

                SetIdWsdlElementToUpdateIssueRequest(idWsdlElement, wsdlElementType, updateIssueRequestBody);

                #region Update the task by the API

                var updateIssueRequest = CreateApiRequest($"api/issues/{idIssue}", HttpMethodENUM.PUT, "application/x-www-form-urlencoded");
                updateIssueRequest.AddRequestBodyParameter(updateIssueRequestBody);
                var updateIssueResponse = await _apiRestClient.ExecuteAsync<Issue_ApiResponseViewModel>(updateIssueRequest);

                #endregion Update the task by the API

                if (updateIssueResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    issue = updateIssueResponse.Data;

                    #region Graph Nodes Locations by User

                    var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = issue.ServiceDescription.GraphJson, IdServiceDescription = issue.ServiceDescription.Id };
                    var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{issue.ServiceDescription.Id}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                    nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                    var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                    if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        issue.ServiceDescription.GraphJson = updatedGraphJsonResponse.Data;
                    }

                    #endregion Graph Nodes Locations by User

                    return Json(new { Issue = issue, Message = WebResource.IssueUpdatedSuccessfully }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idTask"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> MarkTaskAsDone(int idTask, bool done)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the task from the API

            var getTaskRequest = CreateApiRequest($"api/tasks/{idTask}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getTaskResponse = await _apiRestClient.ExecuteAsync<Task_ApiResponseViewModel>(getTaskRequest);

            #endregion Get the task from the API

            if (getTaskResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var task = getTaskResponse.Data;

                var updateTaskRequestBody = new Task_ApiRequestUpdateModel
                {
                    Done = done,
                    Description = task.Description,
                    Id = task.Id,
                    IdServiceDescription = task.IdServiceDescription
                };

                #region Update the task by the API

                var updateTaskRequest = CreateApiRequest($"api/tasks/{idTask}", HttpMethodENUM.PUT, "application/x-www-form-urlencoded");
                updateTaskRequest.AddRequestBodyParameter(updateTaskRequestBody);
                var updateTaskResponse = await _apiRestClient.ExecuteAsync<Task_ApiResponseViewModel>(updateTaskRequest);

                #endregion Update the task by the API

                if (updateTaskResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="emails">todo: describe emails parameter on ShareServiceDescription</param>
        /// <param name="idServiceDescription">todo: describe idServiceDescription parameter on ShareServiceDescription</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ShareServiceDescription(string[] emails, int idServiceDescription, string idOntologies)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var shareServiceDescriptionRequestModel = new ShareInvitation_ApiRequestCreateModel
            {
                Emails = string.Join(",", emails),
                IdServiceDescription = idServiceDescription,
                IdOntologies = idOntologies
            };

            var shareServiceDescriptionRequest = CreateApiRequest($"api/share-invitations", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            shareServiceDescriptionRequest.AddRequestBodyParameter(shareServiceDescriptionRequestModel);
            var shareServiceDescriptionResponse = await _apiRestClient.ExecuteAsync<List<ShareInvitation_ApiResponseViewModel>>(shareServiceDescriptionRequest);

            if (shareServiceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var shareServiceDescriptionResponseData = shareServiceDescriptionResponse.Data;

                return Json(new { shareResponse = shareServiceDescriptionResponseData }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = shareServiceDescriptionResponse.Content }, JsonRequestBehavior.AllowGet);
        }

        #endregion Sharing

        #region Service description and ontology

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetOntologiesAlreadyOpened()
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var ontologies = new List<Ontology_ApiResponseViewModel>();

            #region Get Ontologies Cookie

            string[] ontologiesCookie = null;

            var cookie = GetCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE);

            if (cookie != null)
            {
                ontologiesCookie = cookie.Value.Split(',');

                foreach (var ontologyId in ontologiesCookie)
                {
                    var ontologyRequest = CreateApiRequest($"api/ontologies/{ontologyId}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                    var ontologyResponse = await _apiRestClient.ExecuteAsync<Ontology_ApiResponseViewModel>(ontologyRequest);

                    if (ontologyResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var ontology = ontologyResponse.Data;

                        var htmlTreeViewMenuRequest = CreateApiRequest($"api/ontologies/{ontologyId}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                        var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                        var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                        ontology.HtmlTreeViewMenu = htmlTreeViewMenu;

                        ontologies.Add(ontology);
                    }
                }
            }

            #endregion Get Ontologies Cookie

            return Json(ontologies, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetServiceDescriptionsAlreadyOpened()
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var serviceDescriptions = new List<ServiceDescription_ApiResponseViewModel>();

            #region Get Service Descriptions Cookie

            string[] serviceDescriptionsCookie = null;

            var cookie = GetCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE);

            if (cookie != null)
            {
                serviceDescriptionsCookie = cookie.Value.Split(',');

                var serviceDescriptionOnViewCookie = GetCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

                foreach (var idServiceDescription in serviceDescriptionsCookie)
                {
                    var serviceDescriptionRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                    var serviceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(serviceDescriptionRequest);

                    if (serviceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var serviceDescription = serviceDescriptionResponse.Data;

                        #region Get Html Treeview Menu

                        var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                        var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                        var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                        serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                        #endregion Get Html Treeview Menu

                        if (serviceDescription.Id.ToString() == serviceDescriptionOnViewCookie?.Value)
                        {
                            #region Graph Nodes Positions by User

                            var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = Convert.ToInt32(idServiceDescription) };
                            var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                            nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                            var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                            if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                            }

                            #endregion Graph Nodes Positions by User
                        }

                        serviceDescriptions.Add(serviceDescription);
                    }
                }
            }

            #endregion Get Service Descriptions Cookie

            return Json(serviceDescriptions, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idOntology"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> OpenSavedOntology(int idOntology)
        {
            var isOntologyAlreadyOpened = false;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get Ontologies Cookie

            string[] ontologiesCookie = null;

            var cookie = GetCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE);

            if (cookie != null)
            {
                ontologiesCookie = cookie.Value.Split(',');
            }

            #endregion Get Ontologies Cookie

            #region Check if the ontology is already opened (cookies)

            if (ontologiesCookie?.Length > 0)
            {
                isOntologyAlreadyOpened = ontologiesCookie.Any(x => x == idOntology.ToString());
            }

            if (isOntologyAlreadyOpened)
            {
                return Json(new { message = "Ontology is already opened." }, JsonRequestBehavior.AllowGet);
            }

            #endregion Check if the ontology is already opened (cookies)

            var ontologyRequest = CreateApiRequest($"api/ontologies/{idOntology}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var ontologyResponse = await _apiRestClient.ExecuteAsync<Ontology_ApiResponseViewModel>(ontologyRequest);

            if (ontologyResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var ontology = ontologyResponse.Data;

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/ontologies/{ontology.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                ontology.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                #region Create cookie with ontologies (ids) opened

                var ontologiesForCookie = new List<string>();
                if (ontologiesCookie != null && ontologiesCookie.Length > 0)
                {
                    ontologiesForCookie.AddRange(ontologiesCookie.ToList());
                }
                ontologiesForCookie.Add(ontology.Id.ToString());

                SetCookie(GRASEWS_ONTOLOGIES_OPENED_COOKIE, string.Join(",", ontologiesForCookie));

                #endregion Create cookie with ontologies (ids) opened

                return Json(new { ontology }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> OpenSavedServiceDescription(int idServiceDescription)
        {
            var isServiceDescriptionAlreadyOpened = false;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get Service Descriptions Cookie

            string[] serviceDescriptionsCookie = null;

            var cookie = GetCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE);

            if (cookie != null)
            {
                serviceDescriptionsCookie = cookie.Value.Split(',');
            }

            #endregion Get Service Descriptions Cookie

            #region Check if the service description is already opened (cookies)

            if (serviceDescriptionsCookie?.Length > 0)
            {
                isServiceDescriptionAlreadyOpened = serviceDescriptionsCookie.Any(x => x == idServiceDescription.ToString());
            }

            if (isServiceDescriptionAlreadyOpened)
            {
                return Json(new { message = "Service Description is already opened." }, JsonRequestBehavior.AllowGet);
            }

            #endregion Check if the service description is already opened (cookies)

            #region Get service description from the API

            var serviceDescriptionsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var serviceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(serviceDescriptionsRequest);

            #endregion Get service description from the API

            if (serviceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = serviceDescriptionResponse.Data;

                #region Get html for the tree view menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{serviceDescription.Id}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get html for the tree view menu

                #region Create cookie with service descriptions (ids) opened

                var serviceDescriptionsForCookie = new List<string>();
                if (serviceDescriptionsCookie != null && serviceDescriptionsCookie.Length > 0)
                {
                    serviceDescriptionsForCookie.AddRange(serviceDescriptionsCookie.ToList());
                }
                serviceDescriptionsForCookie.Add(serviceDescription.Id.ToString());

                SetCookie(GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE, string.Join(",", serviceDescriptionsForCookie));

                #endregion Create cookie with service descriptions (ids) opened

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                SetCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE, idServiceDescription.ToString());

                return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> RefreshServiceDescriptionOnView()
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var serviceDescription = new ServiceDescription_ApiResponseViewModel();

            #region Get Service Descriptions Cookie

            var serviceDescriptionOnViewCookie = GetCookie(GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

            var idServiceDescription = serviceDescriptionOnViewCookie?.Value;

            var serviceDescriptionRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var serviceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(serviceDescriptionRequest);

            if (serviceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                serviceDescription = serviceDescriptionResponse.Data;

                #region Get Html Treeview Menu

                var htmlTreeViewMenuRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/html/tree-view-menu", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var htmlTreeViewMenuResponse = await _apiRestClient.ExecuteAsync<string>(htmlTreeViewMenuRequest);
                var htmlTreeViewMenu = htmlTreeViewMenuResponse.Data;

                serviceDescription.HtmlTreeViewMenu = htmlTreeViewMenu;

                #endregion Get Html Treeview Menu

                #region Graph Nodes Positions by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = Convert.ToInt32(idServiceDescription) };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Positions by User
            }

            #endregion Get Service Descriptions Cookie

            return Json(serviceDescription, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <param name="graphJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> UpdateServiceDescription(int idServiceDescription, string graphJson)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            var model = new UpdateGraphJson_ApiRequestUpdateModel
            {
                GraphJson = graphJson
            };

            var request = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph", HttpMethodENUM.PUT, "application/x-www-form-urlencoded");
            request.AddRequestBodyParameter(model);
            var response = await _apiRestClient.ExecuteAsync<int>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = response.Data;
                return Json(new { affectedRegisters = responseData, message = "Service description saved successfully." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { affectedRegisters = 0, message = "Something wrong has happened. Pleasy try again or contact an administrator." }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idServiceDescription"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ViewOpenedServiceDescription(int idServiceDescription)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the service descriptio from the API

            var getServiceDescriptionRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getServiceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(getServiceDescriptionRequest);

            #endregion Get the service descriptio from the API

            if (getServiceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = getServiceDescriptionResponse.Data;

                #region Graph Nodes Locations by User

                var nodesLocationRequestModel = new GraphNodesLocationsByUser_ApiRequestViewModel { OriginalGraphJson = serviceDescription.GraphJson, IdServiceDescription = idServiceDescription };
                var nodesLocationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/graph/nodes-positions", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                nodesLocationsRequest.AddRequestBodyParameter(nodesLocationRequestModel);
                var updatedGraphJsonResponse = await _apiRestClient.ExecuteAsync<string>(nodesLocationsRequest);

                if (updatedGraphJsonResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    serviceDescription.GraphJson = updatedGraphJsonResponse.Data;
                }

                #endregion Graph Nodes Locations by User

                return Json(new { serviceDescription }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        #endregion Service description and ontology

        //[HttpPost]
        //public ActionResult SendInvite(InviteDTO invite)
        //{
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult Share(ShareDTO share)
        //{
        //    _userService.ShareServiceDescriptionWithUser(share.ServiceDescriptionId, share.UserIdToShare);

        //    return Json(new { message = "Service description shared with user successfully." }, JsonRequestBehavior.AllowGet);
        //}

        //[ValidateInput(false)]
        //public ActionResult SaveWebServiceDescription(ServiceDescription wsdlDocument, string graphJson)
        //{
        //    var existingServiceDescription = _serviceDescriptionService.GetByServiceName(wsdlDocument.ServiceName, UserId);
        //    int id;

        //    if (existingServiceDescription == null)
        //    {
        //        var serviceDescriptionCreateViewModel = new ServiceDescriptionCreateViewModel
        //        {
        //            ServiceName = wsdlDocument.ServiceName,
        //            Wsdl = wsdlDocument.Xml,
        //            CytoscapeGraphJson = graphJson
        //        };

        //        var serviceDescription = Mapper.Map<ServiceDescription>(serviceDescriptionCreateViewModel);

        //        serviceDescription.IdOwnerUser = UserId;

        //        _serviceDescriptionService.Create(serviceDescription);

        //        id = serviceDescription.Id;
        //    }
        //    else
        //    {
        //        existingServiceDescription.Xml = wsdlDocument.Xml;
        //        existingServiceDescription.GraphJson = graphJson;

        //        _serviceDescriptionService.Update(existingServiceDescription);

        //        id = existingServiceDescription.Id;
        //    }

        //    return Json(new { id, message = $"Web Service {wsdlDocument.ServiceName} saved successfuly." }, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetOntologyFromSession(int ontologyId)
        //{
        //    var ontologyDocument = GetOntologyDocumentFromSessionById(ontologyId);

        //public ActionResult GetUsersForShare()
        //{
        //    var users = _userService.GetUsersToShare(UserId).Select(x => new { x.Id, x.Email });
        //    return Json(users, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetSavedServiceDescription(string idServiceDescription)
        //{
        //    var serviceDescription = _serviceDescriptionService.Get(int.Parse(idServiceDescription));

        //    var serviceDescriptionsViewModel = Mapper.Map<ServiceDescriptionViewModel>(serviceDescription);

        //    return Json(serviceDescriptionsViewModel, JsonRequestBehavior.AllowGet);
        //}

        //    return Json(ontologyDocument, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetLiftingSchemaMappingFromWebServiceElementInSession(string serviceName, Guid elementId)
        //{
        //    throw new NotImplementedException();
        //    //var webServiceInSession = GetWsdlDocumentFromSessionByWebServiceName(serviceName);
        //    //if (webServiceInSession != null)
        //    //{
        //    //    var liftingSchemaMapping = webServiceInSession.GetLiftingSchemaMappingFromWebServiceElement(elementId);
        //    //    return Json(liftingSchemaMapping, JsonRequestBehavior.AllowGet);
        //    //}
        //    //return Json(null);
        //}

        //public ActionResult GetLoweringSchemaMappingFromWebServiceElementInSession(string serviceName, Guid elementId)
        //{
        //    throw new NotImplementedException();
        //    //var webServiceInSession = GetWsdlDocumentFromSessionByWebServiceName(serviceName);
        //    //if (webServiceInSession != null)
        //    //{
        //    //    var liftingSchemaMapping = webServiceInSession.GetLoweringSchemaMappingFromWebServiceElement(elementId);
        //    //    return Json(liftingSchemaMapping, JsonRequestBehavior.AllowGet);
        //    //}
        //    //return Json(null);
        //}

        //public ActionResult GetModelReferencesFromWebServiceElementInSession(string serviceName, Guid elementId)
        //{
        //    throw new NotImplementedException();
        //    //var webServiceInSession = GetWsdlDocumentFromSessionByWebServiceName(serviceName);
        //    //if (webServiceInSession != null)
        //    //{
        //    //    var modelReferences = webServiceInSession.GetModelReferencesFromWebServiceElement(elementId);
        //    //    return Json(modelReferences, JsonRequestBehavior.AllowGet);
        //    //}
        //    //return Json(null);
        //}

        //public ActionResult GetSavedServicesDescriptions()
        //{
        //    var servicesDescriptions = _serviceDescriptionService.GetAllByUser(UserId);

        //    var servicesDescriptionsViewModel = Mapper.Map<List<ServiceDescriptionViewModel>>(servicesDescriptions);

        //    return Json(servicesDescriptionsViewModel.Select(x => new { x.Id, x.ServiceName }), JsonRequestBehavior.AllowGet);
        //}

        #endregion AJAX Actions

        #endregion Actions

        #region Private methods

        private static string DefineEndpoint(ServiceDescriptionElementTypeEnum wsdlElementType)
        {
            string endpoint = null;

            switch (wsdlElementType)
            {
                case ServiceDescriptionElementTypeEnum.WsdlInterface:
                    endpoint = "interfaces";
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlOperation:
                    endpoint = "operations";
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlInput:
                    endpoint = "inputs";
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlOutput:
                    endpoint = "outputs";
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlInfault:
                    endpoint = "infaults";
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlOutfault:
                    endpoint = "outfaults";
                    break;

                case ServiceDescriptionElementTypeEnum.XsdComplexType:
                    endpoint = "complex-types";
                    break;

                case ServiceDescriptionElementTypeEnum.XsdSimpleType:
                    endpoint = "simple-types";
                    break;

                default:
                    break;
            }

            return endpoint;
        }

        private static void SetIdWsdlElementToAddIssueRequest(int idWsdlElement, ServiceDescriptionElementTypeEnum wsdlElementType, Issue_ApiRequestCreateModel addIssueRequestModel)
        {
            switch (wsdlElementType)
            {
                case ServiceDescriptionElementTypeEnum.WsdlInterface:
                    addIssueRequestModel.IdWsdlInterface = idWsdlElement;
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlOperation:
                    addIssueRequestModel.IdWsdlOperation = idWsdlElement;
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlFault:
                    addIssueRequestModel.IdWsdlFault = idWsdlElement;
                    break;

                //case ServiceDescriptionElementTypeEnum.WsdlInput:
                //    addIssueRequestModel.IdWsdlInput = idWsdlElement;
                //    break;

                //case ServiceDescriptionElementTypeEnum.WsdlOutput:
                //    addIssueRequestModel.IdWsdlOutput = idWsdlElement;
                //    break;

                //case ServiceDescriptionElementTypeEnum.WsdlInfault:
                //    addIssueRequestModel.IdWsdlInfault = idWsdlElement;
                //    break;

                //case ServiceDescriptionElementTypeEnum.WsdlOutfault:
                //    addIssueRequestModel.IdWsdlOutfault = idWsdlElement;
                //    break;

                case ServiceDescriptionElementTypeEnum.XsdComplexType:
                    addIssueRequestModel.IdXsdComplexType = idWsdlElement;
                    break;

                case ServiceDescriptionElementTypeEnum.XsdSimpleType:
                    addIssueRequestModel.IdXsdSimpleType = idWsdlElement;
                    break;

                default:
                    break;
            }
        }

        private static void SetIdWsdlElementToUpdateIssueRequest(int idWsdlElement, ServiceDescriptionElementTypeEnum wsdlElementType, Issue_ApiRequestUpdateModel updateIssueRequestModel)
        {
            switch (wsdlElementType)
            {
                case ServiceDescriptionElementTypeEnum.WsdlInterface:
                    updateIssueRequestModel.IdWsdlInterface = idWsdlElement;
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlOperation:
                    updateIssueRequestModel.IdWsdlOperation = idWsdlElement;
                    break;

                case ServiceDescriptionElementTypeEnum.WsdlFault:
                    updateIssueRequestModel.IdWsdlFault = idWsdlElement;
                    break;

                //case ServiceDescriptionElementTypeEnum.WsdlInput:
                //    updateIssueRequestModel.IdWsdlInput = idWsdlElement;
                //    break;

                //case ServiceDescriptionElementTypeEnum.WsdlOutput:
                //    updateIssueRequestModel.IdWsdlOutput = idWsdlElement;
                //    break;

                //case ServiceDescriptionElementTypeEnum.WsdlInfault:
                //    updateIssueRequestModel.IdWsdlInfault = idWsdlElement;
                //    break;

                //case ServiceDescriptionElementTypeEnum.WsdlOutfault:
                //    updateIssueRequestModel.IdWsdlOutfault = idWsdlElement;
                //    break;

                case ServiceDescriptionElementTypeEnum.XsdComplexType:
                    updateIssueRequestModel.IdXsdComplexType = idWsdlElement;
                    break;

                case ServiceDescriptionElementTypeEnum.XsdSimpleType:
                    updateIssueRequestModel.IdXsdSimpleType = idWsdlElement;
                    break;

                default:
                    break;
            }
        }

        #endregion Private methods
    }
}
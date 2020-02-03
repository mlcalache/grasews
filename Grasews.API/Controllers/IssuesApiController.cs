using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.ExternalService.Cytoscape.Models;
using Newtonsoft.Json;
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
    [RoutePrefix("api/issues")]
    [DisplayName("Issues")]
    public class IssuesApiController : BaseApiController
    {
        #region Private vars

        /// <summary>
        ///
        /// </summary>
        private readonly IIssueService _issueService;

        /// <summary>
        ///
        /// </summary>
        private readonly IIssueAnswerService _issueAnswerService;

        /// <summary>
        ///
        /// </summary>
        private readonly IServiceDescriptionService _serviceDescriptionService;

        /// <summary>
        ///
        /// </summary>
        private readonly IGraphService _graphService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="issueService"></param>
        /// <param name="issueAnswerService"></param>
        /// <param name="userIdentityService"></param>
        /// <param name="serviceDescriptionService"></param>
        /// <param name="graphService"></param>
        public IssuesApiController(IMapper mapper,
            IIssueService issueService,
            IIssueAnswerService issueAnswerService,
            IUserIdentityService userIdentityService,
            IServiceDescriptionService serviceDescriptionService,
            IGraphService graphService)
            : base(mapper, userIdentityService)
        {
            _issueService = issueService;
            _issueAnswerService = issueAnswerService;
            _serviceDescriptionService = serviceDescriptionService;
            _graphService = graphService;
        }

        #endregion Ctors

        #region Actions

        #region CRUD

        // GET: api/issues
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IEnumerable<Issue_ApiResponseViewModel>))]
        public IHttpActionResult GetIssues()
        {
            var issues = _issueService.GetAll();

            if (issues.Any())
            {
                var issuesResponseModel = _mapper.Map<List<Issue_ApiResponseViewModel>>(issues);

                return Ok(issuesResponseModel);
            }

            return NotFound();
        }

        // GET: api/issues/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Issue_ApiResponseViewModel))]
        public IHttpActionResult GetIssue(int id)
        {
            var issue = _issueService.Get(id);

            if (issue == null)
            {
                return NotFound();
            }

            var issueResponseModel = _mapper.Map<Issue_ApiResponseViewModel>(issue);

            return Ok(issueResponseModel);
        }

        // PUT: api/issues/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="issueRequestUpdateModel"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Issue_ApiResponseViewModel))]
        public IHttpActionResult PutIssue(int id, Issue_ApiRequestUpdateModel issueRequestUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest("Invalid Id.");
            }

            var existingIssue = _issueService.Get(id);

            if (existingIssue == null)
            {
                return NotFound();
            }

            var idUser = _userIdentityService.Id;

            if (existingIssue.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            var issue = _mapper.Map<Issue>(issueRequestUpdateModel);

            issue.Id = id;

            var count = _issueService.Update(issue);

            var issueResponseModel = _mapper.Map<Issue_ApiResponseViewModel>(issue);

            var serviceDescription = _serviceDescriptionService.Get(issueResponseModel.IdServiceDescription);

            var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

            //TODO: extract IFs-ELSEs below to private methods
            if (issueResponseModel.IdWsdlInterface.HasValue)
            {
                var wsdlInterface = _serviceDescriptionService.GetWsdlInterfaceWithSemanticAnnotations(issueResponseModel.IdWsdlInterface.Value);

                var allIssuesSolved = !wsdlInterface.Issues.Any(x => !x.Solved);

                if (allIssuesSolved)
                {
                    graphDataObject = _graphService.RemoveIssueFromWsdlInterface(graphDataObject, wsdlInterface) as CytoscapeObject;
                }
                else
                {
                    graphDataObject = _graphService.AddIssueToWsdlInterface(graphDataObject, wsdlInterface) as CytoscapeObject;
                }
            }
            else if (issueResponseModel.IdWsdlOperation.HasValue)
            {
                var wsdlOperation = _serviceDescriptionService.GetWsdlOperationWithSemanticAnnotations(issueResponseModel.IdWsdlOperation.Value);

                var allIssuesSolved = !wsdlOperation.Issues.Any(x => !x.Solved);

                if (allIssuesSolved)
                {
                    graphDataObject = _graphService.RemoveIssueFromWsdlOperation(graphDataObject, wsdlOperation) as CytoscapeObject;
                }
                else
                {
                    graphDataObject = _graphService.AddIssueToWsdlOperation(graphDataObject, wsdlOperation) as CytoscapeObject;
                }
            }
            else if (issueResponseModel.IdXsdComplexType.HasValue)
            {
                var xsdComplexElement = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(issueResponseModel.IdXsdComplexType.Value);

                var allIssuesSolved = !xsdComplexElement.Issues.Any(x => !x.Solved);

                if (allIssuesSolved)
                {
                    graphDataObject = _graphService.RemoveIssueFromXsdComplexType(graphDataObject, xsdComplexElement) as CytoscapeObject;
                }
                else
                {
                    graphDataObject = _graphService.AddIssueToXsdComplexType(graphDataObject, xsdComplexElement) as CytoscapeObject;
                }
            }
            else if (issueResponseModel.IdXsdSimpleType.HasValue)
            {
                var xsdSimpleElement = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(issueResponseModel.IdXsdSimpleType.Value);

                var allIssuesSolved = !xsdSimpleElement.Issues.Any(x => !x.Solved);

                if (allIssuesSolved)
                {
                    graphDataObject = _graphService.RemoveIssueFromXsdSimpleType(graphDataObject, xsdSimpleElement) as CytoscapeObject;
                }
                else
                {
                    graphDataObject = _graphService.AddIssueToXsdSimpleType(graphDataObject, xsdSimpleElement) as CytoscapeObject;
                }
            }

            serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

            _serviceDescriptionService.Update(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            issueResponseModel.ServiceDescription = serviceDescriptionResponseModel;

            return Ok(issueResponseModel);
        }

        // POST: api/issues
        /// <summary>
        ///
        /// </summary>
        /// <param name="issueRequestCreateModel"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Issue_ApiResponseViewModel))]
        public IHttpActionResult PostIssue(Issue_ApiRequestCreateModel issueRequestCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUser = _userIdentityService.Id;

            var issue = _mapper.Map<Issue>(issueRequestCreateModel);

            issue.IdOwnerUser = idUser;

            var count = _issueService.Create(issue);

            var issueResponseModel = _mapper.Map<Issue_ApiResponseViewModel>(issue);

            var serviceDescription = _serviceDescriptionService.Get(issueRequestCreateModel.IdServiceDescription);

            var graphDataObject = JsonConvert.DeserializeObject<CytoscapeObject>(serviceDescription.GraphJson);

            //TODO: extract IFs-ELSEs below to private methods
            if (issueRequestCreateModel.IdWsdlInterface.HasValue)
            {
                var wsdlInterface = _serviceDescriptionService.GetWsdlInterfaceWithSemanticAnnotations(issueRequestCreateModel.IdWsdlInterface.Value);

                graphDataObject = _graphService.AddIssueToWsdlInterface(graphDataObject, wsdlInterface) as CytoscapeObject;
            }
            else if (issueRequestCreateModel.IdWsdlOperation.HasValue)
            {
                var wsdlOperation = _serviceDescriptionService.GetWsdlOperationWithSemanticAnnotations(issueRequestCreateModel.IdWsdlOperation.Value);

                graphDataObject = _graphService.AddIssueToWsdlOperation(graphDataObject, wsdlOperation) as CytoscapeObject;
            }
            else if (issueRequestCreateModel.IdWsdlFault.HasValue)
            {
                var wsdlFault = _serviceDescriptionService.GetWsdlFaultWithSemanticAnnotations(issueRequestCreateModel.IdWsdlFault.Value);

                graphDataObject = _graphService.AddIssueToWsdlFault(graphDataObject, wsdlFault) as CytoscapeObject;
            }
            else if (issueRequestCreateModel.IdXsdComplexType.HasValue)
            {
                var xsdComplexElement = _serviceDescriptionService.GetXsdComplexTypeWithSemanticAnnotations(issueRequestCreateModel.IdXsdComplexType.Value);

                graphDataObject = _graphService.AddIssueToXsdComplexType(graphDataObject, xsdComplexElement) as CytoscapeObject;
            }
            else if (issueRequestCreateModel.IdXsdSimpleType.HasValue)
            {
                var xsdSimpleElement = _serviceDescriptionService.GetXsdSimpleTypeWithSemanticAnnotations(issueRequestCreateModel.IdXsdSimpleType.Value);

                graphDataObject = _graphService.AddIssueToXsdSimpleType(graphDataObject, xsdSimpleElement) as CytoscapeObject;
            }

            serviceDescription.GraphJson = JsonConvert.SerializeObject(graphDataObject);

            _serviceDescriptionService.Update(serviceDescription);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription);

            issueResponseModel.ServiceDescription = serviceDescriptionResponseModel;

            return Ok(issueResponseModel);
        }

        // DELETE: api/issues/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Issue_ApiResponseViewModel))]
        public IHttpActionResult DeleteIssue(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id.");
            }

            var existingIssue = _issueService.Get(id);

            var idUser = _userIdentityService.Id;

            if (existingIssue == null)
            {
                return NotFound();
            }

            if (existingIssue.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            if (existingIssue == null)
            {
                return NotFound();
            }

            var count = _issueService.Remove(existingIssue);

            var issueResponseModel = _mapper.Map<Issue_ApiResponseViewModel>(existingIssue);

            return Ok(issueResponseModel);
        }

        #endregion CRUD

        #region Answers

        // GET: api/issues/1/answers
        /// <summary>
        /// Retrieves a list of Answers according to the Issue
        /// </summary>
        /// <param name="idIssue">todo: describe idIssue parameter on GetIssueAnswers</param>
        /// <returns></returns>
        [Route("{idIssue:int}/answers")]
        [ResponseType(typeof(IEnumerable<IssueAnswer_ApiResponseViewModel>))]
        public IHttpActionResult GetIssueAnswers(int idIssue)
        {
            var issueAnswers = _issueAnswerService.GetAll(idIssue);

            if (issueAnswers.Any())
            {
                var issueAnswersResponseModel = _mapper.Map<List<IssueAnswer_ApiResponseViewModel>>(issueAnswers);

                return Ok(issueAnswersResponseModel);
            }

            return NotFound();
        }

        // GET: api/issues/1/answers/5
        /// <summary>
        /// Retrieves an Answer according to its Id and the Issue
        /// </summary>
        /// <param name="idIssue"></param>
        /// <param name="id">The Id of the Issue Answer</param>
        /// <returns></returns>
        [Route("{idIssue:int}/answers/{id:int}")]
        [ResponseType(typeof(IssueAnswer_ApiResponseViewModel))]
        public IHttpActionResult GetIssueAnswer(int idIssue, int id)
        {
            var issueAnswer = _issueAnswerService.Get(idIssue, id);

            if (issueAnswer == null)
            {
                return NotFound();
            }

            var issueAnswerResponseModel = _mapper.Map<IssueAnswer_ApiResponseViewModel>(issueAnswer);

            return Ok(issueAnswerResponseModel);
        }

        // PUT: api/issues/1/answers/5
        /// <summary>
        /// Updates an Answer from an Issue Task
        /// </summary>
        /// <param name="idIssue"></param>
        /// <param name="id">The Id of the Issue Task Answer</param>
        /// <param name="issueAnswerRequestModel">The Issue Answer object with update values</param>
        /// <returns></returns>
        [Route("{idIssue:int}/answers/{id:int}")]
        [ResponseType(typeof(IssueAnswer_ApiResponseViewModel))]
        public IHttpActionResult PutIssueAnswer(int idIssue, int id, IssueAnswer_ApiRequestUpdateModel issueAnswerRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != issueAnswerRequestModel.Id)
            {
                return BadRequest();
            }

            var issueAnswer = _issueAnswerService.Get(idIssue, id);

            if (issueAnswer == null)
            {
                return NotFound();
            }

            var idUser = _userIdentityService.Id;

            if (issueAnswer.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            issueAnswer = _mapper.Map<IssueAnswer>(issueAnswerRequestModel);

            issueAnswer.IdIssue = idIssue;

            var count = _issueAnswerService.Update(issueAnswer);

            var issueAnswerResponseModel = _mapper.Map<IssueAnswer_ApiResponseViewModel>(issueAnswer);

            return Ok(issueAnswerResponseModel);
        }

        // POST: api/issues/1/answers/5
        /// <summary>
        /// Creates an Answer for an Issue
        /// </summary>
        /// <param name="idIssue"></param>
        /// <param name="issueAnswerRequestModel">The Issue Answer object to be created</param>
        /// <returns></returns>
        [Route("{idIssue:int}/answers")]
        [ResponseType(typeof(IssueAnswer_ApiResponseViewModel))]
        public IHttpActionResult PostIssueAnswer(int idIssue, IssueAnswer_ApiRequestCreateModel issueAnswerRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUser = _userIdentityService.Id;

            var issueAnswer = _mapper.Map<IssueAnswer>(issueAnswerRequestModel);

            issueAnswer.IdOwnerUser = idUser;
            issueAnswer.IdIssue = idIssue;

            var count = _issueAnswerService.Create(issueAnswer);

            var issueAnswerResponseModel = _mapper.Map<IssueAnswer_ApiResponseViewModel>(issueAnswer);

            return Ok(issueAnswerResponseModel);
        }

        // DELETE: api/issues/1/answers/5
        /// <summary>
        /// Deletes an Answer from an Issue
        /// </summary>
        /// <param name="idIssue"></param>
        /// <param name="id">The Id of the Issue Answer</param>
        /// <returns></returns>
        [Route("{idIssue:int}/answers/{id:int}")]
        [ResponseType(typeof(IssueAnswer_ApiResponseViewModel))]
        public IHttpActionResult DeleteIssueAnswer(int idIssue, int id)
        {
            var issueAnswer = _issueAnswerService.Get(idIssue, id);

            if (issueAnswer == null)
            {
                return NotFound();
            }

            var idUser = _userIdentityService.Id;

            if (issueAnswer.IdOwnerUser != idUser)
            {
                return Unauthorized();
            }

            var count = _issueAnswerService.Remove(issueAnswer);

            var issueAnswerResponseModel = _mapper.Map<IssueAnswer_ApiResponseViewModel>(issueAnswer);

            return Ok(issueAnswerResponseModel);
        }

        #endregion Answers

        #endregion Actions
    }
}
using AutoMapper;
using Grasews.API.Models;
using Grasews.Application.DTOs;
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
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
    [RoutePrefix("api/ontologies")]
    [DisplayName("Ontologies")]
    public class OntologiesApiController : BaseApiController
    {
        #region Private vars

        private readonly IOntologyService _ontologyService;
        private readonly IOntology_UserService _ontology_UserService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="ontologyService"></param>
        /// <param name="userIdentityService"></param>
        /// <param name="ontology_UserService"></param>
        public OntologiesApiController(IMapper mapper,
            IOntologyService ontologyService,
            IUserIdentityService userIdentityService,
            IOntology_UserService ontology_UserService)
            : base(mapper, userIdentityService)
        {
            _ontologyService = ontologyService;
            _ontology_UserService = ontology_UserService;
        }

        #endregion Ctors

        #region Actions

        #region HTML

        // GET: api/ontologies/5/html/tree-view-menu
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
            var ontology = _ontologyService.GetComplete(id);

            if (ontology == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, "Ontology with given ID not found");
            }

            var html = _ontologyService.GetHtmlTreeViewMenu(ontology);

            return Ok(html);
        }

        #endregion HTML

        #region XML

        // GET: api/ontologies/5/terms/path?termId1=1&termId2=51
        /// <summary>
        ///
        /// </summary>
        /// <param name="termId1">todo: describe termId1 parameter on FindPathBetweenTerms</param>
        /// <param name="termId2">todo: describe termId2 parameter on FindPathBetweenTerms</param>
        /// <param name="id">todo: describe id parameter on FindPathBetweenTerms</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/terms/path")]
        [ResponseType(typeof(List<Ontology_ApiResponseViewModel.OntologyTermResponseViewModel>))]
        public IHttpActionResult FindPathBetweenTerms(int id, int termId1, int termId2)
        {
            // 1 DomainConcept
            // 2 Food
            // 6 PizzaTopping
            // 7 CheeseTopping
            // 51 FourCheesesTopping

            var ontology = _ontologyService.GetComplete(id);

            if (ontology == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, "Ontology with given ID not found");
            }

            var ontologyTerm1 = _ontologyService.FindTerm(ontology, termId1);

            if (ontologyTerm1 == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, $"Term with given ID {termId1} not found in the ontology");
            }

            var ontologyTerm2 = _ontologyService.FindTerm(ontology, termId2);

            if (ontologyTerm2 == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, $"Term with given ID {termId2} not found in the ontology");
            }

            var termsPath = _ontologyService.FindPathBetweenTerms(ontologyTerm1, ontologyTerm2);

            if (termsPath == null || !termsPath.Any())
            {
                return Content(System.Net.HttpStatusCode.NoContent, $"No relation between the two given terms ids ({termId1} and {termId2})");
            }

            var termsPathResponse = _mapper.Map<List<OntologyTermFindPath_ApiResponseViewModel>>(termsPath);

            return Ok(termsPathResponse);
        }

        // GET: api/ontologies/5/terms/10
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="termId">todo: describe termId parameter on GetHtmlTreeViewMenu</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/terms/{termId:int}")]
        [ResponseType(typeof(FindTerm_ApiResponseViewModel))]
        public IHttpActionResult FindTerm(int id, int termId)
        {
            var ontology = _ontologyService.GetComplete(id);

            if (ontology == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, "Ontology with given ID not found");
            }

            var ontologyTerm = _ontologyService.FindTerm(ontology, termId);

            if (ontologyTerm == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, "Term with given ID not found in the ontology");
            }

            var response = _mapper.Map<FindTerm_ApiResponseViewModel>(ontologyTerm);

            return Ok(response);
        }

        // POST: api/ontologies/xml
        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("xml")]
        [ResponseType(typeof(ParseOwl_ApiResponseViewModel))]
        public IHttpActionResult ParseXml(ParseOwl_ApiRequestViewModel request)
        {
            var requestDTO = _mapper.Map<ParseOwlRequestDTO>(request);

            var responseDTO = _ontologyService.ParseXml(requestDTO);

            var response = _mapper.Map<ParseOwl_ApiResponseViewModel>(responseDTO);

            return Ok(response);
        }

        #endregion XML

        #region CRUD

        // DELETE: api/ontologies/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Ontology_ApiResponseViewModel))]
        public IHttpActionResult DeleteOntology(int id)
        {
            var ontology = _ontologyService.Get(id);

            if (ontology == null)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, "Ontology with given ID not found");
            }

            var count = _ontologyService.Remove(ontology);

            var ontologyResponseModel = _mapper.Map<Ontology_ApiResponseViewModel>(ontology);

            return Ok(ontologyResponseModel);
        }

        // GET: api/ontologies
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(OntologyCollection_ApiResponseViewModel))]
        public IHttpActionResult GetOntologies()
        {
            var idUser = _userIdentityService.Id;

            var ontologies = _ontologyService.GetAllByUser(idUser);

            var ontologiesResponseModel = _mapper.Map<OntologyCollection_ApiResponseViewModel>(ontologies);

            return Ok(ontologiesResponseModel);
        }

        // GET: api/ontologies/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Ontology_ApiResponseViewModel))]
        public IHttpActionResult GetOntology(int id, bool complete = false)
        {
            var ontology = complete ? _ontologyService.GetComplete(id) : _ontologyService.Get(id);

            if (ontology == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, "Ontology with given ID not found");
            }

            var ontologyResponseModel = _mapper.Map<Ontology_ApiResponseViewModel>(ontology);

            return Ok(ontologyResponseModel);
        }

        // POST: api/ontologies
        /// <summary>
        ///
        /// </summary>
        /// <param name="ontologyRequestModel"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Ontology_ApiResponseViewModel))]
        public IHttpActionResult PostOntology(Ontology_ApiRequestCreateModel ontologyRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idUser = _userIdentityService.Id;

            var ontology = _mapper.Map<Ontology>(ontologyRequestModel);

            ontology.Xml = ontology.Xml.Replace("%26", "&");
            ontology.Xml = ontology.Xml.UnescapeXml();

            var ontologyName = _ontologyService.GetOntologyNameFromXml(ontology);

            var ontologyAlreadyExists = _ontologyService.CheckIfOntologyAlreadyExists(ontologyName);

            if (ontologyAlreadyExists)
            {
                ontology = _ontologyService.GetByOntologyName(ontologyName);

                if (ontology.IdOwnerUser == idUser)
                {
                    return Content(System.Net.HttpStatusCode.Conflict, "The ontology already exists for this user.");
                }

                var ontologyAlreadySharedWithUser = _ontology_UserService.CheckIfOntologyIsAlreadySharedWithUser(idUser);

                if (ontologyAlreadySharedWithUser)
                {
                    return Content(System.Net.HttpStatusCode.Conflict, "The ontology already exists for this user.");
                }

                var ontology_User = new Ontology_User { IdOntology = ontology.Id, IdSharedUser = idUser };

                var count = _ontology_UserService.Create(ontology_User);

                _ontologyService.ParseXml(ontology);
            }
            else
            {
                ontology.IdOwnerUser = idUser;
                ontology.OntologyName = ontologyName;

                var count = _ontologyService.Create(ontology);
            }

            var ontologyResponseModel = _mapper.Map<Ontology_ApiResponseViewModel>(ontology);

            return Ok(ontologyResponseModel);
        }

        // PUT: api/ontologies/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ontologyRequestModel"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Ontology_ApiResponseViewModel))]
        public IHttpActionResult PutOntology(int id, Ontology_ApiRequestUpdateModel ontologyRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ontologyRequestModel.Id)
            {
                return BadRequest();
            }

            var ontology = _mapper.Map<Ontology>(ontologyRequestModel);

            var count = _ontologyService.Update(ontology);

            var ontologyResponseModel = _mapper.Map<Ontology_ApiResponseViewModel>(ontology);

            return Ok(ontologyResponseModel);
        }

        #endregion CRUD

        #endregion Actions
    }
}
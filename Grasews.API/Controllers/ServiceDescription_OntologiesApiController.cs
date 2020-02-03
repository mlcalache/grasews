using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
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
    [RoutePrefix("api/service-descriptions-ontologies")]
    [DisplayName("Ontologies linked to Service Descriptions")]
    public class ServiceDescriptions_OntologiesApiController : BaseApiController
    {
        #region Private vars

        private readonly IServiceDescription_OntologyService _serviceDescription_OntologyService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="serviceDescription_OntologyService"></param>
        /// <param name="userIdentityService"></param>
        public ServiceDescriptions_OntologiesApiController(IMapper mapper,
            IServiceDescription_OntologyService serviceDescription_OntologyService,
            IUserIdentityService userIdentityService)
            : base(mapper, userIdentityService)
        {
            _serviceDescription_OntologyService = serviceDescription_OntologyService;
        }

        #endregion Ctors

        #region Actions

        // GET: api/service-descriptions-ontologies
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IEnumerable<ServiceDescription_Ontology_ApiResponseViewModel>))]
        public IHttpActionResult GetServiceDescription_Ontologies()
        {
            var serviceDescriptions = _serviceDescription_OntologyService.GetAll();

            if (serviceDescriptions.Any())
            {
                var serviceDescriptionsResponseModel = _mapper.Map<List<ServiceDescription_ApiResponseViewModel>>(serviceDescriptions);

                return Ok(serviceDescriptionsResponseModel);
            }

            return NotFound();
        }

        // GET: api/service-descriptions-ontologies-ontologies/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_Ontology_ApiResponseViewModel))]
        public IHttpActionResult GetServiceDescription_Ontology(int id)
        {
            var serviceDescription_Ontology = _serviceDescription_OntologyService.Get(id);

            if (serviceDescription_Ontology == null)
            {
                return NotFound();
            }

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription_Ontology);

            return Ok(serviceDescriptionResponseModel);
        }

        // POST: api/service-descriptions-ontologies
        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription_OntologyRequestModel"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(ServiceDescription_ApiResponseViewModel))]
        public IHttpActionResult PostServiceDescription(ServiceDescription_Ontology_ApiRequestCreateModel serviceDescription_OntologyRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceDescription_Ontology = _mapper.Map<ServiceDescription_Ontology>(serviceDescription_OntologyRequestModel);

            var count = _serviceDescription_OntologyService.Create(serviceDescription_Ontology);

            var serviceDescriptionResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription_Ontology);

            return CreatedAtRoute("DefaultApi", new { id = serviceDescriptionResponseModel.Id }, serviceDescriptionResponseModel);
        }

        // DELETE: api/service-descriptions-ontologies/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_Ontology_ApiResponseViewModel))]
        public IHttpActionResult DeleteServiceDescription(int id)
        {
            var serviceDescription_Ontology = _serviceDescription_OntologyService.Get(id);

            if (serviceDescription_Ontology == null)
            {
                return NotFound();
            }

            var count = _serviceDescription_OntologyService.Remove(serviceDescription_Ontology);

            var serviceDescription_OntologyResponseModel = _mapper.Map<ServiceDescription_ApiResponseViewModel>(serviceDescription_Ontology);

            return Ok(serviceDescription_OntologyResponseModel);
        }

        #endregion Actions
    }
}
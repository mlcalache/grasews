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
    [RoutePrefix("api/service-description-users")]
    [DisplayName("Service Descriptions linked to Users")]
    public class ServiceDescription_UsersApiController : BaseApiController
    {
        #region Private vars

        private readonly IServiceDescription_UserService _serviceDescription_UserService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="serviceDescription_UserService"></param>
        /// <param name="userIdentityService"></param>
        public ServiceDescription_UsersApiController(IMapper mapper,
            IServiceDescription_UserService serviceDescription_UserService,
            IUserIdentityService userIdentityService)
            : base(mapper, userIdentityService)
        {
            _serviceDescription_UserService = serviceDescription_UserService;
        }

        #endregion Ctors

        #region Actions

        // GET: api/service-description-users
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IEnumerable<ServiceDescription_User_ApiResponseViewModel>))]
        public IHttpActionResult GetServiceDescription_Users()
        {
            var serviceDescriptions = _serviceDescription_UserService.GetAll();

            if (serviceDescriptions.Any())
            {
                var serviceDescriptionsResponseModel = _mapper.Map<List<ServiceDescription_User_ApiResponseViewModel>>(serviceDescriptions);

                return Ok(serviceDescriptionsResponseModel);
            }

            return NotFound();
        }

        // GET: api/service-description-users/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_User_ApiResponseViewModel))]
        public IHttpActionResult GetServiceDescription_User(int id)
        {
            var serviceDescription = _serviceDescription_UserService.Get(id);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            var serviceDescription_UserResponseModel = _mapper.Map<ServiceDescription_User_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescription_UserResponseModel);
        }

        // PUT: api/service-description-users/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serviceDescription_UserRequestUpdateModel"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_User_ApiResponseViewModel))]
        public IHttpActionResult PutServiceDescription_User(int id, ServiceDescription_User_ApiRequestUpdateModel serviceDescription_UserRequestUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceDescription_UserRequestUpdateModel.Id)
            {
                return BadRequest();
            }

            var serviceDescription_User = _mapper.Map<ServiceDescription_User>(serviceDescription_UserRequestUpdateModel);

            var count = _serviceDescription_UserService.Update(serviceDescription_User);

            var serviceDescription_UserResponseModel = _mapper.Map<ServiceDescription_User_ApiResponseViewModel>(serviceDescription_User);

            return CreatedAtRoute("DefaultApi", new { id = serviceDescription_UserResponseModel.Id }, serviceDescription_UserResponseModel);
        }

        // POST: api/service-description-users
        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceDescription_UserRequestCreateModel"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(ServiceDescription_User_ApiResponseViewModel))]
        public IHttpActionResult PostServiceDescription_User(ServiceDescription_User_ApiRequestCreateModel serviceDescription_UserRequestCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceDescription_User = _mapper.Map<ServiceDescription_User>(serviceDescription_UserRequestCreateModel);

            var count = _serviceDescription_UserService.Create(serviceDescription_User);

            var serviceDescription_UserResponseModel = _mapper.Map<ServiceDescription_User_ApiResponseViewModel>(serviceDescription_User);

            return CreatedAtRoute("DefaultApi", new { id = serviceDescription_UserResponseModel.Id }, serviceDescription_UserResponseModel);
        }

        // DELETE: api/service-description-users/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(ServiceDescription_User_ApiResponseViewModel))]
        public IHttpActionResult DeleteServiceDescription_User(int id)
        {
            var serviceDescription = _serviceDescription_UserService.Get(id);

            if (serviceDescription == null)
            {
                return NotFound();
            }

            var count = _serviceDescription_UserService.Remove(serviceDescription);

            var serviceDescription_UserResponseModel = _mapper.Map<ServiceDescription_User_ApiResponseViewModel>(serviceDescription);

            return Ok(serviceDescription_UserResponseModel);
        }

        #endregion Actions
    }
}
using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Interfaces.Services;
using System.ComponentModel;
using System.Web.Http;
using System.Web.Http.Description;

namespace Grasews.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [RoutePrefix("api/account")]
    [DisplayName("Account")]
    public class AccountApiController : BaseApiController
    {
        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userIdentityService"></param>
        public AccountApiController(IMapper mapper,
            IUserIdentityService userIdentityService)
            : base(mapper, userIdentityService)
        {
        }

        #endregion Ctors

        #region Actions

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Account_ApiResponseViewModel))]
        //[SwaggerOperation(Tags = new[] { "Account 1", "Account 2" })]
        public IHttpActionResult Get()
        {
            var userViewModel = new Account_ApiResponseViewModel
            {
                Email = _userIdentityService.Email,
                FirstName = _userIdentityService.GivenName,
                LastName = _userIdentityService.Surname,
                Id = _userIdentityService.Id,
                IsAdmin = _userIdentityService.IsAdmin,
                Username = _userIdentityService.Username,
                RegistrationDateTime = _userIdentityService.RegistrationDateTime
            };

            return Ok(userViewModel);
        }

        #endregion Actions
    }
}
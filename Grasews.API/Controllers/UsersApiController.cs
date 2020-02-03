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
    [RoutePrefix("api/users")]
    [DisplayName("Users")]
    public class UsersApiController : BaseApiController
    {
        #region Private vars

        private readonly IUserService _userService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userService"></param>
        /// <param name="userIdentityService"></param>
        public UsersApiController(IMapper mapper,
            IUserService userService,
            IUserIdentityService userIdentityService)
            : base(mapper, userIdentityService)
        {
            _userService = userService;
        }

        #endregion Ctors

        #region Actions

        // GET: api/users
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IEnumerable<User_ApiResponseViewModel>))]
        public IHttpActionResult GetUsers()
        {
            var users = _userService.GetAll();

            if (users.Any())
            {
                var usersResponseModel = _mapper.Map<List<User_ApiResponseViewModel>>(users);

                return Ok(usersResponseModel);
            }

            return NotFound();
        }

        // GET: api/users/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(User_ApiResponseViewModel))]
        public IHttpActionResult GetUser(int id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var userResponseModel = _mapper.Map<User_ApiResponseViewModel>(user);

            return Ok(userResponseModel);
        }

        // PUT: api/users/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRequestUpdateModel"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}")]
        [ResponseType(typeof(User_ApiResponseViewModel))]
        public IHttpActionResult PutUser(int id, User_ApiRequestUpdateModel userRequestUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRequestUpdateModel.Id)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userRequestUpdateModel);

            var count = _userService.Update(user);

            var userResponseModel = _mapper.Map<User_ApiResponseViewModel>(userRequestUpdateModel);

            return Ok(userResponseModel);
        }

        // POST: api/users
        /// <summary>
        ///
        /// </summary>
        /// <param name="userRequestCreateModel"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(User_ApiResponseViewModel))]
        public IHttpActionResult PostUser(User_ApiRequestCreateModel userRequestCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userRequestCreateModel);

            var count = _userService.Create(user);

            var userResponseModel = _mapper.Map<User_ApiResponseViewModel>(user);

            return Ok(userResponseModel);
        }

        // DELETE: api/users/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}")]
        [ResponseType(typeof(User_ApiResponseViewModel))]
        public IHttpActionResult DeleteUser(int id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var count = _userService.Remove(user);

            var userResponseModel = _mapper.Map<User_ApiResponseViewModel>(user);

            return Ok(userResponseModel);
        }

        // POST: api/users/1/password-reset-request
        /// <summary>
        ///
        /// </summary>
        /// <param name="resetPasswordRequestCreateModel"></param>
        /// <returns></returns>
        [Route("{id:int}/password-reset-request")]
        [ResponseType(typeof(ResetPassword_ApiResponseModel))]
        public IHttpActionResult PostResetPasswordRequest(ResetPasswordRequest_ApiRequestCreateModel resetPasswordRequestCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resetPassword = _userService.RequestResetPassword(resetPasswordRequestCreateModel.Email, resetPasswordRequestCreateModel.IdUser);

            var resetPasswordModel = _mapper.Map<ResetPassword_ApiResponseModel>(resetPassword);

            return Ok(resetPasswordModel);
        }

        // POST: api/users/1/password-reset
        /// <summary>
        ///
        /// </summary>
        /// <param name="resetPasswordCreateModel"></param>
        /// <returns></returns>
        [Route("{id:int}/password-reset")]
        [ResponseType(typeof(ResetPassword_ApiResponseModel))]
        public IHttpActionResult PostResetPassword(ResetPassword_ApiRequestCreateModel resetPasswordCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resetPassword = _userService.GetResetPasswordRequestByResetSecurity(resetPasswordCreateModel.ResetPasswordSecurity);

            if (resetPassword == null)
            {
                return NotFound();
            }

            if (resetPassword.IdUser != resetPasswordCreateModel.IdUser)
            {
                return BadRequest();
            }

            resetPassword.NewPassword = resetPasswordCreateModel.Password;

            resetPassword = _userService.ResetPassword(resetPassword);

            var resetPasswordModel = _mapper.Map<ResetPassword_ApiResponseModel>(resetPassword);

            return Ok(resetPasswordModel);
        }

        #endregion Actions
    }
}
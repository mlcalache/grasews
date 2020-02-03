using Grasews.API.Models;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Web.ViewModels;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class ShareInvitationController : BaseController
    {
        #region Private vars

        private readonly IShareInvitationService _shareInvitationService;
        private readonly IUserService _userService;

        #endregion Private vars

        #region Private props

        private IAuthenticationManager _auth
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion Private props

        #region Ctors

        public ShareInvitationController(IShareInvitationService shareInvitationService,
            IUserService userService,
            IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
            _shareInvitationService = shareInvitationService;
            _userService = userService;
        }

        #endregion Ctors

        #region Actions

        [AllowAnonymous]
        public async Task<ActionResult> AcceptInvitation(Guid s)
        {
            var shareInvitation = _shareInvitationService.GetByInvitationSecurity(s);

            var viewModel = new ShareInvitation_Acceptance_MvcViewModel
            {
                IdServiceDescription = shareInvitation.IdServiceDescription,
                Email = shareInvitation.Email,
                IdSharedInvitation = shareInvitation.Id
            };

            var isExistingUser = shareInvitation.ExistingUser;

            User_ApiResponseViewModel userResponseViewModel = null;

            #region Get user by e-mail

            var getAllUserRequest = CreateApiRequest($"api/users", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getAllUserResponse = await _apiRestClient.ExecuteAsync<List<User_ApiResponseViewModel>>(getAllUserRequest);

            #endregion Get user by e-mail

            if (getAllUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                userResponseViewModel = getAllUserResponse.Data.FirstOrDefault(x => x.Email == shareInvitation.Email.ToLower());

                if (userResponseViewModel != null)
                {
                    isExistingUser = true;
                }
            }

            if (isExistingUser)
            {
                if (userResponseViewModel != null)
                {
                    var acceptInvitationForExistingUserBodyRequest = Mapper.Map<ShareInvitationAccept_ApiRequestCreateModel>(viewModel);

                    var acceptInvitationForExistingUserRequest = CreateApiRequest($"api/share-invitations/{shareInvitation.Id}/users/{userResponseViewModel.Id}/acceptance", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                    acceptInvitationForExistingUserRequest.AddRequestBodyParameter(acceptInvitationForExistingUserBodyRequest);
                    var acceptInvitationForExistingUserResponse = await _apiRestClient.ExecuteAsync<User_ApiResponseViewModel>(acceptInvitationForExistingUserRequest);

                    return RedirectToAction("Index", "Account");
                }
            }
            else
            {
                return View(viewModel);
            }

            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AcceptInvitation(ShareInvitation_Acceptance_MvcViewModel viewModel)
        {
            var acceptInvitationForNewUserBodyRequest = Mapper.Map<ShareInvitationAccept_ApiRequestCreateModel>(viewModel);

            var acceptInvitationForNewUserRequest = CreateApiRequest($"api/share-invitations/{viewModel.IdSharedInvitation}/users/acceptance", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            acceptInvitationForNewUserRequest.AddRequestBodyParameter(acceptInvitationForNewUserBodyRequest);
            var acceptInvitationForNewUserResponse = await _apiRestClient.ExecuteAsync<int>(acceptInvitationForNewUserRequest);

            if (acceptInvitationForNewUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (Username != null)
                {
                    _auth.SignOut();

                    RemoveCookies(GRASEWS_USER_COOKIE);

                    return RedirectToAction("Index", "Account");
                }
            }

            return RedirectToAction("Index", "Account");
        }

        [AllowAnonymous]
        public ActionResult RejectInvitation(int? idUserInviter, int? idServiceDescription, string email)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RejectInvitation(ShareInvitation_Rejection_MvcViewModel model)
        {
            return View();
        }

        #endregion Actions
    }
}
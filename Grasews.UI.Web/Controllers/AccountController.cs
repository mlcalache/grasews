using Grasews.API.Models;
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.AuthenticationHeader;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Infra.CrossCutting.Security;
using Grasews.Web.DTOs;
using Grasews.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Private vars

        private readonly IServiceDescriptionService _serviceDescriptionService;
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;

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

        public AccountController(IServiceDescriptionService serviceDescriptionAppService,
            IUserService userAppService,
            IAPIRestClient apiRestClient,
            IUserIdentityService userIdentityService)
            : base(apiRestClient)
        {
            _userService = userAppService;
            _serviceDescriptionService = serviceDescriptionAppService;
            _userIdentityService = userIdentityService;
        }

        #endregion Ctors

        #region Actions

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(User_MvcCreateViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = Mapper.Map<User>(userViewModel);

                    var request = CreateApiRequest("api/users", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                    request.AddRequestBodyParameter(user);

                    var userResponse = await _apiRestClient.ExecuteAsync<Token_ApiResponseCreateModel>(request);

                    if (userResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        AddToastSuccessMessage("User created successfully");

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    AddToastDangerMessage("Problem with inputs");

                    return RedirectToAction("Index", userViewModel);
                }
            }
            catch (Exception ex)
            {
                AddToastDangerMessage(ex.InnerException.Message);

                return RedirectToAction("Index", userViewModel);
            }

            return RedirectToAction("Index", userViewModel);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var isAuthenticated = System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                return RedirectToAction("Index", "Site");
            }
            else
            {
                RemoveCookies(GRASEWS_ACCESS_TOKEN_COOKIE,
                    GRASEWS_REFRESHTOKEN_COOKIE,
                    GRASEWS_USER_COOKIE,
                    GRASEWS_USERSERVICEDESCRIPTIONS_COOKIE,
                    GRASEWS_ONTOLOGIES_OPENED_COOKIE,
                    GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE,
                    GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(Login_MvcViewModel loginRequestModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(loginRequestModel.Username))
                    {
                        AddToastWarningMessage(WebResource.UsernameEmpty);

                        return RedirectToAction("Index", loginRequestModel);
                    }

                    if (string.IsNullOrEmpty(loginRequestModel.Password))
                    {
                        AddToastWarningMessage(WebResource.PasswordEmpty);

                        return RedirectToAction("Index", loginRequestModel);
                    }
                    var loginDTO = new LoginRequestDTO
                    {
                        grant_type = GRANT_TYPE_PASSWORD,
                        username = loginRequestModel.Username,
                        password = loginRequestModel.Password
                    };

                    var request = CreateApiRequest(TOKEN_RESOURCE, HttpMethodENUM.POST, "application/x-www-form-urlencoded");
                    request.AddRequestBodyParameter(loginDTO);
                    var authResponse = await _apiRestClient.ExecuteAsync<Token_ApiResponseCreateModel>(request);
                    var loginResponseData = authResponse.Data;

                    //var client = new RestClient($"{ConfigurationManagerHelper.ApiUrl}/token");
                    //var request = new RestRequest(Method.POST);
                    //request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("Cache-Control", "no-cache");
                    //request.AddHeader("Accept", "*/*");
                    //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    //request.AddParameter("undefined", $"grant_type=password&username={loginRequestModel.Username}&password={loginRequestModel.Password}", ParameterType.RequestBody);
                    //var authResponse = client.Execute(request);
                    //var loginResponseDTO = JsonConvert.DeserializeObject<Token_ApiResponseCreateModel>(authResponse.Content);

                    if (loginResponseData != null)
                    {
                        var accessTokenCookie = SetCookie(GRASEWS_ACCESS_TOKEN_COOKIE, loginResponseData.access_token, loginResponseData.expires_in);

                        var request2 = CreateApiRequest("api/account", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                        _apiRestClient.AuthenticationHeaderInitializer = new APIBearerAuthenticationHeaderInitializer(() => loginResponseData.access_token);
                        var accountResponse = await _apiRestClient.ExecuteAsync<Account_MvcViewModel>(request2);
                        var accountResponseData = accountResponse.Data;

                        //var client2 = new RestClient($"{ConfigurationManagerHelper.ApiUrl}/api/account");
                        //var request2 = new RestRequest(Method.GET);
                        //request2.AddHeader("cache-control", "no-cache");
                        //request2.AddHeader("Cache-Control", "no-cache");
                        //request2.AddHeader("Accept", "*/*");
                        //request2.AddHeader("Authorization", $"Bearer {loginResponseDTO.access_token}");
                        //var accountResponse = client2.Execute(request2);
                        //var accountResponseData = JsonConvert.DeserializeObject<Account_MvcViewModel>(accountResponse.Content);

                        CreateIdentity(accountResponseData, false);
                    }
                    else
                    {
                        AddToastDangerMessage(WebResource.UsernameOrPasswordWrong);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                AddToastDangerMessage(WebResource.AnErrorHasHappened);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _auth.SignOut();

            RemoveCookies(GRASEWS_ACCESS_TOKEN_COOKIE,
               GRASEWS_REFRESHTOKEN_COOKIE,
               GRASEWS_USER_COOKIE,
               GRASEWS_USERSERVICEDESCRIPTIONS_COOKIE,
               GRASEWS_ONTOLOGIES_OPENED_COOKIE,
               GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE,
               GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE);

            Session.Clear();

            return RedirectToAction("Index", "Account");
        }

        [AllowAnonymous]
        public ActionResult RequestResetPassword()
        {
            return View(new RequestResetPassword_MvcViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RequestResetPassword(string email)
        {
            var user = _userService.GetByEmail(email);

            if (user == null)
            {
                AddToastDangerMessage(WebResource.EmailNotFound);

                return View("Index");
            }

            var resetPasswordRequestModel = new ResetPasswordRequest_ApiRequestCreateModel
            {
                Email = email,
                IdUser = user.Id
            };

            var resetPasswordRequest = CreateApiRequest($"api/users/{user.Id}/password-reset-request", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            resetPasswordRequest.AddRequestBodyParameter(resetPasswordRequestModel);
            var resetPasswordResponse = await _apiRestClient.ExecuteAsync<ResetPassword_ApiResponseModel>(resetPasswordRequest);

            return View("Index", resetPasswordResponse);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(Guid s)
        {
            var resetPasswordRequest = _userService.GetResetPasswordRequestByResetSecurity(s);

            var viewModel = new ResetPassword_MvcViewModel
            {
                Id = resetPasswordRequest.Id,
                IdUser = resetPasswordRequest.IdUser,
                ResetPasswordSecurity = resetPasswordRequest.ResetPasswordSecurity
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPassword_MvcViewModel resetPasswordModel)
        {
            var resetPasswordRequestModel = new ResetPassword_ApiRequestCreateModel
            {
                Password = resetPasswordModel.NewPassword,
                ResetPasswordSecurity = resetPasswordModel.ResetPasswordSecurity,
                IdUser = resetPasswordModel.IdUser
            };

            var resetPasswordRequest = CreateApiRequest($"api/users/{resetPasswordModel.IdUser}/password-reset", HttpMethodENUM.POST, "application/x-www-form-urlencoded");
            resetPasswordRequest.AddRequestBodyParameter(resetPasswordRequestModel);
            var resetPasswordResponse = await _apiRestClient.ExecuteAsync<ResetPassword_ApiResponseModel>(resetPasswordRequest);

            return View("Index");
        }

        #endregion Actions

        #region Private methods

        private void CreateIdentity(Account_MvcViewModel accountModel, bool remember)
        {
            var claimUserId = new Claim(ClaimTypes.NameIdentifier, accountModel.Id.ToString());
            var claimUserName = new Claim(ClaimTypes.Name, accountModel.Username);
            var claimUserEmail = new Claim(ClaimTypes.Email, accountModel.Email);
            var claimUserFirstName = new Claim(ClaimTypes.GivenName, accountModel.FirstName);
            var claimUserLastName = new Claim(ClaimTypes.Surname, accountModel.LastName);
            var claimRegistrationDateTime = new Claim(SecurityConsts.CLAIMTYPE_REGISTRATIONDATETIME, accountModel.RegistrationDateTime.ToString());

            var claims = new[] { claimUserId, claimUserName, claimUserEmail, claimUserFirstName, claimUserLastName, claimRegistrationDateTime };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var authProperties = new AuthenticationProperties { IsPersistent = remember };

            _auth.SignIn(authProperties, identity);
        }

        #endregion Private methods
    }
}
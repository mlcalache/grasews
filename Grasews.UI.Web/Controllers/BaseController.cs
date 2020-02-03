using AutoMapper;
using Grasews.Infra.CrossCutting.Helpers;
using Grasews.Infra.CrossCutting.Helpers.RestClient;
using Grasews.Infra.CrossCutting.Helpers.RestClient.AuthenticationHeader;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Infra.CrossCutting.Security;
using Grasews.Web.AutoMapper;
using Grasews.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Protected consts

        protected const int COOKIE_DEFAULT_EXPIRATION_TIME = 1440;

        protected const string GRANT_TYPE_PASSWORD = "password";
        protected const string GRASEWS_ACCESS_TOKEN_COOKIE = "GRASEWS_ACCESS_TOKEN_COOKIE";
        protected const string GRASEWS_ONTOLOGIES_OPENED_COOKIE = "GRASEWS_ONTOLOGIES_OPENED_COOKIE";
        protected const string GRASEWS_REFRESHTOKEN_COOKIE = "GRASEWS_REFRESHTOKEN_COOKIE";
        protected const string GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE = "GRASEWS_SERVICEDESCRIPTION_ON_VIEW_COOKIE";
        protected const string GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE = "GRASEWS_SERVICEDESCRIPTIONS_OPENED_COOKIE";
        protected const string GRASEWS_USER_COOKIE = "GRASEWS_USER_COOKIE";
        protected const string GRASEWS_USERSERVICEDESCRIPTIONS_COOKIE = "GRASEWS_USERSERVICEDESCRIPTIONS_COOKIE";
        protected const int PADRAO_TIMEOUT_TOAST = 3000;

        //protected const string SESSION_ONTOLOGIES_OPENED = "SESSION_ONTOLOGIES_OPENED";
        //protected const string SESSION_WEBSERVICES_OPENED = "SESSION_WEBSERVICES_OPENED";

        protected const string TOKEN_RESOURCE = "token";

        #endregion Protected consts

        #region Protected props

        protected string ApiUrl => ConfigurationManagerHelper.ApiUrl;

        protected IEnumerable<Claim> Claims
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;
                return identity.Claims;
            }
        }

        protected int IdUser => int.Parse(User.Identity.GetUserId());
        protected IMapper Mapper => new Mapper(AutoMapperConfig.CreateMapperConfiguration());
        protected string UserFirstName => Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
        protected string UserFullName => $"{UserFirstName} {UserLastName}";
        protected string UserLastName => Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value;
        protected string UserEmail => Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        protected string Username => User.Identity.GetUserName();
        protected DateTime UserRegistrationDate => DateTime.Parse(Claims.FirstOrDefault(x => x.Type == SecurityConsts.CLAIMTYPE_REGISTRATIONDATETIME).Value);

        #endregion Protected props

        #region Protected vars

        protected readonly IAPIRestClient _apiRestClient;

        #endregion Protected vars

        #region Ctors

        public BaseController(IAPIRestClient apiRestClient)
        {
            _apiRestClient = apiRestClient;

            if (_apiRestClient != null)
            {
                _apiRestClient.BaseUrl = new Uri(ApiUrl);
            }
        }

        #endregion Ctors

        #region Protected methods

        #region Toast

        protected ToastMessage_MvcResponseViewModel AddToastDangerMessage(string message, int timeout = PADRAO_TIMEOUT_TOAST)
        {
            var toastr = TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(message, ToastTypeEnum.Danger, timeout);
            TempData["Toastr"] = toastr;

            return toastMessage;
        }

        protected ToastMessage_MvcResponseViewModel AddToastInfoMessage(string message, int timeout = PADRAO_TIMEOUT_TOAST)
        {
            var toastr = TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(message, ToastTypeEnum.Info, timeout);
            TempData["Toastr"] = toastr;

            return toastMessage;
        }

        protected ToastMessage_MvcResponseViewModel AddToastSuccessMessage(string message, int timeout = PADRAO_TIMEOUT_TOAST)
        {
            var toastr = TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(message, ToastTypeEnum.Success, timeout);
            TempData["Toastr"] = toastr;

            return toastMessage;
        }

        protected ToastMessage_MvcResponseViewModel AddToastWarningMessage(string message, int timeout = PADRAO_TIMEOUT_TOAST)
        {
            var toastr = TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(message, ToastTypeEnum.Warning, timeout);
            TempData["Toastr"] = toastr;

            return toastMessage;
        }

        #endregion Toast

        #region Cookie

        protected HttpCookie GetCookie(string nome)
        {
            var cookie = Request.Cookies[nome];

            return cookie;
        }

        protected void RemoveCookies(params string[] cookieNames)
        {
            foreach (var t in cookieNames)
            {
                Response.Cookies.Add(new HttpCookie(t)
                {
                    Expires = DateTime.Now.AddDays(-1)
                });
            }
        }

        protected HttpCookie SetCookie(string cookieName, string cookieValue, int cookieExpires = COOKIE_DEFAULT_EXPIRATION_TIME)
        {
            var cookie = new HttpCookie(cookieName, cookieValue) { Expires = DateTime.Now.AddMinutes(cookieExpires) };

            Response.Cookies.Add(cookie);

            return cookie;
        }

        #endregion Cookie

        #region API Client and API settings

        protected IAPIRestRequest CreateApiRequest(string resource, HttpMethodENUM method, string contentType = "application/json")
        {
            var request = new APIRestRequest(resource, method);
            request.Headers.Add("Content-Type", contentType);

            var timeOut = -1;

            if (int.TryParse(ConfigurationManagerHelper.APITimeout, out timeOut))
            {
                request.Timeout = timeOut;
            }

            return request;
        }

        protected IAPIRestRequest CreateApiRequest(string resource, HttpMethodENUM method, object content)
        {
            var request = CreateApiRequest(resource, method);

            request.Content = content;

            return request;
        }

        protected void SetAuthorizationToAPICalls()
        {
            var accessToken = GetCookie(GRASEWS_ACCESS_TOKEN_COOKIE);
            _apiRestClient.AuthenticationHeaderInitializer = new APIBearerAuthenticationHeaderInitializer(() => accessToken.Value);
        }

        #endregion API Client and API settings

        #endregion Protected methods
    }
}
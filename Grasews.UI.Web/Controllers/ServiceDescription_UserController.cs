using Grasews.API.Models;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class ServiceDescription_UserController : BaseController
    {
        #region Private vars

        private readonly IServiceDescription_UserService _serviceDescription_UserService;
        private readonly IUserService _userService;
        private readonly IServiceDescriptionService _serviceDescriptionService;
        private readonly IShareInvitationService _shareInvitationService;

        #endregion Private vars

        #region Ctors

        public ServiceDescription_UserController(IServiceDescription_UserService serviceDescription_UserService,
            IUserService userService,
            IServiceDescriptionService serviceDescriptionService,
            IShareInvitationService shareInvitationService,
            IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
            _serviceDescription_UserService = serviceDescription_UserService;
            _serviceDescriptionService = serviceDescriptionService;
            _shareInvitationService = shareInvitationService;
            _userService = userService;
        }

        #endregion Ctors

        #region Actions

        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult> Index(int? idServiceDescription)
        {
            var model = new SharedServiceDescription_MvcViewModel();

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the users which the service description is shared to from the API

            var getServiceDescriptionsSharedRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/service-description-users", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getServiceDescriptionsSharedResponse = await _apiRestClient.ExecuteAsync<List<ServiceDescription_User_ApiResponseViewModel>>(getServiceDescriptionsSharedRequest);

            #endregion Get the users which the service description is shared to from the API

            if (getServiceDescriptionsSharedResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescriptions_Users = getServiceDescriptionsSharedResponse.Data;

                model.IdServiceDescription = idServiceDescription.Value;

                var serviceDescriptions_UsersViewModel = Mapper.Map<List<ServiceDescription_User_MvcViewModel>>(serviceDescriptions_Users);

                model.ServiceDescription_UserViewModels = serviceDescriptions_UsersViewModel;

                return View(model);
            }

            return View(model);
        }

        #endregion Actions
    }
}
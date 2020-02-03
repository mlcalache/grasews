using Grasews.API.Models;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Enums;
using Grasews.Infra.CrossCutting.Helpers.RestClient.Interfaces;
using Grasews.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Grasews.Web.Controllers
{
    public class ServiceDescriptionController : BaseController
    {
        #region Private vars

        private readonly IServiceDescription_UserService _serviceDescription_UserService;
        private readonly IServiceDescriptionService _serviceDescriptionService;

        #endregion Private vars

        #region Ctors

        public ServiceDescriptionController(IServiceDescriptionService serviceDescriptionService,
            IServiceDescription_UserService serviceDescription_UserService,
            IAPIRestClient apiRestClient)
            : base(apiRestClient)
        {
            _serviceDescriptionService = serviceDescriptionService;
            _serviceDescription_UserService = serviceDescription_UserService;
        }

        #endregion Ctors

        #region Actions

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int idServiceDescription)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            ServiceDescription_ApiResponseViewModel serviceDescription = null;

            var serviceDescriptionRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var serviceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(serviceDescriptionRequest);

            if (serviceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                serviceDescription = serviceDescriptionResponse.Data;

                if (serviceDescription.IdOwnerUser != IdUser)
                {
                    AddToastDangerMessage(WebResource.UserIsNotTheOwner);
                }
            }

            var serviceDescriptionDeleteViewModel = Mapper.Map<ServiceDescription_MvcDeleteViewModel>(serviceDescription);

            return View(serviceDescriptionDeleteViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int idServiceDescription, ServiceDescription_MvcDeleteViewModel requestModel)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            ServiceDescription_ApiResponseViewModel serviceDescription;

            var serviceDescriptionRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var serviceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(serviceDescriptionRequest);

            if (serviceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                serviceDescription = serviceDescriptionResponse.Data;

                if (serviceDescription != null)
                {
                    if (serviceDescription.IdOwnerUser != IdUser)
                    {
                        AddToastDangerMessage(WebResource.UserIsNotTheOwner);

                        return View();
                    }
                }
                else
                {
                    AddToastWarningMessage(WebResource.AnErrorHasOccuredServiceDescriptionNotFoundInDatabase);

                    return View();
                }

                try
                {
                    var serviceDescriptionRemoveRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.DELETE, "application/x-www-form-urlencoded");
                    var serviceDescriptionRemoveResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(serviceDescriptionRemoveRequest);

                    AddToastSuccessMessage(WebResource.ServiceDescriptionDeleteSuccess);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    AddToastDangerMessage(WebResource.ServiceDescriptionDeleteFail);

                    return View();
                }
            }

            AddToastDangerMessage(WebResource.ServiceDescriptionDeleteFail);

            return View();
        }

        [HttpGet]
        public ActionResult Details(int idServiceDescription)
        {
            var serviceDescription = _serviceDescriptionService.Get(idServiceDescription);

            if (serviceDescription == null)
            {
                AddToastWarningMessage("ops");

                return RedirectToAction("Index");
            }
            else
            {
                var serviceDescriptionViewModel = Mapper.Map<ServiceDescription_MvcViewModel>(serviceDescription);

                return View(serviceDescriptionViewModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int idServiceDescription)
        {
            var serviceDescription = _serviceDescriptionService.Get(idServiceDescription);

            if (serviceDescription != null)
            {
                var serviceDescriptionViewModel = Mapper.Map<ServiceDescription_MvcViewModel>(serviceDescription);

                return View(serviceDescriptionViewModel);
            }
            else
            {
                AddToastWarningMessage("kjsjsjsjssj");

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int idServiceDescription, ServiceDescription_MvcViewModel serviceDescriptionViewModel)
        {
            var serviceDescription = _serviceDescriptionService.Get(idServiceDescription);

            if (serviceDescription != null)
            {
                if (serviceDescription.IdOwnerUser != IdUser)
                {
                    AddToastDangerMessage(WebResource.UserIsNotTheOwner);
                    return View();
                }
            }
            else
            {
                AddToastWarningMessage(WebResource.AnErrorHasOccuredServiceDescriptionNotFoundInDatabase);

                return View();
            }

            try
            {
                serviceDescription.ServiceName = serviceDescriptionViewModel.ServiceName;
                serviceDescription.Xml = serviceDescriptionViewModel.Xml;

                _serviceDescriptionService.Update(serviceDescription);

                AddToastSuccessMessage(WebResource.ServiceDescriptionUpdateSuccess);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                AddToastDangerMessage(WebResource.ServiceDescriptionUpdateFail);

                return View();
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.LoggedInUserFullName = UserFullName;

            var serviceDescriptions = _serviceDescriptionService.GetAllByOwnerUser(IdUser);

            var serviceDescriptionsViewModel = Mapper.Map<List<ServiceDescription_MvcViewModel>>(serviceDescriptions);

            if (serviceDescriptionsViewModel != null && serviceDescriptionsViewModel.Count() == 0)
            {
                AddToastWarningMessage(WebResource.NoServiceDescriptionInDatabase);
            }

            return View(serviceDescriptionsViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveAllSharing(int idServiceDescription)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the service description from the API

            var getServiceDescriptionsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getServiceDescriptionResponse = await _apiRestClient.ExecuteAsync<ServiceDescription_ApiResponseViewModel>(getServiceDescriptionsRequest);

            #endregion Get the service description from the API

            if (getServiceDescriptionResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription = getServiceDescriptionResponse.Data;

                if (serviceDescription != null)
                {
                    //TODO: [Share] Fix bug when trying to remove all sharing
                    if (serviceDescription.IdOwnerUser != IdUser)
                    {
                        AddToastDangerMessage(WebResource.UserIsNotTheOwner);
                    }

                    var serviceDescriptionRemoveAllSharingViewModel = Mapper.Map<ServiceDescription_RemoveAllSharing_MvcViewModel>(serviceDescription);

                    return View(serviceDescriptionRemoveAllSharingViewModel);
                }
            }

            return View(new ServiceDescription_RemoveAllSharing_MvcViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> RemoveAllSharing(int idServiceDescription, ServiceDescription_RemoveAllSharing_MvcViewModel requestModel)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            //Get the share invitations according to the service description
            var getShareInvitationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/share-invitations", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getShareInvitationsResponse = await _apiRestClient.ExecuteAsync<List<ShareInvitation_ApiResponseViewModel>>(getShareInvitationsRequest);

            //If the response of geting the share invitations is Ok
            if (getShareInvitationsResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Get the share invitation from the API response object
                var shareInvitations = getShareInvitationsResponse.Data;

                //For each share invitation gotten from the service description
                foreach (var shareInvitation in shareInvitations)
                {
                    //Create the share invitation request body to revoke the invitation
                    var updateShareInvitationRequestBody = new ShareInvitation_ApiRequestUpdateModel
                    {
                        Email = shareInvitation.Email,
                        Id = shareInvitation.Id,
                        IdServiceDescription = shareInvitation.IdServiceDescription,
                        IdUserInviter = shareInvitation.IdUserInviter,
                        InvitationStatus = Domain.Enums.InvitationStatusEnum.Revoked,
                        InvitationSecurity = shareInvitation.InvitationSecurity
                    };

                    //Revoke the share invitation by updating the share invitation
                    var updateShareInvitationRequest = CreateApiRequest($"api/share-invitations/{shareInvitation.Id}", HttpMethodENUM.PUT, "application/x-www-form-urlencoded");
                    updateShareInvitationRequest.AddRequestBodyParameter(updateShareInvitationRequestBody);
                    var updateShareInvitationResponse = await _apiRestClient.ExecuteAsync<ShareInvitation_ApiResponseViewModel>(updateShareInvitationRequest);
                }

                //Get the relations between the service description and the user
                var getServiceDescriptionUsersRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/service-description-users", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                var getServiceDescriptionUsersResponse = await _apiRestClient.ExecuteAsync<List<ServiceDescription_User_ApiResponseViewModel>>(getServiceDescriptionUsersRequest);

                //If the response of geting the relations between the service description and the users is Ok
                if (getServiceDescriptionUsersResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Get the relation between the service description and the users from the API response object
                    var serviceDescriptionUsers = getServiceDescriptionUsersResponse.Data;

                    //For each relation
                    foreach (var serviceDescriptionUser in serviceDescriptionUsers)
                    {
                        //Remove the relation between the service description and the user
                        var deleteServiceDescriptionUserRequest = CreateApiRequest($"api/service-description-users/{serviceDescriptionUser.Id}", HttpMethodENUM.DELETE, "application/x-www-form-urlencoded");
                        var deleteServiceDescriptionUserResponse = await _apiRestClient.ExecuteAsync<ShareInvitation_ApiResponseViewModel>(deleteServiceDescriptionUserRequest);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveSharing(int idServiceDescription, int idSharedUser)
        {
            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            #region Get the users shared with the service description from the API

            var getSharedServiceDescriptionsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/service-description-users", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getSharedServiceDescriptionsResponse = await _apiRestClient.ExecuteAsync<List<ServiceDescription_User_ApiResponseViewModel>>(getSharedServiceDescriptionsRequest);

            #endregion Get the users shared with the service description from the API

            if (getSharedServiceDescriptionsResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var serviceDescription_Users = getSharedServiceDescriptionsResponse.Data;

                var serviceDescription_User = serviceDescription_Users.FirstOrDefault(x => x.IdSharedUser == idSharedUser);

                if (serviceDescription_User != null)
                {
                    var serviceDescriptionRemoveSharingViewModel = Mapper.Map<ServiceDescription_RemoveSharing_MvcViewModel>(serviceDescription_User);

                    return View(serviceDescriptionRemoveSharingViewModel);
                }
            }

            return RedirectToAction("Index", "ServiceDescription_User", new { idServiceDescription = idServiceDescription });
        }

        [HttpPost]
        public async Task<ActionResult> RemoveSharing(ServiceDescription_RemoveSharing_MvcViewModel serviceDescriptionRemoveSharingViewModel)
        {
            var idServiceDescription = serviceDescriptionRemoveSharingViewModel.IdServiceDescription;

            #region Set authorization to API calls

            SetAuthorizationToAPICalls();

            #endregion Set authorization to API calls

            //Get the share invitations according to the service description
            var getShareInvitationsRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/share-invitations", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
            var getShareInvitationsResponse = await _apiRestClient.ExecuteAsync<List<ShareInvitation_ApiResponseViewModel>>(getShareInvitationsRequest);

            //If the response of geting the share invitations is Ok
            if (getShareInvitationsResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Get the share invitation from the API response object
                var shareInvitations = getShareInvitationsResponse.Data;

                //From all share invitations for the given service description, get the one for the specific user by the email
                var shareInvitation = shareInvitations.FirstOrDefault(x => x.Email == serviceDescriptionRemoveSharingViewModel.SharedUserEmail);

                //If the share invitation exists for the specific service description and also for the user (by the email)
                if (shareInvitation != null)
                {
                    //Create the share invitation request body to revoke the invitation
                    var updateShareInvitationRequestBody = new ShareInvitation_ApiRequestUpdateModel
                    {
                        Email = shareInvitation.Email,
                        Id = shareInvitation.Id,
                        IdServiceDescription = shareInvitation.IdServiceDescription,
                        IdUserInviter = shareInvitation.IdUserInviter,
                        InvitationStatus = Domain.Enums.InvitationStatusEnum.Revoked,
                        InvitationSecurity = shareInvitation.InvitationSecurity
                    };

                    //Revoke the share invitation by updating the share invitation
                    var updateShareInvitationRequest = CreateApiRequest($"api/share-invitations/{shareInvitation.Id}", HttpMethodENUM.PUT, "application/x-www-form-urlencoded");
                    updateShareInvitationRequest.AddRequestBodyParameter(updateShareInvitationRequestBody);
                    var updateShareInvitationResponse = await _apiRestClient.ExecuteAsync<ShareInvitation_ApiResponseViewModel>(updateShareInvitationRequest);

                    //Get the relations between the service description and the user
                    var getServiceDescriptionUsersRequest = CreateApiRequest($"api/service-descriptions/{idServiceDescription}/service-description-users", HttpMethodENUM.GET, "application/x-www-form-urlencoded");
                    var getServiceDescriptionUsersResponse = await _apiRestClient.ExecuteAsync<List<ServiceDescription_User_ApiResponseViewModel>>(getServiceDescriptionUsersRequest);

                    //If the response of geting the relations between the service description and the users is Ok
                    if (getServiceDescriptionUsersResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //Get the relation between the service description and the users from the API response object
                        var serviceDescriptionUsers = getServiceDescriptionUsersResponse.Data;

                        //From all users which the service description is shared with, given service description id, get the one for the specific user by the email
                        var serviceDescriptionUser = serviceDescriptionUsers.FirstOrDefault(x => x.Email == serviceDescriptionRemoveSharingViewModel.SharedUserEmail);

                        //If the user exists for the specific service description and also for the user (by the email)
                        if (serviceDescriptionUser != null)
                        {
                            //Remove the relation between the service description and the user
                            var deleteServiceDescriptionUserRequest = CreateApiRequest($"api/service-description-users/{serviceDescriptionUser.Id}", HttpMethodENUM.DELETE, "application/x-www-form-urlencoded");
                            var deleteServiceDescriptionUserResponse = await _apiRestClient.ExecuteAsync<ShareInvitation_ApiResponseViewModel>(deleteServiceDescriptionUserRequest);
                        }
                    }
                }
            }

            return RedirectToAction("Index", "ServiceDescription_User", new { idServiceDescription = serviceDescriptionRemoveSharingViewModel.IdServiceDescription });
        }

        #endregion Actions
    }
}
using AutoMapper;
using Grasews.API.Models;
using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;

namespace Grasews.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [RoutePrefix("api/share-invitations")]
    [DisplayName("Invitations")]
    public class ShareInvitationsApiController : BaseApiController
    {
        #region Private vars

        private readonly IShareInvitationService _shareInvitationService;
        private readonly IOntologyService _ontologyService;
        private readonly IUserService _userService;

        #endregion Private vars

        #region Ctors

        /// <summary>
        ///
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="shareInvitationService"></param>
        /// <param name="userService"></param>
        /// <param name="userIdentityService"></param>
        /// <param name="ontologyService"></param>
        public ShareInvitationsApiController(IMapper mapper,
            IShareInvitationService shareInvitationService,
            IUserService userService,
            IUserIdentityService userIdentityService,
            IOntologyService ontologyService)
            : base(mapper, userIdentityService)
        {
            _shareInvitationService = shareInvitationService;
            _userService = userService;
            _ontologyService = ontologyService;
        }

        #endregion Ctors

        #region Actions

        #region CRUD

        // DELETE: api/share-invitations/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(ShareInvitation_ApiResponseViewModel))]
        public IHttpActionResult DeleteShareInvitation(int id)
        {
            var shareInvitation = _shareInvitationService.Get(id);

            if (shareInvitation == null)
            {
                return NotFound();
            }

            var count = _shareInvitationService.Remove(shareInvitation);

            var shareInvitationResponseModel = _mapper.Map<ShareInvitation_ApiResponseViewModel>(shareInvitation);

            return Ok(shareInvitationResponseModel);
        }

        // GET: api/share-invitations/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}")]
        [HttpGet]
        [ResponseType(typeof(ShareInvitation_ApiResponseViewModel))]
        public IHttpActionResult GetShareInvitation(int id)
        {
            var shareInvitation = _shareInvitationService.Get(id);

            if (shareInvitation == null)
            {
                return NotFound();
            }

            var shareInvitationResponseModel = _mapper.Map<ShareInvitation_ApiResponseViewModel>(shareInvitation);

            return Ok(shareInvitationResponseModel);
        }

        // GET: api/share-invitations
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ShareInvitation_ApiResponseViewModel>))]
        public IHttpActionResult GetShareInvitations()
        {
            var idUser = _userIdentityService.Id;

            var shareInvitations = _shareInvitationService.GetAllByUserInviter(idUser);

            if (shareInvitations.Any())
            {
                var shareInvitationsResponseModel = _mapper.Map<List<ShareInvitation_ApiResponseViewModel>>(shareInvitations);

                return Ok(shareInvitationsResponseModel);
            }

            return NotFound();
        }

        // POST: api/share-invitations
        /// <summary>
        ///
        /// </summary>
        /// <param name="shareInvitationRequestCreateModel"></param>
        /// <returns></returns>
        [Authorize]
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ShareInvitation_ApiResponseViewModel>))]
        public IHttpActionResult PostShareInvitation(ShareInvitation_ApiRequestCreateModel shareInvitationRequestCreateModel)
        {
            List<int> idOntologiesList = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(shareInvitationRequestCreateModel.IdOntologies))
            {
                idOntologiesList = shareInvitationRequestCreateModel.IdOntologies.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            }

            var idUser = _userIdentityService.Id;

            ShareInvitation shareInvitation;
            ShareInvitation_ApiResponseViewModel shareInvitationResponseModel;
            var shareInvitationResponseModels = new List<ShareInvitation_ApiResponseViewModel>();
            var count = 0;

            var emails = shareInvitationRequestCreateModel.Emails.Split(',');

            List<Ontology> ontologies = null;

            foreach (var email in emails)
            {
                if (idOntologiesList != null && idOntologiesList.Any())
                {
                    ontologies = new List<Ontology>();

                    foreach (var idOntology in idOntologiesList)
                    {
                        var ontology = _ontologyService.Get(idOntology);
                        ontologies.Add(ontology);
                    }
                }

                shareInvitation = new ShareInvitation
                {
                    IdUserInviter = idUser,
                    Email = email,
                    IdServiceDescription = shareInvitationRequestCreateModel.IdServiceDescription
                };

                var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var match = regex.Match(email);

                if (match.Success)
                {
                    shareInvitation.ExistingUser = false;
                }
                else
                {
                    shareInvitation.ExistingUser = true;
                }

                count = count + _shareInvitationService.Create(shareInvitation, ontologies);

                shareInvitationResponseModel = _mapper.Map<ShareInvitation_ApiResponseViewModel>(shareInvitation);

                shareInvitationResponseModels.Add(shareInvitationResponseModel);
            }

            return Ok(shareInvitationResponseModels);
        }

        // PUT: api/share-invitations/5
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shareInvitationRequestUpdateModel"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(ShareInvitation_ApiResponseViewModel))]
        public IHttpActionResult PutShareInvitation(int id, ShareInvitation_ApiRequestUpdateModel shareInvitationRequestUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shareInvitationRequestUpdateModel.Id)
            {
                return BadRequest();
            }

            var shareInvitation = _mapper.Map<ShareInvitation>(shareInvitationRequestUpdateModel);

            var count = _shareInvitationService.Update(shareInvitation);

            var shareInvitationResponseModel = _mapper.Map<ShareInvitation_ApiResponseViewModel>(shareInvitation);

            return Ok(shareInvitationResponseModel);
        }

        #endregion CRUD

        #region Accept invitations

        // POST: api/share-invitations/1/users/acceptance
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shareInvitationAcceptRequestCreateModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("{id:int}/users/acceptance")]
        [HttpPost]
        [ResponseType(typeof(ShareInvitation_ApiResponseViewModel))]
        public IHttpActionResult AcceptShareInvitationForNewUser(int id, ShareInvitationAccept_ApiRequestCreateModel shareInvitationAcceptRequestCreateModel)
        {
            var shareInvitation = _mapper.Map<ShareInvitation>(shareInvitationAcceptRequestCreateModel);

            var newUser = _mapper.Map<User>(shareInvitationAcceptRequestCreateModel);

            _shareInvitationService.AcceptInvitationForNewUser(shareInvitation, newUser);

            return Ok();
        }

        // POST: api/share-invitations/1/users/2/acceptance
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idShareUser"></param>
        /// <param name="shareInvitationAcceptRequestCreateModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("{id:int}/users/{idShareUser:int}/acceptance")]
        [HttpPost]
        [ResponseType(typeof(ShareInvitation_ApiResponseViewModel))]
        public IHttpActionResult AcceptShareInvitationForExistingUser(int id, int idShareUser, ShareInvitationAccept_ApiRequestCreateModel shareInvitationAcceptRequestCreateModel)
        {
            var shareInvitation = _mapper.Map<ShareInvitation>(shareInvitationAcceptRequestCreateModel);

            var user = _userService.Get(idShareUser);

            _shareInvitationService.AcceptInvitationForExistingUser(shareInvitation, user);

            return Ok();
        }

        #endregion Accept invitations

        #endregion Actions
    }
}
using Grasews.Domain.Entities;
using Grasews.Domain.Enums;
using Grasews.Domain.Events;
using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Repositories;
using Grasews.Domain.Interfaces.Services;
using Grasews.Infra.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grasews.Application.Services
{
    /// <summary>
    ///
    /// </summary>
    public class ShareInvitationService : BaseService, IShareInvitationService
    {
        #region Private vars

        private readonly IEventDispatcher _eventDispatcher;
        private readonly IGraphService _graphService;
        private readonly IServiceDescription_UserEntityRepository _serviceDescription_UserRepository;
        private readonly IServiceDescriptionEntityRepository _serviceDescriptionRepository;
        private readonly IServiceDescription_OntologyEntityRepository _serviceDescription_OntologyRepository;
        private readonly IShareInvitationEntityRepository _shareInvitationRepository;
        private readonly IUserEntityRepository _userRepository;
        private readonly IShareInvitation_OntologyEntityRepository _shareInvitation_OntologyRepository;

        #endregion Private vars

        #region Ctors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDispatcher"></param>
        /// <param name="shareInvitationRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="serviceDescription_UserRepository"></param>
        /// <param name="serviceDescription_OntologyRepository"></param>
        /// <param name="serviceDescriptionRepository"></param>
        /// <param name="graphService"></param>
        /// <param name="shareInvitation_OntologyRepository"></param>
        public ShareInvitationService(IEventDispatcher eventDispatcher,
            IShareInvitationEntityRepository shareInvitationRepository,
            IUserEntityRepository userRepository,
            IServiceDescription_UserEntityRepository serviceDescription_UserRepository,
            IServiceDescription_OntologyEntityRepository serviceDescription_OntologyRepository,
            IServiceDescriptionEntityRepository serviceDescriptionRepository,
            IGraphService graphService,
            IShareInvitation_OntologyEntityRepository shareInvitation_OntologyRepository)
        {
            _eventDispatcher = eventDispatcher;
            _shareInvitationRepository = shareInvitationRepository;
            _userRepository = userRepository;
            _serviceDescription_UserRepository = serviceDescription_UserRepository;
            _serviceDescription_OntologyRepository = serviceDescription_OntologyRepository;
            _serviceDescriptionRepository = serviceDescriptionRepository;
            _graphService = graphService;
            _shareInvitation_OntologyRepository = shareInvitation_OntologyRepository;
        }

        #endregion Ctors

        #region IShareInvitationService public methods

        public void AcceptInvitationForExistingUser(ShareInvitation shareInvitation, User user)
        {
            var shareInvitations = _shareInvitationRepository.GetAll()
                .Where(x => x.Email == shareInvitation.Email.ToLower() && x.InvitationStatus == InvitationStatusEnum.Invited).ToList();

            user.ServiceDescription_Users = new List<ServiceDescription_User>();

            foreach (var item in shareInvitations)
            {
                //Update the invitation status to Accepted
                item.InvitationStatus = InvitationStatusEnum.Accepted;

                //Update the entity with the new invitation status in the EF context
                _shareInvitationRepository.Update(item);

                //Get the service description according to the service description id in the input parameters
                var serviceDescription = _serviceDescriptionRepository.GetComplete(item.IdServiceDescription);

                //Generate the graph (original) for the service description
                var graphJson = _graphService.CreateGraphData(serviceDescription.Id, serviceDescription.ServiceName, serviceDescription.WsdlInterfaces);

                //Generate the service description for the (invited) shared user
                var serviceDescription_User = new ServiceDescription_User
                {
                    IdServiceDescription = item.IdServiceDescription,
                    IdSharedUser = user.Id
                };

                //Add the link between the (invited) shared user and the service description
                _serviceDescription_UserRepository.Create(serviceDescription_User);
            }

            //Update the (invited) shared user in the database via the EF context
            _serviceDescription_UserRepository.SaveChanges();

            //Save the updated invitation, with new status, in the database via the EF context
            _shareInvitationRepository.SaveChanges();
        }

        public void AcceptInvitationForNewUser(ShareInvitation shareInvitation, User newUser)
        {
            var shareInvitations = _shareInvitationRepository.GetAll()
                .Where(x => x.Email == shareInvitation.Email.ToLower() && x.InvitationStatus == InvitationStatusEnum.Invited).ToList();

            newUser.ServiceDescription_Users = new List<ServiceDescription_User>();

            foreach (var invitation in shareInvitations)
            {
                //Update the invitation status to Accepted
                invitation.InvitationStatus = InvitationStatusEnum.Accepted;

                //Update the entity with the new invitation status in the EF context
                _shareInvitationRepository.Update(invitation);

                var idServiceDescription = invitation.IdServiceDescription;

                //Generate the service description for the (invited) shared user
                var serviceDescription_User = new ServiceDescription_User
                {
                    IdServiceDescription = idServiceDescription
                };

                //Add the link between the (invited) shared user and the service description
                newUser.ServiceDescription_Users.Add(serviceDescription_User);

                var ontologiesUsedByServiceDescription = _serviceDescription_OntologyRepository.GetAll().Where(x => x.IdServiceDescription == idServiceDescription);
                var ontologiesSharedByOpenedInProject = _shareInvitation_OntologyRepository.GetAll().Where(x => x.IdShareInvitation == invitation.Id);
                newUser.Ontology_Users = new List<Ontology_User>();

                foreach (var idOntology in ontologiesUsedByServiceDescription.Select(x => x.IdOntology).ToList())
                {
                    newUser.Ontology_Users.Add(new Ontology_User { IdOntology = idOntology });
                }

                foreach (var idOntology in ontologiesSharedByOpenedInProject.Select(x => x.IdOntology).ToList())
                {
                    newUser.Ontology_Users.Add(new Ontology_User { IdOntology = idOntology });
                }
            }

            //Hash the password for the invited user
            newUser.Password = HashHelper.GetHash(newUser.Password);

            //Create the the (invited) shared user in the EF context
            _userRepository.Create(newUser);

            //Save the (invited) shared user in the database via the EF context
            _userRepository.SaveChanges();

            //Save the updated invitation, with new status, in the database via the EF context
            _shareInvitationRepository.SaveChanges();
        }

        public int Create(ShareInvitation shareInvitation, List<Ontology> ontologies)
        {
            var count = 0;
            string email = null;
            ShareInvitation alreadyExistingShareInvitation;
            var invitationSecurity = Guid.NewGuid();

            shareInvitation.Email = shareInvitation.Email.Trim().ToLower();

            shareInvitation.InvitationSecurity = invitationSecurity;

            if (shareInvitation.ExistingUser)
            {
                if (int.TryParse(shareInvitation.Email, out int idExistingUser))
                {
                    var user = _userRepository.Get(idExistingUser);

                    if (user != null)
                    {
                        alreadyExistingShareInvitation = _shareInvitationRepository.GetAll(@readonly: false)
                           .FirstOrDefault(x => x.Email == user.Email
                               && x.IdServiceDescription == shareInvitation.IdServiceDescription);

                        if (alreadyExistingShareInvitation == null)
                        {
                            email = user.Email;

                            shareInvitation.Email = email;

                            _shareInvitationRepository.Create(shareInvitation);

                            count = _shareInvitationRepository.SaveChanges();

                            if (shareInvitation.ShareInvitation_Ontologies == null)
                            {
                                shareInvitation.ShareInvitation_Ontologies = new List<ShareInvitation_Ontology>();
                            }

                            if (ontologies != null && ontologies.Any())
                            {
                                foreach (var ontology in ontologies)
                                {
                                    shareInvitation.ShareInvitation_Ontologies.Add(new ShareInvitation_Ontology { IdOntology = ontology.Id, IdShareInvitation = shareInvitation.Id });
                                }

                                count = _shareInvitationRepository.SaveChanges();
                            }
                        }
                        else
                        {
                            if (alreadyExistingShareInvitation.InvitationStatus == InvitationStatusEnum.Accepted)
                            {
                                return count;
                            }

                            if (alreadyExistingShareInvitation.InvitationStatus == InvitationStatusEnum.Revoked)
                            {
                                alreadyExistingShareInvitation.InvitationStatus = InvitationStatusEnum.Invited;

                                _shareInvitationRepository.Update(alreadyExistingShareInvitation);

                                count = _shareInvitationRepository.SaveChanges();

                                if (shareInvitation.ShareInvitation_Ontologies == null)
                                {
                                    shareInvitation.ShareInvitation_Ontologies = new List<ShareInvitation_Ontology>();
                                }

                                if (ontologies != null && ontologies.Any())
                                {
                                    foreach (var ontology in ontologies)
                                    {
                                        shareInvitation.ShareInvitation_Ontologies.Add(new ShareInvitation_Ontology { IdOntology = ontology.Id, IdShareInvitation = shareInvitation.Id });
                                    }

                                    count = _shareInvitationRepository.SaveChanges();
                                }
                            }

                            invitationSecurity = alreadyExistingShareInvitation.InvitationSecurity;
                            email = alreadyExistingShareInvitation.Email;
                        }

                        _eventDispatcher.Dispatch(new SendInvitationEmailEvent(email, invitationSecurity));
                    }
                }
            }
            else
            {
                alreadyExistingShareInvitation = _shareInvitationRepository.GetAll(@readonly: false)
                   .FirstOrDefault(x => x.Email == shareInvitation.Email
                        && x.IdServiceDescription == shareInvitation.IdServiceDescription);

                if (alreadyExistingShareInvitation == null)
                {
                    email = shareInvitation.Email;
                    _shareInvitationRepository.Create(shareInvitation);
                    count = _shareInvitationRepository.SaveChanges();

                    if (shareInvitation.ShareInvitation_Ontologies == null)
                    {
                        shareInvitation.ShareInvitation_Ontologies = new List<ShareInvitation_Ontology>();
                    }

                    if (ontologies != null && ontologies.Any())
                    {
                        foreach (var ontology in ontologies)
                        {
                            shareInvitation.ShareInvitation_Ontologies.Add(new ShareInvitation_Ontology { IdOntology = ontology.Id, IdShareInvitation = shareInvitation.Id });
                        }

                        count = _shareInvitationRepository.SaveChanges();
                    }
                }
                else
                {
                    if (alreadyExistingShareInvitation.InvitationStatus == InvitationStatusEnum.Accepted)
                    {
                        return count;
                    }

                    invitationSecurity = alreadyExistingShareInvitation.InvitationSecurity;
                    email = alreadyExistingShareInvitation.Email;
                }

                _eventDispatcher.Dispatch(new SendInvitationEmailEvent(email, invitationSecurity));
            }

            return count;
        }

        //TODO: Create a new invitation when the user has been revoked or if they rejected the invitation, to keep the history.
        //Today it's using the same invitation but changing the status.
        public int Create(ShareInvitation shareInvitation)
        {
            var count = 0;
            string email = null;
            ShareInvitation alreadyExistingShareInvitation;
            var invitationSecurity = Guid.NewGuid();

            shareInvitation.Email = shareInvitation.Email.Trim().ToLower();

            shareInvitation.InvitationSecurity = invitationSecurity;

            if (shareInvitation.ExistingUser)
            {
                if (int.TryParse(shareInvitation.Email, out int idExistingUser))
                {
                    var user = _userRepository.Get(idExistingUser);

                    if (user != null)
                    {
                        alreadyExistingShareInvitation = _shareInvitationRepository.GetAll(@readonly: false)
                           .FirstOrDefault(x => x.Email == user.Email
                               && x.IdServiceDescription == shareInvitation.IdServiceDescription);

                        if (alreadyExistingShareInvitation == null)
                        {
                            email = user.Email;

                            shareInvitation.Email = email;

                            _shareInvitationRepository.Create(shareInvitation);

                            count = _shareInvitationRepository.SaveChanges();
                        }
                        else
                        {
                            if (alreadyExistingShareInvitation.InvitationStatus == InvitationStatusEnum.Accepted)
                            {
                                return count;
                            }

                            if (alreadyExistingShareInvitation.InvitationStatus == InvitationStatusEnum.Revoked)
                            {
                                alreadyExistingShareInvitation.InvitationStatus = InvitationStatusEnum.Invited;

                                _shareInvitationRepository.Update(alreadyExistingShareInvitation);

                                count = _shareInvitationRepository.SaveChanges();
                            }

                            invitationSecurity = alreadyExistingShareInvitation.InvitationSecurity;
                            email = alreadyExistingShareInvitation.Email;
                        }

                        _eventDispatcher.Dispatch(new SendInvitationEmailEvent(email, invitationSecurity));
                    }
                }
            }
            else
            {
                alreadyExistingShareInvitation = _shareInvitationRepository.GetAll(@readonly: false)
                   .FirstOrDefault(x => x.Email == shareInvitation.Email
                        && x.IdServiceDescription == shareInvitation.IdServiceDescription);

                if (alreadyExistingShareInvitation == null)
                {
                    email = shareInvitation.Email;
                    _shareInvitationRepository.Create(shareInvitation);
                    count = _shareInvitationRepository.SaveChanges();
                }
                else
                {
                    if (alreadyExistingShareInvitation.InvitationStatus == InvitationStatusEnum.Accepted)
                    {
                        return count;
                    }

                    invitationSecurity = alreadyExistingShareInvitation.InvitationSecurity;
                    email = alreadyExistingShareInvitation.Email;
                }

                _eventDispatcher.Dispatch(new SendInvitationEmailEvent(email, invitationSecurity));
            }

            return count;
        }

        public ShareInvitation Get(int idShareInvitation)
        {
            return _shareInvitationRepository.Get(idShareInvitation);
        }

        public List<ShareInvitation> GetAll()
        {
            return _shareInvitationRepository.GetAll().ToList();
        }

        public List<ShareInvitation> GetAllByServiceDescription(int idServiceDescription)
        {
            return _shareInvitationRepository.GetAll().Where(x => x.IdServiceDescription == idServiceDescription).ToList();
        }

        public List<ShareInvitation> GetAllBySharedEmail(string email)
        {
            return _shareInvitationRepository.GetAll().Where(x => x.Email == email.ToLower()).ToList();
        }

        public List<ShareInvitation> GetAllByUserInviter(int idUserInviter)
        {
            return _shareInvitationRepository.GetAll().Where(x => x.IdUserInviter == idUserInviter).ToList();
        }

        public ShareInvitation GetByInvitationSecurity(Guid invitationSecurity)
        {
            return _shareInvitationRepository.GetAll().FirstOrDefault(x => x.InvitationSecurity == invitationSecurity);
        }

        public int Remove(ShareInvitation shareInvitation)
        {
            _shareInvitationRepository.Remove(shareInvitation.Id);

            return _shareInvitationRepository.SaveChanges();
        }

        public int Update(ShareInvitation shareInvitation)
        {
            _shareInvitationRepository.Update(shareInvitation);

            return _shareInvitationRepository.SaveChanges();
        }

        #endregion IShareInvitationService public methods
    }
}
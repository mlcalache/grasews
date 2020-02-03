using Grasews.Domain.Interfaces.Events;
using Grasews.Domain.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Grasews.Domain.Events
{
    public class AddOntologyToSharedUsersHandler : IEventHandler<AddOntologyToSharedUsersEvent>
    {
        private readonly IOntologyService _ontologyService;
        private readonly IServiceDescription_UserService _serviceDescription_UserService;

        public AddOntologyToSharedUsersHandler(IOntologyService ontologyService, IServiceDescription_UserService serviceDescription_UserService)
        {
            _ontologyService = ontologyService;
            _serviceDescription_UserService = serviceDescription_UserService;
        }

        public void Handle(AddOntologyToSharedUsersEvent sendInvitationEmailEvent)
        {
            Task.Run(() =>
            {
                var sharedUsers = _serviceDescription_UserService.GetAllByServiceDescription(sendInvitationEmailEvent.IdServiceDescription);

                var idSharedUsers = sharedUsers.Select(x => x.IdSharedUser);

                _ontologyService.AddOntologyToSharedUsers(sendInvitationEmailEvent.IdOntology, idSharedUsers);
            });
        }
    }
}
using Grasews.Domain.Interfaces.Events;

namespace Grasews.Domain.Events
{
    public class AddOntologyToSharedUsersEvent : IDomainEvent
    {
        public int IdOntology { get; private set; }
        public int IdServiceDescription { get; private set; }

        public AddOntologyToSharedUsersEvent(int idOntology, int idServiceDescription)
        {
            IdOntology = idOntology;
            IdServiceDescription = idServiceDescription;
        }
    }
}
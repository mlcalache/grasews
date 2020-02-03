using Grasews.Domain.Interfaces.Containers;
using Grasews.Domain.Interfaces.Events;

namespace Grasews.Domain.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IDomainContainer _container;

        public EventDispatcher(IDomainContainer container)
        {
            _container = container;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var handles = _container.GetAllInstances<IEventHandler<TEvent>>();

            foreach (var handle in handles)
            {
                handle.Handle(@event);
            }
        }
    }
}
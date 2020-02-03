namespace Grasews.Domain.Interfaces.Events
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}

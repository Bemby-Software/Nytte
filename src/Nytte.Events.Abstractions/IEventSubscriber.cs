namespace Nytte.Events.Abstractions
{
    public interface IEventSubscriber
    {
        IEventSubscriber Subscribe<TEvent>()
            where TEvent : IEvent;
    }
}
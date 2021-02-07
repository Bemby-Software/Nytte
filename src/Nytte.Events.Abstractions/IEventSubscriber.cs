namespace Nytte.Events.Abstractions
{
    public interface IEventSubscriber
    {
        IEventSubscriber Subscribe<TEvent, THandler>()
            where TEvent : IEvent;
    }
}
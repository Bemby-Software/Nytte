namespace Nytte.Events.Abstractions
{
    public interface IEventsRegistryService
    {
        void RegisterEvent<TEvent>() where TEvent : IEvent;
    }
}
using System;

namespace Nytte.Events.Abstractions
{
    public interface IEventsFactory
    {
        IEventRegistration CreateRegistration<T>(ScopedEventHandlerAsync handler, string key) where T : IEvent;

        string GetEventKey<T>() where T : IEvent;

        ScopedEventHandlerAsync CreateHandler<TEvent>() where TEvent : IEvent;
    }
}
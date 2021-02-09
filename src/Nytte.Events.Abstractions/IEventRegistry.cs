using System.Collections.Generic;

namespace Nytte.Events.Abstractions
{
    public interface IEventRegistry
    {
        void Add(IEventRegistration registration);

        IReadOnlyList<IEventRegistration> GetRegistrations<T>() where T : IEvent;

    }
}
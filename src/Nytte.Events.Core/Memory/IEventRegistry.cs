using System.Collections.Generic;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Memory
{
    public interface IEventRegistry
    {
        void Add(IEventRegistration registration);

        IReadOnlyList<IEventRegistration> GetRegistrations<T>() where T : IEvent;
    }
}
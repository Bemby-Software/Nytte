using System;
using System.Threading.Tasks;

namespace Nytte.Events.Abstractions
{
    public interface IEventRegistration
    {
        Type Type { get; }

        string Key { get; }

        ScopedEventHandlerAsync AsyncHandler { get; } 
    }
}
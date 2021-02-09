using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Tests.Models
{
    public class TestEvent : IEvent
    {
        
    }

    [EventOwner("main")]
    public class TestEventAttribute : IEvent
    {
        
    }
}
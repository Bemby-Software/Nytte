using NUnit.Framework;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;
using Nytte.Testing;

namespace Nytte.Events.Core.Tests.Memory
{
    public class MemoryEventRegistryTests : ServiceUnderTest<IEventRegistry, EventRegistry>
    {
        public override void Setup()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void Add_EventRegistration_Adds()
        {
            
        }
        
    }
}
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;
using Nytte.Events.Core.Tests.Models;
using Nytte.Testing;

namespace Nytte.Events.Core.Tests
{
    public class EventsRegistryServiceTests : ServiceUnderTest<IEventsRegistryService, EventsRegistryService>
    {
        private Mock<IEventRegistry> _registry;
        private Mock<IEventsFactory> _factory;

        public override void Setup()
        {
            _registry = Mocker.GetMock<IEventRegistry>();
            _factory = Mocker.GetMock<IEventsFactory>();
        }

        [Test]
        public void RegisterEvent_Event_Registers()
        {
            //Arrange
            var sut = CreateSut();

            var key = "TestEvent";
            var handler = new ScopedEventHandlerAsync(((provider, eventObject) => Task.CompletedTask));
            var registration = new Mock<IEventRegistration>().Object;

            _factory.Setup(o => o.GetEventKey<TestEvent>()).Returns(key);
            _factory.Setup(o => o.CreateHandler<TestEvent>()).Returns(handler);
            _factory.Setup(o => o.CreateRegistration<TestEvent>(handler, key)).Returns(registration);

            //Act
            sut.RegisterEvent<TestEvent>();

            //Assert
            _registry.Verify(o => o.Add(registration));
        }
    }
}
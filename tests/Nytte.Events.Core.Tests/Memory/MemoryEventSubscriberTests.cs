using System;
using Moq;
using NUnit.Framework;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;
using Nytte.Events.Core.Tests.Models;
using Nytte.Testing;

namespace Nytte.Events.Core.Tests.Memory
{
    public class MemoryEventSubscriberTests : ServiceUnderTest<IEventSubscriber, MemoryEventSubscriber>
    {
        private Mock<IEventsRegistryService> _registryService;

        public override void Setup()
        {
            _registryService = Mocker.GetMock<IEventsRegistryService>();
        }

        [Test]
        public void Subscribe_Event_Subscribes()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            sut.Subscribe<TestEvent>();

            //Assert
            _registryService.Verify(o => o.RegisterEvent<TestEvent>());
        }
    }
}
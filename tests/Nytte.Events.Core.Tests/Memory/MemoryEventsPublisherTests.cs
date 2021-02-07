using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;
using Nytte.Events.Core.Tests.Models;
using Nytte.Testing;
using Shouldly;

namespace Nytte.Events.Core.Tests.Memory
{
    public class MemoryEventsPublisherTests : ServiceUnderTest<IEventPublisher, MemoryEventPublisher>
    {
        private Mock<EventsOptions> _options;
        private Mock<IEventTransporter> _transporter;
        private Mock<IEventRegistry> _registry;
        private Mock<IServiceProvider> _serviceProvider;
        private List<IEventRegistration> _registrations;

        public override void Setup()
        {
            _options = Mocker.GetMock<EventsOptions>();
            _transporter = Mocker.GetMock<IEventTransporter>();
            _registry = Mocker.GetMock<IEventRegistry>();
            _serviceProvider = Mocker.GetMock<IServiceProvider>();
            _registrations = new List<IEventRegistration>();
            _registry.Setup(o => o.GetRegistrations<TestEvent>()).Returns(_registrations);
        }   

        [Test]
        public async Task PublishAsync_Event_PublishesToHandlers()
        {
            //Arrange
            var sut = CreateSut();
            var registration = new Mock<IEventRegistration>();
            bool called = false;
            registration.SetupGet(o => o.AsyncHandler)
                .Returns(((provider, @event) =>
                {
                    called = true;
                    return Task.CompletedTask;
                }));
            
            _registrations.Add(registration.Object);

            //Act
            await sut.PublishAsync(new TestEvent());

            //Assert
            called.ShouldBeTrue();
        }

        [Test]
        public void PublishAsync_StrictModeEventWithNoHandlers_Throws()
        {
            //Arrange
            var sut = CreateSut();
            _options.Object.UseStrictMode = true;
            
            //Act
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => sut.PublishAsync(new TestEvent()));
        }
        
        public async Task PublishAsync_NoStrictModeNoHandlers_NeverThrows()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            //Assert
            await sut.PublishAsync(new TestEvent());
        }
        
        
    }
}
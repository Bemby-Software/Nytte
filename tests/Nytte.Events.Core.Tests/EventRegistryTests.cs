using System.Linq;
using Moq;
using NUnit.Framework;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Tests.Models;
using Nytte.Testing;
using Shouldly;

namespace Nytte.Events.Core.Tests
{
    public class EventRegistryTests : ServiceUnderTest<IEventRegistry, EventRegistry>
    {
        private string _key;
        private Mock<IEventsFactory> _factory;

        public override void Setup()
        {
             _key = "eventsKey";
             _factory = Mocker.GetMock<IEventsFactory>();
        }

        [Test]
        public void GetRegistrations_Event_Gets()
        {
            //Arrange
            var sut = CreateSut();
            var reg = new Mock<IEventRegistration>();

            _factory.Setup(o => o.GetEventKey<TestEvent>()).Returns(_key);

            reg.SetupGet(o => o.Key).Returns(_key);
            
            sut.Add(reg.Object);
            sut.Add(new Mock<IEventRegistration>().Object);

            //Act
            var results = sut.GetRegistrations<TestEvent>();

            //Assert
            results.Count.ShouldBe(1);
            results.First().Key.ShouldBe(_key);
        }

    }
}
using System;
using System.Net.Sockets;
using Moq;
using NUnit.Framework;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Testing;
using Shouldly;

namespace Nytte.Modules.Requests.Tests
{
    public class ModuleRequestRegistryTests : ServiceUnderTest<IModuleRequestRegistry, ModuleRequestRegistry>
    {
        private string _key;
        private Mock<IModuleRequestFactory> _factory;
    
        public override void Setup()
        {
            _key = "some/type->key";
            _factory = Mocker.GetMock<IModuleRequestFactory>();
        }

        [Test]
        public void Add_Request_Adds()
        {
            //Arrange
            var sut = CreateSut();

            var spec = new Mock<IModuleRequestSpecification>();
            spec.SetupGet(x => x.Key).Returns(_key);

            _factory.Setup(o => o.GetKey<TestDto, RequestTestObject>()).Returns(_key);

            //Act
            sut.Add(spec.Object);
            var ret = sut.GetRequest<TestDto, RequestTestObject>();

            //Assert
            ret.ShouldBe(spec.Object);
        }
        
        [Test]
        public void Add_RequestWithSameKey_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var spec = new Mock<IModuleRequestSpecification>();
            spec.SetupGet(x => x.Key).Returns(_key);

            sut.Add(spec.Object);

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.Add(spec.Object));
        }

        [Test]
        public void GetRequest_NoSpec_Throws()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.GetRequest<TestDto, RequestTestObject>());
        }
    }
}
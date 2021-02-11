using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Testing;

namespace Nytte.Modules.Requests.Tests
{
    public class ModuleRequestManagerTests : ServiceUnderTest<IModuleRequestManager, ModuleRequestManager>
    {
        private Mock<IModuleRequestFactory> _factory;
        private Mock<IModuleRequestRegistry> _registry;


        public override void Setup()
        {
            _factory = Mocker.GetMock<IModuleRequestFactory>();
            _registry = Mocker.GetMock<IModuleRequestRegistry>();
        }

        [Test]
        public void UseRequest_RequestResponse_AddsToRegistry()
        {
            //Arrange
            var sut = CreateSut();

            var handler = new ScopedRequestHandlerDelegateAsync((_, _) => Task.FromResult(""));

            _factory
                .Setup(o => o.GetKey<TestDto, RequestTestObject>())
                .Returns("key");

            _factory
                .Setup(o => o.CreateHandler<TestDto, RequestTestObject>())
                .Returns(handler);

            var spec = new Mock<IModuleRequestSpecification>();

            _factory
                .Setup(o => o.Create<TestDto, RequestTestObject>("key", handler))
                .Returns(spec.Object);

            //Act
            sut.UseRequest<TestDto, RequestTestObject>();

            //Assert
            _registry.Verify(o => o.Add(spec.Object));
        }
    }
}
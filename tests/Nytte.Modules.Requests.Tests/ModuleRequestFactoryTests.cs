using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Testing;
using Nytte.Wrappers;
using Shouldly;

namespace Nytte.Modules.Requests.Tests
{
    public class ModuleRequestFactoryTests : ServiceUnderTest<IModuleRequestFactory, ModuleRequestFactory>
    {
        private Mock<IServiceProvider> _serviceProvider;
        private Mock<IServiceScope> _scope;

        public override void Setup()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _scope = new Mock<IServiceScope>();
            
            var scopeFactory = new Mock<IServiceScopeFactory>();
            scopeFactory.Setup(o => o.CreateScope()).Returns(_scope.Object);
            _serviceProvider.Setup(o => o.GetService(typeof(IServiceScopeFactory))).Returns(scopeFactory.Object);
            _scope.SetupGet(o => o.ServiceProvider).Returns(_serviceProvider.Object);

        }

        [Test]
        public void Create_Always_CreatesSpec()
        {
            //Arrange
            var sut = CreateSut();
            var handler = new ScopedRequestHandlerDelegateAsync((_, _) => Task.FromResult<string>(""));
            var key = "key";
            
            //Act
            var spec = sut.Create<TestDto, RequestTestObject>(key, handler);
            
            //Assert
            spec.AsyncHandler.ShouldBe(handler);
            spec.Key.ShouldBe(key);
            spec.ReturnType.ShouldBe(typeof(TestDto));
            spec.QueryType.ShouldBe(typeof(RequestTestObject));
        }

        [Test]
        public void GetKey_ReturnRequestTypes_CreatesKey()
        {
            //Arrange
            var sut = CreateSut();
            
            //Act
            var key = sut.GetKey<TestDto, RequestTestObject>();
            
            //Assert
            key.ShouldBe("Test/RequestTestObject->TestDto");
        }

        class RequestNoAttribute : IModuleRequest { }
        
        [Test]
        public void GetKey_RequestNoAttribute_Throws()
        {
            //Arrange
            var sut = CreateSut();
            
            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.GetKey<TestDto, RequestNoAttribute>());
        }

        [Test]
        public async Task CreateHandler_Handler_Always_ReturnsJsonResult()
        {
            //Arrange
            var sut = CreateSut();

            var handler = new Mock<IModuleRequestHandler<TestDto, RequestTestObject>>();

            _serviceProvider
                .Setup(o => o.GetService(typeof(IModuleRequestHandler<TestDto, RequestTestObject>)))
                .Returns(handler.Object);

            var json = new Mock<IJson>();

            _serviceProvider.Setup(o => o.GetService(typeof(IJson))).Returns(json.Object);

            var packedRequest = "request";

            var request = new RequestTestObject();

            json.Setup(o => o.DeserializeAsync<RequestTestObject>(packedRequest))
                .ReturnsAsync(request);

            var dto = new TestDto();

            handler.Setup(o => o.HandleAsync(request))
                .ReturnsAsync(dto);

            var serialisedResponse = "res";

            json.Setup(o => o.SerializeAsync(dto)).ReturnsAsync(serialisedResponse);
            

            //Act
            var asyncHandler = sut.CreateHandler<TestDto, RequestTestObject>();
            var result = await asyncHandler(_serviceProvider.Object, packedRequest);

            //Assert
            result.ShouldBe(serialisedResponse);
            
        }
        
        [Test]
        public void CreateHandler_NoHandler_Always_Throws()
        {
            //Arrange
            var sut = CreateSut();

            var handler = new Mock<IModuleRequestHandler<TestDto, RequestTestObject>>();
            
            var packedRequest = "request";

            //Act
            var asyncHandler = sut.CreateHandler<TestDto, RequestTestObject>();

            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => asyncHandler(_serviceProvider.Object, packedRequest));


        }
    }
}
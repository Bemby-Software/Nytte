using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Testing;
using Nytte.Wrappers;
using Shouldly;

namespace Nytte.Modules.Requests.Tests
{
    public class ModuleRequestClientTests : ServiceUnderTest<IModuleClient, ModuleRequestClient>
    {
        private Mock<IModuleRequestRegistry> _registry;
        private Mock<IJson> _json;
        private string _packed;
        private string _packedResult;

        public override void Setup()
        {
            _registry = Mocker.GetMock<IModuleRequestRegistry>();
            _json = Mocker.GetMock<IJson>();
            _packed = "packed-json";
            _packedResult = "packed-result-json";
        }

        [Test]
        public async Task GetAsync_Always_GetsResult()
        {
            //Arrange
            var sut = CreateSut();
            var spec = new Mock<IModuleRequestSpecification>();

            _registry
                .Setup(o => o.GetRequest<TestDto, RequestTestObject>())
                .Returns(spec.Object);

            var query = new RequestTestObject();

            _json.Setup(o => o.SerializeAsync(query)).ReturnsAsync(_packed);

            spec.Setup(o => o.AsyncHandler).Returns(((_, _) => Task.FromResult(_packedResult)));

            var dto = new TestDto();

            _json.Setup(o => o.DeserializeAsync<TestDto>(_packedResult))
                .ReturnsAsync(dto);

            //Act
            var res = await sut.GetAsync<TestDto, RequestTestObject>(query);

            //Assert
            res.ShouldBe(dto);
        }
        
        
    }
}
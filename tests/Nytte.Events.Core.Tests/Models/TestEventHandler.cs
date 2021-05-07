using System.Threading.Tasks;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Tests.Models
{
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        public Task HandleAsync(TestEvent eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}
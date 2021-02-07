using System.Threading.Tasks;
using Nytte.Events.Abstractions;
using Nytte.Sample.ModuleB.Events;

namespace Nytte.Sample.ModuleB
{
    public class ModuleBService
    {
        private readonly IEventPublisher _eventPublisher;

        public ModuleBService(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public async Task SendMessage(string text)
        {
            await _eventPublisher.PublishAsync(new SampleMessage(text));
        }
    }
}
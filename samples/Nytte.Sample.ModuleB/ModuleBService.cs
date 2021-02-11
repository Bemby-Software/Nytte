using System;
using System.Threading.Tasks;
using Nytte.Events.Abstractions;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Sample.ModuleB.Events;

namespace Nytte.Sample.ModuleB
{
    public class ModuleBService
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ModuleAProxy _moduleAProxy;

        public ModuleBService(IEventPublisher eventPublisher, ModuleAProxy moduleAProxy)
        {
            _eventPublisher = eventPublisher;
            _moduleAProxy = moduleAProxy;
        }

        public async Task SendMessage(string text)
        {
            await _eventPublisher.PublishAsync(new SampleMessage(text));
        }

        public async Task<string> GetUsername(Guid userId)
        {
            var user =  await _moduleAProxy.GetUserByIdAsync(userId);
            return user.Name;
        }
    }
}
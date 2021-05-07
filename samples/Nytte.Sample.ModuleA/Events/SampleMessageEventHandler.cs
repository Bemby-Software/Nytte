using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nytte.Events.Abstractions;
using Nytte.Sample.ModuleA.Events.External;

namespace Nytte.Sample.ModuleA.Events
{
    public class SampleMessageEventHandler : IEventHandler<SampleMessage>
    {
        private readonly ILogger<SampleMessageEventHandler> _logger;

        public SampleMessageEventHandler(ILogger<SampleMessageEventHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleAsync(SampleMessage eventData)
        {
            _logger.LogInformation($"Got event in module A with message {eventData.Text}");
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;

namespace Nytte.Events.Abstractions
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
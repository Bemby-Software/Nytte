using System.Threading.Tasks;

namespace Nytte.Events.Abstractions
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent eventData);
    }
}
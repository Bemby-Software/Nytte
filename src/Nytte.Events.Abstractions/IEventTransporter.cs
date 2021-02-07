using System.Threading.Tasks;

namespace Nytte.Events.Abstractions
{
    public interface IEventTransporter
    {
        Task<string> PackAsync(object data);

        Task<T> UnPackAsync<T>(string data);
    }
}
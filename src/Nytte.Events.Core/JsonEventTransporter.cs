using System.Text.Json;
using System.Threading.Tasks;
using Nytte.Events.Abstractions;
using Nytte.Wrappers;

namespace Nytte.Events.Core
{
    public class JsonEventTransporter : IEventTransporter
    {
        public Task<string> PackAsync(object data) 
            => Task.Run(() => JsonSerializer.Serialize(data));

        public Task<T> UnPackAsync<T>(string data) 
            => Task.Run(() => JsonSerializer.Deserialize<T>(data));
    }
}
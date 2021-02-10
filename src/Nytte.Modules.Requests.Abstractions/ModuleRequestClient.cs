using System;
using System.Threading.Tasks;
using Nytte.Wrappers;

namespace Nytte.Modules.Requests.Abstractions
{
    public class ModuleRequestClient : IModuleClient
    {
        private readonly IModuleRequestRegistry _registry;
        private readonly IServiceProvider _serviceProvider;
        private readonly IJson _json;

        public ModuleRequestClient(IModuleRequestRegistry registry, IServiceProvider serviceProvider, IJson json)
        {
            _registry = registry;
            _serviceProvider = serviceProvider;
            _json = json;
        }
        
        public async Task<TReturns> GetAsync<TReturns, TQuery>(TQuery query) where TReturns : class where TQuery : IModuleQuery
        {
            var spec = _registry.GetRequest<TReturns, TQuery>();

            var packed = await _json.SerializeAsync(query);

            var serialisedResult = await spec.AsyncHandler(_serviceProvider, packed);

            return await _json.DeserializeAsync<TReturns>(serialisedResult);
        }
    }
}
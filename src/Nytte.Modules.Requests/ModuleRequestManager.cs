using Nytte.Modules.Requests.Abstractions;

namespace Nytte.Modules.Requests
{
    public class ModuleRequestManager : IModuleRequestManager
    {
        private readonly IModuleRequestRegistry _registry;
        private readonly IModuleRequestFactory _factory;

        public ModuleRequestManager(IModuleRequestRegistry registry, IModuleRequestFactory factory)
        {
            _registry = registry;
            _factory = factory;
        }

        public void RegisterRequestHandler<TReturns, TRequest>() where TReturns : class where TRequest : IModuleRequest
        {
            var key = _factory.GetKey<TReturns, TRequest>();
            
            var handler = _factory.CreateHandler<TReturns, TRequest>();

            var spec = _factory.Create<TReturns, TRequest>(key, handler);
            
            _registry.Add(spec);
        }
    }
}
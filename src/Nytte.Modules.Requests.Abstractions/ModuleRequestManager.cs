namespace Nytte.Modules.Requests.Abstractions
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

        public void RegisterQueryHandler<TReturns, TQuery>() where TReturns : class where TQuery : IModuleQuery
        {
            var key = _factory.GetKey<TReturns, TQuery>();
            
            var handler = _factory.CreateHandler<TReturns, TQuery>();

            var spec = _factory.Create<TReturns, TQuery>(key, handler);
            
            _registry.Add(spec);
        }
    }
}
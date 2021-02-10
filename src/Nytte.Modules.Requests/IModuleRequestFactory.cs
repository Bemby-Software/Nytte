namespace Nytte.Modules.Requests
{
    public interface IModuleRequestFactory
    {
        ScopedRequestHandlerDelegateAsync CreateHandler<TReturns, TQuery>()
            where TQuery : IModuleQuery
            where TReturns : class;

        string GetKey<TReturns, TQuery>()
            where TQuery : IModuleQuery
            where TReturns : class;

        IModuleRequestSpecification Create<TReturns, TQuery>(string key, ScopedRequestHandlerDelegateAsync handler)
            where TQuery : IModuleQuery
            where TReturns : class;
    }
}
namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestFactory
    {
        ScopedRequestHandlerDelegateAsync CreateHandler<TReturns, TRequest>()
            where TRequest : IModuleRequest
            where TReturns : class;

        string GetKey<TReturns, TRequest>()
            where TRequest : IModuleRequest
            where TReturns : class;

        IModuleRequestSpecification Create<TReturns, TRequest>(string key, ScopedRequestHandlerDelegateAsync handler)
            where TRequest : IModuleRequest
            where TReturns : class;
    }
}
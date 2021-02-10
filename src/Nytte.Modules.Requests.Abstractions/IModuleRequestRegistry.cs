namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestRegistry
    {
        IModuleRequestSpecification GetRequest<TReturns, TRequest>()
            where TRequest : IModuleRequest
            where TReturns : class;

        void Add(IModuleRequestSpecification specification);
    }
}
namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestRegistry
    {
        IModuleRequestSpecification GetRequest<TReturns, TQuery>()
            where TQuery : IModuleQuery
            where TReturns : class;

        void Add(IModuleRequestSpecification specification);
    }
}
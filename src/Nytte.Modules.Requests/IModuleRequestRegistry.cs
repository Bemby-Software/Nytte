namespace Nytte.Modules.Requests
{
    public interface IModuleRequestRegistry
    {
        IModuleRequestSpecification GetRequest<TReturns, TQuery>()
            where TQuery : IModuleQuery
            where TReturns : class;

        void Add(IModuleRequestSpecification specification);
    }
}
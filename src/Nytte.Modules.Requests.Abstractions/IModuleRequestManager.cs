namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestManager
    {
        void RegisterQueryHandler<TReturns, TQuery>()
            where TQuery : IModuleQuery
            where TReturns : class;
    }
}
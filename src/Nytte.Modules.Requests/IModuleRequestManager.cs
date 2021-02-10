namespace Nytte.Modules.Requests
{
    public interface IModuleRequestManager
    {
        void RegisterQueryHandler<TReturns, TQuery>()
            where TQuery : IModuleQuery
            where TReturns : class;
    }
}
namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestManager
    {
        void RegisterRequestHandler<TReturns, TRequest>()
            where TRequest : IModuleRequest
            where TReturns : class;
    }
}
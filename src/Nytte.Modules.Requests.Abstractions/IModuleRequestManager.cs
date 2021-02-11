namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestManager
    {
        void UseRequest<TReturns, TRequest>()
            where TRequest : IModuleRequest
            where TReturns : class;
    }
}
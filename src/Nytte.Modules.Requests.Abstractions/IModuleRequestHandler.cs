using System.Threading.Tasks;

namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleRequestHandler<TReturns, in TRequest> 
        where TRequest : IModuleRequest
        where TReturns : class
    {
        Task<TReturns> HandleAsync(TRequest query);
    }
}
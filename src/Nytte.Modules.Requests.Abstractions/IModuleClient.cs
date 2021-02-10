using System.Threading.Tasks;

namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleClient
    {
        Task<TReturns> GetAsync<TReturns, TRequest>(TRequest query)
            where TRequest : IModuleRequest
            where TReturns : class;
    }
}
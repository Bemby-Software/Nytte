using System.Threading.Tasks;

namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleClient
    {
        Task<TReturns> GetAsync<TReturns, TQuery>(TQuery query)
            where TQuery : IModuleQuery
            where TReturns : class;
    }
}
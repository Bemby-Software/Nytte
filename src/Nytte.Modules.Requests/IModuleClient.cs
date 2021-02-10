using System.Threading.Tasks;

namespace Nytte.Modules.Requests
{
    public interface IModuleClient
    {
        Task<TReturns> GetAsync<TReturns, TQuery>(TQuery query)
            where TQuery : IModuleQuery
            where TReturns : class;
    }
}
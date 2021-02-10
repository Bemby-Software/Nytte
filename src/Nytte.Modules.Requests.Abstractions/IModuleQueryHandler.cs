using System.Threading.Tasks;

namespace Nytte.Modules.Requests.Abstractions
{
    public interface IModuleQueryHandler<TReturns, TQuery> 
        where TQuery : IModuleQuery
        where TReturns : class
    {
        Task<TReturns> HandleAsync(TQuery query);
    }
}
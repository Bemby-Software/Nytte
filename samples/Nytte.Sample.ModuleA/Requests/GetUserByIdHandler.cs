using System.Threading.Tasks;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Sample.ModuleA.Dtos;

namespace Nytte.Sample.ModuleA.Requests
{
    public class GetUserByIdHandler : IModuleRequestHandler<UserDto, GetUserById>
    {
        public Task<UserDto> HandleAsync(GetUserById query)
        {
            return Task.FromResult(new UserDto("Joe", query.Id));
        }
    }
}
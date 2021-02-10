using System.Threading.Tasks;
using Nytte.Modules.Requests;
using Nytte.Sample.ModuleB.Dtos;

namespace Nytte.Sample.ModuleB.Queries
{
    public class GetUserByIdHandler : IModuleQueryHandler<UserDto, GetUserById>
    {
        public async Task<UserDto> HandleAsync(GetUserById query)
        {
            await Task.CompletedTask;
            return new UserDto(query.Id, "Sample");
        }
    }
}
using System;
using System.Threading.Tasks;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Sample.ModuleB.Dtos;
using Nytte.Sample.ModuleB.Dtos.External;
using Nytte.Sample.ModuleB.Requests;
using Nytte.Sample.ModuleB.Requests.External;

namespace Nytte.Sample.ModuleB
{
    public class ModuleAProxy
    {
        private readonly IModuleClient _client;

        public ModuleAProxy(IModuleClient client)
        {
            _client = client;
        }
        
        public async  Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            return await _client.GetAsync<UserDto, GetUserById>(new GetUserById(userId));
        }
    }
}
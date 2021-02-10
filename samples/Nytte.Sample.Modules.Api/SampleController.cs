using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nytte.Modules.Requests;
using Nytte.Sample.ModuleB;
using Nytte.Sample.ModuleB.Dtos;
using Nytte.Sample.ModuleB.Queries;

namespace Nytte.Sample.Modules.Api
{
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ModuleBService _service;
        private readonly IModuleClient _moduleClient;

        public SampleController(ModuleBService service, IModuleClient moduleClient)
        {
            _service = service;
            _moduleClient = moduleClient;
        }
        
        [HttpGet("send")]
        public async Task Get([FromQuery] string message)
        {
            await _service.SendMessage(message);
        }

        [HttpGet("module-requests")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var res = await _moduleClient.GetAsync<UserDto, GetUserById>(new GetUserById(id));
            return Ok(res);
        }
    }
}
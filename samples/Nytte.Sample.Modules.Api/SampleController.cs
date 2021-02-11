using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nytte.Sample.ModuleB;

namespace Nytte.Sample.Modules.Api
{
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ModuleBService _service;

        public SampleController(ModuleBService service)
        {
            _service = service;
        }
        
        [HttpGet("send")]
        public async Task Get([FromQuery] string message)
        {
            await _service.SendMessage(message);
        }

        [HttpGet("username/{userId}")]
        public async Task<IActionResult> GetUsername([FromRoute] Guid userId) 
            => Ok(await _service.GetUsername(userId));
    }
}
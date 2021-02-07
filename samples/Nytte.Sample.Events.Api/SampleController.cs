using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nytte.Sample.ModuleB;

namespace Nytte.Sample.Events.Api
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
    }
}
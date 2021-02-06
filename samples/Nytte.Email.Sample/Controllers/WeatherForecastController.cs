using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nytte.Email.Razor;
using Nytte.Email.Sample.Models;
using Nytte.Email.Sample.Views.ViewModels;

namespace Nytte.Email.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEmailService _emailService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            
            await _emailService.SendEmailAsync(new RazorEmailMessageBuilder(),
                new RazorEmailMessageBlueprint("rob", "robertbennett1998@gmail.com", "Nytte Razor Example Email", "NytteRazorExampleEmail"));
            
            await _emailService.SendEmailAsync(new RazorEmailMessageBuilder(),
                new RazorEmailMessageBlueprint<TestEmailWithVmViewModel>("rob", "robertbennett1998@gmail.com", "Test", "TestEmailWithVm", new TestEmailWithVmViewModel() {Message =  "Hello World!"}));

            var forecast = forecasts.First();
            await _emailService.SendEmailAsync(new RazorEmailMessageBuilder(),
                new RazorEmailMessageBlueprint<WeatherForecast>("rob", "robertbennett1998@gmail.com", "Weather Forecast", "WeatherForecast", forecast));


            return forecasts;
        }
    }
}
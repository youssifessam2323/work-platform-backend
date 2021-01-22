using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace work_platform_backend.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        //[Authorize]
        public IEnumerable<WeatherForecast> Get(string data)
        {
            _logger.LogDebug (HttpContext.User.Identity.AuthenticationType);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("task")]
        [Authorize]
        public async Task<string> getTheTaskByCondition(int roomId)
        {
            var user = HttpContext.User;
            _logger.LogDebug(user.ToString());
            return "you accessed me successfully";
        }
    
       
    }
}

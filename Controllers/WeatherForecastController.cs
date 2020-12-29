using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using work_platform_backend.Authorization;
using work_platform_backend.Models;
using work_platform_backend.Repos;
using work_platform_backend.Services;

namespace work_platform_backend.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserRepository userRepository;
        
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public IEnumerable<WeatherForecast> Get(string data)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

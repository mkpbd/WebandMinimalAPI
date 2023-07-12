using Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace CompanyEmployee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      
        private readonly IRepositoryManager _repository;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {
            _repository.Company.AnyMethodFromCompanyRepository();

            return new[] { "value1", "value 2" };
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace апишка_саф.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
           "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            if (sortStrategy == null)
            {
                return Ok(Summaries);
            }
            else if (sortStrategy == 1)
            {
                return Ok(Summaries.OrderBy(s => s).ToList());
            }
            else if (sortStrategy == -1)
            {
                return Ok(Summaries.OrderByDescending(s => s).ToList());
            }
            else
            {
                return BadRequest("Некорректное значение параметра sortStrategy");
            }
        }

        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("ИНДЕКС НЕВЕРНЫЙ!!!!!!!!!!!!");
            }
            return Ok(Summaries[index]);
        }

        [HttpGet("find-by-name")]
        public IActionResult GetCountByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("ИМЯ НЕ МОЖЕТ БЫТЬ ПУСТЫМ!!!!!!!!!!!!");
            }
            int count = Summaries.Count(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));
            return Ok(count);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("ИМЯ НЕ МОЖЕТ БЫТЬ ПУСТЫМ!!!!!!!!!!!!");
            }
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("ИНДЕКС НЕВЕРНЫЙ!!!!!!!!!!!!");
            }
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("ИМЯ НЕ МОЖЕТ БЫТЬ ПУСТЫМ!!!!!!!!!!!!");
            }
            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("ИНДЕКС НЕВЕРНЫЙ!!!!!!!!!!!!");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
    }
}
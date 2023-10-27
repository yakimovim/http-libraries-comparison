using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private static readonly string Id = Guid.NewGuid().ToString("N");

        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            return Ok($"Hello from {Id}");
        }

        [HttpGet("long")]
        public async Task<IActionResult> GetLong(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);

                return Ok($"Done from {Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Ok($"Problem at {Id}");
            }
        }

        [HttpGet("tee")]
        public IActionResult GetTee()
        {
            return StatusCode(418);
        }

        [HttpGet("rnd")]
        public IActionResult GetRandom()
        {
            if (Random.Shared.Next(0, 2) == 0)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
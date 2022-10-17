using EngineeringWeekly.API.Componets;
using EngineeringWeekly.API.Componets.Interfaces;
using EngineeringWeekly.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Controllers
{
    [Route("api/[controller]")]
    public class ChuckNorrisJokeGetterController : Controller
    {
        /// <summary>
        /// The service configuration accessor
        /// </summary>
        IOptions<ServiceConfigOptions> ServiceConfigAccessor { get; }
        /// <summary>
        /// The logger
        /// </summary>
        ILogger<ChuckNorrisJokeGetterController> Logger { get; }
        IChuckNorrisJokeGetter ChuckNorrisJoke  { get; }

        public ChuckNorrisJokeGetterController(ILogger<ChuckNorrisJokeGetterController> logger, IOptions<ServiceConfigOptions> serviceConfigAccessor, IChuckNorrisJokeGetter chuckNorrisJoke)
        {
            serviceConfigAccessor.Value.ServiceName = serviceConfigAccessor.Value.ServiceName.Replace("{controllerName}", nameof(ChuckNorrisJokeGetterController));

            Logger = logger;
            ServiceConfigAccessor = serviceConfigAccessor;
            ChuckNorrisJoke = chuckNorrisJoke;
        }
        /// <summary>
        /// Health method to verify if service are running
        /// </summary>
        /// <returns></returns>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(ServiceConfigAccessor.Value);
        }
        /// <summary>
        /// Gets a random joke from Chuck Norris Jokes API
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("GetJoke")]
        public async Task<IActionResult> GetJoke(string? category)
        {
            var result = await ChuckNorrisJoke.GetJoke(category);
            if (result.IsFailed)
            {
                var error = result.Errors.First().Message;
                Logger.LogError(error);
                return StatusCode(500, error);
            }

            return Ok(result.Value);
        }
        [HttpGet("GetJokeCategories")]
        public async Task<IActionResult> GetJokeCategories()
        {
            var result = await ChuckNorrisJoke.GetJokeCategories();
            if (result.IsFailed)
            {
                var error = result.Errors.First().Message;
                Logger.LogError(error);
                return StatusCode(500, error);
            }

            return Ok(result.Value);
        }
    }
}
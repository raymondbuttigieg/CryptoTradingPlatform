using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatesService.Services;

namespace RatesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly RatesFetcher _ratesFetcher;

        public RatesController(RatesFetcher ratesFetcher)
        {
            _ratesFetcher = ratesFetcher;
        }

        [HttpGet]
        public async Task<IActionResult> FetchRates()
        {
            await _ratesFetcher.FetchRates();
            return Ok("Rates Fetched");
        }
    }
}

using RatesService.Models;
using Shared
namespace RatesService.Services
{
    public class RatesFetcher
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RatesFetcher(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task FetchRates()
        {
            var client = _httpClientFactory.CreateClient("CoinMarketCapClient");
            var url = "v1/cryptocurrency/listings/latest?convert=USD";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {

                Console.WriteLine("Error rates not fetched");

            }
            var result = await response.Content.ReadFromJsonAsync<CoinMarketResponse>();

            foreach (var crypto in result.Data)
            {
                decimal oldRate = 1000;
                decimal currentRate = crypto.Quote.USD.Price;
                decimal percentageChange = Math.Abs((currentRate - oldRate) / oldRate * 100);

                if (percentageChange > 5) {

                    Console.WriteLine($"Percentage exeded old rate :{oldRate}, New rate : {currentRate}, Percentage Change :{percentageChange}");

                    // should publish a notification to rabbitMQ
                    var rateChanged = new RateChangedEvent 
                
                }

            }

        }
    }
}

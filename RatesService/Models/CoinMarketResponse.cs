using Swashbuckle.AspNetCore.Swagger;

namespace RatesService.Models
{
    public class CoinMarketResponse
    {
        public List<CryptoData> Data { get; set; }
    }

    public class CryptoData
    {
        public string Symbol { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        public USD USD { get; set; }
    }

    public class USD
    {
        public decimal Price { get; set; }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RatesService.Services; // Make sure to use the proper namespace

// Build the host
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register the HTTP Client and RatesFetcher service.
        services.AddHttpClient("CoinMarketCapClient", client =>
        {
            client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/");
            // Read API key from configuration (see below for configuration details)
            var apiKey = context.Configuration["CoinMarketCap:ApiKey"];
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);
        });
        services.AddTransient<RatesFetcher>();

        // Optionally, add more services (like a PositionsCalculator) if needed.
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole(); // All logs will be written to the console.
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        // Optionally load configuration from appsettings.json:
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .Build();

// Use the service(s)
var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Starting the RatesFetcher process...");

try
{
    var ratesFetcher = host.Services.GetRequiredService<RatesFetcher>();
    await ratesFetcher.FetchRates();

    // Inform the user that the process is complete.
    Console.WriteLine("Rates fetching and processing complete.");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while processing rates.");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

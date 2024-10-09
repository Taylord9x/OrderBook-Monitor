using OrderBook_Monitor_API.CheckSumCalculator;
using OrderBook_Monitor_API.CheckSumCalculator.Interfaces;
using OrderBook_Monitor_API.CryptoIndexFund;
using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models.CryptoIndexFund;
using OrderBook_Monitor_API.OrderBookManager;
using OrderBook_Monitor_API.OrderBookManager.Interfaces;
using OrderBook_Monitor_API.PricingCalculator;
using OrderBook_Monitor_API.PricingCalculator.Interfaces;
using OrderBook_Monitor_API.PricingSupplier;
using OrderBook_Monitor_API.PricingSupplier.Interfaces;
using OrderBook_Monitor_API.WebSocketService;
using OrderBook_Monitor_API.WebSocketService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

var webSocketStartupService = app.Services.GetRequiredService<WebSocketStartupService>();
await webSocketStartupService.StartAsync();

ConfigureEndpoints(app, webSocketStartupService);

app.Lifetime.ApplicationStopping.Register(async () =>
{
    await webSocketStartupService.StopAsync();
});

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IWebSocketService, WebSocketService>();
    services.AddSingleton<ICheckSumCalculator, CheckSumCalculator>();
    services.AddSingleton<IOrderBookManager, OrderBookManager>();
    services.AddScoped<ICryptoIndexManager, CryptoIndexManager>();
    services.AddScoped<ISymbolsExtractor, SymbolsExtractor>();
    services.AddScoped<IPercentageSupplier, PercentageSupplier>();
    services.AddScoped<IZarValueSupplier, ZarValueSupplier>();
    services.AddScoped<IPercentageNormalizer, PercentageNormalizer>();
    services.AddScoped<IAmountSupplier, AmountSupplier>();
    services.AddScoped<IExternalPriceService, ExternalPriceService>();
    services.AddScoped<IPricingCalculator, PricingCalculator>();
    services.AddScoped<IPriceSupplier, PriceSupplier>();
    services.AddSingleton<WebSocketStartupService>();
}

static void ConfigureEndpoints(WebApplication app, WebSocketStartupService webSocketStartupService)
{
    app.MapGet("/api/price/get-price", (decimal quantity, IPriceSupplier priceSupplier) =>
    {
        return priceSupplier.GetPriceResult(quantity);
    });

    app.MapPost("/api/crypto-index/calculate-allocations", async (ICryptoIndexManager cryptoIndexManager, CryptoIndexManagerRequest request) =>
    {
        var allocations = await cryptoIndexManager.CalculateAllocations(request.TotalCapital, request.AssetCap, request.Assets);
        return Results.Ok(allocations);
    });
}
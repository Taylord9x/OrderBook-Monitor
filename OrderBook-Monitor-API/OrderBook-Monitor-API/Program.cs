using OrderBook_Monitor_API.CryptoIndexFund;
using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models;
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

var ask = new SortedDictionary<decimal, List<Order>>();

builder.Services.AddSingleton(ask);
builder.Services.AddSingleton<IWebSocketService, WebSocketService>();
builder.Services.AddSingleton<IOrderBookManager, OrderBookManager>();
builder.Services.AddSingleton<ICryptoIndexManager, CryptoIndexManager>();
builder.Services.AddScoped<IPricingCalculator, PricingCalculator>();
builder.Services.AddScoped<IPriceSupplier, PriceSupplier>();
builder.Services.AddSingleton<WebSocketStartupService>();

var app = builder.Build();

var webSocketStartupService = app.Services.GetRequiredService<WebSocketStartupService>();
await webSocketStartupService.StartAsync();

app.MapGet("/api/price/get-price", (decimal quantity, IPriceSupplier priceSupplier) =>
{
    return priceSupplier.GetPriceResult(quantity);
});

app.MapPost("/api/crypto-index/calculate-allocations", async (ICryptoIndexManager cryptoIndexManager, CryptoIndexManagerRequest request) =>
{
    var allocations = await cryptoIndexManager.CalculateAllocations(request.TotalCapital, request.AssetCap, request.Assets);
    return Results.Ok(allocations);
});

app.Lifetime.ApplicationStopping.Register(async () =>
{
    await webSocketStartupService.StopAsync();
});

app.Run();
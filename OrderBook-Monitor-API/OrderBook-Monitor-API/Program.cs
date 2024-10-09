using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.Models.CryptoIndexFund;
using OrderBook_Monitor_API.PricingSupplier.Interfaces;
using OrderBook_Monitor_API.ServiceConfiguration;
using OrderBook_Monitor_API.Swagger;
using OrderBook_Monitor_API.WebSocketService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerConfig.ConfigureSwaggerGen);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var ask = new SortedDictionary<decimal, List<Order>>();
builder.Services.AddSingleton(ask);

ServiceConfig.ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

var webSocketStartupService = app.Services.GetRequiredService<WebSocketStartupService>();
await webSocketStartupService.StartAsync();

ConfigureEndpoints(app, webSocketStartupService);

app.Lifetime.ApplicationStopping.Register(async () =>
{
    await webSocketStartupService.StopAsync();
});

app.Run();

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
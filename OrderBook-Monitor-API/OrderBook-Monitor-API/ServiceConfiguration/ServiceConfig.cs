using OrderBook_Monitor_API.CryptoIndexFund;
using OrderBook_Monitor_API.CryptoIndexFund.Interfaces;
using OrderBook_Monitor_API.OrderBookManager.Interfaces;
using OrderBook_Monitor_API.PricingCalculator.Interfaces;
using OrderBook_Monitor_API.PricingSupplier;
using OrderBook_Monitor_API.PricingSupplier.Interfaces;
using OrderBook_Monitor_API.WebSocketService;
using OrderBook_Monitor_API.WebSocketService.Interfaces;

namespace OrderBook_Monitor_API.ServiceConfiguration;

public static class ServiceConfig
{
  public static void ConfigureServices(IServiceCollection services)
  {
    services.AddSingleton<IWebSocketService, WebSocketService.WebSocketService>();
    services.AddSingleton<IOrderBookManager, OrderBookManager.OrderBookManager>();
    services.AddScoped<ICryptoIndexManager, CryptoIndexManager>();
    services.AddScoped<ISymbolsExtractor, SymbolsExtractor>();
    services.AddScoped<IPercentageSupplier, PercentageSupplier>();
    services.AddScoped<IZarValueSupplier, ZarValueSupplier>();
    services.AddScoped<IPercentageNormalizer, PercentageNormalizer>();
    services.AddScoped<IAmountSupplier, AmountSupplier>();
    services.AddScoped<IExternalPriceService, ExternalPriceService>();
    services.AddScoped<IPricingCalculator, PricingCalculator.PricingCalculator>();
    services.AddScoped<IPriceSupplier, PriceSupplier>();
    services.AddSingleton<WebSocketStartupService>();
  }
}
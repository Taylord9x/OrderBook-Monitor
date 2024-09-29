namespace OrderBook_Monitor_API.PricingSupplier.Interfaces;

public interface IPriceSupplier
{
  Task<decimal> GetPriceAsync(decimal quantity);
}
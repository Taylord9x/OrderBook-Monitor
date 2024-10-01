namespace OrderBook_Monitor_API.PricingSupplier.Interfaces;

public interface IPriceSupplier
{
  IResult GetPriceResult(decimal quantity);
}
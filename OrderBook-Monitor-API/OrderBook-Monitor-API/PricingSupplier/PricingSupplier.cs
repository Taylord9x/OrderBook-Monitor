using OrderBook_Monitor_API.PricingCalculator.Interfaces;
using OrderBook_Monitor_API.PricingSupplier.Interfaces;


namespace OrderBook_Monitor_API.PricingSupplier;
public class PriceSupplier(IPricingCalculator pricingCalculator) : IPriceSupplier
{
  private readonly IPricingCalculator _pricingCalculator = pricingCalculator;

  private decimal GetPrice(decimal quantity)
  {
    if (quantity <= 0)
    {
      throw new ArgumentException("Quantity must be greater than zero.");
    }

    decimal price = _pricingCalculator.CalculatePrice(quantity);
    return price;
  }

  public IResult GetPriceResult(decimal quantity)
  {
    try
    {
      decimal price = GetPrice(quantity);
      return Results.Ok(new { Price = price });
    }
    catch (ArgumentException ex)
    {
      return Results.BadRequest(ex.Message);
    }
    catch (Exception)
    {
      return Results.StatusCode(500);
    }
  }
}
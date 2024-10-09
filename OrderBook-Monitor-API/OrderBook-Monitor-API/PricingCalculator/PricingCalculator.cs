using OrderBook_Monitor_API.OrderBookManager.Interfaces;
using OrderBook_Monitor_API.PricingCalculator.Interfaces;

namespace OrderBook_Monitor_API.PricingCalculator;
public class PricingCalculator(IOrderBookManager orderBookManager) : IPricingCalculator
{
  private readonly IOrderBookManager _orderBookManager = orderBookManager;
  
  public decimal CalculatePrice(decimal quantity)
  {
    var asks = _orderBookManager.GetTop25Asks();
    decimal totalCost = 0;
    decimal remainingQuantity = quantity;

    foreach (var ask in asks)
    {
      if (decimal.TryParse(ask.Price, out decimal price) && 
          decimal.TryParse(ask.Orders[0].Quantity, out decimal availableQuantity))
      {
        if (remainingQuantity <= availableQuantity)
        {
          totalCost += remainingQuantity * price;
          break;
        }
        else
        {
          totalCost += availableQuantity * price;
          remainingQuantity -= availableQuantity;
        }
      }
    }
    return totalCost / quantity;
  }
}
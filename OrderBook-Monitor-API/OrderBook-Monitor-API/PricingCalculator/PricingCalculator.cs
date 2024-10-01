using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.PricingCalculator.Interfaces;

namespace OrderBook_Monitor_API.PricingCalculator;
public class PricingCalculator(SortedDictionary<decimal, List<Order>> asks) : IPricingCalculator
{
  private readonly SortedDictionary<decimal, List<Order>> _asks = asks;
  
  public decimal CalculatePrice(decimal quantity)
  {
    decimal totalCost = 0m;
    decimal remainingQuantity = quantity;

    foreach (var entry in _asks)
    {
      decimal price = entry.Key;
      List<Order> orders = entry.Value;

      foreach (var order in orders)
      {
        if (remainingQuantity <= 0)
          break;

        if (order.Quantity > remainingQuantity)
        {
          totalCost += remainingQuantity * price;
          order.Quantity -= remainingQuantity;
          remainingQuantity = 0;
        }
        else
        {
          totalCost += order.Quantity * price;
          remainingQuantity -= order.Quantity;
          order.Quantity = 0;
        }
      }
    }

    if (remainingQuantity > 0)
    {
      throw new InvalidOperationException("Not enough liquidity in the order book to fulfill the request.");
    }

    return totalCost;
  }
}
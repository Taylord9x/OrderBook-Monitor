using OrderBook_Monitor_API.Models;

namespace OrderBook_Monitor_API.PricingCalculator.Interfaces;

public interface IPricingCalculator
{
    decimal CalculatePrice(decimal quantity, SortedDictionary<decimal, List<Order>> asks);
}
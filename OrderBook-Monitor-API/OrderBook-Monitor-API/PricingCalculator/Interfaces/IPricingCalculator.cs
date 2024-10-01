namespace OrderBook_Monitor_API.PricingCalculator.Interfaces;

public interface IPricingCalculator
{
    decimal CalculatePrice(decimal quantity);
}
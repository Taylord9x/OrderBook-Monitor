namespace OrderBook_Monitor_API.Models.CryptoIndexFund;

public class Asset
{
    public required string Symbol { get; set; }
    public decimal MarketCap { get; set; }
    public decimal Price { get; set; }
}
namespace OrderBook_Monitor_API.Models.CryptoIndexFund;

public class AssetAllocation
{
    public required string Symbol { get; set; }
    public decimal Percentage { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
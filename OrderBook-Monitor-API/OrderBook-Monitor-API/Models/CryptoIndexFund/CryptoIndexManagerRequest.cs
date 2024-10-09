using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderBook_Monitor_API.Models.CryptoIndexFund;

public class CryptoIndexManagerRequest
{
  [SwaggerSchema(Description = "Total capital to be allocated")]
  [DefaultValue(1000)]
  public decimal TotalCapital { get; set; }

  [SwaggerSchema(Description = "Maximum allocation per asset")]
  [DefaultValue(0.5)]
  public decimal AssetCap { get; set; }

  [SwaggerSchema(Description = "List of assets for allocation")]
  public required IEnumerable<Asset> Assets { get; set; }
}
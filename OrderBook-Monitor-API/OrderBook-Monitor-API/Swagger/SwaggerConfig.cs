using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OrderBook_Monitor_API.Models.CryptoIndexFund;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OrderBook_Monitor_API.Swagger;

public static class SwaggerConfig
{
  public static void ConfigureSwaggerGen(SwaggerGenOptions options)
  {
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            
    options.MapType<IEnumerable<Asset>>(() => new OpenApiSchema
    {
      Type = "array",
      Items = new OpenApiSchema
      {
        Type = "object",
        Properties = new Dictionary<string, OpenApiSchema>
        {
          { "symbol", new OpenApiSchema { Type = "string" } },
          { "marketCap", new OpenApiSchema { Type = "number" } },
          { "price", new OpenApiSchema { Type = "number" } }
        }
      },
      Example = new OpenApiArray
      {
        new OpenApiObject
        {
          ["symbol"] = new OpenApiString("BTC"),
          ["marketCap"] = new OpenApiInteger(20000),
          ["price"] = new OpenApiInteger(0)
        },
        new OpenApiObject
        {
          ["symbol"] = new OpenApiString("ETH"),
          ["marketCap"] = new OpenApiInteger(10000),
          ["price"] = new OpenApiInteger(0)
        },
        new OpenApiObject
        {
          ["symbol"] = new OpenApiString("LTC"),
          ["marketCap"] = new OpenApiInteger(5000),
          ["price"] = new OpenApiInteger(0)
        },
        new OpenApiObject
        {
          ["symbol"] = new OpenApiString("DOGE"),
          ["marketCap"] = new OpenApiInteger(2500),
          ["price"] = new OpenApiInteger(0)
        }
      }
    });
  }
}
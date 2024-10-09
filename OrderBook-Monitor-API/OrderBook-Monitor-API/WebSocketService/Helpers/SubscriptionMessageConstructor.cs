using OrderBook_Monitor_API.Models;

namespace OrderBook_Monitor_API.WebSocketService.Helpers;

public static class SubscriptionMessageConsctructor
{
  private static readonly string[] PAIRS = ["USDTZAR"];

  public static object? GetMessage(MessageTypes messageType)
  {
    object? Message = null;
    if (messageType == MessageTypes.Subscribe)
    {
      Message = new
      {
        type = "SUBSCRIBE",
        subscriptions = new[]
        {
          new
          {
            @event = "FULL_ORDERBOOK_UPDATE",
            pairs = PAIRS
          }
        }
      };
    }
    else if (messageType == MessageTypes.Unsubscribe)
    {
      Message = new
      {
        type = "SUBSCRIBE",
        subscriptions = new[]
        {
          new
          {
            @event = "FULL_ORDERBOOK_UPDATE",
            pairs = Array.Empty<string>()
          }
        }
      };
    }
    
    if (Message != null)
      return Message;
    else
    {
      return null;
    }
  }
}
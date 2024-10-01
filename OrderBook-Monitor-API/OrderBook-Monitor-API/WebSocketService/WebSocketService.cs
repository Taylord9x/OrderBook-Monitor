using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.OrderBookManager.Interfaces;
using OrderBook_Monitor_API.WebSocketService.Interfaces;

namespace OrderBook_Monitor_API.WebSocketService;

public class WebSocketService(IOrderBookManager orderBookManager) : IWebSocketService
{
  private readonly IOrderBookManager _orderBookManager = orderBookManager;
  private readonly ClientWebSocket webSocket = new();
  private const string ServiceAddress = "wss://api.valr.com/ws/trade";
  private readonly string[] PAIRS = ["USDTZAR"];

  public async Task Connect()
  {
    var uri = new Uri(ServiceAddress);
    
    webSocket.Options.HttpVersion = new Version(1, 1);
    webSocket.Options.HttpVersionPolicy = HttpVersionPolicy.RequestVersionExact;

    await webSocket.ConnectAsync(uri, CancellationToken.None);
    _ = ReceiveMessages();
  }

  public async Task Subscribe()
  {
    string subscribeMessage = BuildJsonString(MessageTypes.Subscribe);
    await SendMessage(subscribeMessage);
  }

  public async Task Unsubscribe()
  {
    string unsubscribeMessage = BuildJsonString(MessageTypes.Unsubscribe);
    await SendMessage(unsubscribeMessage);
  }
  
  private string BuildJsonString(MessageTypes subscription)
  {
    Object? Message = null;

    if (subscription == MessageTypes.Subscribe)
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
    else if (subscription == MessageTypes.Unsubscribe)
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

    var jsonMessage = JsonSerializer.Serialize(Message);

    return jsonMessage;
  }
  
  private async Task SendMessage(string message)
  {
    if (webSocket.State == WebSocketState.Open)
    {
      var messageBuffer = System.Text.Encoding.UTF8.GetBytes(message);
      await webSocket.SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }
  }

  private async Task ReceiveMessages()
  {
    var buffer = new byte[32768];
    StringBuilder messageBuilder = new();

    while (webSocket.State == WebSocketState.Open)
    {
      var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

      string receivedChunk = Encoding.UTF8.GetString(buffer, 0, result.Count);
      messageBuilder.Append(receivedChunk);

      if (result.EndOfMessage)
      {
        string completeMessage = messageBuilder.ToString();
        messageBuilder.Clear();

        try
        {
          if (completeMessage.Contains("\"type\":\"SUBSCRIBED\""))
          {
            Console.WriteLine("Subscription confirmed.");
            continue;
          }
          else if (completeMessage.Contains("\"type\":\"UNSUBSCRIBED\""))
          {
            Console.WriteLine("Unsubscription confirmed.");
            continue;
          }

          ExternalOrderBook? externalOrderBook = JsonSerializer.Deserialize<ExternalOrderBook>(completeMessage, new JsonSerializerOptions
          {
            PropertyNameCaseInsensitive = true
          });

          if (externalOrderBook != null)
          {
            _orderBookManager.ProcessOrderBookSnapshot(externalOrderBook);
          }
        }
        catch (JsonException ex)
        {
          Console.WriteLine($"JSON parsing error: {ex.Message}");
        }
      }
    }
  }
}
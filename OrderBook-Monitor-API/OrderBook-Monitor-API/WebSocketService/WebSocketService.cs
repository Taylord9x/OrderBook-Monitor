using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.OrderBookManager.Interfaces;
using OrderBook_Monitor_API.WebSocketService.Helpers;
using OrderBook_Monitor_API.WebSocketService.Interfaces;

namespace OrderBook_Monitor_API.WebSocketService;

public class WebSocketService(IOrderBookManager orderBookManager) : IWebSocketService
{
  private readonly IOrderBookManager _orderBookManager = orderBookManager;
  private readonly ClientWebSocket webSocket = new();
  private const string ServiceAddress = "wss://api.valr.com/ws/trade";

  public async Task Connect()
  {
    var uri = new Uri(ServiceAddress);
    
    webSocket.Options.HttpVersion = new Version(1, 1);
    webSocket.Options.HttpVersionPolicy = HttpVersionPolicy.RequestVersionExact;

    await webSocket.ConnectAsync(uri, CancellationToken.None);

    MessageReceiver messageReceiver = new();
    _ = messageReceiver.ReceiveMessages(webSocket,_orderBookManager);
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
  
  private static string BuildJsonString(MessageTypes subscription)
  {
    object? Message = SubscriptionMessageConsctructor.GetMessage(subscription);
    var jsonMessage = JsonSerializer.Serialize(Message);
    return jsonMessage;
  }
  
  private async Task SendMessage(string message)
  {
    if (webSocket.State == WebSocketState.Open)
    {
      var messageBuffer = Encoding.UTF8.GetBytes(message);
      await webSocket.SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }
  }
}
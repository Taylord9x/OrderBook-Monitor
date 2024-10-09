using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.OrderBookManager.Interfaces;

namespace OrderBook_Monitor_API.WebSocketService.Helpers;

public class MessageReceiver
{
  public async Task ReceiveMessages(ClientWebSocket webSocket, IOrderBookManager _orderBookManager)
  {
    var buffer = new byte[262144];
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
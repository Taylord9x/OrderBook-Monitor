using OrderBook_Monitor_API.WebSocketService.Interfaces;

namespace OrderBook_Monitor_API.WebSocketService;

public class WebSocketStartupService(IWebSocketService webSocketService)
{
  private readonly IWebSocketService _webSocketService = webSocketService;

  public async Task StartAsync()
  {
    await _webSocketService.Connect();
    await _webSocketService.Subscribe();
  }

  public async Task StopAsync()
  {
    await _webSocketService.Unsubscribe();
  }
}
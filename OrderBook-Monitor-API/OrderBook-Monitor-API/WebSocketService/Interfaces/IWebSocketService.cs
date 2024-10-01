namespace OrderBook_Monitor_API.WebSocketService.Interfaces;

public interface IWebSocketService
{
    Task Connect();

    Task Subscribe();

    Task Unsubscribe();
}
namespace OrderBook_Monitor_API.WebSocketService.Interfaces;

public interface IWebSocketService
{
    void Connect(string uri);

    void Authenticate(string apiKey, string secret);

    void Subscribe(string channel);

    void Unsubscribe(string channel);

    void OnError(Action<Exception> onError);
}
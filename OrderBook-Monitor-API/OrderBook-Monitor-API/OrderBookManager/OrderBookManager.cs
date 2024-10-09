using OrderBook_Monitor_API.CheckSumCalculator.Interfaces;
using OrderBook_Monitor_API.Models;
using OrderBook_Monitor_API.Models.ExternalOrderBookData;
using OrderBook_Monitor_API.OrderBookManager.Interfaces;

namespace OrderBook_Monitor_API.OrderBookManager;

public class OrderBookManager(ICheckSumCalculator checkSumCalculator) : IOrderBookManager
{
    private readonly SortedDictionary<decimal, List<Order>> _asks = [];
    private readonly SortedDictionary<decimal, List<Order>> _bids = new(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));
    private readonly ICheckSumCalculator _checkSumCalculator = checkSumCalculator;

    public void AddAsk(Order order)
    {
        AddOrder(_asks, order);
    }

    public void AddBid(Order order)
    {
        AddOrder(_bids, order);
    }

    private static void AddOrder(SortedDictionary<decimal, List<Order>> orderBook, Order order)
    {
        if (orderBook.TryGetValue(order.Price, out List<Order>? value))
        {
            value.Add(order);
        }
        else
        {
            orderBook[order.Price] = [order];
        }
    }

    public void ProcessOrderBookSnapshot(ExternalOrderBook externalOrderBook)
    {
        _asks.Clear();
        _bids.Clear();

        ProcessOrders(externalOrderBook.Data.Asks, AddAsk);
        ProcessOrders(externalOrderBook.Data.Bids, AddBid);

        var top10Bids = GetTop25Bids();
        var top10Asks = GetTop25Asks();

        long calculatedCheckSum = _checkSumCalculator.CalculateChecksum(top10Bids, top10Asks);
        if (calculatedCheckSum != externalOrderBook.Data.Checksum)
        {
            Console.WriteLine($"Checksum mismatch: expected {externalOrderBook.Data.Checksum}, calculated {calculatedCheckSum}");
            return;
        }
        Console.WriteLine($"Checksum MATCHED: expected {externalOrderBook.Data.Checksum}, calculated {calculatedCheckSum}");
    }

    private static void ProcessOrders(List<ExternalOrderBookEntry> entries, Action<Order> addOrder)
    {
        foreach (var entry in entries)
        {
            if (decimal.TryParse(entry.Price, out decimal price))
            {
                foreach (var externalOrder in entry.Orders)
                {
                    if (decimal.TryParse(externalOrder.Quantity, out decimal quantity))
                    {
                        var order = new Order
                        {
                            OrderId = externalOrder.OrderId,
                            Price = price,
                            Quantity = quantity
                        };
                        addOrder(order);
                    }
                }
            }
        }
    }

    public List<ExternalOrderBookEntry> GetTop25Asks()
    {
        return GetTop25Orders(_asks);
    }

    public List<ExternalOrderBookEntry> GetTop25Bids()
    {
        return GetTop25Orders(_bids);
    }

    private static List<ExternalOrderBookEntry> GetTop25Orders(SortedDictionary<decimal, List<Order>> orderBook)
    {
        return orderBook.Take(25)
        .Select(kvp => new ExternalOrderBookEntry
        {
            Price = kvp.Key.ToString("0.0000"),
            Orders = kvp.Value.Select(o => new ExternalOrder 
            { 
                OrderId = o.OrderId,
                Quantity = o.Quantity.ToString("0.00000000") 
            }).ToList()
        })
        .ToList();
    }
}
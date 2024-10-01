namespace OrderBook_Monitor_API.Models.ExternalOrderBookData;

public class ExternalOrderBookMetaData
{
  public long LastChange { get; set; }
  public required List<ExternalOrderBookEntry> Asks { get; set; }
  public required List<ExternalOrderBookEntry> Bids { get; set; }
  public long SequenceNumber { get; set; }
  public long Checksum { get; set; }
}
public class Item
{
  public string UpcCode { get; }
  public double Price { get; }

  public Item(string upc, double price)
  {
    UpcCode = upc;
    Price = price;
  }
}
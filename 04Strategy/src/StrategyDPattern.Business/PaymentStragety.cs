public class PaymentStragety
{
  private List<Item> items;

  public PaymentStragety()
  {
    items = new List<Item>();
  }

  public void AddItem(Item item) => items.Add(item);
  public double CalculateTotal()
  {
    double sum = 0;

    foreach (var item in items)
    {
      sum += item.Price;
    }

    return sum;
  }

  public string Pay(IPayment method)
  {
    double total = CalculateTotal();

    return method.Pay(total);
  }
}
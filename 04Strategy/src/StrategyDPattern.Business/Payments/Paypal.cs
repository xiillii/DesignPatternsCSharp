public class Paypal : IPayment
{
  public string Pay(double amount) => $"{amount:c2} paid with Paypal";
}
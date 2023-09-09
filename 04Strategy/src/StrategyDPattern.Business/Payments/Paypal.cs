using StrategyDPattern.Business.Interfaces;

namespace StrategyDPattern.Business.Payments;

public class Paypal : IPayment
{
    public string Pay(double amount) => $"{amount:c2} paid with Paypal";
}
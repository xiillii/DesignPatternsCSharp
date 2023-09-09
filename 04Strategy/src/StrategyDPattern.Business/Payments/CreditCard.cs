using StrategyDPattern.Business.Interfaces;

namespace StrategyDPattern.Business.Payments;

class CreditCard : IPayment
{
    public string Pay(double amount) => $"{amount:c2} paid with Credit Card";
}
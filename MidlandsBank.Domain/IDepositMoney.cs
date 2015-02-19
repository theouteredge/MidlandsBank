namespace MidlandsBank.Domain
{
    public interface IDepositMoney
    {
        void Deposit(double amount, string description);
    }
}
namespace MidlandsBank.Domain
{
    public interface IWithdrawMoney
    {
        void Withdraw(double amount, string description);
    }
}
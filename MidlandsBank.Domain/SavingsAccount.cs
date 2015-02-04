using System;

namespace MidlandsBank.Domain
{
    public class SavingsAccount : Account
    {
        public SavingsAccount()
        {
            // for serialisation :/
        }

        public SavingsAccount(int accountNumber, string accountHoldersName, double openingDeposit)
            : base(accountNumber, accountHoldersName, openingDeposit)
        {
            
        }

        public override void Withdraw(double amount, string description)
        {
            if (CurrentBalance() - amount < 0)
                throw new ArgumentException("The account has insufficient funds to complete the Transactions", "amount");
            
            base.Withdraw(amount, description);
        }
    }
}

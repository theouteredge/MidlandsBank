using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MidlandsBank.Domain
{
    public class SavingsAccount : Account, IApplyInterest
    {
        public SavingsAccount()
        {
            // serialisation
            InterestRate = 1;
        }

        public double InterestRate { get; set; }

        public void CalculateAndApplyInterest()
        {
            var interestAquired = CurrentBalance()*(InterestRate/100);
            Transactions.Add(new Transaction(interestAquired, interestAquired, "Interest Applied"));
        }

        public SavingsAccount(int accountNumber, string accountHolderName, double openingDeposit)
            : base(accountNumber, accountHolderName, openingDeposit)
        {
            InterestRate = 1;
        }

        public override void Withdraw(double amount, string description)
        {
            if (CurrentBalance() - amount < 0)
                throw new ArgumentException("Insufficient funds available", "amount");

            base.Withdraw(amount, description);
        }

        
    }
}

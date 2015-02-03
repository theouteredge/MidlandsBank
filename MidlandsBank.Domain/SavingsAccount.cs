using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public class SavingsAccount : Account, IAquireInterest
    {
        public SavingsAccount()
        {
            // for serialisation
            InterestRate = 3;
        }

        public SavingsAccount(int accountNumber, string accountHoldersName, double openingDeposit)
            : base(accountNumber, accountHoldersName, openingDeposit)
        {
        }

        public override void Withdraw(double amount, string description)
        {
            if (CurrentBalance() - amount < 0)
                throw new ArgumentException("Insufficient funds available for this transaction");
            
            base.Withdraw(amount, description);
        }

        public double InterestRate { get; set; }
        public void CalculateAndApplyInterest()
        {
            double interestAquired = CurrentBalance() * (InterestRate/100);
            Transactions.Add(new Transaction(interestAquired, CurrentBalance(), "Monthly Interest Rate"));
        }
    }
}

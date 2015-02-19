using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions.Configuration;

namespace MidlandsBank.Domain
{
    public class CurrentAccount : Account, IApplyInterest
    {
        public double OverdraftLimit { get; set; }

        public CurrentAccount()
        {
            // serialisation
            InterestRate = 0.5;
        }

        public double InterestRate { get; set; }

        public void CalculateAndApplyInterest()
        {
            var interestAquired = CurrentBalance() * (InterestRate / 100);
            Transactions.Add(new Transaction(interestAquired, interestAquired, "Interest Applied"));
        }

        public CurrentAccount(int accountNumber, string accountHolderName, double openingDeposit)
            : base(accountNumber, accountHolderName, openingDeposit)
        {
            OverdraftLimit = 100;
        }

        public override void Withdraw(double amount, string description)
        {
            base.Withdraw(amount, description);

            if (CurrentBalance() < (OverdraftLimit * -1))
                Transactions.Add(new Transaction(NextMonth(), -25, 0, "Unarranged Overdraft Fee"));
        }
    }
}

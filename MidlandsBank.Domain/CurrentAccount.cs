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
    public class CurrentAccount : Account, IAquireInterest
    {
        public double OverdraftLimit { get; set; }


        public CurrentAccount()
        {
            // for serialisation
            InterestRate = 0.75;
        }

        public CurrentAccount(int accountNumber, string accountHoldersName, double openingDeposit)
            : base(accountNumber, accountHoldersName, openingDeposit)
        {
            OverdraftLimit = 100;
            InterestRate = 0.75;
        }

        public override void Withdraw(double amount, string description)
        {
            base.Withdraw(amount, description);

            if (CurrentBalance() < (OverdraftLimit*-1))
                Transactions.Add(new Transaction(NextMonth(), -25, 0, "Unarranged Overdraft Fee"));
        }

        public double InterestRate { get; set; }
        public void CalculateAndApplyInterest()
        {
            double interestAquired = CurrentBalance() * (InterestRate /100);
            Transactions.Add(new Transaction(interestAquired, CurrentBalance(), "Monthly Interest Rate"));
        }
    }
}

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
    public class CurrentAccount
    {
        public double OverdraftLimit { get; set; }

        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public List<Transaction> Transactions { get; set; }


        public CurrentAccount()
        {
            // for serilaisation
        }

        public CurrentAccount(int accountNumber, string accountHolderName, double openingDeposit)
        {
            if (string.IsNullOrEmpty(accountHolderName))
                throw new ArgumentException("You have to specify the account holders name", "accountHolderName");

            if (openingDeposit < 1)
                throw new ArgumentException("Accounts have to be opened with an minimum deposit of £1", "openingDeposit");

            
            Transactions = new List<Transaction>();

            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            OverdraftLimit = 100;

            Deposit(openingDeposit, "Account Opening Deposit");
        }


        public void Deposit(double amount, string description)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount your are depositing should be greater than 0", "amount");

            var transaction = new Transaction(amount, CurrentBalance(), description);
            Transactions.Add(transaction);
        }

        public void Withdraw(double amount, string description)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount your are withdrawing should be greater than 0", "amount");

            var transaction = new Transaction(amount * -1, CurrentBalance(), description);
            Transactions.Add(transaction);

            if (CurrentBalance() < (OverdraftLimit*-1))
                Transactions.Add(new Transaction(NextMonth(), -25, 0, "Unarranged Overdraft Fee"));
        }


        public double CurrentBalance()
        {
            return Transactions.Where(x => x.Date <= DateTime.Now)
                               .Select(x => x.Amount)
                               .Sum();

            // Old Linq
            //var sum = (from trans in Transactions
            //    where trans.Date <= DateTime.Now
            //    select trans.Amount).Sum();

            // old skool
            //double sum = 0;
            //foreach (var trans in Transactions)
            //{
            //    if (trans.Date <= DateTime.Now)
            //        sum += trans.Amount;
            //}
            //return sum;
        }


        private DateTime NextMonth()
        {
            var date = DateTime.Now.AddMonths(1);
            return new DateTime(date.Year, date.Month, 1);
        }

    }
}

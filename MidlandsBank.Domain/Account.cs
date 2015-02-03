using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public List<Transaction> Transactions { get; set; }


        public Account()
        {
            // for serilaisation
        }

        public Account(int accountNumber, string accountHolderName, double openingDeposit)
        {
            if (string.IsNullOrEmpty(accountHolderName))
                throw new ArgumentException("You have to specify the account holders name", "accountHolderName");

            if (openingDeposit < 1)
                throw new ArgumentException("Accounts have to be opened with an minimum deposit of £1", "openingDeposit");

            
            Transactions = new List<Transaction>();

            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;

            Deposit(openingDeposit, "Account Opening Deposit");
        }


        public virtual void Deposit(double amount, string description)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount your are depositing should be greater than 0", "amount");

            var transaction = new Transaction(amount, CurrentBalance(), description);
            Transactions.Add(transaction);
        }

        public virtual void Withdraw(double amount, string description)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount your are withdrawing should be greater than 0", "amount");

            var transaction = new Transaction(amount * -1, CurrentBalance(), description);
            Transactions.Add(transaction);
        }


        public double CurrentBalance()
        {
            return Transactions.Where(x => x.Date <= DateTime.Now)
                               .Select(transaction => transaction.Amount)
                               .Sum();

            // Old Linq
            //var sum = (from trans in Transactions
            //    where trans.Amount > 100
            //    select trans.Amount).Sum();

            // old skool
            //double sum = 0;
            //foreach (var trans in Transactions)
            //{
            //    sum += trans.Amount;
            //}
            //return sum;
        }


        protected DateTime NextMonth()
        {
            var date = DateTime.Now.AddMonths(1);
            return new DateTime(date.Year, date.Month, 1);
        }
    }
}

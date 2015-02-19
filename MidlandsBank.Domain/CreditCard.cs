using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public class CreditCard : IApplyInterest, IDepositMoney, IWithdrawMoney
    {
        public double Limit { get; set; }

        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public List<Transaction> Transactions { get; set; }


        public CreditCard()
        {
            // for serilaisation
            InterestRate = 24;
        }


        public double InterestRate { get; set; }

        public void CalculateAndApplyInterest()
        {
            var interestAquired = CurrentBalance() * (InterestRate / 100);
            Transactions.Add(new Transaction(interestAquired, interestAquired * -1, "Interest Applied"));
        }


        public CreditCard(int accountNumber, string accountHolderName, double limit)
        {
            if (string.IsNullOrEmpty(accountHolderName))
                throw new ArgumentException("You have to specify the account holders name", "accountHolderName");
            
            Transactions = new List<Transaction>();

            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            Limit = limit;
        }


        public void Deposit(double amount, string description)
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


        protected DateTime NextMonth()
        {
            var date = DateTime.Now.AddMonths(1);
            return new DateTime(date.Year, date.Month, 1);
        }
    }
}

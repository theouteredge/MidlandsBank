using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions.Configuration;

namespace MidlandsBank.Domain
{
    public class CurrentAccount
    {
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
            AddTransaction(openingDeposit, "Initial Cash Deposit");
        }


        public void AddTransaction(double ammount, string description)
        {
            var transaction = new Transaction(ammount, description);
            Transactions.Add(transaction);
        }


        public double AccountBalance()
        {
            return Transactions.Select(transaction => transaction.Ammount).Sum();
        }
    }
}

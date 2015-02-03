using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MidlandsBank.Domain
{
    public class Bank
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public List<Account> Accounts { get; set; }


        public Bank()
        {
            Accounts = new List<Account>();
        }


        private int AssignAccountId()
        {
            return Accounts.Count + 1;
        }



        public void OpenCurrentAccount(string accountHolderName, string deposit)
        {
            double openingDeposit;
            if (!double.TryParse(deposit, out openingDeposit))
                throw new ArgumentException("The opening deposit could not be converted into a valid Money value", "deposit");

            var account = new CurrentAccount(AssignAccountId(), accountHolderName, openingDeposit);
            Accounts.Add(account);
        }


        public void OpenSavingsAccount(string accountHolderName, string deposit)
        {
            double openingDeposit;
            if (!double.TryParse(deposit, out openingDeposit))
                throw new ArgumentException("The opening deposit could not be converted into a valid Money value", "deposit");

            var account = new SavingsAccount(AssignAccountId(), accountHolderName, openingDeposit);
            Accounts.Add(account);
        }

        



        public void Deposit(string accountNumber, string amount, string description)
        {
            double deposit;
            if (!double.TryParse(amount, out deposit))
                throw new ArgumentException("The deposit amount can not be converted into a valid Money value", "amount");


            var account = FindAccount(accountNumber);
            account.Deposit(deposit, description);
        }

        public void Withdraw(string accountNumber, string amount, string description)
        {
            double withdrawl;
            if (!double.TryParse(amount, out withdrawl))
                throw new ArgumentException("The deposit amount can not be converted into a valid Money value", "amount");


            var account = FindAccount(accountNumber);
            account.Withdraw(withdrawl, description);
        }


        public IEnumerable<Transaction> GetTransactionsForAccount(string accountNo)
        {
            var account = FindAccount(accountNo);
            return account.Transactions.Where(x => x.Date <= DateTime.Now);
        }

        public IEnumerable<Transaction> GetPendingTransactionsForAccount(string accountNo)
        {
            var account = FindAccount(accountNo);
            return account.Transactions.Where(x => x.Date > DateTime.Now);
        }



        public void MonthlyRun()
        {
            throw new NotImplementedException();
        }

        
        public void TransferMoney(string from, string to, string amount)
        {
            var fromAccount = FindAccount(from);
            var toAccount = FindAccount(to);


            throw new NotImplementedException();
        }

        
        private Account FindAccount(string accountNo)
        {
            int accountNumber;
            if (!int.TryParse(accountNo, out accountNumber))
                throw new ArgumentException("the from account is not a valid account number, it should be an integer", "accountNo");

            var account = Accounts.SingleOrDefault(x => x.AccountNumber == accountNumber);
            if (account == null)
                throw new ArgumentException(string.Format("Could not find an account with the account number of {0}", accountNo), "accountNo");

            return account;
        }
    }
}

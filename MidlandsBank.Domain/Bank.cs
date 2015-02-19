using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MidlandsBank.Domain
{
    public class Bank
    {
        public List<Account> Accounts { get; set; }
        public List<CreditCard> CreditCards { get; set; }


        public Bank()
        {
            Accounts = new List<Account>();
            CreditCards = new List<CreditCard>();
        }


        /// <summary>
        /// Gets an new account number
        /// </summary>
        private int AssignAccountId()
        {
            return Accounts.Count + 1;
        }

        /// <summary>
        /// Gets a new Credit Card Number
        /// </summary>
        private int AssignCreditCardNumber()
        {
            return CreditCards.Count() + 1;
        }

       
        /// <summary>
        /// Safely Converts a String into a Double
        /// </summary>
        private double ConvertStringToDouble(string str, string errorMessage)
        {
            double result;
            if (!double.TryParse(str, out result))
                throw new ArgumentException(errorMessage, "str");

            return result;
        }


        /// <summary>
        /// Opens a new Current Account
        /// </summary>
        public void OpenCurrentAccount(string accountHolderName, string deposit)
        {
            var openingDeposit = ConvertStringToDouble(deposit, "Cannot convert the deposit ammount into a valid Money value");

            var account = new CurrentAccount(AssignAccountId(), accountHolderName, openingDeposit);
            Accounts.Add(account);
        }


        /// <summary>
        /// Opens a new Savings Account
        /// </summary>
        public void OpenSavingsAccount(string accountHolderName, string deposit)
        {
            var openingDeposit = ConvertStringToDouble(deposit, "The opening deposit could not be converted into a valid Money value");

            var account = new SavingsAccount(AssignAccountId(), accountHolderName, openingDeposit);
            Accounts.Add(account);
        }


        /// <summary>
        /// Opens a new Credit Card account if the the person passes the Credit Check
        /// </summary>
        public void ApplyCreditCard(string accountHolderName, string cardLimit)
        {
            var cardLimitInt = ConvertStringToDouble(cardLimit, "The Card Limit can not be converted into a valid Money value");

            if (!CreditCheckService.IsWorthy(cardLimitInt, accountHolderName, Accounts))
                throw new Exception("You do not quality for this credit card");

            var creditcard = new CreditCard(AssignCreditCardNumber(), accountHolderName, cardLimitInt);
            CreditCards.Add(creditcard);
        }

        

        /// <summary>
        /// Deposits money into a specified account
        /// </summary>
        public void Deposit(string accountNumber, string amount, string description)
        {
            var deposit = ConvertStringToDouble(amount, "The deposit amount can not be converted into a valid Money value");
            var account = FindAccount(accountNumber);

            account.Deposit(deposit, description);
        }


        /// <summary>
        /// Withdraws money into a specified account
        /// </summary>
        public void Withdraw(string accountNumber, string amount, string description)
        {
            var withdrawl = ConvertStringToDouble(amount, "The deposit amount can not be converted into a valid Money value");
            var account = FindAccount(accountNumber);
            
            account.Withdraw(withdrawl, description);
        }


        /// <summary>
        /// gets the all the historic transactions from the specified account
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public IEnumerable<Transaction> GetTransactionsForAccount(string accountNo)
        {
            var account = FindAccount(accountNo);
            return account.Transactions.Where(x => x.Date <= DateTime.Now);
        }

        /// <summary>
        /// gets all the pending transactions from the specified account (if any)
        /// </summary>
        public IEnumerable<Transaction> GetPendingTransactionsForAccount(string accountNo)
        {
            var account = FindAccount(accountNo);
            return account.Transactions.Where(x => x.Date > DateTime.Now);
        }


        /// <summary>
        /// runs all the monthly maintenance jobs required for the bank to operate
        /// </summary>
        public void MonthlyRun()
        {
            var list = new List<IApplyInterest>();
            list.AddRange(Accounts.Select(x => x as IApplyInterest));
            list.AddRange(CreditCards);

            CalculateInterestRates(list);
        }

        /// <summary>
        /// Calls the CalculateAndApplyInterest for all the accounts passed in
        /// </summary>
        /// <param name="accounts"></param>
        private void CalculateInterestRates(List<IApplyInterest> accounts)
        {
            foreach(var acc in accounts)
                acc.CalculateAndApplyInterest();
        }

        /// <summary>
        /// Transfers Money from One account to another
        /// </summary>
        public void TransferMoney(string from, string to, string amount)
        {
            var withdrawl = ConvertStringToDouble(amount, "The deposit amount can not be converted into a valid Money value");
            var fromAccount = FindAccount(from);
            var toAccount = FindAccount(to);

            MoneyTransferService.TransferMoney(toAccount, fromAccount, withdrawl);
        }


        public Account FindAccount(string accountNo)
        {
            int accountNumber;
            if (!int.TryParse(accountNo, out accountNumber))
                throw new ArgumentException("The Account # is not a valid account number, it should be an integer", "accountNo");

            return FindAccount(x => x.AccountNumber == accountNumber);
        }

        /// <summary>
        /// Allows you to search for a Bank Account
        /// </summary>
        public Account FindAccount(Func<Account, bool> predicate)
        {
            foreach (var account in Accounts)
                if (predicate(account))
                    return account;

            return null;
        }


        public CreditCard FindCreditCard(string accountNo)
        {
            int accountNumber;
            if (!int.TryParse(accountNo, out accountNumber))
                throw new ArgumentException("The Account # is not a valid account number, it should be an integer", "accountNo");

            return FindCreditCard(x => x.AccountNumber == accountNumber);
        }


        /// <summary>
        /// Allows you to search for a Credit Card
        /// </summary>
        public CreditCard FindCreditCard(Func<CreditCard, bool> predicate)
        {
            foreach (var card in CreditCards)
                if (predicate(card))
                    return card;

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cmdR;
using MidlandsBank.Domain;
using MidlandsBank.Persistance;

namespace MidlandsBank
{
    public class AccountHandler : ICmdRModule
    {
        private readonly Bank _bank;
        private readonly BankRepository _repository;


        public AccountHandler(CmdR cmdR)
        {
            _repository = new BankRepository();
            _bank = _repository.Get();
        }



        [CmdRoute("save", "Saves the changes made to the bank class", true)]
        public void Save(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _repository.Set(_bank);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to save your bank, we've experiencing a credit crunch, ask the government for a bailout: " + ex.Message);
            }
        }


        [CmdRoute("list", "Lists all the accounts which Midland Bank Holds", true)]
        public void ListAccounts(IDictionary<string, string> param, CmdR cmdR)
        {
            cmdR.Console.WriteLine("#        Type                  Holder                    Balance");
            cmdR.Console.WriteLine("-------  --------------------  --------------------  -----------");

            foreach (var account in _bank.Accounts)
            {

                cmdR.Console.WriteLine("{0}  {1}  {2}  {3}", 
                    account.AccountNumber.ToString().PadLeft(7, '0'),
                    account.GetType().Name.PadRight(20), 
                    account.AccountHolderName.PadRight(20), 
                    account.CurrentBalance().ToString("£0,0.00").PadLeft(11));
            }

            cmdR.Console.WriteLine("");

            foreach(var card in _bank.CreditCards)
                cmdR.Console.WriteLine("{0}  {1}  {2}  {3}",
                    card.AccountNumber.ToString().PadLeft(7, '0'),
                    card.GetType().Name.PadRight(20),
                    card.AccountHolderName.PadRight(20),
                    card.CurrentBalance().ToString("£0,0.00").PadLeft(11));

            cmdR.Console.WriteLine("");
        }


        [CmdRoute("statement account", "Lists all the transactions have happened on an account", true)]
        public void Statement(IDictionary<string, string> param, CmdR cmdR)
        {
            DrawTransactions(_bank.GetTransactionsForAccount(param["account"]), cmdR);
        }

        [CmdRoute("pending account", "Lists all the transactions have happened on an account", true)]
        public void PendingTransactions(IDictionary<string, string> param, CmdR cmdR)
        {
            DrawTransactions(_bank.GetPendingTransactionsForAccount(param["account"]), cmdR);
        }

        [CmdRoute("card-statement account", "Lists all the transactions have happened on an credit card", true)]
        public void CardTransactions(IDictionary<string, string> param, CmdR cmdR)
        {
            int accountNo;
            if (!int.TryParse(param["account"], out accountNo))
                throw new Exception("Unable to convert the account number into an Int");

            var card = _bank.CreditCards.FirstOrDefault(x => x.AccountNumber == accountNo);
            if (card == null)
                throw new Exception("Invalid card account number");

            DrawTransactions(card.Transactions, cmdR);
        }


        private void DrawTransactions(IEnumerable<Transaction> transactions, CmdR cmdR)
        {
            cmdR.Console.WriteLine("Date        Description                      Amount      Balance");
            cmdR.Console.WriteLine("----------  --------------------------  -----------  -----------");

            foreach (var transaction in transactions)
            {

                cmdR.Console.WriteLine("{0}  {1}  {2}  {3}",
                    transaction.Date.ToString("d"),
                    transaction.Description.PadRight(26),
                    transaction.Amount.ToString("c").PadLeft(11),
                    transaction.Balance.ToString("c").PadLeft(11));
            }

            cmdR.Console.WriteLine("");
        }


        [CmdRoute("open-account name balance", "Creates a new Current Account", true)]
        public void OpenCurrentAccount(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.OpenCurrentAccount(param["name"], param["balance"]);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to add your new current account, the following error was raised: " + ex.Message);
            }
        }

        [CmdRoute("open-savings name balance", "Creates a new Savings Account", true)]
        public void OpenSavingsAccount(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.OpenSavingsAccount(param["name"], param["balance"]);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to add your new savings account, the following error was raised: " + ex.Message);
            }
        }


        [CmdRoute("apply-creditcard name limit", "Creates a new Credit Card", true)]
        public void ApplyForCreditCard(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.ApplyCreditCard(param["name"], param["limit"]);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to add your new savings account, the following error was raised: " + ex.Message);
            }
        }




        [CmdRoute("deposit account amount description", "Deposits Money into specified Account", true)]
        public void Deposit(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.Deposit(param["account"], param["amount"], param["description"]);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to deposit any money into your account, the following error was raised: " + ex.Message);
            }
        }

        [CmdRoute("withdraw account amount description", "Withdraws Money Froms a specified Account", true)]
        public void Withdraw(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.Withdraw(param["account"], param["amount"], param["description"]);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to withdraw money from your account, the following error was raised: " + ex.Message);
            }
        }





        [CmdRoute("monthly-run", "Performs the Monthly", true)]
        public void MonthlyRun(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.MonthlyRun();
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to add your new bank account, the following error was raised: " + ex.Message);
            }
        }




        [CmdRoute("transfer fromAccount toAccount amount", "Transfers Money From One Account to Another", true)]
        public void MoneyTransfer(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.TransferMoney(param["fromAccount"], param["toAccount"], param["amount"]);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to complete the Money Transfer, the following error was raised: " + ex.Message);
            }
        }
    }
}

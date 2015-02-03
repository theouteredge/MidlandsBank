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


        [CmdRoute("open-account name balance", "Creates a new Current Account", true)]
        public void OpenCurrentAccount(IDictionary<string, string> param, CmdR cmdR)
        {
            try
            {
                _bank.OpenCurrentAccount(param["name"], param["balance"]);
            }
            catch (Exception ex)
            {
                cmdR.Console.WriteLine("Unable to add your new bank account, the following error was raised: " + ex.Message);
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
                    account.AccountBalance().ToString("£0.00").PadLeft(11));
            }
        }
    }
}

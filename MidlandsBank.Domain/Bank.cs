using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public class Bank
    {
        public List<CurrentAccount> Accounts { get; set; }


        public Bank()
        {
            Accounts = new List<CurrentAccount>();
        }


        public void OpenCurrentAccount(string accountHolderName, string deposit)
        {
            double openingDeposit;
            if (!double.TryParse(deposit, out openingDeposit))
                throw new ArgumentException("The opening deposit could not be converted into a valid Money value", "deposit");

            var account = new CurrentAccount(AssignAccountId(), accountHolderName, openingDeposit);
            Accounts.Add(account);
        }

        private int AssignAccountId()
        {
            return Accounts.Count + 1;
        }
    }
}

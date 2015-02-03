using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public static class MoneyTransferService
    {
        public static void TransferMoney(Account fromAccount, Account toAccount, double amount)
        {
            // start TRANSACTION
            
            fromAccount.Withdraw(amount, string.Format("Transfer To {0}", toAccount.AccountNumber.ToString().PadLeft(6, '0')));
            toAccount.Deposit(amount, string.Format("Transfer From {0}", fromAccount.AccountNumber.ToString().PadLeft(6, '0')));
            
            //commit TRANSACTION
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public static class MoneyTransferService
    {
        public static void TransferMoney(IDepositMoney to, IWithdrawMoney from, double amount)
        {
            from.Withdraw(amount, "Money Transfer");
            to.Deposit(amount, "Money Transfer");
        }
    }
}

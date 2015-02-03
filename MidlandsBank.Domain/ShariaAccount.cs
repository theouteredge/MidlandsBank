using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public class ShariaAccount : Account
    {
        public ShariaAccount()
        {
            // for serialisation
        }

        public ShariaAccount(int accountNumber, string accountHoldersName, double openingDeposit)
            : base(accountNumber, accountHoldersName, openingDeposit)
        {
           
        }
    }
}

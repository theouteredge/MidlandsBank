using System.Collections.Generic;
using System.Linq;

namespace MidlandsBank.Domain
{
    public static class CreditCheckService
    {
        /// <summary>
        /// checks to see if you are eligable to have a credit card
        /// </summary>
        public static bool IsWorthy(double cardLimitInt, string accountHolderName, List<Account> accounts)
        {
            var account = accounts.FirstOrDefault(x => x.AccountHolderName == accountHolderName);
            if (account == null)
            {
                // new customers who dont hold and account can only have a limit of £1000 or less
                return cardLimitInt < 1000;
            }

            // customers can have a card limit less 3000
            return cardLimitInt < 3000;
        }
    }
}
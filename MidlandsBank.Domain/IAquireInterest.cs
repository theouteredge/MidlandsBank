using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    interface IAquireInterest
    {
        double InterestRate { get; set; }

        void CalculateAndApplyInterest();
    }
}

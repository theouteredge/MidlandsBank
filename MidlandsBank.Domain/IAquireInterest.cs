using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidlandsBank.Domain
{
    public interface IAquireInterest
    {
        double InterestRate { get; set; }
        void CalculateAndApplyInterest();
    }
}

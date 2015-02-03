using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cmdR;

namespace MidlandsBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmdR = new CmdR("> ", new [] { "exit" });

            cmdR.Console.WriteLine("Welcome to Midlands Bank Plc");
            cmdR.Console.WriteLine("----------------------------");
            cmdR.Console.WriteLine("type ? for help on available commands\n");

            cmdR.AutoRegisterCommands();
            cmdR.Run(args);
        }
    }
}

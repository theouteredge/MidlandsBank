using System;
using System.Security.Cryptography;

namespace MidlandsBank.Domain
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        
        public double Amount { get; set; }
        public double Balance { get; set; }

        public string Description { get; set; }
        

        public Transaction()
        {
            // for serialisation :/
        }

        public Transaction(double amount, double balance, string description)
            : this(DateTime.Now, amount, balance, description)
        {
        }

        public Transaction(DateTime date, double amount, double balance, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("You have to specify a description for your account", "description");

            if (amount.Equals(0))
                throw new ArgumentException("Your transaction cannot be 0 it must be a number greater than or less than 0", "amount");

            Date = date;
            
            Amount = amount;
            Balance = balance + amount;

            Description = description;
        }
    }
}
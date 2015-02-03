using System;

namespace MidlandsBank.Domain
{
    public class Transaction
    {
        public double Ammount { get; set; }
        public string Description { get; set; }

        public Transaction(double ammount, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("You have to specify a description for your account", "description");

            Ammount = ammount;
            Description = description;
        }
    }
}
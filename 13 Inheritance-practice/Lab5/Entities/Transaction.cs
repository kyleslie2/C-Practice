using System;
namespace Lab5.Entities
{
    public class Transaction
    {

        public double Amount;                             
        public TransactionType Type;
        public DateTime TransactionDate;



        public Transaction(double amount, TransactionType type)
        {
            Amount = amount;
            Type = type;
            TransactionDate = DateTime.Now;
        }

    }
}

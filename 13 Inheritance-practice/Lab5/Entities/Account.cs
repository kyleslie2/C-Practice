using System;
using System.Collections.Generic;

namespace Lab5.Entities
{
    public class Account
    {
        public Customer Owner;
        public double Balance;
        public List<Transaction> TransactionHistory = new List<Transaction>();



        public Account(Customer owner)
        {
            Owner = owner;

        }

        public virtual TransactionResult Deposit(Transaction amount)
        {
            
            Balance = Balance + amount.Amount;

            TransactionHistory.Add(amount);
            return TransactionResult.SUCCESS;

        }
        public virtual TransactionResult Withdraw(Transaction amount)
        {

            if (Balance >= amount.Amount)
            {
                Balance = Balance - amount.Amount;
                TransactionHistory.Add(amount);
                return TransactionResult.SUCCESS;
            }

            else{
                
                return TransactionResult.INSUFFICIENT_FUND;
            }
         

        }





    }
}

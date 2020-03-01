using System;
namespace Lab5.Entities
{
    public class CheckingAccount : Account
    {
        static double MaxWithdrawAmount = 300.0;


        public CheckingAccount(Customer name)
            :base(name)
        {
            
        }

        public override TransactionResult Deposit(Transaction amount)
        {
            Balance = Balance + amount.Amount;
            TransactionHistory.Add(amount);
            return TransactionResult.SUCCESS;
        }


        public override TransactionResult Withdraw(Transaction amount)
        {
            if (Owner.Status == CustomerStatus.REGULAR)
            {
                if (amount.Amount > MaxWithdrawAmount)
                {
                    return TransactionResult.EXCEED_MAX_WITHDRAW_AMOUNT;
                }
                else
                {
                    return base.Withdraw(amount);
                
                }
                   
            }
            else
            {
                return base.Withdraw(amount);
             }

        }

    }
}

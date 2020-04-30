using System;
namespace Lab5.Entities
{
    public class SavingAccount : Account
    {
        public static double PremiereAmount = 2000.0;
        public static double WithdrawPenaltyAmount = 10.0;



        public SavingAccount(Customer name)
            : base(name)
        {


            if (Balance >= PremiereAmount)
            {
                Owner.Status = CustomerStatus.PREMIER;
            }
            else
            {
                Owner.Status = CustomerStatus.REGULAR;
            }
        }



        public override TransactionResult Deposit(Transaction amount)
        {
            Balance = Balance + amount.Amount;
            TransactionHistory.Add(amount);

            if (Balance >= PremiereAmount)
            {
                Owner.Status = CustomerStatus.PREMIER;
                return TransactionResult.SUCCESS;
            }
            else
            {
                Owner.Status = CustomerStatus.REGULAR;
                return TransactionResult.SUCCESS;

            }
        }



        public override TransactionResult Withdraw(Transaction amount)
        {

            if (amount.Amount < Balance)
            {
                if (Balance < PremiereAmount)
                {
                    Balance = Balance - WithdrawPenaltyAmount;
                    TransactionHistory.Add(new Transaction(WithdrawPenaltyAmount, TransactionType.PENALTY));
                    return base.Withdraw(amount);

                }
                else
                {
                    return base.Withdraw(amount);
                
                }
            }

            else
            {
                return TransactionResult.INSUFFICIENT_FUND;
            }


        }

    }

}
    


using System;

namespace Lab4
{
    class SavingAccount
    {
        public string Owner;
        public int AccountNumber;
        public double Balance;
        public double MonthlyDepositAmount;


        // this class needs to take arguments for it to automatically fill
        public SavingAccount(string owner, double balance, double monthlyDeposit)
        {
            //make random number starting with 9xxxxx for account number
            Random rnd = new Random();
            AccountNumber = rnd.Next(90000, 99999);

            Owner = owner;
            Balance = balance;
            MonthlyDepositAmount = monthlyDeposit;
            //this.balance = balance;
        }

        //static properties
        public static double MonthlyFee = 4.0;
        public static double MonthlyInterestRate = 0.0025;
        public static double MinimumMonthBalance = 1000;
        public static double MinimumMonthDeposit = 50;



        //Methods

        //Deposit method
        public double classDEPOSIT(double monthlyDeposit)
        {
            // takes double as parameter to inrease the Balance
            Balance = Balance + monthlyDeposit;

            return Balance;

        }

        //Withdrawl method
        public double classWITHDRAW(double MonthlyFee)
        {
            Balance = Balance - MonthlyFee;
            return Balance;
        }


        //Update monthly method
        public void classUPDATE(int months)
        {
            for (int x = 0; x < months; x++)
            {
                classWITHDRAW(MonthlyFee);
                classDEPOSIT(Balance * MonthlyInterestRate);
                classDEPOSIT(MonthlyDepositAmount);
            }
        }

    }

}


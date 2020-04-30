using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //List that will contain the objects created by the class
            List<SavingAccount> BankList = new List<SavingAccount>();


            //while loop until empty string is entered
            while (true)
            {
                Console.WriteLine("\nEnter a Customer Name: ");
                string owner = Console.ReadLine();


                if (owner == "")
                {
                    //add months variable for calculations
                    Console.WriteLine("Input the number of months you wish to calculate: ");
                    int months = int.Parse(Console.ReadLine());
                    //int months = 6;


                    foreach (SavingAccount i in BankList)
                    {
                        i.classUPDATE(months);

                        //Summary message for each account (object)
                        Console.Write("\nAfter " + months + " month(s), " + i.Owner + "s account (#" + i.AccountNumber+"), has a balance of: $" + i.Balance);

                    }

                    //exit system
                    Console.WriteLine("\n\nPress Enter to Complete");
                    string exit = Console.ReadLine();
                    if (exit == "")
                    {
                        Console.WriteLine("\nGoodbye!");
                        Environment.Exit(0);
                    }
                    //break;
                   
                }
                else
                {
                    //add starting balance for account
                    Console.WriteLine("\nEnter the Starting Balance Amount for this Account (minimum $1000): ");
                    double balance = double.Parse(Console.ReadLine());


                    //add monthly deposit info to object
                    Console.WriteLine("\nEnter the amount you will deposit monthly (must be a minimum of $50): ");
                    double monthlyDeposit = double.Parse(Console.ReadLine());


                    //making a new object using the SavingAccount class
                    SavingAccount newAccount = new SavingAccount(owner, balance, monthlyDeposit);


                    //adding the new object to the list array of objects
                    BankList.Add(newAccount);

                 }

            }

        }
    }
}

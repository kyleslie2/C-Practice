using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab5
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            List<Entities.Customer> CustomerList = new List<Entities.Customer>();                           //create list of accounts 
            while (true)
            {
                Console.WriteLine("\nEnter a Customer Name: ");
                string name = Console.ReadLine();
                if (name != "")
                {
                    Console.WriteLine("\nEnter the Starting Balance Amount for this the Savings Account: ");     //add starting balance for SavingsAccount
                    try
                    {
                        double initialDeposit = double.Parse(Console.ReadLine());
                        if (initialDeposit > 0)
                        {
                            Entities.Customer newCustomer = new Entities.Customer(name);              //making a new object using the Account class
                            Entities.Transaction newTransaction = new Entities.Transaction(initialDeposit, Entities.TransactionType.DEPOSIT);
                            newCustomer.Saving.Deposit(newTransaction);
                            CustomerList.Add(newCustomer);                                             //adding the new object to the list array of objects
                        }
                        else
                        {
                            Console.WriteLine("That is an invalid input!! Try again!");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("That is an invalid input!! Try again!");
                    }
                }
                else
                {
                    Console.WriteLine("Here are all the Customers and their Status. \nSelect one of the following customers: \n");
                    var index = 1;
                   
                    foreach (Entities.Customer i in CustomerList)
                    {
                        Console.Write("\n" + index++ + ". Customer name: " + i.Name + ", current status: " + i.Saving.Owner.Status + "\n");
                    }
                    Console.WriteLine("\nEnter your selection, 1 to " + (index - 1) + ": ");
                    int choice = int.Parse(Console.ReadLine());

                    if (choice >= 1 && choice <= (index - 1))
                    {
                        var index2 = 0;
                        foreach (Entities.Customer i in CustomerList)
                        {
                            index2++;
                            while (index2 == choice)
                            {
                                Console.WriteLine("\nWelcome " + i.Name + "! You are currently a " + i.Status + " customer.");
                                while (true)
                                {
                                    Console.WriteLine("\n\n############################################################################\nSelect one of the following activities: \n\n1. Deposit ... \n2. Withdraw ...\n3. Transfer ...\n4. Balance Enquiry ...\n5. Account Activity Enquiry ...\n6. Exit ...\n\nEnter your selection (1 to 6): \n");
                                    int choice2 = int.Parse(Console.ReadLine());
                                    if (choice2 >= 1 && choice2 <= 6)
                                    {
                                        //DEPOSIT
                                        if (choice2 == 1)
                                        {
                                            Console.WriteLine("\nSelect account (1 - Checking Account, 2 - Saving Account): ");
                                            int x = int.Parse(Console.ReadLine());
                                            try
                                            {
                                                if (x >= 1 && x <= 2)
                                                {
                                                    //Deposit to CHECKING ACCOUNT
                                                    if (x == 1)
                                                    {
                                                        Console.Write("\nEnter Amount: ");
                                                        try
                                                        {
                                                            double amount = double.Parse(Console.ReadLine());
                                                            if (amount > 0)
                                                            {
                                                                i.Checking.Deposit(new Entities.Transaction(amount, Entities.TransactionType.DEPOSIT));
                                                                Console.WriteLine("\nDeposit complete, current account balance: $" + i.Checking.Balance);
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("That is an invalid input!! Try again!");
                                                            }
                                                        }
                                                        catch (FormatException)
                                                        {
                                                            Console.WriteLine("That is an invalid input!! Try again!");
                                                        }
                                                    }
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("That is an invalid input!! Try again!");
                                            }

                                            //Deposit to SAVING ACCOUNT
                                            if (x == 2)
                                            {
                                                Console.Write("\nEnter Amount: ");
                                                try
                                                {
                                                    double amount = double.Parse(Console.ReadLine());
                                                    if (amount > 0)
                                                    {
                                                        i.Saving.Deposit(new Entities.Transaction(amount, Entities.TransactionType.DEPOSIT));
                                                        Console.WriteLine("\nDeposit complete, current account balance: $" + i.Saving.Balance);
                                                        Console.WriteLine("Customer status is currently: " + i.Status);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("That is an invalid input!! Try again!");
                                                    }
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("That is an invalid input!! Try again!");
                                                }
                                            }

                                            else
                                            {
                                                Console.WriteLine("Input Invalid, please try again!!!!!");
                                            }
                                        }


                                        //WITHDRAW
                                        if (choice2 == 2)
                                        {
                                            Console.WriteLine("\nSelect account (1 - Checking Account, 2 - Saving Account): ");
                                            int x = int.Parse(Console.ReadLine());
                                            if (x >= 1 && x <= 2)
                                            {
                                                //Withdraw from CHECKING ACCOUNT
                                                if (x == 1)
                                                {
                                                    Console.WriteLine(":::::::::::::::::::::::REMINDER:::::::::::::::::::::::::::: \nREGULAR status customers may only take out $300 from their account. \nNo limit for PREMIERE customers.");
                                                    Console.Write("\nEnter Amount: ");
                                                    try
                                                    {
                                                        double amount = double.Parse(Console.ReadLine());
                                                        if (amount > 0)
                                                        {
                                                            Entities.Transaction newTransaction = new Entities.Transaction(amount, Entities.TransactionType.WITHDRAW);
                                                            Entities.TransactionResult r;

                                                            r = i.Checking.Withdraw(newTransaction);

                                                            if (r == Entities.TransactionResult.SUCCESS)
                                                            {
                                                                Console.WriteLine("\nWithdraw successful! Current account balance: $" + i.Checking.Balance);
                                                            }
                                                            if (r == Entities.TransactionResult.INSUFFICIENT_FUND)
                                                            {
                                                                Console.WriteLine("\nINSUFFICENT FUNDS");
                                                            }
                                                            if (r == Entities.TransactionResult.EXCEED_MAX_WITHDRAW_AMOUNT)
                                                            {
                                                                Console.WriteLine("\nYou are a REGULAR status customer and you have EXCEEDED MAX WITHDRAW AMOUNT($300)");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("That is an invalid input!! Try again");
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("That is an invalid input!! Try again");
                                                    }
                                                }

                                                //Withdraw from SAVING ACCOUNT
                                                if (x == 2)
                                                {
                                                    Console.WriteLine(":::::::::::::::::::::::REMINDER::::::::::::::::::::::::::::\nThere is a $10 Withdraw Penalty for all REGULAR customer. \nNo penalty for PREMIERE customers.");
                                                    Console.Write("\nEnter Amount: ");
                                                    try
                                                    {
                                                        double amount = double.Parse(Console.ReadLine());
                                                        Entities.Transaction newTransaction = new Entities.Transaction(amount, Entities.TransactionType.WITHDRAW);
                                                        Entities.TransactionResult r;
                                                        r = i.Saving.Withdraw(newTransaction);

                                                        if (r == Entities.TransactionResult.SUCCESS)
                                                        {
                                                            Console.WriteLine("\nWithdraw successful! Current account balance: $" + i.Saving.Balance);
                                                        }
                                                        if (r == Entities.TransactionResult.INSUFFICIENT_FUND)
                                                        {
                                                            Console.WriteLine("\nINSUFFICENT FUNDS");
                                                        }
                                                        if (r == Entities.TransactionResult.EXCEED_MAX_WITHDRAW_AMOUNT)
                                                        {
                                                            Console.WriteLine("\nYou are a REGULAR status customer and you have EXCEEDED MAX WITHDRAW AMOUNT($300)");
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("That is an invalid input!! Try again :)");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Input, Try again.");
                                            }
                                        }

                                        //TRANSFERS
                                        if (choice2 == 3)
                                        {
                                            Console.WriteLine("\nSelect accounts (1 - From Checking to Saving; 2 - From Saving to Checking): ");
                                            int x = int.Parse(Console.ReadLine());
                                            if (x >= 1 && x <= 2)
                                            {
                                                //Transfer from CHECKING ACCOUNT to SAVING ACCOUNT
                                                if (x == 1)
                                                {
                                                    Console.WriteLine("\nEnter Amount: $");
                                                    try
                                                    {
                                                        double amount = double.Parse(Console.ReadLine());
                                                        if (amount > 0)
                                                        {
                                                            Entities.Transaction newTransaction = new Entities.Transaction(amount, Entities.TransactionType.TRANSFER_OUT);
                                                            Entities.TransactionResult rOUT;
                                                            rOUT = i.Checking.Withdraw(newTransaction);

                                                            if (rOUT == Entities.TransactionResult.SUCCESS)
                                                            {
                                                                Entities.Transaction newTransaction2 = new Entities.Transaction(amount, Entities.TransactionType.TRANSFER_IN);
                                                                Entities.TransactionResult rIN;
                                                                rIN = i.Saving.Deposit(newTransaction2);

                                                                if (rIN == Entities.TransactionResult.SUCCESS)
                                                                {
                                                                Console.WriteLine("\nTransfer successful! \nCurrent Checking account balance: $" + i.Checking.Balance + "\nCurrent Saving Account Balance: $" + i.Saving.Balance);
                                                                }
                                                            }
                                                            if (rOUT == Entities.TransactionResult.INSUFFICIENT_FUND) 
                                                            {
                                                                Console.WriteLine("\nINSUFFICENT FUNDS");
                                                            }
                                                            if (rOUT == Entities.TransactionResult.EXCEED_MAX_WITHDRAW_AMOUNT)
                                                            {
                                                                Console.WriteLine("\nYou are a REGULAR status customer and you have EXCEEDED MAX WITHDRAW AMOUNT($300)");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("That is an invalid input!! Try again");
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("That is an invalid input!! Try again");
                                                    }
                                                }

                                                //Transfer from SAVING ACCOUNT to CHECKING ACCOUNT
                                                if (x == 2)
                                                {
                                                    Console.WriteLine("\nEnter Amount: $");
                                                    try
                                                    {
                                                        double amount = double.Parse(Console.ReadLine());
                                                        if (amount > 0)
                                                        {
                                                            Entities.Transaction newTransaction = new Entities.Transaction(amount, Entities.TransactionType.TRANSFER_OUT);
                                                            Entities.TransactionResult rOUT;
                                                            rOUT = i.Saving.Withdraw(newTransaction);

                                                            if (rOUT == Entities.TransactionResult.SUCCESS)
                                                            {
                                                                Entities.Transaction newTransaction2 = new Entities.Transaction(amount, Entities.TransactionType.TRANSFER_IN);
                                                                Entities.TransactionResult rIN;
                                                                rIN = i.Checking.Deposit(newTransaction2);

                                                                if (rIN == Entities.TransactionResult.SUCCESS)
                                                                {
                                                                    Console.WriteLine("\nTransfer successful! \nCurrent Saving account balance: $" + i.Saving.Balance + "\nCurrent Checking Account Balance: $" + i.Checking.Balance);
                                                                }
                                                            }
                                                            if (rOUT == Entities.TransactionResult.INSUFFICIENT_FUND)
                                                            {
                                                                Console.WriteLine("\nINSUFFICENT FUNDS");
                                                            }
                                                            if (rOUT == Entities.TransactionResult.EXCEED_MAX_WITHDRAW_AMOUNT)
                                                            {
                                                                Console.WriteLine("\nYou are a REGULAR status customer and you have EXCEEDED MAX WITHDRAW AMOUNT($300)");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("That is an invalid input!! Try again");
                                                        }
                                                    }
                                                    catch (FormatException)
                                                    {
                                                        Console.WriteLine("That is an invalid input!! Try again");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Input. Please try again");
                                            }
                                        }

                                        //BANK ENQUIRY
                                        if (choice2 == 4)
                                        {
                                            Console.WriteLine("\nHere are the balances for both accounts. \nAccount:                     Balance:\n_________                    _________\n\n" + "Checking:                    $" + i.Checking.Balance + "\nSaving:                      $" + i.Saving.Balance);
                                        }

                                        //BALANCE INQUIRY
                                        if (choice2 == 5)
                                        {
                                            Console.WriteLine("\tChecking Account:");
                                            Console.WriteLine("\n\tAmount\t\tDate\t\tActivity\n\t_____\t\t____\t\t________\n");
                                         
                                            foreach (Entities.Transaction e in i.Checking.TransactionHistory)
                                            {
                                                Console.WriteLine("\t" + e.Amount + "\t" + e.TransactionDate + "\t" + e.Type);
                                            }
                                            Console.WriteLine("\nSaving Account:");
                                            Console.WriteLine("\n\tAmount\t\tDate\t\tActivity\n\t_____\t\t____\t\t________\n");
                                            foreach (Entities.Transaction e in i.Saving.TransactionHistory)
                                            {
                                                Console.WriteLine("\t" + e.Amount + "\t" + e.TransactionDate + "\t" + e.Type);
                                            }
                                        }

                                        //EXIT
                                        if (choice2 == 6)
                                        {
                                            Console.WriteLine("\n\nExiting Program. Press Enter to Complete");
                                            string exit = Console.ReadLine();
                                            if (exit == "")
                                            {
                                                Console.WriteLine("\nGoodbye!");
                                                Environment.Exit(0);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Input Invalid, please try again");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input Invalid, please try again");
                    }
                }
            }
        }
    }
}
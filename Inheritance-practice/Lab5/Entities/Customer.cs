using System;
namespace Lab5.Entities
{
    public class Customer
    {

        public string Name;
        public CustomerStatus Status;
        public CheckingAccount Checking;
        public SavingAccount Saving;


        public Customer(string name)  
        {
            Name = name;

            Saving = new SavingAccount(this);
            Checking = new CheckingAccount(this);
                       
        }
    }
}

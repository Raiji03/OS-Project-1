using System;
using System.Diagnostics;
using System.Threading; //Phase 2 | Synchronization validation

class bankAccount
{
    private int accountBalance = 20000; //default account balance
    private object lockObj = new object(); //locks safety for synchronization
    
    public void Deposit(int amount)
    {
        lock (lockObj) //keeps the thread safe
        {
            int depositAmount = (int)amount;
            accountBalance += depositAmount; //add money to the customer account
            Console.WriteLine("Deposited: " +amount+ " Remaining balance: "+ accountBalance);
            Thread.Sleep(200); //adding an intentional delay to test the synchornization
        }
    }
    public void Withdraw(int amount)
    {
        lock (lockObj)
        {
            if (accountBalance < amount) //if customers balance is less than the withdraw amount the transaction doesn't go through
            {
                Console.WriteLine("You don't have enough money to withdraw");
            }
            else //else if the customer balance is enough then they can withdraw money.
            {
                Thread.Sleep(200); //testing synchronization
                accountBalance -= amount;
                Console.WriteLine("Withdrawn: "+amount+" Remaining balance: " + accountBalance);
            }
            
        }
    }

}

class Phase2
{
    static void Main(string[] args)
    {
        bankAccount customerAccount = new bankAccount(); //creates the object
        Thread[] customers = new Thread[8]; //creation of the customer threads 
        
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i] = new Thread(() =>
            {
                //starts creating separate customers for transactions
                for (int j = 0; j < 10; j++)
                {
                    customerAccount.Withdraw(9700);
                    customerAccount.Deposit(6500);
                }
            });
            customers[i].Start(); // Start all threads first
        }

        // Wait for all threads to finish execution
        foreach (var customer in customers)
        {
            customer.Join();
        }

        Console.WriteLine("Teller: All transactions are complete."); // Indicates that all threads are complete
    }
    
}
////Adding the lock prevents the race condition from occuring making the threads more safer.
/// The program runs slower.
//Testing under the delayed conditions that the synchronization still operates as normal.

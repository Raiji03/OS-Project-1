using System;
using System.Diagnostics;
using System.Threading; //Phase 2


class Phase2
{
    static int accountBalance = 20000; //bank balance that is shared between the two methods.
    static object lockObject = new object(); //lock object
    static void Main(string[] args)
    {
        Thread thread1 = new Thread(Deposit); //creation of the concurrent threads
        Thread thread2 = new Thread(Withdraw);
        thread1.Start(1500);
        thread2.Start(9000); //thread 1 and thread 2 start at the same time to perform deposit and withdrawal

        thread1.Join();
        thread2.Join();
        Console.WriteLine("Final account balance: " + accountBalance);
    }

    static void Deposit(object amount)
    {
        lock (lockObject) //Initiates the lock
        {
            int depositAmount = (int)amount;
            accountBalance += depositAmount;                                   //this time the locks are placed to protect the shared resource accountBalance
            Console.WriteLine("Deposited:" + accountBalance);
        } //Releases the lock
    }
    static void Withdraw(object amount)
    {
        lock (lockObject) //intiates the lock
        {
            int withdrawAmount = (int)amount;
            accountBalance -= withdrawAmount;
            Console.WriteLine("Withdrawn:" + accountBalance);
        } //Releases the lock
    }
    //Adding the lock prevents the race condition from occuring making the threads more safer. With the addtion of the lock only one thread at a time can access the accountBalance variable.
}

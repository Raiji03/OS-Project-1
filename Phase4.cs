using System;
using System.Diagnostics;
using System.Threading; //Phase 4


class Phase4
{
    static int accountBalance = 20000; //first bank account
    static int accountBalance2 = 10000; //2nd bank account
    static object lock1 = new object();
    static object lock2 = new object(); //lock objects
    static void Main(string[] args)
    {
        Thread thread1 = new Thread(TransferBank1to2); //creation of the threads
        Thread thread2 = new Thread(TransferBank2to1);
        thread1.Start();
        thread2.Start(); //thread 1 and thread 2 starting

        thread1.Join();
        thread2.Join();
        Console.WriteLine("Final account balance: Account 1: " + accountBalance + "Account 2: " + accountBalance2);
    }

    static void TransferBank1to2()
    {
        lock (lock1) //Initiates the lock
        {
            Console.WriteLine("Thread 1 acquired first lock.");
            Thread.Sleep(1000);

        }
        lock (lock2) //one thread delay
        {
            Console.WriteLine("Thread1 acquired second lock.");
            accountBalance -= 100;
            accountBalance2 += 100;

        }
        //Releases the lock
    }
    static void TransferBank2to1()
    {
        //keeping the locks in the same order
        lock (lock1) //intiates the lock
        {
            Console.WriteLine("Thread 2 acquired first lock.");
            Thread.Sleep(1000);
        }
        lock (lock2) //the other thread delay
        {
            Console.WriteLine("Thread 2 acquired second lock.");
            accountBalance2 -= 100;
            accountBalance += 100;
        }
    }
}
//the code now fixes the deadlock situtation changing making the threads to run consistently aquiring lock 1 before it gets to lock 2.
//if one thread holds on to lock 1 the other thread will wait for lock 1 to be release to proceed that eliminates the deadlock possibility.
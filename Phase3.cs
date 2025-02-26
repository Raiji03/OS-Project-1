using System;
using System.Diagnostics;
using System.Threading; //Phase 3


class Phase3
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
    static void TransferBank2to1() //the lock is out of order making in inconcistent
    {
        lock (lock2) //intiates the lock
        {
            Console.WriteLine("Thread 2 acquired first lock.");
            Thread.Sleep(1000); 
        }
        lock (lock1) //the other thread delay
        {
            Console.WriteLine("Thread 2 acquired second lock.");
            accountBalance2 -= 100;
            accountBalance += 100;
        }
    }
}
//the code creates a deadlock situtation because one thread holds a lock at a time resulting in the delay for the other thread to start.
//solutuion to the problem is on the next code.
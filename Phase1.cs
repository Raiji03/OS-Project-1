using System;
using System.Threading; //Phase 1


class Phase1 {
    static int accountBalance = 20000; //bank balance that is shared between the two methods.
     static void Main(string[] args) {
        Thread thread1 = new Thread(Deposit); //creation of the concurrent threads
        Thread thread2 = new Thread(Withdraw);
        thread1.Start(1500);
        thread2.Start(9000); //thread 1 and thread 2 start at the same time to perform deposit and withdrawal

        thread1.Join();
        thread2.Join();
        Console.WriteLine("Final account balance: "+accountBalance);
        }

    static void Deposit(object amount)
    {
        int depositAmount = (int)amount;
        accountBalance += depositAmount;
        Console.WriteLine("Deposited:"+accountBalance);
    }
    static void Withdraw(object amount)
    {
        int withdrawAmount = (int)amount;
        accountBalance -= withdrawAmount;
        Console.WriteLine("Withdrawn:" + accountBalance);
    }

}


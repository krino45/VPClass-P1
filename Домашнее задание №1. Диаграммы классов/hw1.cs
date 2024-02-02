// Used for console functions. 
using System;
// Used for List type.
using System.Collections.Generic;

// Interface forces the heredatory (did i spell that right?) classes to implement the Notify method.
public interface INotifyer {
    public void Notify(decimal balance);
}

// User account mock-up
public class Account {
    private decimal _balance;
    private List<INotifyer> _notifyers;

// Constructors
    public Account(){
        _balance = 0.0M;
        _notifyers = new List<INotifyer>();
    }

    public Account(decimal balance){
        _balance = balance;
        _notifyers = new List<INotifyer>();
    }

// Adds a notifyer to the list.
    public void AddNotifyer(INotifyer notifyer){
        _notifyers.Add(notifyer);
    }
// Changes the balance and calls the Notification method.
    public void ChangeBalance(decimal value){
        _balance = value;
        Notification();
    }
// Parameter that obtains the balance.
    public decimal Balance {
        get { return _balance; }
    }
// Goes through each notifyer and calls the virtual method of the interface. 
// That is the usage of interface class - to force every type of notifyer to have this method!
    private void Notification(){
        foreach(INotifyer element in _notifyers){
            element.Notify(_balance);
        }
    }
}

// A mock-up for a SMS notifyer. Pretty self-explanatory.
class SMSLowBalanceNotifyer : INotifyer {

    private string _phone;
    private decimal _lowBalanceValue;

    public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue){
        _phone = phone;
        _lowBalanceValue = lowBalanceValue;
    }

// Implementation of the interface's method. Only writes to console when the balance
// is lower than that of a specified lower border.
    public void Notify(decimal balance){
        if(balance <= _lowBalanceValue){
            Console.WriteLine($"Sending a notification via SMSLowBalanceNotifyer to {_phone}. Balance: {balance}. ");
        }
    }

};

// E-Mail notifyer mock-up. Simply prints every balance change.
class EMailBalanceChangedNotifyer : INotifyer {

    private string _email;

    public EMailBalanceChangedNotifyer(string email){
        _email = email;
    }

    public void Notify(decimal balance){
            Console.WriteLine($"Sending a notification via EMailBalanceChangedNotifyer to {_email}. Balance: {balance}. ");
    }

}

// Program class. Contains a main method, which is apparently the enter point of a program, and I'm 
// just not sure about any of this
class Program
{
    static void Main()
    {   
        // Clears console. Duh.
        Console.Clear();
        // The IDE is telling me I can write it as "Account U1 = new(20)" which looks
        // weird, so I won't be doing that for now.
        Account U1 = new Account(20);
        SMSLowBalanceNotifyer n1 = new SMSLowBalanceNotifyer("+79296125531", 10);
        EMailBalanceChangedNotifyer n2 = new EMailBalanceChangedNotifyer("darkbark21@yahoo.com");

        U1.AddNotifyer(n1);
        U1.AddNotifyer(n2);
        Console.WriteLine($"Current balance is: {U1.Balance}");

        for (int i = 0; i < 5; i++) {
            U1.ChangeBalance(U1.Balance - 3);
        }
        // Just a thing that ensures the console doesn't close immediatly. 
        // True being whether it hides the symbol user types to exit or not.
        Console.ReadKey(true);
    }
}
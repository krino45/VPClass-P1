using System;
using System.Collections.Generic;

public interface INotifyer {
    public void Notify(decimal balance);
}

public class Account {
    private decimal _balance;
    private List<INotifyer> _notifyers;

    public Account(){
        _balance = 0.0M;
        _notifyers = new List<INotifyer>();
    }

    public Account(decimal balance){
        _balance = balance;
        _notifyers = new List<INotifyer>();
    }

    public void AddNotifyer(INotifyer notifyer){
        _notifyers.Add(notifyer);
    }
    public void ChangeBalance(decimal value){
        _balance = value;
        Notification();
    }
    public decimal Balance {
        get { return _balance; }
    }
    private void Notification(){
        foreach(INotifyer element in _notifyers){
            element.Notify(_balance);
        }
    }
}

class SMSLowBalanceNotifyer : INotifyer {

    private string _phone;
    private decimal _lowBalanceValue;

    public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue){
        _phone = phone;
        _lowBalanceValue = lowBalanceValue;
    }

    public void Notify(decimal balance){
        if(balance <= _lowBalanceValue){
            Console.WriteLine($"Sending a notification via SMSLowBalanceNotifyer to {_phone}. Balance: {balance}. ");
        }
    }

};

class EMailBalanceChangedNotifyer : INotifyer {

    private string _email;

    public EMailBalanceChangedNotifyer(string email){
        _email = email;
    }

    public void Notify(decimal balance){
            Console.WriteLine($"Sending a notification via EMailBalanceChangedNotifyer to {_email}. Balance: {balance}. ");
    }

}

class Program
{
    static void Main()
    { 
        Console.Clear();
        Account U1 = new Account(20);
        SMSLowBalanceNotifyer n1 = new SMSLowBalanceNotifyer("+79296125531", 10);
        EMailBalanceChangedNotifyer n2 = new EMailBalanceChangedNotifyer("darkbark21@yahoo.com");

        U1.AddNotifyer(n1);
        U1.AddNotifyer(n2);
        Console.WriteLine($"Current balance is: {U1.Balance}");

        for (int i = 0; i < 5; i++) {
            U1.ChangeBalance(U1.Balance - 3);
        }
        Console.ReadKey(true);
    }
}
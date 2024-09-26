using System;
using System.Collections.Generic;

// Subject (StockMarket)
public class StockMarket
{
    private List<IInvestor> _investors = new List<IInvestor>();
    private string _stock;
    private double _price;

    public StockMarket(string stock, double price)
    {
        _stock = stock;
        _price = price;
    }

    public void Attach(IInvestor investor)
    {
        _investors.Add(investor);
    }

    public void Detach(IInvestor investor)
    {
        _investors.Remove(investor);
    }

    public void Notify()
    {
        foreach (IInvestor investor in _investors)
        {
            investor.Update(_stock, _price);
        }
    }

    public double Price
    {
        get { return _price; }
        set
        {
            if (_price != value)
            {
                _price = value;
                Notify();
            }
        }
    }
}

// Observer (Investor)
public interface IInvestor
{
    void Update(string stock, double price);
}

// Concrete Observer (Individual Investor)
public class Investor : IInvestor
{
    private string _name;

    public Investor(string name)
    {
        _name = name;
    }

    public void Update(string stock, double price)
    {
        Console.WriteLine($"Notified {_name} of {stock}'s price change to {price:C}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create stock and investors
        StockMarket stock = new StockMarket("AAPL", 120.00);
        Investor investor1 = new Investor("Alice");
        Investor investor2 = new Investor("Bob");

        // Attach investors to stock market notifications
        stock.Attach(investor1);
        stock.Attach(investor2);

        // Change stock price, notify investors
        stock.Price = 125.00;
        stock.Price = 130.00;

        // Detach one investor and change price again
        stock.Detach(investor1);
        stock.Price = 140.00;
    }
}

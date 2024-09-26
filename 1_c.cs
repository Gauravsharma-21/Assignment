using System;

public class Logger
{
    private static Logger _instance;

    private Logger() { }

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Logger logger1 = Logger.GetInstance();
        Logger logger2 = Logger.GetInstance();

        logger1.Log("First log entry");
        logger2.Log("Second log entry");

        // Both logger1 and logger2 are the same instance
        Console.WriteLine(object.ReferenceEquals(logger1, logger2)); // True
    }
}

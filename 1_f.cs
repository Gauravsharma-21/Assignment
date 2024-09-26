using System;

// Subsystem classes
public class Light
{
    public void TurnOn() => Console.WriteLine("Light is ON");
    public void TurnOff() => Console.WriteLine("Light is OFF");
}

public class TV
{
    public void TurnOn() => Console.WriteLine("TV is ON");
    public void TurnOff() => Console.WriteLine("TV is OFF");
}

public class AirConditioner
{
    public void TurnOn() => Console.WriteLine("Air Conditioner is ON");
    public void TurnOff() => Console.WriteLine("Air Conditioner is OFF");
}

// Facade class
public class HomeAutomationFacade
{
    private Light _light;
    private TV _tv;
    private AirConditioner _ac;

    public HomeAutomationFacade(Light light, TV tv, AirConditioner ac)
    {
        _light = light;
        _tv = tv;
        _ac = ac;
    }

    public void EnterHome()
    {
        Console.WriteLine("Entering home...");
        _light.TurnOn();
        _tv.TurnOn();
        _ac.TurnOn();
    }

    public void LeaveHome()
    {
        Console.WriteLine("Leaving home...");
        _light.TurnOff();
        _tv.TurnOff();
        _ac.TurnOff();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Light light = new Light();
        TV tv = new TV();
        AirConditioner ac = new AirConditioner();
        
        HomeAutomationFacade facade = new HomeAutomationFacade(light, tv, ac);
        
        facade.EnterHome();
        facade.LeaveHome();
    }
}

using System;

// Command Interface
public interface ICommand
{
    void Execute();
}

// Receiver Class (Light)
public class Light
{
    public void TurnOn()
    {
        Console.WriteLine("The light is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("The light is OFF");
    }
}

// Concrete Command for turning on the light
public class TurnOnLightCommand : ICommand
{
    private Light _light;

    public TurnOnLightCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }
}

// Concrete Command for turning off the light
public class TurnOffLightCommand : ICommand
{
    private Light _light;

    public TurnOffLightCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }
}

// Invoker (Remote Control)
public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create receiver object (Light)
        Light livingRoomLight = new Light();

        // Create command objects
        ICommand turnOnCommand = new TurnOnLightCommand(livingRoomLight);
        ICommand turnOffCommand = new TurnOffLightCommand(livingRoomLight);

        // Create invoker (Remote Control)
        RemoteControl remote = new RemoteControl();

        // Turn on the light
        remote.SetCommand(turnOnCommand);
        remote.PressButton();

        // Turn off the light
        remote.SetCommand(turnOffCommand);
        remote.PressButton();
    }
}
